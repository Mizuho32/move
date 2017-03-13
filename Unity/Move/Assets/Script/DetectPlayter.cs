using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectPlayter : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GetComponentInParent<Renderer>().material.color = new Color(0, 0, 0, 0);
            GetComponentInParent<EnemySuperVortexFire>().StartFire();
            Debug.Log(string.Format("{0} StartFire", other.name));
        }
    }
}
