using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateOpen : MonoBehaviour
{
    public ButtonToggle[] buttonToggleScript;
    public float range = 3.5f;
    public float speed = 0.1f;
    const float constTime = 100;
    public bool invertState = false;
    public bool startOpen = false;
    public bool orType = false;
    private Vector3 startPosition;
    private float gateOffset = 0f;
    private int count;

    // Use this for initialization
    void Start()
    {
        startPosition = transform.position;
        if (startOpen == true)
        {
            gateOffset = range;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (invertState == false)
        {
            count = 0;
            for (int i = 0; i < buttonToggleScript.Length; i++)
            {
                if (buttonToggleScript[i].state) { count++; }
            }

            if (orType == false)
            {
                if (count == buttonToggleScript.Length) // All buttons must be pressed
                {
                    if (gateOffset < range)
                    {
                        gateOffset = gateOffset + speed * Time.deltaTime * constTime;
                    }
                }
                else
                {
                    if (gateOffset > 0f)
                    {
                        gateOffset = gateOffset - speed * Time.deltaTime * constTime;
                    }
                }
            }
            else
            {
                if (count > 0) // Only one button needs to be pressed
                {
                    if (gateOffset < range)
                    {
                        gateOffset = gateOffset + speed * Time.deltaTime * constTime;
                    }
                }
                else
                {
                    if (gateOffset > 0f)
                    {
                        gateOffset = gateOffset - speed * Time.deltaTime * constTime;
                    }
                }
            }
        }
        else
        {
            count = 0;
            for (int i = 0; i < buttonToggleScript.Length; i++)
            {
                if (buttonToggleScript[i].state) { count++; }
            }

            if (orType == false)
            {
                if (count == buttonToggleScript.Length)
                {
                    if (gateOffset > 0f)
                    {
                        gateOffset = gateOffset - speed * Time.deltaTime * constTime;
                    }
                }
                else
                {
                    if (gateOffset < range)
                    {
                        gateOffset = gateOffset + speed * Time.deltaTime * constTime;
                    }
                }
            }
            else
            {
                if (count > 0)
                {
                    if (gateOffset > 0f)
                    {
                        gateOffset = gateOffset - speed * Time.deltaTime * constTime;
                    }
                }
                else
                {
                    if (gateOffset < range)
                    {
                        gateOffset = gateOffset + speed * Time.deltaTime * constTime;
                    }
                }
            }
        }
        if (gateOffset < 0)
        {
            gateOffset = 0;
        }
        if (gateOffset > range)
        {
            gateOffset = range;
        }
        transform.position = new Vector3(startPosition.x, startPosition.y, startPosition.z);
        transform.Translate(Vector3.forward * gateOffset);
    }
}
