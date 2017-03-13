using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemySuperVortexFire : MonoBehaviour
{

    public GameObject bullet;
    public GameObject FireSource;
    public int NFireSource;
    public float Radius;
    public float shootperiod = 0.1f; //second
    public float moveperiod = 2.0f;
    public float rotateperiod = 2.0f;
    public float Xrotspeed = 360.0f;
    public float Yrotspeed = 360.0f;

    private GameObject[] FireSources;
    private bool firestarted = false;
    private Rigidbody rig;
    private Transform tr;
    private float time4rotate = 0;

    // Use this for initialization
    void Start()
    {
        rig = GetComponent<Rigidbody>();
        tr = GetComponent<Transform>();

        GenerateFireSources();
        //FireAll();
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

        if (NFireSource < 2) return;

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

        foreach (var source in FireSources)
            source.transform.parent = transform;

    }

    public void StartFire()
    {
        if (firestarted) return;

        FireAll();
        StartCoroutine(Move());
        StartCoroutine(Rotate());

        firestarted = true;
    }

    private IEnumerator Rotate()
    {
        while (true)
        {
            //tr.Rotate(0, Mathf.Sin(2*Mathf.PI*4*time4rotate*time4rotate) * 90, 0);
            rig.AddTorque(0, Mathf.Sin(2 * Mathf.PI * 0.5f * time4rotate * time4rotate) * 90, 0.5f*Mathf.Sin(2 * Mathf.PI * 0.5f * time4rotate * time4rotate) * 90);
            time4rotate += Time.deltaTime;
            yield return new WaitForSeconds(rotateperiod);
        }
    }
    private IEnumerator Move()
    {
        while (true)
        {
            //transform.Translate(Random.onUnitSphere * Xrotspeed);
            //rig.velocity = Random.onUnitSphere * Xrotspeed;
            rig.AddForce(Random.onUnitSphere * Xrotspeed);
            yield return new WaitForSeconds(moveperiod);
        }
    }

    public void FireAll()
    {
        foreach (var source in FireSources)
        {
            var script = source.GetComponent<FireSourceCtrl>();
            script.bullet = bullet;
            script.shootperiod = shootperiod;
            script.Fire();
        }
    }
    
    public void StopAll()
    {
        foreach (var source in FireSources)
        {
            var script = source.GetComponent<FireSourceCtrl>();
            script.Stop();
        }
    }
    
}
