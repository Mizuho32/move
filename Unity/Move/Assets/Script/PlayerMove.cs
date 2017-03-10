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

	private ManyMouse[] _manyMouseMice;
    private bool[,] buttons;
    private int[] wheels;
    private Vector2[] deltas;
    private bool ready = false;
    private int right, left;

    public float moveSpeed = 30.0f;
    public float rotSpeed = 100.0f;
    public UnityEngine.UI.Text text;

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

        Debug.Log(string.Format("H={0}, V={1}, X={2}, Y={3}", h.ToString(), v.ToString(), rotX, rotY));
        //Debug.Log(string.Format("{0} {1}", MultiInput.Mice[left].Delta, MultiInput.Mice[right].Delta));


        // move
        tr.Translate(Vector3.forward * moveSpeed * speed * Time.deltaTime);
        tr.Translate(Vector3.right * moveSpeed * h * Time.deltaTime);
        tr.Translate(Vector3.up * moveSpeed * v * Time.deltaTime);

        if (MultiInput.GetMouseButton(left, 0)) // right button, Roll
        {
            tr.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime * MultiInput.GetAxis(left, MultiInput.Axis.Mouse_X));
        }
        // Pitch, Yaw
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * rotX);
        tr.Rotate(Vector3.left * rotSpeed * Time.deltaTime * rotY);
    }
}
