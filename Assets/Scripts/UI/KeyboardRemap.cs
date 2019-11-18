﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerInput;

public class KeyboardRemap : MonoBehaviour
{
    PlayerInput.PlayerButton action;
    KeyCode button;
    IInputPlayer player;
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
                action = PlayerInput.PlayerButton.Jump;
                break;
            case 1:
                action = PlayerInput.PlayerButton.Swap;
                break;
            case 2:
                action = PlayerInput.PlayerButton.PickUp;
                break;
            case 3:
                action = PlayerInput.PlayerButton.Grapple;
                break;
            default:
                break;
        }
        button = GameObject.Find("Managers").GetComponent<InputManager>().buttons[index].keyboardButton;
        keyName = GameObject.Find("Managers").GetComponent<InputManager>().buttons[index].keyboardButton.ToString();
    }
    public void SetButton(KeyCode passed)
    {
        List<string> keyboardCodes = GameObject.Find("Player 1 Camera").GetComponentInChildren<PauseMenu>().keyboardCodes;
        player = GameObject.Find("PlayerDefault").GetComponentInChildren<IInputPlayer>();
        foreach (string key in keyboardCodes)
        {
            if (passed.ToString() == key)
            {
                return;
            }
        }
        InputManager.RemapKeyboardButton(action,passed,player);
        keyboardCodes.Remove(keyName);
        keyName = passed.ToString();
        keyboardCodes.Add(keyName);
        GetComponentInChildren<Text>().text = keyName;
    }

}
