using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerPiggyScript : MonoBehaviour
{
    public float KeepDistance = 4f;
    public List<GameObject> Enemies = new List<GameObject>();

    private Rigidbody rb;
    private bool runhome;

    private Vector3 home;

    public GameObject DeathParticlePrefab;

    void Start()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Wolf"))
        {
            Enemies.Add(g);
        }
        Enemies.Add(GameObject.FindGameObjectWithTag("Player"));
        home = this.transform.position;
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        Vector3 _tpos = this.transform.position;

        foreach(GameObject enemy in Enemies)
        {
            if (enemy == null)
            {
                Enemies.Remove(enemy);
                break;
            }
            else
            {
                if (Vector3.Distance(_tpos, enemy.transform.position) < KeepDistance)
                {
                    Vector3 _epos = enemy.transform.position;
                    this.transform.rotation = Quaternion.Euler(0, -Mathf.Atan2(_tpos.z - _epos.z, _tpos.x - _epos.x) * Mathf.Rad2Deg + 90, 0);
                    rb.AddForce(transform.forward * 7f);
                    runhome = false;
                }
            }
        }

        //Stops piggies from wall hugging after running away from the player or wolves
        if (runhome)
        {
            this.transform.rotation = Quaternion.Euler(0, -Mathf.Atan2(home.z - _tpos.z, home.x - _tpos.x) * Mathf.Rad2Deg + 90, 0);
            rb.AddForce(transform.forward * 3f);
        }
        runhome = (Vector3.Distance(home, _tpos) < 0.5f) ? false : true;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Wolf"))
        {
            GameObject deathpart = Instantiate(DeathParticlePrefab);
            deathpart.transform.position = this.transform.position;
        }
    }
}
