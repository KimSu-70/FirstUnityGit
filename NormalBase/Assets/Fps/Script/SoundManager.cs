using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance {  get; private set; }

    [SerializeField] AudioSource bgm;
    [SerializeField] AudioSource sfx;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
            return;
        bgm.Pause();
    }

    public void SetBGM(float volume, float pitch = 1f)
    {
        bgm.volume = volume;
        bgm.pitch = pitch;
    }




    public void PlaySFX(AudioClip clip)
    {
        sfx.clip = clip;
        sfx.Play();
    }

    public void StopSFX()
    {
        if (bgm.isPlaying == false)
            return;
        bgm.Stop();
    }

    public void PauseSFX()
    {
        if (bgm.isPlaying == false)
            return;
        bgm.Pause();
    }

    public void SetSFX(float volume, float pitch = 1f)
    {
        bgm.volume = volume;
        bgm.pitch = pitch;
    }
}
