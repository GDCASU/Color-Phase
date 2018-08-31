using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flyingcamera : MonoBehaviour
{
    
    public int controlState = 0;
    public float speed = 2.0F;
    public float direction = 0.0F;

    //An array of cameras to switch between
    [Header("Cameras")]
    public GameObject[] cams = new GameObject[3]; //Third,overhead,side
    public int activecam = 0;
    public float sensitivity = 80.0f;

    [Header("Inputs")]
    public KeyCode forward = KeyCode.W;
    public KeyCode backward = KeyCode.S;
    public KeyCode right = KeyCode.D;
    public KeyCode left = KeyCode.A;
    public string camHorizAxis = "Mouse X";
    public string camVertAxis = "Mouse Y";
    public KeyCode jumpInput = KeyCode.Space;


    private KeyCode orbitCamInput = KeyCode.Mouse2;
    private Vector3 ledgeMemory;
    private Rigidbody rb;
    private bool jumping = false;
    private float cameraHorizAngle = 0;
    private float cameraVertAngle = 0;

    // Use this for initialization
    void Start()
    {

        // For keyboard Alternatives
        switch (controlState)
        {
            case 1: // Keyboard ASDW Space
                forward = KeyCode.W;
                backward = KeyCode.S;
                right = KeyCode.D;
                left = KeyCode.A;
                jumpInput = KeyCode.Space;
                break;
            case 2: // Keyboard GHJY U
                forward = KeyCode.Y;
                backward = KeyCode.H;
                right = KeyCode.J;
                left = KeyCode.G;
                jumpInput = KeyCode.U;
                break;
            case 3: // Keyboard L:"P [
                forward = KeyCode.P;
                backward = KeyCode.Semicolon;
                right = KeyCode.Quote;
                left = KeyCode.L;
                jumpInput = KeyCode.LeftBracket;
                break;
            case 4: // Keyboard Arrow Keys + numpad 0
                forward = KeyCode.UpArrow;
                backward = KeyCode.DownArrow;
                right = KeyCode.RightArrow;
                left = KeyCode.LeftArrow;
                jumpInput = KeyCode.Keypad0;
                break;
        }

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        cameraHorizAngle = cams[0].transform.rotation.eulerAngles.y;
        cameraVertAngle = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Movement Input
        float xAxis = 0;
        float zAxis = 0;

        if (Input.GetKey(left)) xAxis--;
        if (Input.GetKey(right)) xAxis++;
        if (Input.GetKey(forward)) zAxis++;
        if (Input.GetKey(backward)) zAxis--;

        if (xAxis != 0 && zAxis != 0)
        {
            xAxis *= Mathf.Sqrt(2) / 2;
            zAxis *= Mathf.Sqrt(2) / 2;
        }
        
        if (Input.GetKey(KeyCode.Q))
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

            if (Input.GetKey(jumpInput) && !jumping)
            {
                rb.velocity = new Vector3(rb.velocity.x, 10, rb.velocity.z);
                jumping = true;
            }
        }
        if (!Input.GetKey(jumpInput))
        {
            jumping = false;
        }

        //uncomment to prevent movement mid-air
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.81f))
        {
            Vector3 push = (cams[activecam].transform.forward * zAxis + cams[activecam].transform.right * xAxis) * speed * 50;
            rb.AddForce(push, ForceMode.Acceleration);
            transform.LookAt(transform.position + push);
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
        }
    }

    void LateUpdate()
    {
        // Camera Angle Input
        float xAxis = Input.GetAxis(camHorizAxis) * Time.deltaTime * sensitivity;
        float yAxis = -Input.GetAxis(camVertAxis) * Time.deltaTime * sensitivity;
        cameraHorizAngle += xAxis;
        cameraVertAngle += yAxis;
        switch (activecam)
        {
            case 0:
                cams[0].transform.rotation = Quaternion.Euler(0, cameraHorizAngle, 0);
                cams[0].transform.Rotate(Vector3.right, cameraVertAngle);
                cams[0].transform.position = transform.position + Vector3.up * 0.4f + cams[0].transform.forward * -2.5f;
                break;
            case 1:
                //edit to look down
                break;
            case 3:
                break;
        }
    }

    public void ChangeCamera(int camNumber)
    {
        if (camNumber == -1)
        {
            if (cams[activecam] != null)
                cams[activecam].SetActive(false);
            activecam++;
            activecam = activecam % cams.Length;
            if (cams[activecam] != null)
                cams[activecam].SetActive(true);
            else
                ChangeCamera(camNumber);
        }
        else
        {
            if (cams[camNumber] != null)
            {
                cams[activecam].SetActive(false);
                activecam = camNumber;
                cams[activecam].SetActive(true);
            }
        }
    }
}
