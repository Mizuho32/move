using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMove : MonoBehaviour
{
    private float h;
    private float v;
    private float rotX;
    private float rotY;
    private float speed = 0.0f;
    private Transform tr;
    private Rigidbody rig;

    private bool ready = false;
    private int right, left;

    public float moveSpeed = 30.0f;
    public float rotSpeed = 100.0f;
    public UnityEngine.UI.Text text;
    public Camera maincam;
    
    public bool IsReady { get { return ready; } }

    //awake
    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    // Use this for initialization
    void Start()
    {
        tr = GetComponent<Transform>();
        rig = GetComponent<Rigidbody>();

        MultiInput.Init(MultiInput.Mode.Multi);

        StartCoroutine(mousemap());
    }

    IEnumerator mousemap()
    {
        text.text = "Click Left";

        // Set Left Mouse
        while (true) {
            var clicked = MultiInput.Mice.Where(m => m.MouseButtons[0]);
            if (clicked.Count() == 0)
            {
                yield return null;
                continue;
            }

            Debug.Log(string.Format("{0} is Left", left = clicked.ElementAt(0).id));
            goto right;
        }

        right:

        yield return new WaitForSeconds(0.5f);

        text.text = "Click Right";

        // Set Right Mouse
        while (true) {
            var clicked = MultiInput.Mice.Where(m => m.MouseButtons[0]);
            if (clicked.Count() == 0)
            {
                yield return null;
                continue;
            }

            Debug.Log(string.Format("{0} is Right", right = clicked.ElementAt(0).id));
            goto fin;
        }

        fin:

        text.text = string.Format("Left{0}, Right{1}", left, right);
        ready = true;
    }

    static string tos(bool[,] bu){
        var r = "";
        for (var i = 0; i < bu.GetLength(0); i++) {
            r += i.ToString() + ": [";
            for (var i2 = 0; i2 < bu.GetLength(1); i2++)
                r += bu[i, i2].ToString() + " ";

            r += "] ";
        }
        return r;
    }


    // Update is called once per frame
    void Update()
    {
        if (!ready)
            return;

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        rotX = MultiInput.GetAxis(right, MultiInput.Axis.Mouse_X);
        rotY = MultiInput.GetAxis(right, MultiInput.Axis.Mouse_Y);

        var wheel = -MultiInput.GetAxis(right, MultiInput.Axis.Mouse_ScrollWheel);
        if (wheel > 0 && speed > -1) speed -= 0.2f;
        if (wheel < 0 && speed < 1) speed += 0.2f;
        if (MultiInput.GetMouseButton(right, 2)) speed = 0.0f;

        //Debug.Log(string.Format("H={0}, V={1}, X={2}, Y={3}", h.ToString(), v.ToString(), rotX, rotY));
        //Debug.Log(string.Format("{0} {1}", MultiInput.Mice[left].Delta, MultiInput.Mice[right].Delta));
        //Debug.Log(GetComponent<Rigidbody>().velocity);


        // move
        rig.velocity = transform.forward * moveSpeed * speed + Vector3.right * moveSpeed *h + Vector3.up * moveSpeed * v;

        // Rotate
        if (MultiInput.GetMouseButton(left, 0)) // Roll by left mouse
        {
            tr.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime * MultiInput.GetAxis(left, MultiInput.Axis.Mouse_X));
        } else
        {
            // Camera rot
            maincam.transform.Rotate(Vector3.up * rotSpeed * Time.deltaTime * MultiInput.GetAxis(left, MultiInput.Axis.Mouse_X));
            maincam.transform.Rotate(Vector3.left * rotSpeed * Time.deltaTime * MultiInput.GetAxis(left, MultiInput.Axis.Mouse_Y));
        }

        // Pitch, Yaw
        rig.MoveRotation(rig.rotation * Quaternion.Euler(Vector3.up * rotSpeed * Time.deltaTime * rotX + Vector3.left * rotSpeed * Time.deltaTime * rotY));

        // Rotate Camera
        if (MultiInput.GetMouseButton(left, 2))
        {
            maincam.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

    }
}
