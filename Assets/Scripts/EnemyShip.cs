using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyShip : MonoBehaviour
{
    [Header("For Petroling")]
    public int CurrentWayPoint = 0;
    public float shipSpeed, reachDistance = 0.2f;
    public List<Transform> wayPoints = new List<Transform>();
    float distance;
    float rotationSpeed = 10f;

    // Follow the player
    [Header("For Following the Player")]
    public GameObject player;
    public float radarRange;

    public Transform shootPoint;
    public GameObject enemyBullet;
    float shootRate;
    bool allowFire;

    //HealthBar
    [Header("Health Bar")]
    public HealthBar healthBar;
    public int MaxHealth;
    public int currentHealth;
    public GameObject blastA;





    public enum EnemyStates
    {
        ON_PATH,
        FIGHT
    }
    public EnemyStates enemyStates;




    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);
        player = GameObject.FindGameObjectWithTag("Player");
        enemyStates = EnemyStates.ON_PATH;
        shootRate = 1;
        allowFire = true;



    }//start

    // Update is called once per frame
    void Update()
    {

        if (!allowFire)
        {
            if (shootRate < 0)
            {
                allowFire = true;
            }
            else
            {
                shootRate -= 1.5f * Time.deltaTime;
            }
        }




        FindThePlayer();
        switch (enemyStates)
        {
            case EnemyStates.ON_PATH:
                {
                    MoveEnemyShip();
                }
                break;
            case EnemyStates.FIGHT:
                {
                    ShootBullets();
                }
                break;
        }

    }//update


    void MoveEnemyShip()
    {
        distance = Vector3.Distance(wayPoints[CurrentWayPoint].position, transform.position);
        transform.position = Vector3.MoveTowards(transform.position, wayPoints[CurrentWayPoint].position, shipSpeed * Time.deltaTime);//move the enemy ship towards the way point

        Vector3 dir = wayPoints[CurrentWayPoint].position - transform.position;

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), rotationSpeed * Time.deltaTime);

        if (distance < reachDistance)
        {
            CurrentWayPoint++;
        }
        if (CurrentWayPoint >= wayPoints.Count)
        {
            CurrentWayPoint = 0;
        }
    }//MoveEnemyShip

    void FindThePlayer()
    {
        if (player != null)
        {
            float findDistanceToPlayer = Vector2.Distance(transform.position, player.transform.position);
            if (findDistanceToPlayer <= radarRange)
            {
                enemyStates = EnemyStates.FIGHT;
            }
            else
            {
                enemyStates = EnemyStates.ON_PATH;
            }

        }

        else
        {
            enemyStates = EnemyStates.ON_PATH;
        }
    }//FindThePlayer



    void ShootBullets()
    {
        // follow the player
        Vector3 dir = player.transform.position - transform.position;
        transform.position = Vector3.MoveTowards(transform.position, player.transform.position, shipSpeed * Time.deltaTime);
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), rotationSpeed * Time.deltaTime);

        if (allowFire)
        {
            Instantiate(enemyBullet, shootPoint.position, shootPoint.rotation);
            allowFire = false;
            shootRate = 1;
        }


    }//ShootBullets




    public void TakeDamage(int damageValue)
    {
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            GameObject blast = Instantiate(blastA, transform.position, transform.rotation);
            Destroy(blast, 1f);
            Destroy(gameObject);
        }
        else
        {
            healthBar.SetValue(currentHealth);
        }
        healthBar.SetValue(currentHealth);
    }//takedamage

}
