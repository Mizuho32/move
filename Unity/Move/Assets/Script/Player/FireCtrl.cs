using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class FireCtrl : MonoBehaviour
{
    public GameObject bullet;
    public Transform[] fireposs;
    public float fireperiod = 0.5f;

    private Coroutine firing;
    private bool fire = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            firing = StartCoroutine(Fire());
        }

        if (Input.GetMouseButtonUp(0))
        {
            fire = false;
            StopCoroutine(firing);
        }
    }

    IEnumerator Fire()
    {
        fire = true;
        while (fire)
        {
            foreach (var firepos in fireposs)
            {
                Instantiate(bullet, firepos.position, firepos.rotation);
                yield return null;
            }
            yield return new WaitForSeconds(fireperiod);
        }
    }

    IEnumerator CreateBullet()
    {
        foreach (var firepos in fireposs)
        {
            Instantiate(bullet, firepos.position, firepos.rotation);
            yield return null;
        }
    }
}
