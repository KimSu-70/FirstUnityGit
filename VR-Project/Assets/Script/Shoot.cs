using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [SerializeField] GameObject bullet;
    [SerializeField] Transform Pos;
    [SerializeField] float Speed;


    public void Fire()
    {
        GameObject bullets = Instantiate(bullet, Pos.position, Pos.rotation);
        Rigidbody bulletRb = bullets.GetComponent<Rigidbody>();

        if (bulletRb != null )
        {
            bulletRb.velocity = Pos.forward * Speed;
            Destroy(bullets, 6);
        }
    }
}
