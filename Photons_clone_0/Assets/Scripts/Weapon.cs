using Fusion;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : NetworkBehaviour
{
    [SerializeField] Transform muzzlePoint;

    [SerializeField] int damage;
    [Range(0, 20)]
    [SerializeField] float range;

    public void Fire()
    {
        if (Runner.GetPhysicsScene().Raycast(muzzlePoint.position, muzzlePoint.forward, out RaycastHit info, range))
        {
            Debug.Log(info.transform.gameObject.name);
            if (info.transform.CompareTag("Player"))
            {
                Debug.Log("°¨Áö");
                PlayerController player = info.transform.GetComponent<PlayerController>();
                player.TakeHitRpc(damage);
            }
        }
    }
}
