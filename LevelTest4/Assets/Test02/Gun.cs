using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Pos;
    [SerializeField] float speed;
    [SerializeField] float Time;

    Coroutine gun;
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            gun = StartCoroutine(GunFire());
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            StopCoroutine(gun);
        }
    }

    IEnumerator GunFire()
    {
        while(true)
        {
            Fire();
            yield return new WaitForSeconds(Time);
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
