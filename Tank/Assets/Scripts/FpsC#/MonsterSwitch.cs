using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterSwitch : MonoBehaviour
{
    public static MonsterSwitch Instance;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Deactivate(GameObject monster)
    {
        // ���͸� ��Ȱ��ȭ
        monster.SetActive(false);
    }

    public void Activate(GameObject monster)
    {
        // ���͸� Ȱ��ȭ
        monster.SetActive(true);
    }
}
