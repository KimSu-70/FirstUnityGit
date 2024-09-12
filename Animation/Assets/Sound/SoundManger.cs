using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManger : MonoBehaviour
{
    public static SoundManger Instance { get; private set; }

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlayBGM(AudioClip clip)
    {
        bgm.clip = clip;
        bgm.Play();
    }

    public void StopBGM()
    {
        if (bgm.isPlaying == false)
            return;

        bgm.Stop();
    }

    public void PauseBGM()
    {
        if (bgm.isPlaying == false)
            return ;

        bgm.Pause();
    }

    public void SetBGM(float volume, float pitch = 1f)
    {
        bgm.volume = volume;
        bgm.pitch = pitch;
    }

    public void PlaySFX(AudioClip clip)
    {
        sfx.PlayOneShot(clip);          // 여러 개의 소리를 겹쳐서 내기 가능
    }

    public void SetSFX(float volume, float pitch = 1f)
    {
        sfx.volume = volume;
        sfx.pitch = pitch;
    }
}
