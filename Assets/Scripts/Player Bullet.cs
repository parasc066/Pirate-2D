using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody2D rb;
    public GameObject blastA;
    public int bulletPower = 20;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }///start
    



    // Update is called once per frame

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            BlastIffects();
        }
        if(collision.gameObject.CompareTag("EnemyShip"))
        {
            collision.gameObject.GetComponent<EnemyShip>().TakeDamage(bulletPower);
            BlastIffects();
        }

    }//oncollisionenter2d

    void BlastIffects()
    {
        GameObject blast = Instantiate(blastA, transform.position, transform.rotation);
        
        Destroy(gameObject);
    }





}//class
