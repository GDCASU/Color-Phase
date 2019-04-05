using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour
{
    public GameColor colorValue;
    public bool destroyOnContact = false;
    public void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other);
        if (other.tag.StartsWith("Player"))
        {
            if (other.GetComponent<ColorState>().currentColor != colorValue)
            {
                other.GetComponent<ColorState>().currentColor = colorValue;
                if (destroyOnContact)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
}
