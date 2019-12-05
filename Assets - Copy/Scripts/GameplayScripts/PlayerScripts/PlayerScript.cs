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
        Input.simulateMouseWithTouches = false;
    }

    void Update()
    {
        //Pause the game otherwise
        //used timescale instead of changing this to fixedupdate as there is a strange issue with defining the force otherwise.
        if (Time.timeScale != 0) 
        {
            ArrowUI.transform.position = this.transform.position + (Vector3.up * 0.3f);

            if (Input.GetMouseButtonDown(0))
            {
                ShooterInput(Input.mousePosition);
            }

            if (Input.touchCount > 0)
            {
                ShooterInput(Input.GetTouch(0).position);
            }
        }


        //sling
        if (shooting)
        {
            if (Input.GetMouseButton(0))
            {
                ForceDefine(Input.mousePosition);
            }
            if (Input.GetMouseButtonUp(0))
            {
                ForceApply();
            }

            if (Input.touchCount > 0)
            {
                ForceDefine(Input.GetTouch(0).position);
                if (Input.GetTouch(0).phase == TouchPhase.Moved)
                {

                }
                if (Input.GetTouch(0).phase == TouchPhase.Ended)
                {
                    ForceApply();
                }
            }
        }

        if (mystats.curHealth <= 0)
        {
            sus.StatsUI.SetActive(false);
            Destroy(this.gameObject);
        }

    }
    void ForceApply()
    {
        rb.AddForce(-transform.right * 700f * Force / 5f);
        sfxScript.Slingshot();
        StartCoroutine(forceReset());

        mystats.curStamina -= Force;
        mystats.Force = Mathf.RoundToInt(Force);
        shooting = false;
        SlingUI.SetActive(false);
        ArrowUI.SetActive(false);

        //pass through force for hit tests in other scripts
    }

    void ForceDefine(Vector3 _mp)
    {

        //This checks the distance between the mouse position and the players screen point
        Vector3 _pos = Camera.main.WorldToScreenPoint(this.transform.position);
        _pos.z = 0;
        Force = Vector2.Distance(_pos, _mp) / ((Input.touchCount > 0) ? 30 : 15);
        Force =  Mathf.Clamp(Force, 0, (mystats.curStamina >= 5) ? 5 : mystats.curStamina);
        SlingSlide.value = Force;

        //set the position and rotation of the power bar and the Pointer arrow
        SlingUI.transform.position = Camera.main.WorldToScreenPoint(this.transform.position);

        float _rot = Mathf.Atan2(_mp.y - SlingUI.transform.position.y, _mp.x - SlingUI.transform.position.x) * Mathf.Rad2Deg;

        SlingUI.transform.localRotation = Quaternion.Euler(0, 0, _rot);

        this.transform.rotation = Quaternion.Euler(0, -_rot, 0);
        ArrowUI.transform.rotation = Quaternion.Euler(0, -_rot, 0);
    }

    void ShooterInput(Vector3 _origin)
    {
        //check if pressed on player
        RaycastHit _hit;
        Ray _ray = Camera.main.ScreenPointToRay(_origin);
        if (Physics.Raycast(_ray, out _hit))
        {
            if (_hit.collider.gameObject.CompareTag("RaycastOrb"))
            {
                //activate the sling
                SlingUI.SetActive(true);
                ArrowUI.SetActive(true);
                shooting = true;
            }
        }
    }

    IEnumerator forceReset()
    {
        yield return new WaitForSeconds(1.25f);
        mystats.Force = 0;
    }

}

