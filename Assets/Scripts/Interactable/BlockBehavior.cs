using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehavior : MonoBehaviour {
    public int color;//0=red,1=green,2=blue,3=yellow,4=no color
    [Header("Crumble")]
    public bool enableTheCrumble;
    public bool enableCrumbleReset;
    public bool crumbling=false;
    public bool cumbleBasedOnColor;
    public int crumbleTime = 200;
    public int crumbleResetTime = 100;
    public int crumbleValue=200;
    public int crumbleResetValue = 100;

    [Header("Semi-Solid")]
    public bool enableSemiSolid;

    [Header("Movement")]
    public bool enableMovement;
    public bool loopMovement;
    public bool reverseMovement;
    public Transform[] placesToGo;
    public int currentTransform = 0,nextTransform=1;
    public int speed;
    private float startTime, journeyLength;
    private bool MovementBegan=false;
    public int direction = 1;


    //Internal use
    BoxCollider physical, trigger;
    // Use this for initialization
    void Start () {
        physical=GetComponents<BoxCollider>()[0];
        trigger = GetComponents<BoxCollider>()[1];
        if(enableSemiSolid)
            this.gameObject.layer = 20 + color;
        beginMove();
    }
    
    // Update is called once per frame
    void Update () {
        //Crumbel
        if (enableTheCrumble)
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
        //Movement
        if (enableMovement&&MovementBegan)
        {
            // Distance moved = time * speed.
            float distCovered = (Time.time - startTime) * speed;

            // Fraction of journey completed = current distance divided by total distance.
            float fracJourney = distCovered / journeyLength;
            Debug.print("journy "+fracJourney);

            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(placesToGo[currentTransform].position, placesToGo[nextTransform].position, fracJourney);
            if (fracJourney > .99)
            {
                beginMove();
            }
        }
	}

    private void OnTriggerEnter(Collider other)
    {
        if (enableTheCrumble)
        {
            if (other.tag.StartsWith("Player"))
            {
                if (cumbleBasedOnColor && other.GetComponent<ColorSwap>().currentColor != color)
                {
                    crumbling = true;
                }
                else if (!cumbleBasedOnColor)
                {
                    crumbling = true;
                }
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (enableTheCrumble)
        {
            if (other.tag.StartsWith("Player"))
            {
                crumbling = false;
            }
        }
    }
    public void beginMove()
    {
        if (loopMovement)
        {
            //loops in a circle
            currentTransform++;
            currentTransform = currentTransform % placesToGo.Length;
            Debug.print("current"+currentTransform);
            nextTransform = (currentTransform + 1) % placesToGo.Length;
            Debug.print("if");
            Debug.print(nextTransform);
            Debug.print(placesToGo.Length);
        }
        else if (reverseMovement)
        {
            //reveses direction at end
            currentTransform = currentTransform + direction;

            if (currentTransform + direction<0|| currentTransform + direction>= placesToGo.Length)
            {
                direction = -direction;
            }
            nextTransform = currentTransform + direction;
        }
        else
        {
            //Goes until hits the end
            currentTransform++;                     
            nextTransform =currentTransform + 1;
            Debug.print("else");
        }
        //nextTransform = loopMovement ? currentTransform + 1 % placesToGo.Length : currentTransform + 1;
        if (nextTransform < placesToGo.Length)
        {
            MovementBegan = true;
            // Keep a note of the time the movement started.
            startTime = Time.time;
            // Calculate the journey length.
            journeyLength = Vector3.Distance(placesToGo[currentTransform].position, placesToGo[nextTransform].position);
        }
        else
        {
            MovementBegan = false;
            Debug.print("end");
        }
    }
}
