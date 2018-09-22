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
        float f = 0;
        if ((f = input.GetAxis("LeftHorizontal", this)) != 0) Debug.InputLog("Left Horizontal: " + f);
        if ((f = input.GetAxis("LeftVertical", this)) != 0) Debug.InputLog("Left Vertical: " + f);
        if ((f = input.GetAxis("RightHorizontal", this)) != 0) Debug.InputLog("Right Horizontal: " + f);
        if ((f = input.GetAxis("RightVertical", this)) != 0) Debug.InputLog("Right Vertical: " + f);
        if ((f = input.GetAxis("LeftTrigger", this)) != 0) Debug.InputLog("Left Trigger: " + f);
        if ((f = input.GetAxis("RightTrigger", this)) != 0) Debug.InputLog("Right Trigger: " + f);

        if (input.GetButtonDown("A", this)) Debug.InputLog("A Pressed");
        if (input.GetButton("B", this)) Debug.InputLog("B Pressed");
        if (input.GetButton("X", this)) Debug.InputLog("X Pressed");
        if (input.GetButton("Y", this)) Debug.InputLog("Y Pressed");
        if (input.GetButton("LeftStick", this)) Debug.InputLog("LeftStick Pressed");
        if (input.GetButton("RightStick", this)) Debug.InputLog("RightStick Pressed");
        if (input.GetButton("LB", this)) Debug.InputLog("LB Pressed");
        if (input.GetButton("RB", this)) Debug.InputLog("RB Pressed");
        if (input.GetButton("Up", this)) Debug.InputLog("Up Pressed");
        if (input.GetButton("Right", this)) Debug.InputLog("Right Pressed");
        if (input.GetButton("Down", this)) Debug.InputLog("Down Pressed");
        if (input.GetButton("Left", this)) Debug.InputLog("Left Pressed");
        if (input.GetButton("Back", this)) Debug.InputLog("Back Pressed");
        if (input.GetButton("Start", this)) Debug.InputLog("Start Pressed");
        if (input.GetButton("Menu", this)) Debug.InputLog("Guide Pressed");
    }
}