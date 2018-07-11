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
        if (other.tag.StartsWith("Player"))
        {
            if (playerColor1.currentColor != colorValue)
            {
                other.GetComponent<ColorSwap>().setColor(colorValue);
                other.GetComponent<ColorSwap>().currentColor = colorValue;
                if (destroyOnContact)
                {
                    Destroy(this.gameObject);
                }
            }
        }
    }

}
