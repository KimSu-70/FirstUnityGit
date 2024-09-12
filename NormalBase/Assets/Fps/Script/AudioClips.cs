using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioClips : MonoBehaviour
{
    [SerializeField] AudioClip bgm;
    [SerializeField] AudioClip sfx;

    private bool issfx;

    private void Start()
    {
        SoundManager.instance.PlayBGM(bgm);
        issfx = false;
}

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (issfx == true)
            {
                SoundManager.instance.PlayBGM(bgm);
                issfx = false;
            }

            else
            {
                SoundManager.instance.StopBGM();
                issfx = true;
            }
        }
    }

    public void Gun()
    {
        SoundManager.instance.PlaySFX(sfx);
    }
}
