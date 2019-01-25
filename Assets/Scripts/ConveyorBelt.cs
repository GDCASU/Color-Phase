using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    //speed of belt
    public float beltVelocity = 100;

    //direction the player goes in
    public bool reverseDirection = false;

    void OnCollisionStay(Collision obj)
    {
        if (reverseDirection == false)
        {
            obj.gameObject.GetComponent<Rigidbody>().velocity = obj.gameObject.GetComponent<Rigidbody>().velocity + beltVelocity * Time.deltaTime * transform.right;
        }

        else
        {
            obj.gameObject.GetComponent<Rigidbody>().velocity = obj.gameObject.GetComponent<Rigidbody>().velocity + beltVelocity * Time.deltaTime * -transform.right;
        }
    }
}
