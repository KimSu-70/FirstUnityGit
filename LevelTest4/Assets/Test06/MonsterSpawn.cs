using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSpawn : MonoBehaviour
{
    public GameObject monster;
    public Transform[] spawn;
    public int MonsterTotal;

    public Coroutine StartMonster;

    IEnumerator SpawnMonsters()
    {
        for (int i = 1; i < MonsterTotal; i++)
        {
            SpawnMonster();
            yield return new WaitForSeconds(2f);
        }
    }

    private void Start()
    {
        StartCoroutine(SpawnMonsters());
    }

    private void SpawnMonster()
    {
        int randomin = Random.Range(0, spawn.Length);
        Transform spawns = spawn[randomin];

        Instantiate(monster, spawns.position, spawns.rotation);
    }
}
