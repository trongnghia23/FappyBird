using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : NghiaMono
{
    protected static SoundManager instance;
    public static SoundManager Instance { get => instance; }
    public AudioSource Despawnnoise;
    public AudioSource BackgroundMusic;
    public AudioSource Movenoise;
    public AudioSource UInoise;
    public AudioSource Scorenoise;
    protected override void Awake()
    {
        base.Awake();
        if (SoundManager.instance != null) Debug.LogError("only one SoundManager allow to exist");
        SoundManager.instance = this;
    }
    public virtual void PlayBackGroundMusic()
    {
        
        BackgroundMusic.Play();
    }
    public virtual void PlayDespawNoise()
    {
       
        Despawnnoise.Play();
    }
    public virtual void PlayMoveNoise()
    {
       
        Movenoise.Play();
    }
    public virtual void PlayUInoise()
    {

        UInoise.Play();
    }
    public virtual void PlayScorenoise()
    {

        Scorenoise.Play();
    }
}
