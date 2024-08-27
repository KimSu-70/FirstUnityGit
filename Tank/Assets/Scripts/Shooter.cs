using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] ObjectPool[] bulletPools; // 탄환 종류별 Object Pool
    [SerializeField] Transform muzzlePoint;

    [Range(1, 10)]
    [SerializeField] float bulletSpeed;

    private int selectedBulletIndex = 0;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }

        // 숫자키 1, 2, 3으로 탄환 종류 선택
        if (Input.GetKeyDown(KeyCode.Alpha1)) SelectBullet(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) SelectBullet(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) SelectBullet(2);
    }

    public void Fire()
    {
        if (bulletPools.Length == 0 || selectedBulletIndex >= bulletPools.Length)
            return;

        ObjectPool currentPool = bulletPools[selectedBulletIndex];
        PooledObject instance = currentPool.GetPool(muzzlePoint.position, muzzlePoint.rotation);
        if (instance != null)
        {
            Bullet bullet = instance.GetComponent<Bullet>();
            bullet.SetSpeed(bulletSpeed);
        }
    }

    void SelectBullet(int index)
    {
        if (index >= 0 && index < bulletPools.Length)
        {
            selectedBulletIndex = index;
        }
    }
}
