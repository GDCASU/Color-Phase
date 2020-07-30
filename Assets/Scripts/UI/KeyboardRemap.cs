using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerInput;

public class KeyboardRemap : MonoBehaviour
{
    PlayerButton action;
    KeyCode button;
    int index;
    public string keyName;
    bool remaping;
    
    public void Update()
    {
        if (remaping)
        {
            if (Input.anyKey)
            {
                foreach (KeyCode vKey in System.Enum.GetValues(typeof(KeyCode)))
                {
                    if (Input.GetKeyDown(vKey))
                    {
                        SetButton(vKey);
                        remaping = false;
                    }
                }
            }
        }
    }
    public void Remaping()
    {
        remaping = true;
    }
    public void InitiateButton(int passed)
    {
        index = passed;
        switch (index)
        {
            case 0:
                action = PlayerButton.Jump;
                break;
            case 1:
                action = PlayerButton.Swap;
                break;
            case 2:
                action = PlayerButton.PickUp;
                break;
            case 3:
                action = PlayerButton.Grapple;
                break;
            default:
                break;
        }
        button = InputManager.playerButtons[action].keyboardKey;
        keyName = button.ToString();
    }
    public void SetButton(KeyCode passed)
    {
        List<string> keyboardCodes = GameObject.Find("Player 1 Camera").GetComponentInChildren<PauseMenu>().keyboardCodes;

        foreach (string key in keyboardCodes)
        {
            if (passed.ToString() == key)
            {
                return;
            }
        }
        PlayerAction actn = InputManager.playerButtons[action];
        actn.keyboardKey = passed;
        InputManager.playerButtons[action]=actn;
        keyboardCodes.Remove(keyName);
        keyName = passed.ToString();
        keyboardCodes.Add(keyName);
        GetComponentInChildren<Text>().text = keyName;
    }

}
