using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour {

    // Use this for initialization
    public ColorSwap playerColor1;
    public ColorSwap playerColor2;
    public ColorSwap playerColor3;
    public ColorSwap playerColor4;
    public int colorValue = 0;
    public bool destroyOnContact = false;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.gameObject.tag == "Player 1")
        {
            if (playerColor1.currentColor != colorValue)
            {
                playerColor1.setColor(colorValue);
                playerColor1.currentColor = colorValue;
                if (destroyOnContact)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        if (other.gameObject.tag == "Player 2")
        {
            if (playerColor2.currentColor != colorValue)
            {
                playerColor2.setColor(colorValue);
                playerColor2.currentColor = colorValue;
                if (destroyOnContact)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        if (other.gameObject.tag == "Player 3")
        {
            if (playerColor3.currentColor != colorValue)
            {
                playerColor3.setColor(colorValue);
                playerColor3.currentColor = colorValue;
                if (destroyOnContact)
                {
                    Destroy(this.gameObject);
                }
            }
        }
        if (other.gameObject.tag == "Player 4")
        {
            if (playerColor4.currentColor != colorValue)
            {
                playerColor4.setColor(colorValue);
                playerColor4.currentColor = colorValue;
                if (destroyOnContact)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

}
