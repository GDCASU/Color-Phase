using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomInput : MonoBehaviour
{


    public float GetInputf(int inputValue)
    {

        // https://docs.unity3d.com/ScriptReference/KeyCode.html

        // 87 : A
        // 105 : S
        // 90 : D
        // 109 : W


        switch (inputValue)
        {

            case 0:
                /*
                if (Input.GetKey(KeyCode.None))
                {
                    return 1f;
                } else {
                    return 0f;
                }
                */
                break;

            case 1:
                if (Input.GetKey(KeyCode.Backspace))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 2:
                if (Input.GetKey(KeyCode.Delete))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 3:
                if (Input.GetKey(KeyCode.Tab))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 4:
                if (Input.GetKey(KeyCode.Clear))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 5:
                if (Input.GetKey(KeyCode.Return))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 6:
                if (Input.GetKey(KeyCode.Pause))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 7:
                if (Input.GetKey(KeyCode.Escape))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 8:
                if (Input.GetKey(KeyCode.Space))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 9:
                if (Input.GetKey(KeyCode.Keypad0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 10:
                if (Input.GetKey(KeyCode.Keypad1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 11:
                if (Input.GetKey(KeyCode.Keypad2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 12:
                if (Input.GetKey(KeyCode.Keypad3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 13:
                if (Input.GetKey(KeyCode.Keypad4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 14:
                if (Input.GetKey(KeyCode.Keypad5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 15:
                if (Input.GetKey(KeyCode.Keypad6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 16:
                if (Input.GetKey(KeyCode.Keypad7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 17:
                if (Input.GetKey(KeyCode.Keypad8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 18:
                if (Input.GetKey(KeyCode.Keypad9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 19:
                if (Input.GetKey(KeyCode.KeypadPeriod))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 20:
                if (Input.GetKey(KeyCode.KeypadDivide))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 21:
                if (Input.GetKey(KeyCode.KeypadMultiply))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 22:
                if (Input.GetKey(KeyCode.KeypadMinus))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 23:
                if (Input.GetKey(KeyCode.KeypadPlus))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 24:
                if (Input.GetKey(KeyCode.KeypadEnter))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 25:
                if (Input.GetKey(KeyCode.KeypadEquals))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 26:
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 27:
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 28:
                if (Input.GetKey(KeyCode.RightArrow))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 29:
                if (Input.GetKey(KeyCode.LeftArrow))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 30:
                if (Input.GetKey(KeyCode.Insert))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 31:
                if (Input.GetKey(KeyCode.Home))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 32:
                if (Input.GetKey(KeyCode.End))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 33:
                if (Input.GetKey(KeyCode.PageUp))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 34:
                if (Input.GetKey(KeyCode.PageDown))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 35:
                if (Input.GetKey(KeyCode.F1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 36:
                if (Input.GetKey(KeyCode.F2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 37:
                if (Input.GetKey(KeyCode.F3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 38:
                if (Input.GetKey(KeyCode.F4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 39:
                if (Input.GetKey(KeyCode.F5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 40:
                if (Input.GetKey(KeyCode.F6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 41:
                if (Input.GetKey(KeyCode.F7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 42:
                if (Input.GetKey(KeyCode.F8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 43:
                if (Input.GetKey(KeyCode.F9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 44:
                if (Input.GetKey(KeyCode.F10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 45:
                if (Input.GetKey(KeyCode.F11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 46:
                if (Input.GetKey(KeyCode.F12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 47:
                if (Input.GetKey(KeyCode.F13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 48:
                if (Input.GetKey(KeyCode.F14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 49:
                if (Input.GetKey(KeyCode.F15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 50:
                if (Input.GetKey(KeyCode.Alpha0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 51:
                if (Input.GetKey(KeyCode.Alpha1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 52:
                if (Input.GetKey(KeyCode.Alpha2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 53:
                if (Input.GetKey(KeyCode.Alpha3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 54:
                if (Input.GetKey(KeyCode.Alpha4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 55:
                if (Input.GetKey(KeyCode.Alpha5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 56:
                if (Input.GetKey(KeyCode.Alpha6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 57:
                if (Input.GetKey(KeyCode.Alpha7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 58:
                if (Input.GetKey(KeyCode.Alpha8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 59:
                if (Input.GetKey(KeyCode.Alpha9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 60:
                if (Input.GetKey(KeyCode.Exclaim))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 61:
                if (Input.GetKey(KeyCode.DoubleQuote))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 62:
                if (Input.GetKey(KeyCode.Hash))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 63:
                if (Input.GetKey(KeyCode.Dollar))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 64:
                if (Input.GetKey(KeyCode.Ampersand))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 65:
                if (Input.GetKey(KeyCode.Quote))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 66:
                if (Input.GetKey(KeyCode.LeftParen))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 67:
                if (Input.GetKey(KeyCode.RightParen))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 68:
                if (Input.GetKey(KeyCode.Asterisk))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 69:
                if (Input.GetKey(KeyCode.Plus))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 70:
                if (Input.GetKey(KeyCode.Comma))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 71:
                if (Input.GetKey(KeyCode.Minus))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 72:
                if (Input.GetKey(KeyCode.Period))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 73:
                if (Input.GetKey(KeyCode.Slash))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 74:
                if (Input.GetKey(KeyCode.Colon)) 
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 75:
                if (Input.GetKey(KeyCode.Semicolon))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 76:
                if (Input.GetKey(KeyCode.Less))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 77:
                if (Input.GetKey(KeyCode.Equals))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 78:
                if (Input.GetKey(KeyCode.Greater))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 79:
                if (Input.GetKey(KeyCode.Question))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 80:
                if (Input.GetKey(KeyCode.At))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 81:
                if (Input.GetKey(KeyCode.LeftBracket))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 82:
                if (Input.GetKey(KeyCode.Backslash))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 83:
                if (Input.GetKey(KeyCode.RightBracket))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 84:
                if (Input.GetKey(KeyCode.Caret))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 85:
                if (Input.GetKey(KeyCode.Underscore))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 86:
                if (Input.GetKey(KeyCode.BackQuote))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 87:
                if (Input.GetKey(KeyCode.A))  // A
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 88:
                if (Input.GetKey(KeyCode.B))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 89:
                if (Input.GetKey(KeyCode.C))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 90:
                if (Input.GetKey(KeyCode.D))  // D
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 91:
                if (Input.GetKey(KeyCode.E))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 92:
                if (Input.GetKey(KeyCode.F))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 93:
                if (Input.GetKey(KeyCode.G))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 94:
                if (Input.GetKey(KeyCode.H))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 95:
                if (Input.GetKey(KeyCode.I))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 96:
                if (Input.GetKey(KeyCode.J))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 97:
                if (Input.GetKey(KeyCode.K))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 98:
                if (Input.GetKey(KeyCode.L))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 99:
                if (Input.GetKey(KeyCode.M))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 100:
                if (Input.GetKey(KeyCode.N))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 101:
                if (Input.GetKey(KeyCode.O))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 102:
                if (Input.GetKey(KeyCode.P))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 103:
                if (Input.GetKey(KeyCode.Q))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 104:
                if (Input.GetKey(KeyCode.R))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 105:
                if (Input.GetKey(KeyCode.S)) // S
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 106:
                if (Input.GetKey(KeyCode.T))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 107:
                if (Input.GetKey(KeyCode.U))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 108:
                if (Input.GetKey(KeyCode.V))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 109:
                if (Input.GetKey(KeyCode.W)) // W
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 110:
                if (Input.GetKey(KeyCode.X))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 111:
                if (Input.GetKey(KeyCode.Y))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 112:
                if (Input.GetKey(KeyCode.Z))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 113:
                if (Input.GetKey(KeyCode.Numlock))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 114:
                if (Input.GetKey(KeyCode.CapsLock))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 115:
                if (Input.GetKey(KeyCode.ScrollLock))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 116:
                if (Input.GetKey(KeyCode.RightShift))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 117:
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 118:
                if (Input.GetKey(KeyCode.RightControl))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 119:
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 120:
                if (Input.GetKey(KeyCode.RightAlt))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 121:
                if (Input.GetKey(KeyCode.LeftAlt))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 122:
                if (Input.GetKey(KeyCode.LeftCommand))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 123:
                if (Input.GetKey(KeyCode.LeftApple))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 124:
                if (Input.GetKey(KeyCode.LeftWindows))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 125:
                if (Input.GetKey(KeyCode.RightCommand))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 126:
                if (Input.GetKey(KeyCode.RightApple))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 127:
                if (Input.GetKey(KeyCode.RightWindows))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 128:
                if (Input.GetKey(KeyCode.AltGr))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 129:
                if (Input.GetKey(KeyCode.Help))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 130:
                if (Input.GetKey(KeyCode.Print))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 131:
                if (Input.GetKey(KeyCode.SysReq))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 132:
                if (Input.GetKey(KeyCode.Break))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 133:
                if (Input.GetKey(KeyCode.Menu))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 134:
                if (Input.GetKey(KeyCode.Mouse0)) // Left Mouse
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 135:
                if (Input.GetKey(KeyCode.Mouse1)) // Right Mouse
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 136:
                if (Input.GetKey(KeyCode.Mouse2)) // Middle Mouse
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 137:
                if (Input.GetKey(KeyCode.Mouse3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 138:
                if (Input.GetKey(KeyCode.Mouse4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 139:
                if (Input.GetKey(KeyCode.Mouse5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 140:
                if (Input.GetKey(KeyCode.Mouse6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 141: // Any Joystick
                if (Input.GetKey(KeyCode.JoystickButton0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 142:
                if (Input.GetKey(KeyCode.JoystickButton1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 143:
                if (Input.GetKey(KeyCode.JoystickButton2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 144:
                if (Input.GetKey(KeyCode.JoystickButton3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 145:
                if (Input.GetKey(KeyCode.JoystickButton4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 146:
                if (Input.GetKey(KeyCode.JoystickButton5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 147:
                if (Input.GetKey(KeyCode.JoystickButton6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 148:
                if (Input.GetKey(KeyCode.JoystickButton7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 149:
                if (Input.GetKey(KeyCode.JoystickButton8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 150:
                if (Input.GetKey(KeyCode.JoystickButton9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 151:
                if (Input.GetKey(KeyCode.JoystickButton10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 152:
                if (Input.GetKey(KeyCode.JoystickButton11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 153:
                if (Input.GetKey(KeyCode.JoystickButton12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 154:
                if (Input.GetKey(KeyCode.JoystickButton13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 155:
                if (Input.GetKey(KeyCode.JoystickButton14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 156:
                if (Input.GetKey(KeyCode.JoystickButton15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 157:
                if (Input.GetKey(KeyCode.JoystickButton16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 158:
                if (Input.GetKey(KeyCode.JoystickButton17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 159:
                if (Input.GetKey(KeyCode.JoystickButton18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 160:
                if (Input.GetKey(KeyCode.JoystickButton19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 161: // Joystick 1
                if (Input.GetKey(KeyCode.Joystick1Button0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 162:
                if (Input.GetKey(KeyCode.Joystick1Button1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 163:
                if (Input.GetKey(KeyCode.Joystick1Button2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 164:
                if (Input.GetKey(KeyCode.Joystick1Button3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 165:
                if (Input.GetKey(KeyCode.Joystick1Button4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 166:
                if (Input.GetKey(KeyCode.Joystick1Button5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 167:
                if (Input.GetKey(KeyCode.Joystick1Button6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 168:
                if (Input.GetKey(KeyCode.Joystick1Button7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 169:
                if (Input.GetKey(KeyCode.Joystick1Button8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 170:
                if (Input.GetKey(KeyCode.Joystick1Button9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 171:
                if (Input.GetKey(KeyCode.Joystick1Button10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 172:
                if (Input.GetKey(KeyCode.Joystick1Button11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 173:
                if (Input.GetKey(KeyCode.Joystick1Button12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 174:
                if (Input.GetKey(KeyCode.Joystick1Button13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 175:
                if (Input.GetKey(KeyCode.Joystick1Button14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 176:
                if (Input.GetKey(KeyCode.Joystick1Button15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 177:
                if (Input.GetKey(KeyCode.Joystick1Button16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 178:
                if (Input.GetKey(KeyCode.Joystick1Button17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 179:
                if (Input.GetKey(KeyCode.Joystick1Button18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 180:
                if (Input.GetKey(KeyCode.Joystick1Button19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 181: // Joystick 2
                if (Input.GetKey(KeyCode.Joystick2Button0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 182:
                if (Input.GetKey(KeyCode.Joystick2Button1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 183:
                if (Input.GetKey(KeyCode.Joystick2Button2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 184:
                if (Input.GetKey(KeyCode.Joystick2Button3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 185:
                if (Input.GetKey(KeyCode.Joystick2Button4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 186:
                if (Input.GetKey(KeyCode.Joystick2Button5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 187:
                if (Input.GetKey(KeyCode.Joystick2Button6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 188:
                if (Input.GetKey(KeyCode.Joystick2Button7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 189:
                if (Input.GetKey(KeyCode.Joystick2Button8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 190:
                if (Input.GetKey(KeyCode.Joystick2Button9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 191:
                if (Input.GetKey(KeyCode.Joystick2Button10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 192:
                if (Input.GetKey(KeyCode.Joystick2Button11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 193:
                if (Input.GetKey(KeyCode.Joystick2Button12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 194:
                if (Input.GetKey(KeyCode.Joystick2Button13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 195:
                if (Input.GetKey(KeyCode.Joystick2Button14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 196:
                if (Input.GetKey(KeyCode.Joystick2Button15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 197:
                if (Input.GetKey(KeyCode.Joystick2Button16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 198:
                if (Input.GetKey(KeyCode.Joystick2Button17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 199:
                if (Input.GetKey(KeyCode.Joystick2Button18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 200:
                if (Input.GetKey(KeyCode.Joystick2Button19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 201: // Joystick 3
                if (Input.GetKey(KeyCode.Joystick3Button0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 202:
                if (Input.GetKey(KeyCode.Joystick3Button1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 203:
                if (Input.GetKey(KeyCode.Joystick3Button2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 204:
                if (Input.GetKey(KeyCode.Joystick3Button3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 205:
                if (Input.GetKey(KeyCode.Joystick3Button4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 206:
                if (Input.GetKey(KeyCode.Joystick3Button5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 207:
                if (Input.GetKey(KeyCode.Joystick3Button6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 208:
                if (Input.GetKey(KeyCode.Joystick3Button7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 209:
                if (Input.GetKey(KeyCode.Joystick3Button8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 210:
                if (Input.GetKey(KeyCode.Joystick3Button9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 211:
                if (Input.GetKey(KeyCode.Joystick3Button10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 212:
                if (Input.GetKey(KeyCode.Joystick3Button11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 213:
                if (Input.GetKey(KeyCode.Joystick3Button12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 214:
                if (Input.GetKey(KeyCode.Joystick3Button13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 215:
                if (Input.GetKey(KeyCode.Joystick3Button14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 216:
                if (Input.GetKey(KeyCode.Joystick3Button15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 217:
                if (Input.GetKey(KeyCode.Joystick3Button16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 218:
                if (Input.GetKey(KeyCode.Joystick3Button17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 219:
                if (Input.GetKey(KeyCode.Joystick3Button18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 220:
                if (Input.GetKey(KeyCode.Joystick3Button19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 221: // Joystick 4
                if (Input.GetKey(KeyCode.Joystick4Button0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 222:
                if (Input.GetKey(KeyCode.Joystick4Button1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 223:
                if (Input.GetKey(KeyCode.Joystick4Button2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 224:
                if (Input.GetKey(KeyCode.Joystick4Button3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 225:
                if (Input.GetKey(KeyCode.Joystick4Button4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 226:
                if (Input.GetKey(KeyCode.Joystick4Button5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 227:
                if (Input.GetKey(KeyCode.Joystick4Button6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 228:
                if (Input.GetKey(KeyCode.Joystick4Button7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 229:
                if (Input.GetKey(KeyCode.Joystick4Button8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 230:
                if (Input.GetKey(KeyCode.Joystick4Button9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 231:
                if (Input.GetKey(KeyCode.Joystick4Button10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 232:
                if (Input.GetKey(KeyCode.Joystick4Button11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 233:
                if (Input.GetKey(KeyCode.Joystick4Button12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 234:
                if (Input.GetKey(KeyCode.Joystick4Button13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 235:
                if (Input.GetKey(KeyCode.Joystick4Button14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 236:
                if (Input.GetKey(KeyCode.Joystick4Button15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 237:
                if (Input.GetKey(KeyCode.Joystick4Button16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 238:
                if (Input.GetKey(KeyCode.Joystick4Button17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 239:
                if (Input.GetKey(KeyCode.Joystick4Button18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 240:
                if (Input.GetKey(KeyCode.Joystick4Button19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 241: // Joystick 5
                if (Input.GetKey(KeyCode.Joystick5Button0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 242:
                if (Input.GetKey(KeyCode.Joystick5Button1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 243:
                if (Input.GetKey(KeyCode.Joystick5Button2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 244:
                if (Input.GetKey(KeyCode.Joystick5Button3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 245:
                if (Input.GetKey(KeyCode.Joystick5Button4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 246:
                if (Input.GetKey(KeyCode.Joystick5Button5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 247:
                if (Input.GetKey(KeyCode.Joystick5Button6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 248:
                if (Input.GetKey(KeyCode.Joystick5Button7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 249:
                if (Input.GetKey(KeyCode.Joystick5Button8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 250:
                if (Input.GetKey(KeyCode.Joystick5Button9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 251:
                if (Input.GetKey(KeyCode.Joystick5Button10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 252:
                if (Input.GetKey(KeyCode.Joystick5Button11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 253:
                if (Input.GetKey(KeyCode.Joystick5Button12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 254:
                if (Input.GetKey(KeyCode.Joystick5Button13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 255:
                if (Input.GetKey(KeyCode.Joystick5Button14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 256:
                if (Input.GetKey(KeyCode.Joystick5Button15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 257:
                if (Input.GetKey(KeyCode.Joystick5Button16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 258:
                if (Input.GetKey(KeyCode.Joystick5Button17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 259:
                if (Input.GetKey(KeyCode.Joystick5Button18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 260:
                if (Input.GetKey(KeyCode.Joystick5Button19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 261: // Joystick 6
                if (Input.GetKey(KeyCode.Joystick6Button0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 262:
                if (Input.GetKey(KeyCode.Joystick6Button1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 263:
                if (Input.GetKey(KeyCode.Joystick6Button2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 264:
                if (Input.GetKey(KeyCode.Joystick6Button3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 265:
                if (Input.GetKey(KeyCode.Joystick6Button4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 266:
                if (Input.GetKey(KeyCode.Joystick6Button5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 267:
                if (Input.GetKey(KeyCode.Joystick6Button6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 268:
                if (Input.GetKey(KeyCode.Joystick6Button7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 269:
                if (Input.GetKey(KeyCode.Joystick6Button8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 270:
                if (Input.GetKey(KeyCode.Joystick6Button9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 271:
                if (Input.GetKey(KeyCode.Joystick6Button10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 272:
                if (Input.GetKey(KeyCode.Joystick6Button11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 273:
                if (Input.GetKey(KeyCode.Joystick6Button12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 274:
                if (Input.GetKey(KeyCode.Joystick6Button13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 275:
                if (Input.GetKey(KeyCode.Joystick6Button14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 276:
                if (Input.GetKey(KeyCode.Joystick6Button15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 277:
                if (Input.GetKey(KeyCode.Joystick6Button16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 278:
                if (Input.GetKey(KeyCode.Joystick6Button17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 279:
                if (Input.GetKey(KeyCode.Joystick6Button18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 280:
                if (Input.GetKey(KeyCode.Joystick6Button19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 281: // Joystick 7
                if (Input.GetKey(KeyCode.Joystick7Button0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 282:
                if (Input.GetKey(KeyCode.Joystick7Button1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 283:
                if (Input.GetKey(KeyCode.Joystick7Button2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 284:
                if (Input.GetKey(KeyCode.Joystick7Button3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 285:
                if (Input.GetKey(KeyCode.Joystick7Button4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 286:
                if (Input.GetKey(KeyCode.Joystick7Button5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 287:
                if (Input.GetKey(KeyCode.Joystick7Button6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 288:
                if (Input.GetKey(KeyCode.Joystick7Button7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 289:
                if (Input.GetKey(KeyCode.Joystick7Button8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 290:
                if (Input.GetKey(KeyCode.Joystick7Button9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 291:
                if (Input.GetKey(KeyCode.Joystick7Button10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 292:
                if (Input.GetKey(KeyCode.Joystick7Button11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 293:
                if (Input.GetKey(KeyCode.Joystick7Button12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 294:
                if (Input.GetKey(KeyCode.Joystick7Button13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 295:
                if (Input.GetKey(KeyCode.Joystick7Button14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 296:
                if (Input.GetKey(KeyCode.Joystick7Button15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 297:
                if (Input.GetKey(KeyCode.Joystick7Button16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 298:
                if (Input.GetKey(KeyCode.Joystick7Button17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 299:
                if (Input.GetKey(KeyCode.Joystick7Button18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 300:
                if (Input.GetKey(KeyCode.Joystick7Button19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 301: // Joystick 8
                if (Input.GetKey(KeyCode.Joystick8Button0))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 302:
                if (Input.GetKey(KeyCode.Joystick8Button1))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 303:
                if (Input.GetKey(KeyCode.Joystick8Button2))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 304:
                if (Input.GetKey(KeyCode.Joystick8Button3))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 305:
                if (Input.GetKey(KeyCode.Joystick8Button4))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 306:
                if (Input.GetKey(KeyCode.Joystick8Button5))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 307:
                if (Input.GetKey(KeyCode.Joystick8Button6))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 308:
                if (Input.GetKey(KeyCode.Joystick8Button7))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 309:
                if (Input.GetKey(KeyCode.Joystick8Button8))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 310:
                if (Input.GetKey(KeyCode.Joystick8Button9))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 311:
                if (Input.GetKey(KeyCode.Joystick8Button10))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 312:
                if (Input.GetKey(KeyCode.Joystick8Button11))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 313:
                if (Input.GetKey(KeyCode.Joystick8Button12))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 314:
                if (Input.GetKey(KeyCode.Joystick8Button13))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 315:
                if (Input.GetKey(KeyCode.Joystick8Button14))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 316:
                if (Input.GetKey(KeyCode.Joystick8Button15))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 317:
                if (Input.GetKey(KeyCode.Joystick8Button16))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 318:
                if (Input.GetKey(KeyCode.Joystick8Button17))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 319:
                if (Input.GetKey(KeyCode.Joystick8Button18))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 320:
                if (Input.GetKey(KeyCode.Joystick8Button19))
                {
                    return 1f;
                }
                else
                {
                    return 0f;
                }
            case 321: // Mouse Up Movement
                if (Input.GetAxis("Mouse Y") > 0.0f)
                {
                    return Input.GetAxis("Mouse Y")/5f;
                }
                else
                {
                    return 0f;
                }
            case 322: // Mouse Down Movement
                if (Input.GetAxis("Mouse Y") < 0.0f)
                {
                    return -Input.GetAxis("Mouse Y") / 5f;
                }
                else
                {
                    return 0f;
                }
            case 323: // Mouse Right Movement
                if (Input.GetAxis("Mouse X") > 0.0f)
                {
                    return Input.GetAxis("Mouse X") / 5f;
                }
                else
                {
                    return 0f;
                }
            case 324: // Mouse  Left Movement
                if (Input.GetAxis("Mouse X") < 0.0f)
                {
                    return -Input.GetAxis("Mouse X") / 5f;
                }
                else
                {
                    return 0f;
                }
            case 325: // Joystick 1 Up Movement
                if (Input.GetAxis("Joystick 1 Y") > 0.0f)
                {
                    return Input.GetAxis("Joystick 1 Y");
                }
                else
                {
                    return 0f;
                }
            case 326: // Joystick 1 Down Movement
                if (Input.GetAxis("Joystick 1 Y") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 1 Y");
                }
                else
                {
                    return 0f;
                }
            case 327: // Joystick 1 Right Movement
                if (Input.GetAxis("Joystick 1 X") > 0.0f)
                {
                    return Input.GetAxis("Joystick 1 X");
                }
                else
                {
                    return 0f;
                }
            case 328: // Joystick 1 Left Movement
                if (Input.GetAxis("Joystick 1 X") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 1 X");
                }
                else
                {
                    return 0f;
                }
            case 329: // Joystick 1 Up Movement 2
                if (Input.GetAxis("Joystick 1 Z") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 1 Z");
                }
                else
                {
                    return 0f;
                }
            case 330: // Joystick 1 Down Movement 2
                if (Input.GetAxis("Joystick 1 Z") > 0.0f)
                {
                    return Input.GetAxis("Joystick 1 Z");
                }
                else
                {
                    return 0f;
                }
            case 331: // Joystick 1 Right Movement 2
                if (Input.GetAxis("Joystick 1 T") > 0.0f)
                {
                    return Input.GetAxis("Joystick 1 T");
                }
                else
                {
                    return 0f;
                }
            case 332: // Joystick 1 Left Movement 2
                if (Input.GetAxis("Joystick 1 T") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 1 T");
                }
                else
                {
                    return 0f;
                }


            case 333: // Joystick 2 Up Movement
                if (Input.GetAxis("Joystick 2 Y") > 0.0f)
                {
                    return Input.GetAxis("Joystick 2 Y");
                }
                else
                {
                    return 0f;
                }
            case 334: // Joystick 2 Down Movement
                if (Input.GetAxis("Joystick 2 Y") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 2 Y");
                }
                else
                {
                    return 0f;
                }
            case 335: // Joystick 2 Right Movement
                if (Input.GetAxis("Joystick 2 X") > 0.0f)
                {
                    return Input.GetAxis("Joystick 2 X");
                }
                else
                {
                    return 0f;
                }
            case 336: // Joystick 2 Left Movement
                if (Input.GetAxis("Joystick 2 X") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 2 X");
                }
                else
                {
                    return 0f;
                }
            case 337: // Joystick 2 Up Movement 2
                if (Input.GetAxis("Joystick 2 Z") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 2 Z");
                }
                else
                {
                    return 0f;
                }
            case 338: // Joystick 2 Down Movement 2
                if (Input.GetAxis("Joystick 2 Z") > 0.0f)
                {
                    return Input.GetAxis("Joystick 2 Z");
                }
                else
                {
                    return 0f;
                }
            case 339: // Joystick 2 Right Movement 2
                if (Input.GetAxis("Joystick 2 T") > 0.0f)
                {
                    return Input.GetAxis("Joystick 2 T");
                }
                else
                {
                    return 0f;
                }
            case 340: // Joystick 2 Left Movement 2
                if (Input.GetAxis("Joystick 2 T") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 2 T");
                }
                else
                {
                    return 0f;
                }


            case 341: // Joystick 3 Up Movement
                if (Input.GetAxis("Joystick 3 Y") > 0.0f)
                {
                    return Input.GetAxis("Joystick 3 Y");
                }
                else
                {
                    return 0f;
                }
            case 342: // Joystick 3 Down Movement
                if (Input.GetAxis("Joystick 3 Y") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 3 Y");
                }
                else
                {
                    return 0f;
                }
            case 343: // Joystick 3 Right Movement
                if (Input.GetAxis("Joystick 3 X") > 0.0f)
                {
                    return Input.GetAxis("Joystick 3 X");
                }
                else
                {
                    return 0f;
                }
            case 344: // Joystick 3 Left Movement
                if (Input.GetAxis("Joystick 3 X") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 3 X");
                }
                else
                {
                    return 0f;
                }
            case 345: // Joystick 3 Up Movement 2
                if (Input.GetAxis("Joystick 3 Z") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 3 Z");
                }
                else
                {
                    return 0f;
                }
            case 346: // Joystick 3 Down Movement 2
                if (Input.GetAxis("Joystick 3 Z") > 0.0f)
                {
                    return Input.GetAxis("Joystick 3 Z");
                }
                else
                {
                    return 0f;
                }
            case 347: // Joystick 3 Right Movement 2
                if (Input.GetAxis("Joystick 3 T") > 0.0f)
                {
                    return Input.GetAxis("Joystick 3 T");
                }
                else
                {
                    return 0f;
                }
            case 348: // Joystick 3 Left Movement 2
                if (Input.GetAxis("Joystick 3 T") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 3 T");
                }
                else
                {
                    return 0f;
                }


            case 349: // Joystick 4 Up Movement
                if (Input.GetAxis("Joystick 4 Y") > 0.0f)
                {
                    return Input.GetAxis("Joystick 4 Y");
                }
                else
                {
                    return 0f;
                }
            case 350: // Joystick 4 Down Movement
                if (Input.GetAxis("Joystick 4 Y") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 4 Y");
                }
                else
                {
                    return 0f;
                }
            case 351: // Joystick 4 Right Movement
                if (Input.GetAxis("Joystick 4 X") > 0.0f)
                {
                    return Input.GetAxis("Joystick 4 X");
                }
                else
                {
                    return 0f;
                }
            case 352: // Joystick 4 Left Movement
                if (Input.GetAxis("Joystick 4 X") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 4 X");
                }
                else
                {
                    return 0f;
                }
            case 353: // Joystick 4 Up Movement 2
                if (Input.GetAxis("Joystick 4 Z") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 4 Z");
                }
                else
                {
                    return 0f;
                }
            case 354: // Joystick 4 Down Movement 2
                if (Input.GetAxis("Joystick 4 Z") > 0.0f)
                {
                    return Input.GetAxis("Joystick 4 Z");
                }
                else
                {
                    return 0f;
                }
            case 355: // Joystick 4 Right Movement 2
                if (Input.GetAxis("Joystick 4 T") > 0.0f)
                {
                    return Input.GetAxis("Joystick 4 T");
                }
                else
                {
                    return 0f;
                }
            case 356: // Joystick 4 Left Movement 2
                if (Input.GetAxis("Joystick 4 T") < 0.0f)
                {
                    return -Input.GetAxis("Joystick 4 T");
                }
                else
                {
                    return 0f;
                }


        }

        return 0.0f;
    }
}
