using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySuperVortexFire : MonoBehaviour
{

    public Transform[] firepos;
    public GameObject bullet;
    public GameObject FireSource;
    public int NFireSource;
    public int NRow;
    public float Radius;
    public Transform[] dammyposX;
    public Transform[] dammyposY;
    public float shootperiod = 0.1f; //second
    public float changeperiod = 2.0f;
    public float Xrotspeed = 360.0f;
    public float Yrotspeed = 360.0f;

    private Transform tr;
    private float changesec = 0.0f;
    private GameObject[] FireSources;

    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();

        StartCoroutine(GenerateFireSources());
        StartCoroutine(Fire());
        //StartCoroutine(RotateDammyPos());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private static Vector3 PointOnUnitSphere(float theta, float phi, Transform transform)
    {
        return Quaternion.AngleAxis(phi, transform.up) * Quaternion.AngleAxis(theta, transform.forward) * transform.up;
    }
    
    IEnumerator GenerateFireSources()
    {

        if (NFireSource < 2 || NRow == 0) yield break;

        FireSources = new GameObject[NFireSource];

        FireSources[0] = Instantiate(FireSource, transform.position + transform.up * Radius, Quaternion.LookRotation(transform.up));
        yield return null;

        FireSources[1] = Instantiate(FireSource, transform.position - transform.up * Radius, Quaternion.LookRotation(-transform.up));
        yield return null;

        var NCol = (NFireSource - 2) / NRow;
        var Dtheta = 180.0f / (NRow + 1);
        var Dphi = 360.0f / NCol;
        var index = 2;

        for (var row = 0; row < NRow; row++)
        {
            var ini = Dphi / 2 * (row % 2);
            for (var i = 0; i < NCol; i++)
            {
                //var pos = transform.position + (Quaternion.AngleAxis(ini + Dtheta * i, transform.up) * Quaternion.AngleAxis(Dphi * (row + 1), transform.forward) * transform.up * Radius);
                var pos = transform.position + PointOnUnitSphere(Dtheta*(row+1), ini + Dphi*i, transform) * Radius;
                var rot = Quaternion.LookRotation(pos - transform.position);
                FireSources[index] = Instantiate(FireSource, pos, rot);
                yield return null;
                index++;
            }
        }
    }

    IEnumerator RotateDammyPos()
    {
        while (true)
        {
            var axis = Random.onUnitSphere;
            for (var i = 0; i < dammyposX.Length; i++)
            {
                //if (changesec < changeperiod / 4)
                //{
                dammyposX[i].Rotate(axis, Xrotspeed);
                dammyposY[i].Rotate(transform.up, Yrotspeed);
                //}
                //else if (changesec < changeperiod / 2)
                //{
                //    dammyposY[i].Rotate(transform.up, Yrotspeed * Time.deltaTime);
                //}
                //else if (changesec < 3 * changeperiod / 4)
                //{
                //    dammyposX[i].Rotate(transform.right, -Xrotspeed * Time.deltaTime);
                //}
                //else
                //{
                //    dammyposY[i].Rotate(transform.up, -Yrotspeed * Time.deltaTime);
                //}
            }
            yield return new WaitForSeconds(changeperiod);
        }
    }

    IEnumerator Fire()
    {
        while (true)
        {
            for (int i = 0; i < FireSources.Length; i++)
            {
                Instantiate(bullet, FireSources[i].transform.position, FireSources[i].transform.rotation);
                yield return null;
            }
            yield return new WaitForSeconds(shootperiod);
        }
    }
    
}
