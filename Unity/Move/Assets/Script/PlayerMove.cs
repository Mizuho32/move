using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

	    int numMice = ManyMouseWrapper.MouseCount;
        buttons = new bool[numMice, 3];
        wheels = new int[numMice];
        deltas = new Vector2[numMice];

        if (numMice > 0)
        {
            _manyMouseMice = new ManyMouse[numMice];
            for (int i = 0; i < numMice; i++)
            {
                _manyMouseMice[i] = ManyMouseWrapper.GetMouseByID(i);
                Debug.Log(_manyMouseMice[i].DeviceName);
                _manyMouseMice[i].EventButtonDown += (ManyMouse mouse, int id) =>
                {
                    buttons[mouse.id, id] = true;
                    Debug.Log(string.Format("{0} down", mouse.id));
                };
                _manyMouseMice[i].EventButtonUp += (ManyMouse mouse, int id) =>
                {
                    buttons[mouse.id, id] = false;
                    Debug.Log(string.Format("{0} up", mouse.id));
                };
                /*_manyMouseMice[i].EventWheelScroll += (ManyMouse m, int delta) => {
                    wheels[m.id] = delta;
                    Debug.Log(string.Format("wheel{0}: {1}", m.id, delta));
                };
                _manyMouseMice[i].EventMouseDelta += (ManyMouse m, Vector2 delta) =>
                {
                    //deltas[m.id].x = delta.x;
                    //deltas[m.id].y = delta.y;
                    //deltas[m.id].x = m.Delta.x;
                    //deltas[m.id].y = m.Delta.y;
                    Debug.Log(string.Format("delta{0}: {1}", m.id, m.Delta));
                };*/
                //you'll want to pay attention to things disconnecting.
                //_manyMouseMice[i].EventMouseDisconnected += EventMouseDisconnected;
            }
        }

        StartCoroutine("mousemap");
    }

    IEnumerator mousemap()
    {
        text.text = "Click Left";

        // Set Left Mouse
        while (true) {
            var l = buttons.GetLength(0);
            for(var i = 0; i < l; i++)
            {
                if (buttons[i, 0])
                {
                    Debug.Log(string.Format("{0} is Left", left = i));
                    goto right;
                }
            }
            yield return null;
        }

        right:

        yield return new WaitForSeconds(0.5f);

        text.text = "Click Right";

        // Set Right Mouse
        while (true) {
            var l = buttons.GetLength(0);
            for(var i = 0; i < l; i++)
            {
                if (buttons[i, 0])
                {
                    Debug.Log(string.Format("{0} is Right", right = i));
                    goto fin;
                }
            }
            yield return null;
        }

        fin:

        text.text = string.Format("Left{0}, Right{1}", left, right);
        //yield break;
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
        rotX = _manyMouseMice[right].Delta.x;
        rotY = -_manyMouseMice[right].Delta.y;

        var wheel = -_manyMouseMice[right].MouseWheel;
        if (wheel > 0 && speed > -1) speed -= 0.2f;
        if (wheel < 0 && speed < 1) speed += 0.2f;
        if (_manyMouseMice[right].MouseButtons[2]) speed = 0.0f;

        //Debug.Log(string.Format("H={0}, V={1}, X={2}, Y={3}", h.ToString(), v.ToString(), rotX, rotY));
        Debug.Log(string.Format("{0} {1}", _manyMouseMice[left].Delta, _manyMouseMice[right].Delta));


        // move
        tr.Translate(Vector3.forward * moveSpeed * speed * Time.deltaTime);
        tr.Translate(Vector3.right * moveSpeed * h * Time.deltaTime);
        tr.Translate(Vector3.up * moveSpeed * v * Time.deltaTime);

        if (_manyMouseMice[left].MouseButtons[0]) // right button, Roll
        {
            tr.Rotate(-Vector3.forward * rotSpeed * Time.deltaTime * _manyMouseMice[left].Delta.x);
        }
        // Pitch, Yaw
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * rotX);
        tr.Rotate(Vector3.left * rotSpeed * Time.deltaTime * rotY);
    }
}
