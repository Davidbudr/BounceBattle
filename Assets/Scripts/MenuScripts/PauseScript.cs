using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseScript : MonoBehaviour
{
    public GameObject PausePanel;
    public GameObject PauseButton;
    public GameObject MuteButton;
    public GameObject UnMuteButton;
    public GameObject GameCanvas;

    private SoundManagerScript soundMan;

    private void Start()
    {
        soundMan = GameObject.FindGameObjectWithTag("Jukebox").GetComponent<SoundManagerScript>();
        if (PausePanel != null) { PausePanel.SetActive(false); }
    }

    private void Update()
    {
        if (UnMuteButton.activeSelf != soundMan.mute || MuteButton.activeSelf == soundMan.mute)
        {
            UnMuteButton.SetActive(soundMan.mute);
            MuteButton.SetActive(!soundMan.mute);
        }
    }
    public void Mute()
    {
        soundMan.mute = true;
    }
    public void Unmute()
    {
        soundMan.mute = false;
    }
    public void PauseGame()
    {
        PauseButton.SetActive(false);
        PausePanel.SetActive(true);
        GameCanvas.SetActive(false);
        Time.timeScale = 0;
    }
    public void UnPauseGame()
    {
        PausePanel.SetActive(false);
        PauseButton.SetActive(true);
        GameCanvas.SetActive(true);
        Time.timeScale = 1;
    }

}
