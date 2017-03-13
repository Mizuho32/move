using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVortex2 : MonoBehaviour
{

    public Transform firepos;
    public GameObject bullet;
    public float shootperiod = 1.0f; //second
    private Transform tr;
    private float sec = 0.0f;
    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        //Fire();
    }

    // Update is called once per frame
    void Update()
    {
        sec += Time.deltaTime;
        if (sec >= shootperiod)
        {
            Fire();
            sec = 0.0f;
        }
    }

    void Fire()
    {
        StartCoroutine(this.CreateBullet());
    }

    IEnumerator CreateBullet()
    {
        Instantiate(bullet, firepos.position + new Vector3(0.0f, 0.5f, 0.0f), firepos.rotation);
        yield return null;
    }
}
