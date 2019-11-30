using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public GameObject WolfPrefab;
    public GameObject PigPrefab;

    public float SpawnRadius;

    public int[] minMaxWolfs = new int[2];
    public int[] minMaxPigs = new int [2];

    public LevelMaster Master;

    void Start()
    {
        int Pigs = Random.Range(minMaxPigs[0], minMaxPigs[1]+1);
        int Wolves = Random.Range(minMaxWolfs[0], minMaxWolfs[1]+1);

        for (var i = 0; i < Pigs; i++)
        {
            GameObject pig = Instantiate(PigPrefab);
            pig.transform.position = new Vector3(Random.Range(-SpawnRadius, SpawnRadius), 2, Random.Range(-SpawnRadius, SpawnRadius));
        }
        for (var j = 0; j < Wolves; j++)
        {
            GameObject wolf = Instantiate(WolfPrefab);
            wolf.transform.position = new Vector3(Random.Range(-SpawnRadius, SpawnRadius), 2, Random.Range(-SpawnRadius, SpawnRadius));
            Master.Wolves.Add(wolf);
        }

    }
    
    void Update()
    {
        
    }
}
