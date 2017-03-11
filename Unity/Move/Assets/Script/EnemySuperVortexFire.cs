using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperVortexFire : MonoBehaviour
{

    public Transform[] firepos;
    public GameObject bullet;
    public Transform[] dammyposX;
    public Transform[] dammyposY;
    public float shootperiod = 0.1f; //second
    public float changeperiod = 2.0f;
    public float Xrotspeed = 360.0f;
    public float Yrotspeed = 360.0f;
    private Transform tr;
    private float sec = 0.0f;
    private float changesec = 0.0f;
    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dammyposX.Length; i++)
        {
            if (changesec < changeperiod / 4)
            {
                dammyposX[i].Rotate(transform.right, Xrotspeed * Time.deltaTime);
                dammyposY[i].Rotate(transform.up, Yrotspeed * Time.deltaTime);
            }
            else if (changesec < changeperiod / 2)
            {
                dammyposY[i].Rotate(transform.up, Yrotspeed * Time.deltaTime);
            }
            else if (changesec < 3 * changeperiod / 4)
            {
                dammyposX[i].Rotate(transform.right, -Xrotspeed * Time.deltaTime);
            }
            else
            {
                dammyposY[i].Rotate(transform.up, -Yrotspeed * Time.deltaTime);
            }
        }
        sec += Time.deltaTime;
        changesec += Time.deltaTime;
        if (changesec >= changeperiod)
        {
            changesec = 0.0f;
        }
        if (sec >= shootperiod)
        {
            Fire();
            sec = 0.0f;
        }
    }

    void Fire()
    {
        for (int i = 0; i < dammyposX.Length; i++)
        {
            StartCoroutine(this.CreateBullet(i));
        }
    }

    IEnumerator CreateBullet(int i)
    {
        Instantiate(bullet, firepos[i].position, firepos[i].rotation);
        yield return null;
    }
}
