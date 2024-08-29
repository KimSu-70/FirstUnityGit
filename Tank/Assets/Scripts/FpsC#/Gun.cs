using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] Transform pos;

    private void Update()
    {
        Debug.DrawRay(pos.position, pos.right * -10, Color.red);

        if(Physics.Raycast(pos.position, pos.right * -1, out RaycastHit hit))
        {
            Debug.Log("±¤¼±");
        }
    }
}
