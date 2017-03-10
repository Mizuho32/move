using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BiggerBullet : MonoBehaviour
{

    public float speed = 1000.0f;
    public float bigspeed = 1.0f;
    public float maxScale = 10.0f;
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
        if (tr.localScale.x < maxScale)
        {
            tr.localScale += new Vector3(bigspeed * Time.deltaTime, bigspeed * Time.deltaTime, bigspeed * Time.deltaTime);
        }
    }
}
