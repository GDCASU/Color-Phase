using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{

    private int forwardInput = 109;
    private int backwardInput = 105;
    private int rightInput = 90;
    private int leftInput = 87;
    private int jumpInput = 8; // space key = 8
    private int sprintInput = 000000000000000;

    private int jumpKey = 0;

    public Rigidbody rb;
    public Camera cam;


    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        if (GetInputf(forwardInput) == 1f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);

            transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);

        }
        if (GetInputf(backwardInput) == 1f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);

            transform.rotation = Quaternion.Euler(00.0f, 180.0f, 0.0f);

        }
        if (GetInputf(leftInput) == 1f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);

            transform.rotation = Quaternion.Euler(00.0f, -90.0f, 0.0f);

        }
        if (GetInputf(rightInput) == 1f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);

            transform.rotation = Quaternion.Euler(00.0f, 90, 0.0f);

        }
        /*
        if (GetInputf(forwardInput) == 1f)
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
        }
        if (GetInputf(backwardInput) == 1f)
        {
            transform.Translate(Vector3.back * Time.deltaTime);
        }
        if (GetInputf(leftInput) == 1f)
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (GetInputf(rightInput) == 1f)
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        */

        if (GetInputf(jumpInput) == 1f && jumpKey == 0 && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.05f))
        {
            rb.velocity = new Vector3(0, 5, 0);
            jumpKey = 1;
            //transform.Translate(Vector3.back * Time.deltaTime);
        }
        if (GetInputf(jumpInput) == 0f)
        {
            jumpKey = 0;
        }


        /*
        if (Input.GetKey("w"))
        {
            transform.Translate(Vector3.forward * Time.deltaTime);
            //transform.Translate(Vector3.up * Time.deltaTime, Space.World);
        }
        if (Input.GetKey("s"))
        {
            transform.Translate(Vector3.back * Time.deltaTime);
        }
        if (Input.GetKey("a"))
        {
            transform.Translate(Vector3.left * Time.deltaTime);
        }
        if (Input.GetKey("d"))
        {
            transform.Translate(Vector3.right * Time.deltaTime);
        }
        */

    }


    float GetInputf(int inputValue)
    {

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

        }

        return 0.0f;
    }

}
