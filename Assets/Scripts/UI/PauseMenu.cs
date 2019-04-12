using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;

public class PauseMenu : MonoBehaviour
{
    UI ui;
    IInputPlayer player;
    private void Start()
    {
        GetComponent<IInputPlayer>();
        ui = GetComponent<UI>();

    }
    private void Update()
    {
       if(InputManager.GetButtonDown(PlayerInput.PlayerButton.UI_Submit, player))
        {
            if(ui.enabled)
            {
                ui.enabled=false;
            }

        }
    }
}
