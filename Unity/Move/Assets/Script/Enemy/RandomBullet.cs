using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomBullet : MonoBehaviour
{

    public float speed = 1000.0f;
    private Transform tr;
    // Use this for initialization
    void Start()
    {
        //Random.InitState((int)Time.deltaTime * 100);
        //GetComponent<Rigidbody>().velocity = Random.onUnitSphere * speed;
        GetComponent<Rigidbody>().AddForce(Random.onUnitSphere * speed);
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
