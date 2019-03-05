using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonToggle : MonoBehaviour
{
    public Material off;
    public Material on;
    public bool state = false;
    public GameColor colorValue;

    // Used for animated button
    public bool holdState = false;
    public bool onOnly = false;
    private float offset = 0.0f;
    private Vector3 startPosition;

    private int onButton = 0;

    void Start()
    {
        GetComponent<MeshRenderer>().material = state ? on : off;
        startPosition = transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (holdState == false)
        {
            if (!state || onOnly == false)
            {
                if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
                {
                    ColorState color = other.GetComponent<ColorState>();
                    if (color.currentColor == colorValue)
                    {
                        state = !state;
                        GetComponent<MeshRenderer>().material = state ? on : off;
                    }
                }
            }
        }
    }

    // Used for the hold down button trigger collision
    public void OnTriggerStay(Collider other)
    {
        if (holdState == true)
        {
            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Box"))
            {
                ColorState color = other.GetComponent<ColorState>();
                if (color.currentColor == colorValue)
                {
                    // color.transform.Translate(Vector3.down * 0.07f);
                    offset = offset + 0.04f;
                    if (offset > 0.64f)
                    {
                        offset = 0.64f;
                    }
                    onButton = 4;
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
