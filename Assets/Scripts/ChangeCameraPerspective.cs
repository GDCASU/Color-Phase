using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPerspective : MonoBehaviour
{
    public int changeTo;
    public int removeNumber;
    public bool addCamera, changeCamera, removeCamera;
    public GameObject cam;

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Player"))
        {
            if (addCamera)
            {
                other.GetComponent<PlayerCamControl>().cams[2] = cam;
            }
            if (changeCamera)
            {
                other.GetComponent<PlayerCamControl>().ChangeCamera(changeTo);
            }
            if (removeCamera)
            {
                other.GetComponent<PlayerCamControl>().cams[2] = null;
                if (other.GetComponent<PlayerCamControl>().activecam == 2)
                {
                    other.GetComponent<PlayerCamControl>().ChangeCamera(changeTo);
                }
            }
        }
    }
}
