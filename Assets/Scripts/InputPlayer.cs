using UnityEngine;

/*
 * Author:      Zachary Schmalz
 * Version:     1.0.0
 * Date:        September 19, 2018
 */

/// <summary>
/// This player class is used to communicate with the InputManager, and handle any player related functions
/// </summary>
public class InputPlayer : MonoBehaviour, IInputPlayer
{
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
        
	}
	
	void Update ()
    {
        // If getting specific player input (in this case, this player)

        float f = 0;
        if ((f = InputManager.GetAxis("LeftHorizontal", this)) != 0) Debug.InputLog("Player " + (PlayerIndex + 1) + " - Left Horizontal: " + f);
        if ((f = InputManager.GetAxisDown("LeftHorizontal", this)) != 0) Debug.InputLog("Player " + (PlayerIndex + 1) + " - Left Horizontal Down: " + f);
        if (InputManager.GetButton("A", this)) Debug.InputLog("Player " + (PlayerIndex + 1) + " - A Pressed");
        if (InputManager.GetButtonDown("A", this)) Debug.InputLog("Player " + (PlayerIndex + 1) + " - A pressed this frame");
        if (InputManager.GetButtonUp("A", this)) Debug.InputLog("Player " + (PlayerIndex + 1) + " - A released this frame");

        // If getting any input, simply delete "this"

        //f = 0;
        //if ((f = InputManager.GetAxis("LeftHorizontal")) != 0) Debug.InputLog("Left Horizontal: " + f);
        //if ((f = InputManager.GetAxisDown("LeftHorizontal")) != 0) Debug.InputLog("Left Horizontal Down: " + f);
        //if (InputManager.GetButton("A")) Debug.InputLog("A Pressed");
        //if (InputManager.GetButtonDown("A")) Debug.InputLog("A pressed this frame");
        //if (InputManager.GetButtonUp("A")) Debug.InputLog("A released this frame");
    }
}