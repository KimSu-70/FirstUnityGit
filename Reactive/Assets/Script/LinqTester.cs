using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LinqTester : MonoBehaviour
{
    //private List<int> databese = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

    //private void Start()
    //{
    //    var quary = from element in databese

    //             // where element < 8 && element > 3
    //             //         || �� �Ʒ� ���� ����
    //                where element < 8
    //                where element > 3

    //                // ���� �������� ����
    //                // orderby element ascending

    //                // ���� �������� ����
    //                orderby element descending

    //                select element;
    //    List<int> list = quary.ToList();

    //    for (int i = 0; i < list.Count; i++)
    //    {
    //        Debug.Log(list[i]);
    //    }
    //}

    //public List<int> Search()
    //{
    //    List<int> result = new List<int>();

    //    foreach (int element in databese)
    //    {
    //        if (element > 5)
    //        {
    //            result.Add(element);
    //        }
    //    }

    //    return result;
    //}


    private List<MonsterData> monsters = new List<MonsterData>()
    {
        new MonsterData(MonsterType.Normal, 10, "����"),
        new MonsterData(MonsterType.Fire, 8, "���̸�"),
        new MonsterData(MonsterType.Water, 12, "���α�"),
        new MonsterData(MonsterType.Grass, 8, "�̻��ؾ�"),
        new MonsterData(MonsterType.Normal, 5, "������"),
    };

    private List<GameObject> targets = new List<GameObject>();

    private void Start()
    {
        var q = from target in targets
                where target.layer == LayerMask.NameToLayer("Monster")
                where Vector3.Distance(transform.position, target.transform.position) < 3f
                select gameObject.GetComponent<Collider>();


        var quary = from monster in monsters
                    where monster.type == MonsterType.Normal
                    orderby monster.level ascending, monster.name ascending
                    select monster.name;
        var list = quary.ToList();

        for (int i = 0; i < list.Count; i++)
        {
            Debug.Log(list[i]);
        }
    }
}

public enum MonsterType { Normal, Fire, Water, Grass }

public class MonsterData
{
    public MonsterType type;
    public int level;
    public string name;

    public MonsterData(MonsterType type, int level, string name)
    {
        this.type = type;
        this.level = level;
        this.name = name;
    }

    public override string ToString()
    {
        return $"{name} {level} {type}";
    }
}
