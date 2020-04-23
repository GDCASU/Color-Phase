using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;
namespace PlayerInput {
    /// <summary>
    /// Enum for all Player axis actions (float vales)
    /// </summary>
    public enum PlayerAxis
    {
        MoveHorizontal,
        MoveVertical,
        CameraHorizontal,
        CameraVertical,
        UI_Horizontal,
        UI_Vertical,
        None,
    };

    /// <summary>
    /// Enum for all Player button actions (bool values)
    /// </summary>
    public enum PlayerButton
    {
        Jump,
        Swap,
        PickUp,
        Grapple,
        UI_Submit,     // UI Button
        UI_Cancel,     // UI Button
        Pause,
        None,
    };
}


public class InputManager : MonoBehaviour {
    public enum InputMode {
        both,
        controller,
        keyboard
    }
    public static InputMode inputMode = InputMode.both;
     public static Dictionary<PlayerButton, string > playerButtons = new Dictionary <PlayerButton, string> {
        {PlayerButton.Jump, "Jump"},
        {PlayerButton.Swap,"Swap"},
        {PlayerButton.PickUp,"PickUp"},
        {PlayerButton.Grapple,"Grapple"},
        {PlayerButton.UI_Submit,"UISubmit"},     // UI Button
        {PlayerButton.UI_Cancel, "UICancel"},   // UI Button
        {PlayerButton.Pause, "Pause"},
    };

     public static Dictionary<PlayerAxis, string > joyAxis = new Dictionary <PlayerAxis, string> {
        {PlayerAxis.MoveHorizontal, "JoystickX1"},
        {PlayerAxis.MoveVertical, "JoystickY1"},
        {PlayerAxis.CameraHorizontal, "JoystickT1"},
        {PlayerAxis.CameraVertical, "JoystickZ1"},
        {PlayerAxis.UI_Horizontal, "JoystickX1"},
        {PlayerAxis.UI_Vertical, "JoystickY1"},
    };

    public static Dictionary<PlayerAxis, string > mouseAxis = new Dictionary <PlayerAxis, string> {
        {PlayerAxis.MoveHorizontal, "KeyboardX"},
        {PlayerAxis.MoveVertical, "KeyboardY"},
        {PlayerAxis.CameraHorizontal, "MouseX"},
        {PlayerAxis.CameraVertical, "MouseY"},
        {PlayerAxis.UI_Horizontal, "KeyboardX"},
        {PlayerAxis.UI_Vertical, "KeyboardY"},
    };
    public static bool GetButtonDown (PlayerButton button) {
        return Input.GetButtonDown(playerButtons[button]);
    }

    public static bool GetButtonUp (PlayerButton button) {
        return Input.GetButtonUp(playerButtons[button]);
    }

    public static bool GetButton (PlayerButton button) {
        return Input.GetButton(playerButtons[button]);
    }
    public static float GetAxis (PlayerAxis axis) {
        var mouse = mouseAxis.ContainsKey(axis) ? Input.GetAxis(mouseAxis[axis]) : 0;
        var controller = joyAxis.ContainsKey(axis) ? Input.GetAxis(joyAxis[axis]) : 0;
        
        return (inputMode == InputMode.both && controller != 0) || inputMode == InputMode.controller ? controller : mouse;
    }

}
