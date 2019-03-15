using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScifiDoorwayLarge : MonoBehaviour {


    public GameObject doorLeft;
    public GameObject doorRight;
    public GameColor colorValue = GameColor.Red;
    public static float timer;

    public bool open;

    //Stop must be initialized as false on the first teleporter and true on the second.
    //public bool stop; // WHY? - stop is not used in the script.
    public bool neutralColor;

    private Vector3 doorPos;

    // Use this for initialization
    void Start()
    {
        doorPos = doorLeft.transform.position;
        timer = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (open == false && timer > 0f)
        {
            timer = timer - 0.05f; // close door
        } else
        {
            if (open == true && timer < 1.8f)
            {
                timer = timer + 0.05f; // open door
            }
        }

        if (timer < 0f)
        {
            timer = 0f;
        }
        if (timer > 1.8f)
        {
            timer = 1.8f;
        }

        doorLeft.transform.localPosition = doorPos + new Vector3(timer, 0, 0) - transform.localPosition;
        doorRight.transform.localPosition = doorPos + new Vector3(-timer, 0, 0) - transform.localPosition;

    }

    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
                //this operates on the same logic as ColorChangeTrigger
                if (other.GetComponent<ColorState>().currentColor == colorValue || neutralColor == true)
                {
                    open = true;
                }
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            open = false;
        }
    }

}
