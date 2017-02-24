using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour {
    private Transform tr;
    public float rotSpeed = 100.0f;
    // Use this for initialization
    void Start () {
        tr = GetComponent<Transform>();
    }
	
	// Update is called once per frame
	void Update () {
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "BULLET")
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
