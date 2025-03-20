using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Torepedo : MonoBehaviour
{
    public Transform target;
    public string enemyTag = "EnemyShip";
    public float range = 100f;
    public float speed;
    Vector3 centerPos;
    float distanceFromCamera = 10f;
    public GameObject blastTorepedo;
    public int damage =100;



    



    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
        centerPos = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 0.5f, distanceFromCamera));


    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {

            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

            //Rotation of the missile
            Vector3 dir = target.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var direction = target.transform.position - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0,0,angle-90), 10f * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, centerPos, speed * Time.deltaTime);

            //rotation 
            Vector3 dir = centerPos - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            var direction = centerPos - transform.position;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(0, 0, angle - 90), 10f * Time.deltaTime);    
            if(Vector2.Distance(transform.position,centerPos) < 0.5f)
            {
                BlastTorepedo();
            }
        }


    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject nearestEnemy = null;


        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < shortestDistance)
            {
                shortestDistance = distanceToEnemy;
                nearestEnemy = enemy;
            }
        }

        if (nearestEnemy != null && shortestDistance <= range)
        {
            target = nearestEnemy.transform;
        }
        else
        {
            target = null;
        }




    }//UpdateTarget

    void BlastTorepedo()
    {
        GameObject blast = Instantiate(blastTorepedo, transform.position, Quaternion.identity);
        Destroy(blast, 1f); 
        Destroy(gameObject);
    }//blastTorepedo

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("EnemyShip"))
        {
            collision.gameObject.GetComponent<EnemyShip>().TakeDamage(damage);
            BlastTorepedo();
        }
        
    }




}
