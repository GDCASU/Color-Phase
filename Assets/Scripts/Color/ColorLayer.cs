using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class exists to swap the collision layer for things with colors 
/// </summary>

[RequireComponent(typeof(ColorState))]
[ExecuteInEditMode]
public class ColorLayer : MonoBehaviour {
	void Start () {
		// Get component references
        // add to the color swap events
        GetComponent<ColorState>().onSwap += PlayerSetColor;
	}
    public void PlayerSetColor(GameColor prev, GameColor color)
    {
        gameObject.layer = 20 + (int)color;
    }
}
