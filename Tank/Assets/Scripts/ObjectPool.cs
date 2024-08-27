using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] List<PooledObject> pool = new List<PooledObject>();
    [SerializeField] PooledObject prefab;
    [SerializeField] int size;
    private int currentBulletCount = 0;
    [SerializeField] int maxBullets = 5;

    private void Awake()
    {
        for (int i = 0; i < size; i++)
        {
            PooledObject instance = Instantiate(prefab);
            instance.gameObject.SetActive(false);
            instance.transform.parent = transform;
            instance.returnPool = this;
            pool.Add(instance);
        }
    }

    public PooledObject GetPool(Vector3 position, Quaternion rotation)
    {
        if (currentBulletCount >= maxBullets)
        {
            return null; // 최대 탄환 수에 도달하면 발사하지 않음
        }

        if (pool.Count > 0)
        {
            PooledObject instance = pool[pool.Count - 1];
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = null;
            instance.returnPool = this;
            instance.gameObject.SetActive(true);

            pool.RemoveAt(pool.Count - 1);
            currentBulletCount++;

            return instance;
        }
        else
        {
            PooledObject instance = Instantiate(prefab, position, rotation);
            instance.returnPool = this;
            currentBulletCount++;

            return instance;
        }
    }

    public void ReturnPool(PooledObject instance)
    {
        instance.gameObject.SetActive(false);
        instance.transform.parent = transform;
        pool.Add(instance);
        currentBulletCount--;
    }
}
