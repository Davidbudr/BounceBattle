using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectScript : MonoBehaviour
{
    public AudioSource aSource;

    public AudioClip PigCatchClip;
    public AudioClip SlingClip;
    public AudioClip BounceClip;
    public AudioClip EnemyClip;

    public void CatchPig()
    {
        aSource.clip = PigCatchClip;
        aSource.Play();
    }

    public void Slingshot()
    {
        aSource.clip = SlingClip;
        aSource.Play();
    }


    public void Bounce()
    {
        aSource.clip = BounceClip;
        aSource.Play();
    }

    public void PlayEnemy()
    {
        aSource.clip = EnemyClip;
        aSource.Play();
    }
    
}
