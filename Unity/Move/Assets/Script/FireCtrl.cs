using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireCtrl : MonoBehaviour {
    public GameObject bullet;
    public Transform firepos;
    public Camera maincam;

    private PlayerMove pmove;

	// Use this for initialization
	void Start () {
        pmove = GetComponent<PlayerMove>();
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0) && pmove.IsReady)
        {
            Fire();
        }	
	}

    void Fire()
    {
        StartCoroutine(this.CreateBullet());
    }

    IEnumerator CreateBullet()
    {
        var forw = maincam.transform.forward;
        var inst = Instantiate(bullet, firepos.position, firepos.rotation);
        yield return null;

        var rigit = inst.GetComponent<Rigidbody>();
        rigit.velocity = transform.root.gameObject.GetComponent<Rigidbody>().velocity;
        rigit.AddForce(forw * 1000f);
    }
}
