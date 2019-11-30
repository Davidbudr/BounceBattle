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

    private List<GameObject> PigList = new List<GameObject>();
    private List<GameObject> WolfList = new List<GameObject>();

    void Start()
    {
        int Pigs = Random.Range(minMaxPigs[0], minMaxPigs[1]+1);
        int Wolves = Random.Range(minMaxWolfs[0], minMaxWolfs[1]+1);

        for (var i = 0; i < Pigs; i++)
        {
            Vector3 _myposition = ReturnEmptyPosition();
            

            GameObject pig = Instantiate(PigPrefab);
            pig.transform.SetParent(this.transform);
            pig.transform.position = _myposition;
            PigList.Add(pig);
        }
        for (var j = 0; j < Wolves; j++)
        {
            Vector3 _myposition = ReturnEmptyPosition();

            GameObject wolf = Instantiate(WolfPrefab);
            wolf.transform.SetParent(this.transform);
            wolf.transform.position = _myposition;
            wolf.GetComponent<WolfScript>().Piggies = PigList;
            WolfList.Add(wolf);
        }
        Master.Wolves = WolfList;


    }
    Vector3 ReturnEmptyPosition()
    {
        while (true)
        {
            Vector3 _mpos = new Vector3(Random.Range(-SpawnRadius, SpawnRadius), 2, Random.Range(-SpawnRadius, SpawnRadius));
            if (Physics.CheckSphere(_mpos, 1f))
            {
                continue;
            }
            else
            {
                return _mpos;
            }
        }
    }
}
