using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    float _horizontalInput = 0, _verticalInput = 0;
    public int moveSpeed;
    Rigidbody2D rb2D;
    public Joystick joystick;


    float x, y;


    public ParticleSystem particle1, particle2;

    //torepedo
    public GameObject torpedo;
    public Button TorepedoBtn;
    int totaltorpedo = 3;
    public Text totaltorpedoText;


    //HealthBar
    [Header("Health Bar")]
    public HealthBar healthBar;
    public int MaxHealth;
    public int currentHealth;
    public GameObject blastA;











    // Start is called before the first frame update
    void Start()
    {
        currentHealth = MaxHealth;
        healthBar.SetMaxHealth(MaxHealth);


        rb2D = GetComponent<Rigidbody2D>();
        totaltorpedoText.text = totaltorpedo.ToString();

    }//start

    // Update is called once per frame
    void Update()
    {
        if (totaltorpedo <= 0)
        {
            TorepedoBtn.interactable = false;
        }
        else
        {
            TorepedoBtn.interactable = true;
        }

    }//Update


    private void FixedUpdate()
    {
        MovePlayer();
        GetPlayerInput();
    }//FixedUpdate

    void MovePlayer()
    {
        Vector3 directionVector = new Vector3(_horizontalInput, _verticalInput, 0);
        rb2D.velocity = directionVector.normalized * moveSpeed;
        if (joystick.Horizontal == 0 && joystick.Vertical == 0)
        {
            rb2D.velocity = new Vector3(x, y, 0) * moveSpeed * 0.1f;


            particle1.Stop();
            particle2.Stop();
        }

        else
        {
            RotatePlayer();


            if (particle1.isStopped)
            {
                particle1.Play();
            }
            if (particle2.isStopped)
            {
                particle2.Play();
            }

        }
    }//MovePlayer

    void GetPlayerInput()
    {
        _horizontalInput = joystick.Horizontal;
        _verticalInput = joystick.Vertical;

        if (_horizontalInput != 0)
        {
            x = _horizontalInput;
        }
        if (_verticalInput != 0)
        {
            y = _verticalInput;
        }
    }//GetPlayerInput

    void RotatePlayer()
    {
        float angle = Mathf.Atan2(_verticalInput, _horizontalInput) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }//rotateplayer



    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("coin"))
        {
            GameManager.instance.IncreaseCoin();

        }

        if (collision.gameObject.CompareTag("diamond"))
        {
            GameManager.instance.IncreaseDiamond();

        }
    }//oncollisionenter2d



    public void ShootTorepedo()
    {
        totaltorpedo -= 1;
        totaltorpedoText.text = totaltorpedo.ToString();

        Instantiate(torpedo, transform.position, Quaternion.identity);
    }//shoottorepedo





    public void TakeDamage(int damageValue)
    {
        currentHealth -= damageValue;
        if (currentHealth <= 0)
        {
            GameManager.instance.GameOver();
            GameObject blast = Instantiate(blastA, transform.position, transform.rotation);
            
            Destroy(blast, 1f);
            Destroy(gameObject);

        }
        else
        {
            healthBar.SetValue(currentHealth);
        }


    }//takedamage



}//class
