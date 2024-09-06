using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gunCanvas;
    public GameObject Bullet;
    
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            gunCanvas.SetActive(true);
        }
        else
        {
            gunCanvas.SetActive(false);
        }
    }
}
