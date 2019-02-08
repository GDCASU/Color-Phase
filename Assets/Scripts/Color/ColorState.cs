using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// Provides a single class that can be accessed for color information
/// </summary>
[ExecuteInEditMode]
public class ColorState : MonoBehaviour {
    [SerializeField]
    private GameColor colorToSet;
    [SerializeField]
    private bool updateColor;
    public static Dictionary<GameColor, Color > RGBColors = new Dictionary <GameColor, Color> {
        {GameColor.Red, new Color(0.5490196f, 0.2614016f, 0.2588235f) },
        {GameColor.Green, new Color(0.2659206f, 0.5471698f, 0.26068f) },
        {GameColor.Blue, new Color(0.2588235f, 0.4211917f, 0.5490196f)},
        {GameColor.Yellow, new Color(0.5490196f, 0.5487493f, 0.2588235f)},
    };

    public delegate void colorSwapEvent(GameColor prev, GameColor next);
    public event colorSwapEvent onSwap;
    private GameColor currentcolor;
    public GameColor currentColor {
        set {
            onSwap(currentcolor, value);
            currentcolor = value;
            // Update this one too so we can see in editor
            colorToSet = value;
        }
        get {
            return currentcolor;
        } 
    }
    /// <summary>
    /// This is called from the editor and nowhere else
    /// It allows us to update our property 
    /// </summary>
    private void Update() {
        if(updateColor) {
            updateColor = false;
            currentColor = colorToSet;
        }
    }

    // Refresh color on start
    private void Start() {
        currentColor = colorToSet;
    }
}

// We put this in the unity engine namespace so we don't need to import anything in the other classes
// Idk if this is bad practice or not someone can debate me 
namespace UnityEngine
{   
    public enum GameColor { Red, Green, Blue, Yellow, };
}