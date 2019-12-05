using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PigScript : MonoBehaviour
{
    private NavMeshAgent navi;

    public float KeepDistance = 4f;
    public List<GameObject> Enemies = new List<GameObject>();

    public GameObject DeathParticlePrefab;

    void Start()
    {
        navi = this.GetComponent<NavMeshAgent>();
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("Wolf"))
        {
            Enemies.Add(g);
        }
        Enemies.Add(GameObject.FindGameObjectWithTag("Player"));

        InvokeRepeating("NewHome", 0, 2f);
    }

    void NewHome()
    {
        navi.SetDestination(new Vector3(Random.Range(-5, 5), 0, Random.Range(-5, 5)));
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
