using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    public ColorSwap playerColor1;
    public ColorSwap playerColor2;
    public ColorSwap playerColor3;
    public ColorSwap playerColor4;
    public Material off;
    public Material on;
    public bool state = false;
    public int colorValue = 0;
    public bool neutralColor=false;

    // Used for animated button
    public bool holdState = false;
    public bool onOnly = false;
    private float offset = 0.0f;
    private Vector3 startPosition;

    private int onButton = 0;

    void Start()
    {
        if (colorValue==-1)
        {
            neutralColor = true;
        }
        GetComponent<MeshRenderer>().material = state ? on : off;
        startPosition = transform.position;
    }
    
    public void OnTriggerExit(Collider other)
    {
        if(neutralColor==true)
        {
            colorValue = -1;
        }
        state =false;
        GetComponent<MeshRenderer>().material = state ? on : off;
    }
    // Used for the hold down button trigger collision
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag.StartsWith("Player"))
        {
            if (colorValue == -1)
            {
                colorValue = other.GetComponent<ColorSwap>().currentColor;
            }
            if (holdState == true)
            {
                if (!state || onOnly == false)
                {
                
                    ColorSwap color = other.GetComponent<ColorSwap>();
                    if (color.currentColor == colorValue)
                    {
                        state = true;
                        GetComponent<MeshRenderer>().material = state ? on : off;
                        color.transform.Translate(Vector3.down * 0.07f);
                        offset = offset + 0.08f;
                        if (offset > 0.64f)
                        {
                            offset = 0.64f;
                        }
                        onButton = 4;
                    }
                
                }
            }
            else
            {
                ColorSwap color = other.GetComponent<ColorSwap>();
                if (color.currentColor == colorValue)
                {
                    GetComponent<MeshRenderer>().material = state ? on : off;
                    state = true;
                }
            }
        }
    }


    private void Update()
    {
        // Animate and detect state of the hold down buttons
        if (holdState == true)
        {
            state = offset > 0.60f;

            if (offset > 0.0f)
            {
                if (onButton == 0)
                {
                    offset = offset - 0.01f;
                }
            }

            if (onButton > 0)
            {
                onButton = onButton - 1;
            }

            transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
            transform.Translate(Vector3.back * offset);
        }
    }
}
