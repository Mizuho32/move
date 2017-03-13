using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletCtrl : MonoBehaviour
{
    public float speed = 1000.0f;
    private Transform tr;
    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * speed);
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
