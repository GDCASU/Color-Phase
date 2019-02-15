using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * This class is to be used to prevent the camera from going
 * into the walls. Note that in order for this to work the camera needs a 
 * sphere collider attached to it and have it set to being a trigger.
 * 
 * Initially I set the radius of the collider to be 0.6 and had the distToWall
 * be 0.5. This gap in radius was to help prevent the camera from bouncing back and forth.
 * If you have any questions on this ask Nicholas Nguyen
 */
public class CamWallPrevention : MonoBehaviour {

    [SerializeField] private PlayerCamControl camControl;

    [SerializeField] private string layerMaskName = "PermamentWall"; //This is the string name of the layermask for the script to detect
    private int layerMask;
    [SerializeField] private float distToWall = 0.5f; //This is the distance to the wall that the radius will shorten
    [SerializeField] private float radiusReductionRate = 0.04f;
    [SerializeField] private float radiusIncreaseRate = 0.1f;
    [SerializeField] private float smallestRadius = 0f; //This is the smallest value that the radius can be
    private float originalRadius;
    private bool isTouching;

    private void Awake()
    {
        layerMask = LayerMask.NameToLayer(layerMaskName); //This gets the int index of the specified layer mask
        originalRadius = camControl.radius;
        isTouching = false;
    }

    private void Update()
    {
        if (!isTouching)
        {
            if (camControl.radius > originalRadius)
            {
                camControl.radius -= radiusIncreaseRate;
            }
            else
            {
                camControl.radius = originalRadius;
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == layerMask)
            isTouching = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.layer == layerMask)
        {
            isTouching = true;

            Vector3 closestPoint = other.ClosestPoint(this.transform.position); //Finds the closest point of the wall to the camera
            float distance = Vector3.Distance(closestPoint, this.transform.position);

            if(distance < distToWall)
            {
                if(camControl.radius + radiusReductionRate < smallestRadius)
                    camControl.radius += radiusReductionRate;
            }
        }
    }
}
