using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameObject gunCanvas;
    public GameObject Bullet;

    [SerializeField] CinemachineVirtualCamera playerCam1;
    [SerializeField] CinemachineFreeLook playerCam2;

    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            gunCanvas.SetActive(true);
            playerCam2On();
        }
        else
        {
            gunCanvas.SetActive(false);
            playerCam2Off();
        }
    }

    private void playerCam2On()
    {
        playerCam1.Priority = 10;
        playerCam2.Priority = 20;
    }

    private void playerCam2Off()
    {
        playerCam1.Priority = 20;
        playerCam2.Priority = 10;
    }
}
