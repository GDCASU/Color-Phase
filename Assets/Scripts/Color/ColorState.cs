using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorState : MonoBehaviour {

    public enum GameColor { Red, Green, Blue, Yellow, };
    
    public Dictionary<GameColor, Color > RGBColors = new Dictionary <GameColor, Color> {
        {GameColor.Red, new Color(0.5490196f, 0.2614016f, 0.2588235f) },
        {GameColor.Green, new Color(0.2659206f, 0.5471698f, 0.26068f) },
        {GameColor.Blue, new Color(0.2588235f, 0.4211917f, 0.5490196f)},
        {GameColor.Yellow, new Color(0.5490196f, 0.5487493f, 0.2588235f)},
    };

    public event onSwap Action;
    
    public GameColor currentColor {
        set {

        }
        get => 
    }

}
