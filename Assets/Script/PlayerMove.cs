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
    public float moveSpeed = 30.0f;
    public float rotSpeed = 100.0f;
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
    }

    // Update is called once per frame
    void Update()
    {
        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        rotX = Input.GetAxis("Mouse X");
        rotY = Input.GetAxis("Mouse Y");

        var wheel = -Input.GetAxis("Mouse ScrollWheel");
        if (wheel > 0 && speed > -1) speed -= 0.2f;
        if (wheel < 0 && speed < 1) speed += 0.2f;
        if (Input.GetMouseButton(2)) speed = 0.0f;

        Debug.Log("H=" + h.ToString());
        Debug.Log("V=" + v.ToString());

        tr.Translate(Vector3.forward * moveSpeed * speed * Time.deltaTime);
        tr.Translate(Vector3.right * moveSpeed * h * Time.deltaTime);
        tr.Translate(Vector3.up * moveSpeed * v * Time.deltaTime);
        tr.Rotate(Vector3.up * rotSpeed * Time.deltaTime * rotX);
        tr.Rotate(Vector3.left * rotSpeed * Time.deltaTime * rotY);
    }
}
