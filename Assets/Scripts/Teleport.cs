using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject teleporter;
    public int colorValue = 0;
    public double timer;

    //Stop must be initialized as false on the first teleporter and true on the second.
    public bool stop;

    // Use this for initialization
    void Start()
    {
        timer = 0;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(ColorSwap.players[0].currentPlayerColor);

        //The purpose of the countdown timer is so that it doesn't teleport the player infinitely.
        if (timer > 0 && timer <= 0.2)
        {
            timer -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Player"))
        {
            if (stop == false)
            {
                //this operates on the same logic as ColorChangeTrigger
                if (other.GetComponent<ColorSwap>().currentColor == colorValue)
                {
                    if (other.tag.StartsWith("Player"))
                    {
                        //starts a countdown timer at 0.3 seconds
                        timer = 0.2;

                        //teleports the player to the teleporter and stops the player's movement
                        other.transform.position = teleporter.transform.position;
                        other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                    }
                }
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag.StartsWith("Player"))
        {
            //This will stop the player from teleporting again immediately after teleporting.
            if (timer > 0 && timer <= 0.2)
            {
                timer = 0;
                stop = true;
            }

            //This allows the player to teleport again once they exit the teleporter.
            else
            {
                stop = false;
            }
        }
    }
}
