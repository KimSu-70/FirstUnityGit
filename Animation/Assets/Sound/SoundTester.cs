using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTester : MonoBehaviour
{
    [SerializeField] AudioClip bgm1;
    [SerializeField] AudioClip bgm2;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SoundManger.Instance.PlayBGM(bgm1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SoundManger.Instance.PlayBGM(bgm2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SoundManger.Instance.StopBGM();
        }
    }
}
