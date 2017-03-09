﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexBullet : MonoBehaviour
{
    public float speed = 1000.0f;
    public float rotspeed = 1000.0f;
    public float radius = 1.0f;
    private Rigidbody rb;
    private Transform tr;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * speed);
        rb.centerOfMass = new Vector3(0, radius, 0);
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.angularVelocity = new Vector3(0, 0, rotspeed*Time.deltaTime);
        if (Mathf.Pow(tr.position.x, 2) + Mathf.Pow(tr.position.y, 2) + Mathf.Pow(tr.position.z, 2) >= 1000 * 1000)
        {
            Destroy(gameObject);
        }
    }
}
