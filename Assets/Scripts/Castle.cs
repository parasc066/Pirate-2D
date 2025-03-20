using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public int castlePower;
    int bulletPower = 5;
    public GameObject blastB;

    void Start()
    {

    }//start

    // Update is called once per frame
    void Update()
    {

    }//update

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("PlayerBullet"))
        {

            takeDamage();
            Debug.Log("Castle Hit");
        }
    }//oncollisionenter2d

    void takeDamage()
    {
        if (castlePower > 0)
        {
            castlePower -= bulletPower;
        }
        else
        {

            GameObject blast = Instantiate(blastB, transform.position, transform.rotation);//create the blast effect
            Destroy(blast, 1f);//destroy the blast effect after 1 second
            
            Destroy(gameObject);//destroy the castle
            

        }

    }



}
