using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] Quest[] Quests;
    [SerializeField] Skill[] Skill;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha1))
        {
            Skill[0].Use();
        }
    }
}
