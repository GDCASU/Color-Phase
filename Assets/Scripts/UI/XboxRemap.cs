﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class XboxRemap : MonoBehaviour
{
    PlayerInput.PlayerButton action;
    XboxController.XboxButton button;
    IInputPlayer player;
    int index;
    public string keyName;
    bool remaping;

    public void Update()
    {
        if (remaping)
        {
            if (InputManager.xboxControllers[0].AnyButtonDown)
            {
                button = InputManager.GetNextXboxButton(player);
                SetButton(button);
                remaping = false;
            }

        }
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
        button = GameObject.Find("Managers").GetComponent<InputManager>().buttons[passed].xboxButton;
        keyName = GameObject.Find("Managers").GetComponent<InputManager>().buttons[passed].xboxButton.ToString();
    }
    public void Remaping()
    {
        StartCoroutine(timerRemaping());
    }
    public void SetButton(XboxController.XboxButton passed)
    {
        List<string> temp = new List<string>();
        temp = GameObject.Find("Player 1 Camera").GetComponentInChildren<PauseMenu>().xboxCodes;
        player = GameObject.Find("PlayerDefault").GetComponentInChildren<IInputPlayer>();
        foreach (string xKey in temp)
        {
            if (passed.ToString() == xKey)
            {
                return;
            }
        }
        InputManager.RemapXboxButton(action, passed, player);
        temp.Remove(keyName);
        keyName = passed.ToString();
        temp.Add(keyName);
        GetComponentInChildren<Text>().text = keyName;
        GameObject.Find("Managers").GetComponent<InputManager>().buttons[index].xboxButton = passed;
    }
    public IEnumerator timerRemaping()
    {
        for (int x = 0; x < 10; x++)
        {
            yield return new WaitForEndOfFrame();
        }
        remaping = true;
    }
}

