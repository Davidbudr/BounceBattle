using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterVariables))]
public class WolfScript : MonoBehaviour
{

    public enum AIMode {Hungry, Angry, Scared}
    
    public AIMode currentMood = AIMode.Angry;

    public List<GameObject> Piggies;
    public GameObject DeathParticlePrefab;

    private GameObject Player;

    private Rigidbody rb;

    private CharacterVariables mystats;

    private Vector3 home;
    private GameObject targetPiggy;

    private StatsUIScript suis;

    private GameObject halo;
    public GameObject haloPrefab;

    void Start()
    {
        suis = this.GetComponent<StatsUIScript>();
        Player = GameObject.Find("Player");
        home = this.transform.position;
        halo = Instantiate(haloPrefab);

        mystats = this.GetComponent<CharacterVariables>();
        rb = this.GetComponent<Rigidbody>();

        //foreach (GameObject g in GameObject.FindGameObjectsWithTag("Piggy"))
        //{
        //    Piggies.Add(g);
        //}
        
        InvokeRepeating("Activity", 3f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        halo.transform.position = this.transform.position;
        if (mystats.curHealth < 3)
        {
            currentMood = AIMode.Scared;
        }
        else
        {
            currentMood = AIMode.Angry;

            if (Piggies.ToArray().Length > 0)
            {
                foreach (GameObject pig in Piggies)
                {
                    if (pig != null)
                    {
                        if (Vector3.Distance(pig.transform.position, this.transform.position) < 5f)
                        {
                            currentMood = AIMode.Hungry;
                            targetPiggy = pig;
                        }
                    }
                    else
                    {
                        Piggies.Remove(pig);
                        break;
                    }
                }
            }
        }
        if (Player == null)
        {
            DestroyImmediate(this);
        }

        if (mystats.curHealth <= 0)
        {
            GameObject deathpart = Instantiate(DeathParticlePrefab);
            deathpart.transform.position = this.transform.position;
            Destroy(suis.StatsUI);
            Destroy(this.gameObject);
            Destroy(halo);
        }

    }

    void Activity()
    {

        if (currentMood == AIMode.Angry)
        {
            Follow(Player);
        }
        else if (currentMood == AIMode.Hungry)
        {
            Follow(targetPiggy);
        }
        else if (currentMood == AIMode.Scared)
        {
            //cant use follow since hes running away from player position
            Vector3 _tpos = this.transform.position;
            Vector3 _ppos = Player.transform.position;

            if (Vector3.Distance(_tpos,_ppos) < 4f)
            {
                if (mystats.curStamina > 1)
                {
                    Move((_tpos - _ppos), Mathf.RoundToInt(mystats.curStamina - 1));
                }
            }
            else
            {
                if (targetPiggy != null)
                {
                    Follow(targetPiggy);
                }
                else
                {
                    Move(home,1);
                }
            }

        }

        void Follow(GameObject _target)
        {
            Vector3 _thisPos = this.transform.position;
            Vector3 _tarPos = _target.transform.position;
            //following

            if (Vector3.Distance(_thisPos, _tarPos) > 4)
            {
                //sneak up
                if (mystats.curStamina > 1)
                {
                    Move(_target.transform.position, 1);
                }
            }
            else
            {
                //pounce
                if (mystats.curStamina >= 3)
                {
                    Move(_target.transform.position, 3);
                }
            }
        }

        void Move(Vector3 _targetpos, int _force)
        {
            this.transform.LookAt(_targetpos);

            mystats.curStamina -= _force;
            mystats.Force = _force;
            rb.AddForce(transform.forward * 500f * _force / 5);
            StartCoroutine(forceReset());
        }



    }

    IEnumerator forceReset()
    {
        yield return new WaitForSeconds(1f);
        mystats.Force = 0;
    }

}
