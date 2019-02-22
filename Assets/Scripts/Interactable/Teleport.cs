using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    public GameObject teleporter;
    public GameColor colorValue = GameColor.Red;
    public static double timer;

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
        //The purpose of the countdown timer is so that it doesn't teleport the player infinitely.
        if (timer > 0)
        {
            timer -= Time.deltaTime;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //ideally would be 0 but timer isn't decremented perfectly
            if (timer >= -0.02 && timer <= 0.02)
            {
                //this operates on the same logic as ColorChangeTrigger
                if (other.GetComponent<ColorState>().currentColor == colorValue)
                {
                    //starts a countdown timer at 0.2 seconds
                    timer = 0.2;

                    //teleports the player to the teleporter and stops the player's movement
                    other.transform.position = teleporter.transform.position;
                    other.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }       
    }
}
