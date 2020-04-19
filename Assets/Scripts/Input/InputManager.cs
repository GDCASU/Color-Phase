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
     public static Dictionary<PlayerButton, string > playerButtons = new Dictionary <PlayerButton, string> {
        {PlayerButton.Jump, "Jump"},
        {PlayerButton.Swap,""},
        {PlayerButton.PickUp,""},
        {PlayerButton.Grapple,""},
        {PlayerButton.UI_Submit,""},     // UI Button
        {PlayerButton.UI_Cancel, ""},   // UI Button
        {PlayerButton. Pause, ""},
    };

     public static Dictionary<PlayerAxis, string > playerAxise = new Dictionary <PlayerAxis, string> {
        {PlayerAxis.MoveHorizontal, ""},
        {PlayerAxis.MoveVertical, ""},
        {PlayerAxis.CameraHorizontal, ""},
        {PlayerAxis.CameraVertical, ""},
        {PlayerAxis.UI_Horizontal, ""},
        {PlayerAxis.UI_Vertical, ""},
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
        return Input.GetAxis(playerAxise[axis]);
    }

}
