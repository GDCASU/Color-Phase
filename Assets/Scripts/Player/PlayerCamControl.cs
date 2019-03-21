using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;

public class PlayerCamControl : MonoBehaviour
{
    public int controlState = 0;

    //An array of cameras to switch between
    [Header("Cameras")]
    public GameObject[] cams = new GameObject[3]; //Third,overhead,side
    public int activecam = 0;
    public float sensitivity = 80.0f;

    [Header("Inputs")]
    public string camHorizAxis = "Mouse X";
    public string camVertAxis = "Mouse Y";

    [Header("Wall Prevention")]
    [SerializeField] private float sphereCastRadius = 2f;
    [SerializeField] private float radiusChangeRate = 0.25f;
    private float originalRadius;
    [SerializeField] private float castExtension = 3f; //This is an extension of the raycast maxDistance
    [SerializeField] private float distToWall = 1f; //This helps create more distance to the wall
    [SerializeField] private string layerMaskName = "PermamentWall"; //This is the string name of the layermask for the script to detect
    private int layerMask;

    [Header("Misc")]
    private KeyCode orbitCamInput = KeyCode.Mouse2;
    private float cameraHorizAngle = 0;
    private float cameraVertAngle = 0;
    private IInputPlayer player;
    public float radius = -2.5f;
    private float yOffset = 1.0f;

    //Camera restraint variables
    [SerializeField] private float minVertAngle = -20f;
    [SerializeField] private float maxVertAngle = 90f;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraHorizAngle = cams[0].transform.rotation.eulerAngles.y;
        cameraVertAngle = 0;

        originalRadius = radius;
        layerMask = ~LayerMask.NameToLayer(layerMaskName);
    }

    private void Update()
    {
        RaycastHit hit;
        Vector3 direction =  cams[0].transform.position - this.transform.position;

        //if (Physics.SphereCast(this.transform.position, sphereCastRadius, direction, out hit, Mathf.Abs(radius) + castExtension, layerMask))
        if (Physics.Raycast(this.transform.position, direction, out hit, Mathf.Abs(radius) + castExtension, layerMask))
        {
            Debug.Log("is touching");
            //Debug.Log("Distance: " + hit.distance);

            if(Mathf.Abs(radius) > hit.distance - distToWall)
            {
                radius += radiusChangeRate;
            }
        }
        else if (radius > originalRadius)
        {
            radius -= radiusChangeRate;
        }
        else if (radius < originalRadius)
        {
            radius = originalRadius;
        }
    }

    void LateUpdate()
    {        
        // Camera Angle Input
        // THIS IS OBSOLETE AND MUST BE UPDATED WHEN MOUSE IS ADDED TO INPUT MANAGER
        float xAxis = Input.GetAxis(camHorizAxis) * Time.deltaTime * sensitivity / 2;
        float yAxis = -Input.GetAxis(camVertAxis) * Time.deltaTime * sensitivity / 2;
        
        xAxis += InputManager.GetAxis(PlayerAxis.CameraHorizontal, player) * Time.deltaTime * sensitivity * 2;
        yAxis += -InputManager.GetAxis(PlayerAxis.CameraVertical, player) * Time.deltaTime * sensitivity;
        
        cameraHorizAngle += xAxis;
        if(cameraVertAngle + yAxis > -20f && cameraVertAngle + yAxis < maxVertAngle)
            cameraVertAngle += yAxis;

        switch (activecam)
        {
            case 0:
                cams[0].transform.rotation = Quaternion.Euler(0, cameraHorizAngle, 0);
                cams[0].transform.Rotate(Vector3.right, cameraVertAngle);
                cams[0].transform.position = transform.position + Vector3.up * 0.4f + cams[0].transform.forward * radius + Vector3.up*yOffset;
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
