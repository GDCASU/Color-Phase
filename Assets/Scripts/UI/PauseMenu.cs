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
    PlayerCamControl camControl;
    PlayerMovement playerMovement;
    public Scene[] scenes; 
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject selectLevel;
    public GameObject ArrowPrefab;
    private bool isPaused = false;
    private bool building = false;
    int index;
    public Button buttonPrefab;
    Button button;
    int numerOfScenes;
    Scene scene;

    private void Start()
    {
        numerOfScenes= SceneManager.sceneCount;
        index = 0;
        player = GetComponent<IInputPlayer>();
        playerUI = GetComponent<UI>();
        camControl = gameObject.transform.parent.transform.parent.GetComponentInChildren<PlayerCamControl>();
        playerMovement = gameObject.transform.parent.transform.parent.GetComponentInChildren<PlayerMovement>();
    }
    private void Update()
    {
        if (InputManager.GetButtonDown(PlayerInput.PlayerButton.UI_Submit, player))
        {
            if (!isPaused && pauseMenu.active==false)
            {
                Pause();
            }
            else if(selectLevel.active==true)
            {
                selectLevel.SetActive(false);
                pauseMenu.SetActive(true);
            }
            else
            {
                ResumeGame();
            }
        }

    }
    public void FixedUpdate()
    {
        if (index < 10 && index <  numerOfScenes && building==true)
        {
            BuildLevelsUI();
            index++;
        }
        else
        {
            building = false;
        }
    }
    public void Pause()
    {
        isPaused = true;
        camControl.enabled = false;
        playerMovement.enabled = false;
        playerUI.enabled = false;
        HUD.SetActive(false);
        pauseMenu.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ResumeGame()
    {
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        camControl.enabled = true;
        playerMovement.enabled = true;
        playerUI.enabled = true;
        HUD.SetActive(true);
    }
    public void SaveGame()
    {

    }
    public void LevelSelect()
    {
        pauseMenu.SetActive(false);
        selectLevel.SetActive(true);
        building = true;

        
    }
    public void BuildLevelsUI()
    {
        float xPosition = 160;
        float yPosition = -90;
        scene = SceneManager.GetSceneAt(index);
        button = Instantiate(buttonPrefab, Vector2.zero, Quaternion.identity);
        button.transform.parent = selectLevel.transform;
        if (index < 5)
        {
            button.GetComponent<RectTransform>().localPosition = new Vector2(-xPosition,180- (yPosition*index));
        }
        else
        {
            button.GetComponent<RectTransform>().localPosition = new Vector2(xPosition, 180 - (yPosition * index-5));
        }
        button.GetComponent<RectTransform>().localScale =Vector3.one;
        button.GetComponent<ButtonProperties>().SetScene(scene);
        button.GetComponentInChildren<Text>().text = scene.name;

    }
    public void Settings()
    {

    }
    public void ExitGame()
    {

    }
}
