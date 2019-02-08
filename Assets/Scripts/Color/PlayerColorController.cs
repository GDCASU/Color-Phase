using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This class contains an event to run for the player on colorswap
/// It also provides a singleton to access the player color from any class
/// </summary>


[RequireComponent(typeof(ColorState))]
public class PlayerColorController : MonoBehaviour
{
    public static PlayerColorController singleton;
    public ColorState playerColor;
    public GameObject playerModel;
    public GameObject playerCamera;
    public GameObject crossHair;
    public GameObject[] lights;
    private SpriteRenderer crossHairRender;

    void Awake() {
        // Get component references
        crossHairRender = crossHair.GetComponent<SpriteRenderer>();
        playerColor = GetComponent<ColorState>();

        // add to the color swap events
        GetComponent<ColorState>().onSwap += PlayerSetColor;

        singleton = this;
    }

    public void PlayerSetColor(GameColor prev, GameColor color)
    {
        gameObject.layer = 20 + (int)color;

        for (int i = 0; i < lights.Length; i++)
            lights[i].SetActive(i == (int)color);

        crossHairRender.color = ColorState.RGBColors[color];
    }
}
