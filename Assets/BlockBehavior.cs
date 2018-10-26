using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {
    public int color;//0=red,1=green,2=blue,3=yellow
    [Header("Crumble")]
    public bool enableThecrumble;
    public bool enableCrumbleReset;
    public bool crumbling=false;
    public int crumbleTime = 200;
    public int crumbleResetTime = 100;
    public int crumbleValue=200;
    public int crumbleResetValue = 100;

    [Header("Semi-Solid")]
    int replace = 0;

    [Header("Movement")]
    Transform end;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (enableThecrumble)
        {
            if (crumbling)
            {
                crumbleValue--;
                if (crumbleValue < 0)
                {
                    GetComponent<MeshRenderer>().enabled = false;
                    foreach (BoxCollider b in GetComponents<BoxCollider>())
                    {
                        b.enabled = false;
                    }
                    crumbling = false;
                }
            }
            //look for crumble reset
            else if (enableCrumbleReset&&crumbleValue < 0)
            {
                crumbleResetValue--;
                //Reset
                if (crumbleResetValue < 0)
                {
                    crumbleResetValue = crumbleResetTime;
                    crumbleValue = crumbleTime;
                    GetComponent<MeshRenderer>().enabled = true;
                    foreach (BoxCollider b in GetComponents<BoxCollider>())
                    {
                        b.enabled = true;
                    }
                }
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (enableThecrumble)
        {
            if (other.tag.StartsWith("Player"))
            {
                if (other.GetComponent<ColorSwap>().currentColor != color)
                {
                    crumbling = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (enableThecrumble)
        {
            if (other.tag.StartsWith("Player"))
            {
                crumbling = false;
            }
        }
    }
}
