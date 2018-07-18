using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPerspective : MonoBehaviour {
    public int changeTo;
    public int removeNumber;
    public bool addCamera, changeCamera,removeCamera;
    public GameObject cam;

    public void OnTriggerEnter(Collider other)
    {
        if (addCamera)
        {
            if (other.tag.StartsWith("Player"))
            {
                other.GetComponent<Flyingcamera>().cams[2]=cam;
            }
        }
        if (changeCamera)
        {
            //Debug.Log(other);
            if (other.tag.StartsWith("Player"))
                other.GetComponent<Flyingcamera>().ChangeCamera(changeTo);
        }
        if (removeCamera)
        {
            if (other.tag.StartsWith("Player"))
            {
                other.GetComponent<Flyingcamera>().cams[2] = null;
                if (other.GetComponent<Flyingcamera>().activecam == 2)
                {
                    other.GetComponent<Flyingcamera>().ChangeCamera(changeTo);
                }
            }
        }
    }
}
