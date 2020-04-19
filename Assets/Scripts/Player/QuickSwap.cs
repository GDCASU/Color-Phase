using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;
[RequireComponent(typeof(ColorState))]
[RequireComponent(typeof(PlayerColorController))]
public class QuickSwap : MonoBehaviour {
    /*
    Allows the player to swap between a stored color and their currect color
     */
    private ColorState playerColor;
    public GameColor storedColor;
    public ColorState followerState;
    void Awake () {
        playerColor = GetComponent<ColorState>(); 
    }
	
	void Update () {
		if(InputManager.GetButtonDown(PlayerButton.Swap)) {
            GameColor temp = storedColor; 
            storedColor = playerColor.currentColor;
            playerColor.currentColor = temp;
            followerState.currentColor = storedColor;
        }
	}
}
