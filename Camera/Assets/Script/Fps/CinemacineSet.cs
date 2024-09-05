using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinemacineSet : MonoBehaviour
{
    [SerializeField] CinemachineVirtualCamera playerCam;
    [SerializeField] CinemachineVirtualCamera minMapCam;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            minMapCam.Priority = 20;
        }

        else if (Input.GetKeyUp(KeyCode.Tab))
        {
            minMapCam.Priority = 5;
        }
    }
}
