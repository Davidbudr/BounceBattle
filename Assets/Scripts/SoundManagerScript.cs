using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{
    public bool mute;
    private AudioSource asource;

    private void Start()
    {
        asource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        if (asource.mute != mute)
        {
            asource.mute = mute;
        }
    }
}
