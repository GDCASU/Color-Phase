using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPerspective : MonoBehaviour {
    public Flyingcamera playerColor1;
    public Flyingcamera playerColor2;
    public Flyingcamera playerColor3;
    public Flyingcamera playerColor4;
    public int colorValue = 0;
    public bool destroyOnContact = false;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.gameObject.tag == "Player 1")
        {
            playerColor1.ChangeCamera();
        }
        if (other.gameObject.tag == "Player 2")
        {
            playerColor2.ChangeCamera();
        }
        if (other.gameObject.tag == "Player 3")
        {
            playerColor3.ChangeCamera();
        }
        if (other.gameObject.tag == "Player 4")
        {
            playerColor4.ChangeCamera();
        }
    }
}
