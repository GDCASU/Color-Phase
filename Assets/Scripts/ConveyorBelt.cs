using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    //speed of belt
    public float beltVelocity = 100;

    //direction the player goes in
    public bool reverseDirection = false;

    void OnCollisionStay(Collision collision)
    {
        Rigidbody objOnBelt = collision.gameObject.GetComponent<Rigidbody>();

        if (!reverseDirection)
        {
            objOnBelt.velocity = objOnBelt.velocity + beltVelocity * Time.deltaTime * transform.right;
        }

        else
        {
            objOnBelt.velocity = objOnBelt.velocity + beltVelocity * Time.deltaTime * -transform.right;
        }
    }
}
