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

    public GameObject HealthAddPrefab;
    public GameObject EnergyAddPrefab;
    
    private Image damageImg;

    public Color DamageColour;

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
        hitui.GetComponent<TextMesh>().color = DamageColour;
        hitui.transform.position = this.transform.position;
    }

    public void Poweredui()
    {
        //LevelupText.color = new Color32(255,0,0,255);
        GameObject hpadd = Instantiate(EnergyAddPrefab);
        hpadd.transform.position = this.transform.position + (Vector3.right * 0.35f);

        GameObject enadd = Instantiate(HealthAddPrefab);
        enadd.transform.position = this.transform.position + (Vector3.left * 0.35f);
    }

}
