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


    private KeyCode orbitCamInput = KeyCode.Mouse2;
    private float cameraHorizAngle = 0;
    private float cameraVertAngle = 0;
    private IInputPlayer player;
    public float MaxRadius = 2.5f;
    public float MinRadius = 0.1f;
    public float radius = 2.5f;
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
    }
    void Update () {
        // layer mask for color and color ignore laeyrs (interact and barrier)
        var layerMask = ~(1 << 20 | 1 << 21 | 1 << 22 | 1 << 23 | 1 << 25 | 1 << 26 | 1 << 27 | 1 << 28);
        RaycastHit hit;
        radius = 999;
        if( Physics.Linecast( transform.position+ Vector3.up*2, getCamPosition(), out hit, layerMask, QueryTriggerInteraction.Ignore ) )
            radius = hit.distance - 0.2f;
        
        radius = Mathf.Clamp( radius, MinRadius, MaxRadius );
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
                cams[0].transform.position = getCamPosition ();
                break;
            case 1:
                //edit to look down
                break;
            case 3:
                break;
        }
    }

    public Vector3 getCamPosition () {
        cams[0].transform.rotation = Quaternion.Euler(0, cameraHorizAngle, 0);
        cams[0].transform.Rotate(Vector3.right, cameraVertAngle);
        return transform.position + Vector3.up * 0.4f - cams[0].transform.forward * radius + Vector3.up*yOffset * (radius/MaxRadius);
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
