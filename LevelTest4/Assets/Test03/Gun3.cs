using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun3 : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Pos;
    [SerializeField] float speed;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
    }


    public void Fire()
    {
        GameObject bullet = Instantiate(Bullet, Pos.position, Pos.rotation);

        Rigidbody bulletRb = bullet.GetComponent<Rigidbody>();
        if (bulletRb != null)
        {
            bulletRb.velocity = Pos.forward * speed;
        }
    }
}
