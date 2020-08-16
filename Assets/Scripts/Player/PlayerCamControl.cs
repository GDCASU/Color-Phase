using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;

public class PlayerCamControl : MonoBehaviour
{
    public GameObject cam;
    public float sensitivity = 80.0f;
    private float cameraHorizAngle = 0;
    private float cameraVertAngle = 0;
    public float MaxRadius = 3.0f;
    public float MinRadius = 0.1f;
    public float radius = 2.5f;
    
    private float yOffset = 1.0f;
    private Vector3 hitNormal;
    private Vector3 nextHitNormal;
    private Vector3 lastHitNormal;
    private float ticker = 25f;

    public float upVector = 2f;

    //Camera restraint variables
    [SerializeField] private float minVertAngle = -20f;
    [SerializeField] private float maxVertAngle = 90f;

    // Use this for initialization
    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        cameraHorizAngle = cam.transform.rotation.eulerAngles.y;
        cameraVertAngle = 0;
    }
    void Update()
    {
        // layer mask for color and color ignore laeyrs (interact and barrier)
        var layerMask = ~(1 << 20 | 1 << 21 | 1 << 22 | 1 << 23 | 1 << 25 | 1 << 26 | 1 << 27 | 1 << 28 | 1 << 29);
        RaycastHit hit;
        radius = MaxRadius+1;
        if (Physics.Linecast(transform.position + Vector3.up * upVector, getCamPosition(), out hit, layerMask, QueryTriggerInteraction.Ignore))
        {
            radius = hit.distance - 0.2f;
            if (nextHitNormal != hit.normal)
            {
                lastHitNormal = hitNormal;
                ticker = 0.25f;
            }
            nextHitNormal = hit.normal;
        }
        else
        {
            nextHitNormal = Vector3.zero;
            ticker = 0.25f;
        }

        // Ticker is an interpolator for our hit normal
        // We use this too offset from anything on the xaxis from the camera 
        // This is not used as part of the testing raycast
        // Note that we test the position directly out and then offset when *setting* the position only
        hitNormal = Vector3.Lerp(lastHitNormal, nextHitNormal, ticker);
        ticker += 0.1f;
        ticker = Mathf.Clamp(ticker, 0, 1);
        radius = Mathf.Clamp(radius, MinRadius, MaxRadius);
    }
    void LateUpdate()
    {
        // Camera Angle Input
        var xAxis = InputManager.GetAxis(PlayerAxis.CameraHorizontal) * Time.deltaTime * sensitivity * 2;
        var yAxis = -InputManager.GetAxis(PlayerAxis.CameraVertical) * Time.deltaTime * sensitivity;

        cameraHorizAngle += xAxis;
        if (cameraVertAngle + yAxis > minVertAngle && cameraVertAngle + yAxis < maxVertAngle)
            cameraVertAngle += yAxis;

        // Additional dynamic scaling from interpolated hit normal 
        cam.transform.position = getCamPosition() + hitNormal * 0.25f;
    }

    public Vector3 getCamPosition()
    {
        cam.transform.rotation = Quaternion.Euler(0, cameraHorizAngle, 0);
        cam.transform.Rotate(Vector3.right, cameraVertAngle);
        // Position taken as combination of player location + static height offset + the radius out from player + a dynamic height offset based on the camera zoom + a dynamic height based on camera angle 
        return transform.position + Vector3.up * 0.4f - cam.transform.forward * (radius + 0.1f) + Vector3.up * yOffset * (radius / MaxRadius) + Vector3.up * -Mathf.Min(0, cameraVertAngle / 100 );
    }
    
    // Legacy
    /* 
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
    }*/
}
