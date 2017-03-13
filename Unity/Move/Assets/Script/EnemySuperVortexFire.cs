using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

        GenerateFireSources();
        FireAll();
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
    
    void GenerateFireSources()
    {

        if (NFireSource < 2 || NRow == 0) return;

        FireSources = new GameObject[NFireSource];

        // Generate Points on Sphere
        // http://d.hatena.ne.jp/MikuHatsune/20160714/1468397633

        FireSources[0] = Instantiate(FireSource, transform.position - transform.up * Radius, Quaternion.LookRotation(-transform.up));

        var phi = 0.0f;
        var sqN = Mathf.Sqrt(NFireSource);
        for (var i = 2; i < NFireSource; i++)
        {
            var hk = 2 * (i - 1.0f) / (NFireSource - 1.0f) - 1.0f;
            var theta = Mathf.Acos(hk);
            var sqhk = Mathf.Sqrt(1 - hk * hk);
            phi = phi + 3.6f / sqN * 1.0f / sqhk;
            var pos = transform.position + PointOnUnitSphere(theta / Mathf.PI * 180, phi / Mathf.PI * 180, transform) * Radius;
            var rot = Quaternion.LookRotation(pos - transform.position);
            FireSources[i - 1] = Instantiate(FireSource, pos, rot);
        }

        FireSources[NFireSource - 1] = Instantiate(FireSource, transform.position + transform.up * Radius, Quaternion.LookRotation(transform.up));

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

    void FireAll()
    {
        foreach (var source in FireSources)
        {
            var script = source.GetComponent<FireSourceCtrl>();
            script.bullet = bullet;
            script.shootperiod = shootperiod;
            script.Fire();
        }
    }

    
}
