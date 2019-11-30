using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterVariables),typeof(StatsUIScript), typeof(SoundEffectScript))]
public class PlayerCollisions : MonoBehaviour
{
    private CharacterVariables mystats;
    private StatsUIScript statsui;
    private SoundEffectScript sfxScript;
    private Rigidbody rb;
    void Start()
    {
        mystats = this.GetComponent<CharacterVariables>();
        statsui = this.GetComponent<StatsUIScript>();
        sfxScript = this.GetComponent<SoundEffectScript>();
        rb = this.GetComponent<Rigidbody>();
    }

    public void Gethit(int _hitfor)
    {
        mystats.curHealth -= _hitfor;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Wolf"))
        {
            other.gameObject.SendMessage("Gethit", mystats.Force);
            rb.AddForce((this.transform.position - other.transform.position) * 200f);
            sfxScript.Bounce();
            if (other.gameObject.GetComponent<CharacterVariables>().curHealth <= 0)
            {
                sfxScript.PlayEnemy();
            }
            mystats.Force = 0;
        }
        if (other.gameObject.CompareTag("Piggy"))
        {
            Destroy(other.gameObject);

            sfxScript.CatchPig();

            mystats.Force = 0;
            mystats.maxHealth++;
            mystats.maxStamina++;

            mystats.curHealth = mystats.maxHealth;

            statsui.Updateui();
        }
    }
}
