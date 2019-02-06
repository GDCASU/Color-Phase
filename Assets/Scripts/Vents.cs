using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vents : MonoBehaviour
{
    public float vent_strength = 0;
    
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == ("Player"))
        {
            other.GetComponent<Rigidbody>().velocity = other.GetComponent<Rigidbody>().velocity + transform.up * vent_strength;
        }
    }
    
}
