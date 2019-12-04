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

    public Color CharacterColour;

    private Image damageImg;
    public GameObject DamagedPrefab;
    

    void Start()
    {
        uicanvas = GameObject.Find("Canvas");
        StatsUI = Instantiate(uiPrefab);
        StatsUI.transform.SetParent(uicanvas.transform);

        mystats = this.GetComponent<CharacterVariables>();
        Healthbar = StatsUI.transform.GetChild(0).GetComponent<Slider>();
        StaminaBar = StatsUI.transform.GetChild(1).GetComponent<Slider>();

        damageImg = StatsUI.transform.GetChild(2).GetComponent<Image>();
        damageImg.color = Color.clear;

        LevelupText = StatsUI.transform.GetChild(3).GetComponent<Text>();
        LevelupText.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        StatsUI.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);
        Healthbar.value = mystats.curHealth / mystats.maxHealth;
        StaminaBar.value = mystats.curStamina / mystats.maxStamina;
        
        if (damageImg.color != Color.clear)
        {
            damageImg.color = Color.Lerp(damageImg.color, Color.clear, 0.05f);
        }

    }

    public void Turnoffui()
    {
        StatsUI.SetActive(false);
    }

    public void DamagedUI(int dmg)
    {
        damageImg.color = Color.white;
        GameObject hitui = Instantiate(DamagedPrefab);
        hitui.GetComponent<TextMesh>().text = "-" + dmg;
        hitui.GetComponent<TextMesh>().color = CharacterColour;
        hitui.transform.position = this.transform.position;
    }

    public void Poweredui()
    {
        LevelupText.gameObject.SetActive(true);
        StartCoroutine(LevelupDisabler());
        LevelupText.color = CharacterColour;

    }

    IEnumerator LevelupDisabler()
    {
        yield return new WaitForSeconds(1f);
        LevelupText.gameObject.SetActive(false);
    }

}
