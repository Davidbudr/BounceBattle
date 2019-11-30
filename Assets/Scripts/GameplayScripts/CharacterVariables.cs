using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterVariables : MonoBehaviour
{
    [Header("Health Vars")]
    public float maxHealth;
    public float curHealth;
    public int[] HealthRange;
    public float HealthRegenRate;
    [Space]
    [Header("Stamina Vars")]
    public float maxStamina;
    public float curStamina;
    public int[] StaminaRange;
    public float StaminaRegenRate;
    
    public int Force;


    private void Start()
    {
        maxHealth = (HealthRange.Length > 0)?Random.Range(HealthRange[0], HealthRange[1]): maxHealth;
        maxStamina = (StaminaRange.Length > 0) ? Random.Range(StaminaRange[0], StaminaRange[1]): maxStamina;

        curHealth = maxHealth;
        curStamina = maxStamina;

        InvokeRepeating("Recharge", 0f, 1f);
    }

    void Update()
    {
        curHealth = Mathf.Clamp(curHealth, 0, maxHealth);
        curStamina = Mathf.Clamp(curStamina, 0, maxStamina);
    }

    void Recharge()
    {
        curHealth += HealthRegenRate;
        curStamina += StaminaRegenRate;
    }
}
