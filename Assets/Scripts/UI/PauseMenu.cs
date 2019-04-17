using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    UI playerUI;
    IInputPlayer player;
    public GameObject pauseMenu;
    public GameObject HUD;
    public bool isPaused=false;

    private void Start()
    {
        GetComponent<IInputPlayer>();
        playerUI = GetComponent<UI>();

    }
    private void Update()
    {
       if(InputManager.GetButtonDown(PlayerInput.PlayerButton.UI_Submit, player))
        {
            if(playerUI.enabled && !isPaused)
            {
                isPaused = true;
                playerUI.enabled = false;
                HUD.SetActive(false);
                pauseMenu.SetActive(true);

            }
            else
            {
                isPaused = false;
                pauseMenu.SetActive(false);
                playerUI.enabled = true;
                HUD.SetActive(true);
            }

        }


    }
    public void resumeGame()
    {

    }
}
