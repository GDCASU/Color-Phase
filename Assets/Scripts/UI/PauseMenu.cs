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
    public List<GameObject> panels;
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject selectLevel;
    public GameObject UIPrefab;
    public Button leftArrowPrefab;
    public Button rightArrowPrefab;
    public Button buttonPrefab;
    private Button button;
    private bool isPaused = false;
    private int numberOfPanels;
    public int currentPanel;
    int index;
    int numberOfScenes;

    private void Start()
    {
        panels = new List<GameObject>();
        currentPanel = 0;
        index = 0;
        numberOfPanels = 0;
        numberOfScenes = UnityEditor.EditorBuildSettings.scenes.Length;
        scenes = UnityEditor.EditorBuildSettings.scenes;
        player = GetComponent<IInputPlayer>();
        playerUI = GetComponent<UI>();
        camControl = gameObject.transform.parent.transform.parent.GetComponentInChildren<PlayerCamControl>();
        playerMovement = gameObject.transform.parent.transform.parent.GetComponentInChildren<PlayerMovement>();
        while (numberOfPanels <= (numberOfScenes/10))
        {
            int temp = 0;
            if((numberOfScenes % 10) ==0)
            {
                numberOfPanels++;
            }
            while(temp<10 && index<numberOfScenes)
            {
                BuildLevelsUI(temp);
                temp++;
                index++;
            }
            currentPanel++;
            numberOfPanels++;
        }
        currentPanel = 0;
    }
    private void Update()
    {
        if (InputManager.GetButtonDown(PlayerInput.PlayerButton.UI_Submit, player))
        {
            if (!isPaused && pauseMenu.active==false)
            {
                Pause();
            }
            else if(panels[currentPanel].active== true)
            {
                panels[currentPanel].SetActive(false);
                pauseMenu.SetActive(true);
                currentPanel = 0;

            }
            else
            {
                ResumeGame();
            }
            EventSystem.current.SetSelectedGameObject(pauseMenu.transform.GetChild(0).gameObject);
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
    public void MainMenu()
    {
        SceneManager.LoadScene("TitleScreen");
    }
    public void LevelSelect()
    {
        pauseMenu.SetActive(false);
        panels[currentPanel].SetActive(true);
        selectLevel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(panels[currentPanel].transform.GetChild(1).gameObject );
    }
    public void BuildLevelsUI(int passed)
    {
        float xPosition = 160;
        float yPosition = -90;
        float canvasWidth=UIPrefab.GetComponent<RectTransform>().rect.width;
        float canvasHeight = UIPrefab.GetComponent<RectTransform>().rect.width;

        if (passed == 0)
        {
            GameObject panel = Instantiate(selectLevel, Vector2.zero, Quaternion.identity);
            panel.transform.parent = UIPrefab.transform;
            panel.GetComponent<RectTransform>().localScale = Vector3.one;
            panel.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            panel.GetComponent<RectTransform>().localScale = Vector3.one;
            panel.GetComponent<RectTransform>().sizeDelta = new Vector2(canvasWidth, canvasHeight);
            panel.SetActive(false);
            panels.Add(panel);
            if ((numberOfScenes - index) > 10)
            {
                Button temp = Instantiate(rightArrowPrefab, Vector2.zero, Quaternion.identity);
                temp.transform.parent = panels[currentPanel].transform;
                temp.GetComponent<RectTransform>().localPosition = new Vector2(360, 0);
                temp.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
            }
            if (index >=10)
            {
                Button temp = Instantiate(leftArrowPrefab, Vector2.zero, Quaternion.identity);
                temp.transform.parent = panels[currentPanel].transform;
                temp.GetComponent<RectTransform>().localPosition = new Vector2(-360, 0);
                temp.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 90);
            }
        }
        scene = scenes[index];
        button = Instantiate(buttonPrefab, Vector2.zero, Quaternion.identity);
        button.transform.parent = panels[currentPanel].transform;
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

    }
    public void nextLevels()
    {
        PauseMenu temp = GameObject.Find("PlayerUI").GetComponent<PauseMenu>();
        print(temp.currentPanel);
        print(temp.panels.Count);
        temp.panels[temp.currentPanel].SetActive(false);
        temp.currentPanel++;
        temp.panels[temp.currentPanel].SetActive(true);
        EventSystem.current.SetSelectedGameObject(temp.panels[temp.currentPanel].transform.GetChild(1).gameObject);
    }
    public void previousLevles()
    {
        PauseMenu temp = GameObject.Find("PlayerUI").GetComponent<PauseMenu>();
        print(temp.currentPanel);
        print(temp.panels.Count);
        temp.panels[temp.currentPanel].SetActive(false);
        temp.currentPanel--;
        temp.panels[temp.currentPanel].SetActive(true);
        EventSystem.current.SetSelectedGameObject(temp.panels[temp.currentPanel].transform.GetChild(1).gameObject);
    }
    public void Settings()
    {

    }
    public void ExitGame()
    {
        SaveGame();
        Application.Quit();
    }
}
