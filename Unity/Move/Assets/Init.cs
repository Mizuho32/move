using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init : MonoBehaviour {

    // Use this for initialization
    private Vector3 initRot;
	void Start () {
        //var size = 20;
        //for(var x = 0; x < size; x++) 
        //    for(var y = 0; y < size; y++)
        //        for (var z = 0; z < size; z++)
        //        {
        //            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
        //            cube.transform.position = new Vector3(x, y, z);
        //        }
        initRot = transform.localEulerAngles;
    }

    // Update is called once per frame
    // screen ?x450
    private const float sensX = 6.0f, sensY = 6.0f;
    private float speed = 0.0f;
    private Vector3? clicked = null;
    private Vector3 centor = new Vector3(Screen.width/2, Screen.height/2, 0);
    private Quaternion q;
    void Update () {
        


	    if (Input.GetMouseButton(0))
        {
            if (!clicked.HasValue)
            {
                clicked = Input.mousePosition;
                //initRot = transform.localEulerAngles;
                q = transform.rotation;
            }

            Vector3 delta = Input.mousePosition - clicked.Value;
            //transform.localEulerAngles = initRot + new Vector3( 0, 0, -delta.x/Screen.width*180f);
            //transform.localEulerAngles += new Vector3( -delta.y/Screen.height*100f, 0, 0);
            transform.rotation = q * Quaternion.AngleAxis(-delta.x/Screen.width*180f, new Vector3(0,0,1)) * Quaternion.AngleAxis(-delta.y / Screen.height * 100f, new Vector3(1, 0, 0));
            

            //transform.Rotate( (delta.y > 0f ? -1f : 1f)*Mathf.Pow(delta.y/Screen.height, 2)*sensY, 0, (delta.x > 0f ? -1f : 1f)*Mathf.Pow(delta.x/Screen.width,2)*sensX);
            //transform.Rotate( -delta.y/Screen.height*2*sensY, 0, -delta.x/Screen.width*2*sensX);

        }else if (Input.GetMouseButton(1))
        {
            transform.position += transform.TransformDirection(Vector3.up) * (-speed);
        }else if (Input.GetMouseButton(2))
        {
            speed = 0f;
        }else
        {
            Vector3 delta = Input.mousePosition - centor;
            transform.Rotate( 0, 0, -delta.x/Screen.width*sensX);
            transform.Rotate(-delta.y / Screen.height * sensY, 0, 0);
            //transform.rotation *= Quaternion.AngleAxis(-delta.x / Screen.width * sensX, new Vector3(0, 0, 1)) *
            //    Quaternion.AngleAxis(-delta.y / Screen.height * sensY, new Vector3(1,0,0));

            //transform.localEulerAngles = initRot + new Vector3( -delta.y/Screen.height*100f, 0, -delta.x/Screen.width*180f);

            if (clicked.HasValue)
            {
                clicked = null;
                //initRot = transform.localEulerAngles;
            }
        }

        var wheel = -Input.GetAxis("Mouse ScrollWheel");
        if (wheel > 0) speed -= 0.05f;
        if (wheel < 0) speed += 0.05f;

        transform.position += transform.TransformDirection(Vector3.up) * speed;
        //float deltax = Input.GetAxis("Mouse X");
        //Debug.logger.Log(deltax);
	}
}
