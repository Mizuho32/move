using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVortex : MonoBehaviour
{
    public Transform firepos;
    public GameObject bullet;
    public Transform dammypos;
    public float shootperiod = 1000.0f; //second
    public float uprotspeed = 360.0f;
    public float dammyrotspeed = 360.0f;
    private Transform tr;
    private float sec = 0.0f;
    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        firepos.RotateAround(dammypos.position, dammypos.up, uprotspeed * Time.deltaTime);
        dammypos.Rotate(transform.right, dammyrotspeed * Time.deltaTime);
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
        Instantiate(bullet, firepos.position, firepos.rotation);
        yield return null;
    }
}
