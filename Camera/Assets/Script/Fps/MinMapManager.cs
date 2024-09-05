using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinMapManager : MonoBehaviour
{
    public GameObject minimapCanvas;
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            minimapCanvas.SetActive(true);
        }
        else
        {
            minimapCanvas.SetActive(false);
        }
    }
}
