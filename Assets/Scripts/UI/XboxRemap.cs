using System.Collections;
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
    XboxController xbox;

    public void Update()
    {
        if (remaping)
        {
            if (xbox.AnyButtonDown)
            {
                button = InputManager.GetNextXboxButton();
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
    //Curently not working
    public void SetButton(XboxController.XboxButton passed)
    {
        GameObject.Find("Managers").GetComponent<InputManager>().buttons[index].xboxButton = passed;
    }
}

