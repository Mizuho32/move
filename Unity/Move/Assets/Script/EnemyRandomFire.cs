using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRandomFire : MonoBehaviour
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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "BULLET")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
        }
    }
}