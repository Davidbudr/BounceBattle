using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    private AudioSource Jukebox;

    public AudioClip Music;

    public AudioClip winSfx;
    public AudioClip loseSfx;

    private void Start() {
        Jukebox = GameObject.FindGameObjectWithTag("Jukebox").GetComponent<AudioSource>();
    }

    public void ResetMusic()
    {
        Jukebox.clip = Music;
        Jukebox.loop = true;
        Jukebox.Play();
    }

    public void PlayWin()
    {
        Jukebox.loop = false;
        Jukebox.clip = winSfx;
        Jukebox.Play();
    }


    public void Playlose()
    {
        Jukebox.loop = false;
        Jukebox.clip = loseSfx;
        Jukebox.Play();
    }

}
