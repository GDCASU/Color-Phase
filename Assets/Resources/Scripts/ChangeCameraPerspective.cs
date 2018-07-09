using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPerspective : MonoBehaviour {
    public int changeTo;
    public bool addCamera, changeCamera;
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
    }
}
