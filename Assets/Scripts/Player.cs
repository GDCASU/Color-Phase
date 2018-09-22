using UnityEngine;

/*
 * Author:      Zachary Schmalz
 * Version:     1.0.0
 * Date:        September 19, 2018
 */

/// <summary>
/// This player class is used to communicate with the InputManager, and handle any player related functions
/// </summary>
public class Player : MonoBehaviour
{
    private InputManager input;

    // Public class properties
    /// <summary>
    /// Property to get and set the players' input method type
    /// </summary>
    public InputManager.InputMethod InputMethod { get; set; }
    /// <summary>
    /// Property to get and set the player index
    /// </summary>
    public int PlayerIndex { get; set; }

	void Start ()
    {
        input = InputManager.singleton;
	}
	
	void Update ()
    {
        // If getting specific player input (in this case, this player)

        //float f = 0;
        //if ((f = input.GetAxis("LeftHorizontal", this)) != 0) Debug.InputLog("Player " + (PlayerIndex + 1) + " - Left Horizontal: " + f);
        //if ((f = input.GetAxisDown("LeftHorizontal", this)) != 0) Debug.InputLog("Player " + (PlayerIndex + 1) + " - Left Horizontal Down: " + f);
        //if (input.GetButton("A", this)) Debug.InputLog("Player " + (PlayerIndex + 1) + " - A Pressed");
        //if (input.GetButtonDown("A", this)) Debug.InputLog("Player " + (PlayerIndex + 1) + " - A pressed this frame");
        //if (input.GetButtonUp("A", this)) Debug.InputLog("Player " + (PlayerIndex + 1) + " - A released this frame");

        // If getting any input, simply delete "this"

        //f = 0;
        //if ((f = input.GetAxis("LeftHorizontal")) != 0) Debug.InputLog("Left Horizontal: " + f);
        //if ((f = input.GetAxisDown("LeftHorizontal")) != 0) Debug.InputLog("Left Horizontal Down: " + f);
        //if (input.GetButton("A")) Debug.InputLog("A Pressed");
        //if (input.GetButtonDown("A")) Debug.InputLog("A pressed this frame");
        //if (input.GetButtonUp("A")) Debug.InputLog("A released this frame");
    }
}