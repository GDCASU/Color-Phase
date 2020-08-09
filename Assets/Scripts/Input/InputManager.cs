﻿using System.Collections;
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

    public struct PlayerAction 
    {
        public KeyCode keyboardKey;
        public KeyCode xboxKey;
    }
}
public class InputManager : MonoBehaviour {
    public enum InputMode {
        both,
        controller,
        keyboard
    }
    public static InputMode inputMode = InputMode.both;
    [SerializeField]
    public static PlayerAction[] playerActions = new PlayerAction[7];

    public static Dictionary<KeyCode, string> playerXboxButtons = new Dictionary<KeyCode, string> {
        {KeyCode.Joystick1Button0, "A"},
        {KeyCode.Joystick1Button1, "B"},
        {KeyCode.Joystick1Button2, "X"},
        {KeyCode.Joystick1Button3, "Y"},
        {KeyCode.Joystick1Button4,"Left Bumper"},
        {KeyCode.Joystick1Button5, "Right Bumper"},
        {KeyCode.Joystick1Button6, "Back"},
        {KeyCode.Joystick1Button7, "Start"},
        {KeyCode.Joystick1Button8, "L3"},
        {KeyCode.Joystick1Button9, "R3"},
    };
    public static Dictionary<PlayerButton, PlayerAction> playerButtons = new Dictionary<PlayerButton, PlayerAction> { };
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
    void Start()
    {       
        playerActions[0].keyboardKey = KeyCode.Space;
        playerActions[0].xboxKey = KeyCode.Joystick1Button0;
        playerActions[1].keyboardKey = KeyCode.LeftShift;
        playerActions[1].xboxKey = KeyCode.Joystick1Button3;
        playerActions[2].keyboardKey = KeyCode.Mouse0;
        playerActions[2].xboxKey = KeyCode.Joystick1Button4;
        playerActions[3].keyboardKey = KeyCode.Mouse0;
        playerActions[3].xboxKey = KeyCode.Joystick1Button5;
        playerActions[4].keyboardKey = KeyCode.KeypadEnter;
        playerActions[4].xboxKey = KeyCode.Joystick1Button0;
        playerActions[5].keyboardKey = KeyCode.Escape;
        playerActions[5].xboxKey = KeyCode.Joystick1Button1;
        playerActions[6].keyboardKey = KeyCode.Escape;
        playerActions[6].xboxKey = KeyCode.Joystick1Button7;
        playerButtons.Add(PlayerButton.Jump, playerActions[0]);
        playerButtons.Add(PlayerButton.Swap, playerActions[1]);
        playerButtons.Add(PlayerButton.PickUp, playerActions[2]);
        playerButtons.Add(PlayerButton.Grapple, playerActions[3]);
        playerButtons.Add(PlayerButton.UI_Submit, playerActions[4]);
        playerButtons.Add(PlayerButton.UI_Cancel, playerActions[5]);
        playerButtons.Add(PlayerButton.Pause, playerActions[6]);
    }
    public static bool GetButtonDown(PlayerButton button) {
        return Input.GetKeyDown((inputMode==InputMode.controller)? playerButtons[button].xboxKey : playerButtons[button].keyboardKey);
    }

    public static bool GetButtonUp (PlayerButton button) {
        return Input.GetKeyUp((inputMode == InputMode.controller) ? playerButtons[button].xboxKey : playerButtons[button].keyboardKey);
    }

    public static bool GetButton (PlayerButton button) {
        return Input.GetKey((inputMode == InputMode.controller) ? playerButtons[button].xboxKey : playerButtons[button].keyboardKey);
    }
    public static float GetAxis (PlayerAxis axis) {
        var mouse = mouseAxis.ContainsKey(axis) ? Input.GetAxis(mouseAxis[axis]) : 0;
        var controller = joyAxis.ContainsKey(axis) ? Input.GetAxis(joyAxis[axis]) : 0;
        
        return (inputMode == InputMode.both && controller != 0) || inputMode == InputMode.controller ? controller : mouse;
    }
}