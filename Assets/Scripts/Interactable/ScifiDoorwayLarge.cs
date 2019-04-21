using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScifiDoorwayLarge : MonoBehaviour {

    public GameObject doorLeft;
    public GameObject doorRight;
    public GameColor colorValue = GameColor.Red;
    public float move;

    private bool open;

    //Stop must be initialized as false on the first teleporter and true on the second.
    //public bool stop; // WHY? - stop is not used in the script.
    public bool neutralColor;

    private Vector3 doorPos;

    // Use this for initialization
    void Start()
    {
        doorPos = doorLeft.transform.localPosition;
        move = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (open == false && move > 0f)
        {
            move = move - 0.05f; // close door
        }
        if (open == true && move < 1.8f)
        {
            move = move + 0.05f; // open door
        }

        if (open == false && move < 0f)
        {
            move = 0f;
        }
        if (open == true && move > 1.8f)
        {
            move = 1.8f;
        }

        doorLeft.transform.localPosition = doorPos + new Vector3(move, 0, 0) - transform.localPosition;
        doorRight.transform.localPosition = doorPos + new Vector3(-move, 0, 0) - transform.localPosition;
        
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
            if (other.GetComponent<ColorState>().currentColor == colorValue || neutralColor == true)
            {
                open = false;
            }
        }
    }
    
    

}
