using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] CinemachineFreeLook freeLookview;

    private void Awake()
    {
        freeLookview.Priority = 5;
    }
    private void LateUpdate()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            freeLookview.Priority = 20;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            freeLookview.Priority = 5;
        }
    }
}
