using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingcamera : MonoBehaviour {

    

    public CustomInput control;

    public int controlState = 0;

    public float horizontalSpeed = 2.0F;
    public float verticalSpeed = 2.0F;
    public float speed = 2.0F;
    public float direction = 0.0F;
    public GameObject Cam;
    //public GameObject water1, water2;

    private float h = 0F;
    private float v = 0F;
    private float offsetH = 0F;
    private float desOffset = 0F;

    private float xSpeed = 0.0F;
    private float ySpeed = 0.0F;

    private float gravity = 0.0f;

    private float tempNum = 0.0f;

    public int forwardInput = 109; // w = 109
    public int backwardInput = 105; // s = 105
    public int rightInput = 90; // d = 90
    public int leftInput = 87; // a = 87

    /* // Replace with mouse movement inputs - or joystick directions
    public int forwardInputCam = 109; // w = 109
    public int backwardInputCam = 105; // s = 105
    public int rightInputCam = 90; // d = 90
    public int leftInputCam = 87; // a = 87
    */

    public int forwardCamInput = 325;
    public int backwardCamInput = 326;
    public int rightCamInput = 327;
    public int leftCamInput = 328;

    public float sensitivity = 80.0f;

    private float xAxis2Input = 0f;
    private float yAxis2Input = 0f;

    private float xAxisInput = 0f;
    private float yAxisInput = 0f;
    public int jumpInput = 8; // space key = 8
    private int jumpKey = 0;
    public Rigidbody rb;

    private int orbitCamInput = 136; // middle click = 136

    private Vector3 ledgeMemory;

    // Use this for initialization
    void Start () {

        // For keyboard Alternatives
        switch (controlState)
        {
            case 1: // Keyboard ASDW E
                forwardInput = 109; // w = 109
                backwardInput = 105; // s = 105
                rightInput = 90; // d = 90
                leftInput = 87; // a = 87
                jumpInput = 91; // e = 91
                break;
            case 2: // Keyboard GHJY U
                forwardInput = 111; // y = 111
                backwardInput = 94; // h = 94
                rightInput = 96; // j = 96
                leftInput = 93; // g = 93
                jumpInput = 107; // u = 107
                break;
            case 3: // Keyboard L:"P [
                forwardInput = 102; // p = 102
                backwardInput = 75; // ; = 75
                rightInput = 65; // ' = 65
                leftInput = 98; // l = 98
                jumpInput = 81; // [ = 81
                break;
            case 4: // Keyboard Arrow Keys + "Space"
                forwardInput = 26; // up = 26
                backwardInput = 27; // down = 27
                rightInput = 28; // right = 28
                leftInput = 29; // left = 29
                jumpInput = 8; // space = 8
                break;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        h = direction*90f; // 180f;
	}
	
	// Update is called once per frame
	void Update () {

        /*
        control.GetInputf(forwardCamInput)
        control.GetInputf(backwardCamInput)
        control.GetInputf(rightCamInput)
        control.GetInputf(leftCamInput)
        */

        // Camera Angle Input
        xAxis2Input = (control.GetInputf(rightCamInput) - control.GetInputf(leftCamInput)) * Time.deltaTime * sensitivity;
        yAxis2Input = (control.GetInputf(forwardCamInput) - control.GetInputf(backwardCamInput)) * Time.deltaTime * sensitivity;

        // Movement Input
        xAxisInput = control.GetInputf(rightInput) - control.GetInputf(leftInput);
        yAxisInput = control.GetInputf(forwardInput) - control.GetInputf(backwardInput);

        if (xAxisInput > 1.0f)
        {
            xAxisInput = 1.0f;
        }
        if (xAxisInput < -1.0f)
        {
            xAxisInput = -1.0f;
        }
        if (yAxisInput > 1.0f)
        {
            yAxisInput = 1.0f;
        }
        if (yAxisInput < -1.0f)
        {
            yAxisInput = -1.0f;
        }

        if (Mathf.Abs(xAxisInput) == 1.0f && Mathf.Abs(yAxisInput) == 1.0f)
        {
            xAxisInput = xAxisInput * 0.7071067811865475f;
            yAxisInput = yAxisInput * 0.7071067811865475f;
        }


        xSpeed = xSpeed + xAxisInput * speed;
        ySpeed = ySpeed + yAxisInput * speed;

        /*
        if (GetInputf(leftInput) == 1f)
        {
            xSpeed = xSpeed - speed;
            //transform.Translate(Vector3.left * speed * Time.deltaTime);
        }
        if (GetInputf(rightInput) == 1f)
        {
            xSpeed = xSpeed + speed;
            //transform.Translate(Vector3.right * speed * Time.deltaTime);
        }
        if (GetInputf(backwardInput) == 1f)
        {
            ySpeed = ySpeed - speed;
            //transform.Translate(Vector3.back * speed * Time.deltaTime);
        }
        if (GetInputf(forwardInput) == 1f)
        {
            ySpeed = ySpeed + speed;
            //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
        */

        desOffset = Mathf.Atan2(xSpeed, ySpeed)*56f;
        //*3.1415f * 180f

        if (control.GetInputf(103) == 1f)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }


        // If the player falls off of the map then set the player on the last ledge
        if (transform.position.y < -30)
        {
            //TODO: Remove health from player(s)
            rb.velocity = new Vector3(0, 1, 0);
            transform.position = ledgeMemory;
        }


        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.05f))
        {

            // TODO: Detect if it is a valid platform (Not a moving object)
            ledgeMemory = transform.position; // Remember the ledge position of the player

            if (control.GetInputf(jumpInput) == 1f && jumpKey == 0)
            {
                rb.velocity = new Vector3(0, 10, 0);
                jumpKey = 1;
                //transform.Translate(Vector3.back * Time.deltaTime);
            }
        }
        if (control.GetComponent<CustomInput>().GetInputf(jumpInput) == 0f)
        {
            jumpKey = 0;
        }

        // Cheat Jump
        /*
        if (GetInputf(jumpInput) == 1f)
        {
            gravity = 0.0f;
            rb.velocity = new Vector3(0, 20, 0);
            //jumpKey = 1;
            //transform.Translate(Vector3.back * Time.deltaTime);
        }
        */



        xSpeed = xSpeed * 0.92F;
        ySpeed = ySpeed * 0.92F;

        if (xSpeed > -0.1F && xSpeed < 0.1F)
        {
            xSpeed = 0.0F;
        }
        if (ySpeed > -0.1F && ySpeed < 0.1F)
        {
            ySpeed = 0.0F;
        }





        //transform.Translate(Vector3.right * xSpeed * Time.deltaTime);
        //transform.Translate(Vector3.forward * ySpeed * Time.deltaTime);


        if (control.GetInputf(orbitCamInput) == 0f)
        {
            offsetH = offsetH * 0.9f;
        }

        //tempNum = horizontalSpeed * Input.GetAxis("Mouse X");
        tempNum = horizontalSpeed * xAxis2Input + xAxisInput*2.5f * Time.deltaTime * 80.0f;
        h = h + tempNum;
        offsetH = offsetH + tempNum*0.25f;

        if (offsetH > 180f)
        {
            offsetH = offsetH - 360f;
        }
        if (offsetH < -180f)
        {
            offsetH = offsetH + 360f;
        }

        //Debug.Log(Input.GetAxis("Mouse Y"));
        //Debug.Log(xAxis2Input);
        //v = v + verticalSpeed * -Input.GetAxis("Mouse Y");
        v = v + verticalSpeed * -yAxis2Input;
        if (v < -15.0f)
        {
            v = -15.0f;
        }
        if (v > 80.0f)
        {
            v = 80.0f;
        }


        //print(offsetH);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.Rotate(0, h+offsetH, 0);

        //rb.velocity = new Vector3(xSpeed * Time.deltaTime, rb.velocity.y, rb.velocity.z);
        //rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, ySpeed * Time.deltaTime);


        rb.AddForce(transform.forward * 50 * ySpeed);

        //rb.MovePosition(Vector3.right * xSpeed * Time.deltaTime);
        //rb.MovePosition(Vector3.forward * ySpeed * Time.deltaTime);

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.81f))
        {
            gravity = 0.0f;
            if (rb.velocity.y < 0) {
                rb.velocity = new Vector3(0, 0, 0);
                //rb.velocity = new Vector3(rb.velocity.x * 0.8f * Time.deltaTime, rb.velocity.y, rb.velocity.z * 0.8f * Time.deltaTime);
            }
        } else
        {
            gravity = gravity + 0.25f;
        }

        //transform.Translate(Vector3.down * gravity * Time.deltaTime);

        transform.Rotate(v, 0, 0);

        transform.Translate(Vector3.forward * -2.5f);

        Cam.transform.SetPositionAndRotation(new Vector3(transform.position.x, transform.position.y+0.4f, transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        //Cam.transform.Translate(Vector3.right * 0.3f);

        //water1.transform.SetPositionAndRotation(new Vector3(transform.position.x, 24f, transform.position.z), new Quaternion(0, 0, 0, 0));
        //water2.transform.SetPositionAndRotation(new Vector3(transform.position.x, 24f, transform.position.z), new Quaternion(180, 0, 0, 0));

        //transform.eulerAngles = new Vector3(0, 0, 0);
        //transform.Rotate(0, 0, 0);
        //transform.PositionAndRotation(new Vector3(transform.position.x, transform.position.y, transform.position.z)
        transform.Translate(Vector3.forward * 2.5f);
        transform.eulerAngles = new Vector3(0, 0, 0);
        transform.Rotate(0, h+180+desOffset, 0);


    }






    









}
