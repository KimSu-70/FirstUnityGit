using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Skill", fileName = "skill")]
public class Skill : ScriptableObject
{
    public string name;
    public float coolTime;

    public Sprite icon;

    public void Use()
    {
        Debug.Log($"{name} 스킬 사용!");
    }
}
