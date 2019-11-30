using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterVariables), typeof(StatsUIScript), typeof(SoundEffectScript))]
public class WolfCollisions : MonoBehaviour
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
        if (mystats != null) //without this there is sometimes an error that pops during the spawning phase.
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.SendMessage("Gethit", mystats.Force);
                rb.AddForce((this.transform.position - other.transform.position) * 300f);
                mystats.Force = 0;
                sfxScript.Bounce();
            }
            if (other.gameObject.CompareTag("Piggy"))
            {

                mystats.Force = 0;
                Destroy(other.gameObject);

                mystats.maxHealth++;
                mystats.maxStamina++;

                mystats.curHealth = mystats.maxHealth;
                sfxScript.CatchPig();

                statsui.Updateui();
            }
        }
    }
}
