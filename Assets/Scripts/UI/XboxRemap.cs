using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class XboxRemap : MonoBehaviour
{
    PlayerInput.PlayerButton action;
    int index;
    public string keyName;
    bool remaping;

    public void Update()
    {

    }
    /*
        if (remaping)
        {
            if (InputManager.xboxControllers.FirstOrDefault().AnyButtonDown)
            {
                SetButton(InputManager.GetNextXboxButton(player));
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
        List<string> xboxCodes = GameObject.Find("Player 1 Camera").GetComponentInChildren<PauseMenu>().xboxCodes;
        player = GameObject.Find("PlayerDefault").GetComponentInChildren<IInputPlayer>();
        foreach (string xKey in xboxCodes)
        {
            if (passed.ToString() == xKey)
            {
                return;
            }
        }
        InputManager.RemapXboxButton(action, passed, player);
        xboxCodes.Remove(keyName);
        keyName = passed.ToString();
        xboxCodes.Add(keyName);
        GetComponentInChildren<Text>().text = keyName;
    }
    public IEnumerator timerRemaping()
    {
        for (int x = 0; x < 10; x++)
        {
            yield return new WaitForEndOfFrame();
        }
        remaping = true;
    } */
}

