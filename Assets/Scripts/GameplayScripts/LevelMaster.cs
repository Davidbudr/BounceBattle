using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMaster : MonoBehaviour
{
    public List<GameObject> Wolves;
    public MusicScript mScript;

    public GameObject Player;

    public GameObject WinPanel;
    public GameObject LosePanel;

    private bool gate = false;

    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");

        WinPanel.SetActive(false);
        LosePanel.SetActive(false);

        mScript = this.GetComponent<MusicScript>();

        //dont need to check every frame for win state
        InvokeRepeating("EndScenario",5f,1f);

    }
    private void EndScenario()
    {

        if (Wolves.ToArray().Length > 0)
        {
            foreach (GameObject wolf in Wolves)
            {
                if (wolf == null)
                {
                    Wolves.Remove(wolf);
                    break;
                }
            }
        }
        else
        {
            if (gate == false)
            {
                gate = true;
                WinPanel.SetActive(true);
                mScript.PlayWin();
                Player.SendMessage("Turnoffui");
                Player.SetActive(false);
            }
        }
        if (Player == null)
        {
            if (gate == false)
            {
                gate = true;
                foreach (GameObject wolf in Wolves)
                {
                    wolf.SendMessage("Turnoffui");
                    wolf.SetActive(false);
                }
                LosePanel.SetActive(true);
                mScript.Playlose();
            }
        }
    }
}
