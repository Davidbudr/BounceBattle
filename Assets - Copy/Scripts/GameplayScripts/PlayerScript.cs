using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Rigidbody), typeof(CharacterVariables), typeof(SoundEffectScript))]
public class PlayerScript : MonoBehaviour
{
    private Rigidbody rb;
    private CharacterVariables mystats;

    public GameObject SlingUI;
    public GameObject ArrowUI;
    private Slider SlingSlide;
    public float Force;

    private bool shooting;

    private StatsUIScript sus;
    private SoundEffectScript sfxScript;

    void Start()
    {
        sfxScript = this.GetComponent<SoundEffectScript>();
        sus = this.GetComponent<StatsUIScript>();
        rb = this.GetComponent<Rigidbody>();
        mystats = this.GetComponent<CharacterVariables>();
        SlingUI.SetActive(false);
        SlingSlide = SlingUI.transform.GetChild(0).GetComponent<Slider>();
        InvokeRepeating("ForceReset", 0, 0.25f);
        lastpos = this.transform.position;
    }
    
    void Update()
    {

        ArrowUI.transform.position = this.transform.position + (Vector3.up * 0.3f);

        if (Input.GetMouseButtonDown(0))
        {
            //check if pressed on player
            RaycastHit _hit;
            Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(_ray, out _hit))
            {
                if (_hit.collider.gameObject == this.gameObject)
                {
                    //activate the sling
                    SlingUI.SetActive(true);
                    ArrowUI.SetActive(true);
                    shooting = true;
                }
            }
        }

        //sling
        if (shooting)
        {
            Vector3 _mp = Input.mousePosition;

            //This checks the distance between the mouse position and the players screen point
            Vector3 _pos = Camera.main.WorldToScreenPoint(this.transform.position);
            _pos.z = 0;
            Force = Vector2.Distance(_pos, _mp) / 15;

            //stop the player from using more stamina than they have
            Force = (mystats.curStamina >= 5) ? Mathf.Clamp(Force, 0, 5) : Mathf.Clamp(Force, 0, mystats.curStamina);

            SlingSlide.value = Force;

            //set the position and rotation of the power bar and the Pointer arrow
            SlingUI.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);

            float _rot = Mathf.Atan2(_mp.y - SlingUI.transform.position.y, _mp.x - SlingUI.transform.position.x) * Mathf.Rad2Deg;
            
            SlingUI.transform.localRotation = Quaternion.Euler(0, 0, _rot);

            this.transform.rotation = Quaternion.Euler(0, -_rot, 0);
            ArrowUI.transform.rotation = Quaternion.Euler(0, -_rot, 0);



            if (Input.GetMouseButtonUp(0))
            {

                if (Force != 0)
                {
                    rb.AddForce(-transform.right * 500f * Force / 5f);
                    sfxScript.Slingshot();
                }
                shooting = false;
                SlingUI.SetActive(false);
                ArrowUI.SetActive(false);

                mystats.curStamina -= Force;

                //pass through force for hit tests in other scripts
                mystats.Force = Mathf.RoundToInt(Force);
            }
        }

        if (mystats.curHealth <= 0)
        {
            sus.StatsUI.SetActive(false);
            Destroy(this.gameObject);
        }
        
    }

    private Vector3 lastpos;
    void ForceReset()
    {
        //this is to prevent enemies from getting hurt for 5 health even if the player isnt moving anymore
        if (Vector3.Distance(lastpos, this.transform.position) < 0.2f)
        {
            mystats.Force = 0;
        }
        lastpos = this.transform.position;

    }
}

