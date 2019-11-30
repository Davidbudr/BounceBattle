using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CharacterVariables))]
public class StatsUIScript : MonoBehaviour
{
    private CharacterVariables mystats;

    public GameObject uicanvas;
    public GameObject uiPrefab;

    public GameObject StatsUI;
    private Slider Healthbar;
    private Slider StaminaBar;
    private Text LevelupText;

    void Start()
    {
        uicanvas = GameObject.Find("Canvas");
        StatsUI = Instantiate(uiPrefab);
        StatsUI.transform.parent = uicanvas.transform;

        mystats = this.GetComponent<CharacterVariables>();
        Healthbar = StatsUI.transform.GetChild(0).GetComponent<Slider>();
        StaminaBar = StatsUI.transform.GetChild(1).GetComponent<Slider>();
        LevelupText = StatsUI.transform.GetChild(2).GetComponent<Text>();
        LevelupText.color = Color.clear;
    }

    // Update is called once per frame
    void Update()
    {
        StatsUI.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
        Healthbar.value = mystats.curHealth / mystats.maxHealth;
        StaminaBar.value = mystats.curStamina / mystats.maxStamina;

        if (LevelupText.color != Color.clear)
        {
            LevelupText.color = Color.Lerp(LevelupText.color, Color.clear, 0.05f);
        }

    }

    public void Turnoffui()
    {
        StatsUI.SetActive(false);
    }

    public void Updateui()
    {
        LevelupText.color = new Color32(255,0,0,255);
    }

}
