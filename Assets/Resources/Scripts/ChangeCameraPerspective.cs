using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCameraPerspective : MonoBehaviour {
    public Flyingcamera player1;
    public Flyingcamera player2;
    public Flyingcamera player3;
    public Flyingcamera player4;
    public int changeTo;
    public bool addCamera, changeCamera;
    public GameObject cam;

    public void OnTriggerEnter(Collider other)
    {
        if (addCamera)
        {
            if (other.gameObject.tag == "Player 1")
            {
                player1.cams[2]=cam;
            }
            if (other.gameObject.tag == "Player 2")
            {
                player2.cams[2] = cam;
            }
            if (other.gameObject.tag == "Player 3")
            {
                player3.cams[2] = cam;
            }
            if (other.gameObject.tag == "Player 4")
            {
                player4.cams[2] = cam;
            }
        }
        if (changeCamera)
        {
            //Debug.Log(other);
            if (other.gameObject.tag == "Player 1")
            {
                player1.ChangeCamera(changeTo);
            }
            if (other.gameObject.tag == "Player 2")
            {
                player2.ChangeCamera(changeTo);
            }
            if (other.gameObject.tag == "Player 3")
            {
                player3.ChangeCamera(changeTo);
            }
            if (other.gameObject.tag == "Player 4")
            {
                player4.ChangeCamera(changeTo);
            }
        }
    }
}
