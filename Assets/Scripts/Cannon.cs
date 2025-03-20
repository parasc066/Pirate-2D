using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject playerBullet;
    public Transform shootPoint;

    float shootRate;
    bool allowFire;

    float _horizontalInput = 0, _verticalInput = 0;
    public Joystick CannonJoystick;





    void Start()
    {
        shootRate = 1;
        allowFire = true;

    }//start

    // Update is called once per frame
    void Update()
    {
        GetPlayerInput();
        if (allowFire == false)
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
    }//update

    void GetPlayerInput()
    {
        _horizontalInput = CannonJoystick.Horizontal;
        _verticalInput = CannonJoystick.Vertical;
    }


    private void FixedUpdate()
    {
        if (_verticalInput != 0 || _horizontalInput != 0)
        {
            Shoot();
            CannonRotation();
        }
    }//fixedupdate

    void CannonRotation()
    {
        float angle = Mathf.Atan2(_verticalInput, _horizontalInput) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90, Vector3.forward);
    }//cannonrotation

    void Shoot()
    {
        if (allowFire)
        {
            Instantiate(playerBullet, shootPoint.position, shootPoint.rotation);
            allowFire = false;
            shootRate = 1;
        }

    }//shoot


}
