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
    public int state = 0;
    public int colorValue = 0;

    // Used for animated button
    public bool holdState = false;
    public bool onOnly = false;
    private float offset = 0.0f;
    private Vector3 startPosition;

    private int onButton = 0;

    void Start()
    {
        setMaterialState(state);
        startPosition = transform.position;
    }

    public void OnTriggerEnter(Collider other)
    {
        if (holdState == false)
        {
            if (state == 0 || onOnly == false)
            {
                if (other.gameObject.tag == "Player 1")
                {
                    if (playerColor1.currentColor == colorValue)
                    {
                        toggleState();
                        setMaterialState(state);
                    }
                }
                if (other.gameObject.tag == "Player 2")
                {
                    if (playerColor2.currentColor == colorValue)
                    {
                        toggleState();
                        setMaterialState(state);
                    }
                }
                if (other.gameObject.tag == "Player 3")
                {
                    if (playerColor3.currentColor == colorValue)
                    {
                        toggleState();
                        setMaterialState(state);
                    }
                }
                if (other.gameObject.tag == "Player 4")
                {
                    if (playerColor4.currentColor == colorValue)
                    {
                        toggleState();
                        setMaterialState(state);
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
            if (other.gameObject.tag == "Player 1")
            {
                if (playerColor1.currentColor == colorValue)
                {
                    playerColor1.transform.Translate(Vector3.down * 0.07f);
                    offset = offset + 0.08f;
                    if (offset > 0.64f)
                    {
                        offset = 0.64f;
                    }
                    onButton = 4;
                }
            }
            if (other.gameObject.tag == "Player 2")
            {
                if (playerColor2.currentColor == colorValue)
                {
                    playerColor2.transform.Translate(Vector3.down * 0.07f);
                    offset = offset + 0.08f;
                    if (offset > 0.64f)
                    {
                        offset = 0.64f;
                    }
                    onButton = 4;
                }
            }
            if (other.gameObject.tag == "Player 3")
            {
                if (playerColor3.currentColor == colorValue)
                {
                    playerColor3.transform.Translate(Vector3.down * 0.07f);
                    offset = offset + 0.08f;
                    if (offset > 0.64f)
                    {
                        offset = 0.64f;
                    }
                    onButton = 4;
                }
            }
            if (other.gameObject.tag == "Player 4")
            {
                if (playerColor4.currentColor == colorValue)
                {
                    playerColor4.transform.Translate(Vector3.down * 0.07f);
                    offset = offset + 0.08f;
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
            if (offset > 0.60f)
            {
                state = 1;
            }
            else
            {
                state = 0;
            }

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

    private void toggleState()
    {
        state = state + 1;
        if (state > 1)
        {
            state = 0;
        }
    }

    private void setMaterialState(int state)
    {
        switch (state)
        {
            case 0:
                GetComponent<MeshRenderer>().material = off;
                break;
            case 1:
                GetComponent<MeshRenderer>().material = on;
                break;
        }
    }
}
