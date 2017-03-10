using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomFire : MonoBehaviour
{
    public Transform firepos;
    public GameObject bullet;
    public float shootperiod = 1000.0f; //second
    private Transform tr;
    private float sec = 0.0f;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        //Random.InitState((int)Time.deltaTime * 100);
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(RandomFire(10, 2));
    }

    IEnumerator RandomFire(float spd, int count)
    {

        yield return new WaitForSeconds(shootperiod);
        for (var i = 0; i < count; i++)
            Instantiate(bullet, firepos.position, firepos.rotation);
            //Instantiate(bullet, firepos.position, firepos.rotation).GetComponent<Rigidbody>().velocity = Random.onUnitSphere * spd;
    }
}