using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public float bulletSpeed;
    Rigidbody2D rb;
    public GameObject blastA;
    public int bulletPower = 20;




    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * bulletSpeed;
    }//start

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            BlastEffects();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(bulletPower);
            BlastEffects();
        }
    }//oncollisionenter2d


    void BlastEffects()
    {
        GameObject blast = Instantiate(blastA, transform.position, transform.rotation);
        Destroy(gameObject);
    }//blasteffects


}
