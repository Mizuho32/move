﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireSourceCtrl : MonoBehaviour {

    public GameObject bullet = null;
    public float shootperiod = 0.1f;

    private bool fire = false;
    private Coroutine firing;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Stop()
    {
        fire = false;
        StopCoroutine(firing);
    }

    public void Fire()
    {
        fire = true;
        if (bullet == null) Debug.LogError("bullet is Null");

        firing = StartCoroutine(_Fire());
    }

    private IEnumerator _Fire()
    {
        while (fire)
        {
            Instantiate(bullet, transform.position, transform.rotation);
            yield return new WaitForSeconds(shootperiod);
        }
    }
}
