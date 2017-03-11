using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillOwn : MonoBehaviour
{
    private Transform tr;
    public static float Arealength = 300.0f;
    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Pow(tr.position.x, 2) + Mathf.Pow(tr.position.y, 2) + Mathf.Pow(tr.position.z, 2) >= Arealength * Arealength)
        {
            Destroy(gameObject);
        }
    }
}
