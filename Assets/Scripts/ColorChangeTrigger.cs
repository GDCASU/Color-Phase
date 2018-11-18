using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour
{
    // Use this for initialization
    public int colorValue = 0;
    public bool destroyOnContact = false;

    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.tag.StartsWith("Player"))
        {
            if (other.GetComponent<ColorSwap>().currentColor != colorValue)
            {
                other.GetComponent<ColorSwap>().SetColor(colorValue);
                other.GetComponent<ColorSwap>().currentColor = colorValue;
                if (destroyOnContact)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
