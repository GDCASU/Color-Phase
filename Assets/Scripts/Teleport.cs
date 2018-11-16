using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject player;
    public GameObject teleporter;
    public Transform destination;
    public int colorValue = 0;
    public double timer;
    public bool stop;

    // Use this for initialization
    void Start ()
    {
        destination = teleporter.transform;
        GetComponent<Renderer>().material.color = gameObject.GetComponent<Renderer>().material.GetColor("_Color");
        timer = 1000000000001;
    }

    // Update is called once per frame
    void Update ()
    {
        //Debug.Log(ColorSwap.players[0].currentPlayerColor);

        //The purpose of the timer is so that it doesn't teleport the player infinitely.
        if (timer > 0 && timer < 1000000000000)
        {
            timer += Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (stop == false)
        {
            //this operates on the same logic as ColorChangeTrigger
            if (other.GetComponent<ColorSwap>().currentColor == colorValue)
            {
                if (other.tag.StartsWith("Player"))
                {
                    //starts a timer at 0.1 seconds
                    timer = 0.1;

                    //teleports the player to the teleporter
                    other.transform.position = destination.position;
                    other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        //This will stop the player from teleporting again immediately after teleporting. It also stops the player's movement.
        if (timer < 0.3)
        {
            stop = true;
        }

        //This allows the player to teleport again once they exit the teleporter.
        else
        {
            stop = false;
        }
    }
}
