using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Monster", fileName = "Monster")]
public class MonsterData : ScriptableObject
{
    public int attack;
    public int defense;
    public float speed;
    public float range;

    public Sprite icom;
    public Color color;
}
