using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingUIScript : MonoBehaviour
{
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(Vector3.up * 800f);
    }
    void Update()
    {
        if (this.transform.position.y <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
