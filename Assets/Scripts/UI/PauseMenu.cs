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
    UnityEditor.EditorBuildSettingsScene scene;
    UnityEditor.EditorBuildSettingsScene[] scenes; 
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject selectLevel;
    public Button ArrowPrefab;
    public Button buttonPrefab;
    private Button button;
    private bool isPaused = false;
    private bool building = true;
    private int panels;
    int index;
    int numberOfScenes;

    private void Start()
    {
        index = 0;
        numberOfScenes = UnityEditor.EditorBuildSettings.scenes.Length;
        print(numberOfScenes);
        print(numberOfScenes/10);
        while (panels <(numberOfScenes/10))
        {

        }
        scenes = UnityEditor.EditorBuildSettings.scenes;
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
    public void CreateUI()
    {
        if (index <  numberOfScenes && building)
        {
            if(index<10)
            {
                BuildLevelsUI(index);
            }
            else if(index>=10)
            {
                BuildLevelsUI(index - 10);
            }
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
        Time.timeScale = 0;
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
        Time.timeScale = 1;
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
        EventSystem.current.SetSelectedGameObject( selectLevel.transform.GetChild(0).gameObject );
    }
    public void BuildLevelsUI(int passed)
    {
        float xPosition = 160;
        float yPosition = -90;
        scene = scenes[index];
        button = Instantiate(buttonPrefab, Vector2.zero, Quaternion.identity);
        button.transform.parent = selectLevel.transform;
        if (passed < 5)
        {
            button.GetComponent<RectTransform>().localPosition = new Vector2(-xPosition,180 + (yPosition*passed));
        }
        else
        {
            button.GetComponent<RectTransform>().localPosition = new Vector2(xPosition, 180 + (yPosition * (passed-5)));
        }
        button.GetComponent<RectTransform>().localScale =Vector3.one;  
        button.GetComponent<ButtonProperties>().SetScene(scene);

        string name = scene.path.Substring(scene.path.LastIndexOf('/') + 1);
        name = name.Substring(0, name.Length - 6);
        button.GetComponentInChildren<Text>().text = name;
        if(passed==0 && (numberOfScenes-index)>10)
        {
            Button temp = Instantiate(ArrowPrefab, Vector2.zero, Quaternion.identity);
            temp.transform.parent = selectLevel.transform;
            temp.GetComponent<RectTransform>().localPosition = new Vector2(360,0);
            temp.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 90, 0);
        }
        if(index==9)
        {
            building = false;
        }

    }
    public void Settings()
    {

    }
    public void ExitGame()
    {

    }
}
