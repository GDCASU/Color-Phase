using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    UI playerUI;
    IInputPlayer player;
    PlayerCamControl camControl;
    PlayerMovement playerMovement;
    string scene;
    string[] scenes;
    public List<GameObject> panels;
    public List<string> keyboardCodes;
    public List<string> xboxCodes;
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject settings;
    public GameObject selectLevel;
    public GameObject UIPrefab;
    public Button leftArrowPrefab;
    public Button rightArrowPrefab;
    public Button buttonPrefab;
    private Button button;
    public Button xboxPrefab;
    public Button keyboardPrefab;
    private bool isPaused = false;
    private int numberOfPanels;
    public int currentPanel;
    int index;
    int numberOfScenes;

    private void Start()
    {
        settings.GetComponentInChildren<Slider>().value = GameObject.Find("Audio Source").GetComponent<AudioSource>().volume;
        panels = new List<GameObject>();
        keyboardCodes = new List<string>();
        xboxCodes = new List<string>();
        currentPanel = 0;
        index = 1;
        numberOfPanels = 0;
        numberOfScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        scenes = new string[numberOfScenes];
        for(int i = 0; i < numberOfScenes; i++) scenes[i]=UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);
        player = GetComponent<IInputPlayer>();
        playerUI = GetComponent<UI>();
        Debug.Log(PlayerColorController.singleton);
        camControl = PlayerColorController.singleton.GetComponent<PlayerCamControl>();
        playerMovement = PlayerColorController.singleton.GetComponent<PlayerMovement>();
        keyboardCodes.Add("W");
        keyboardCodes.Add("S");
        keyboardCodes.Add("D");
        keyboardCodes.Add("A ");
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
        for(int x=0;x<4;x++)
        {
            BuildSettingsUI(x);
        }
        currentPanel = 0;
    }
    
    private void Update()
    {
        if (InputManager.GetButtonDown(PlayerInput.PlayerButton.Pause, player))
        {
            if (!isPaused && !pauseMenu.activeInHierarchy)
            {
                Pause();
            }
            else if(panels[currentPanel].activeInHierarchy)
            {
                panels[currentPanel].SetActive(false);
                pauseMenu.SetActive(true);
                currentPanel = 0;

            }
            else if(settings.activeInHierarchy)
            {
                settings.SetActive(false);
                pauseMenu.SetActive(true);
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
        SceneManager.LoadScene("Title");
    }
    public void LevelSelect()
    {
        pauseMenu.SetActive(false);
        panels[currentPanel].SetActive(true);
        selectLevel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(panels[currentPanel].transform.GetChild(1).gameObject );
    }
    public void Settings()
    {
        pauseMenu.SetActive(false);
        settings.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settings.transform.GetChild(0).gameObject);
    }
    public void ExitGame()
    {
        Application.Quit();
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
                temp.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 180);
            }
            if (index >=10)
            {
                Button temp = Instantiate(leftArrowPrefab, Vector2.zero, Quaternion.identity);
                temp.transform.parent = panels[currentPanel].transform;
                temp.GetComponent<RectTransform>().localPosition = new Vector2(-360, 0);
                temp.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
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

        string name = scene.Substring(scene.LastIndexOf('/') + 1);
        name = name.Substring(0, name.Length - 6);
        button.GetComponentInChildren<Text>().text = name;

    }
    public void nextLevels()
    {
        if (GameObject.Find("PlayerUI"))
        {
            PauseMenu temp = GameObject.Find("PlayerUI").GetComponent<PauseMenu>();
            temp.panels[temp.currentPanel].SetActive(false);
            temp.currentPanel++;
            temp.panels[temp.currentPanel].SetActive(true);
            EventSystem.current.SetSelectedGameObject(temp.panels[temp.currentPanel].transform.GetChild(1).gameObject);
        }
        else
        {
            TitleScreenController temp = GameObject.Find("TitleUI").GetComponent<TitleScreenController>();
            temp.panels[temp.currentPanel].SetActive(false);
            temp.currentPanel++;
            temp.panels[temp.currentPanel].SetActive(true);
            EventSystem.current.SetSelectedGameObject(temp.panels[temp.currentPanel].transform.GetChild(1).gameObject);
        }
        
    }
    public void previousLevles()
    {
        if (GameObject.Find("PlayerUI"))
        {
            PauseMenu temp = GameObject.Find("PlayerUI").GetComponent<PauseMenu>();
            temp.panels[temp.currentPanel].SetActive(false);
            temp.currentPanel--;
            temp.panels[temp.currentPanel].SetActive(true);
            EventSystem.current.SetSelectedGameObject(temp.panels[temp.currentPanel].transform.GetChild(1).gameObject);
        }
        else
        {
            TitleScreenController temp = GameObject.Find("TitleUI").GetComponent<TitleScreenController>();
            temp.panels[temp.currentPanel].SetActive(false);
            temp.currentPanel--;
            temp.panels[temp.currentPanel].SetActive(true);
            EventSystem.current.SetSelectedGameObject(temp.panels[temp.currentPanel].transform.GetChild(1).gameObject);
        }
    }
    public void SetVolume(float passed)
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = passed;
    }
    public void BuildSettingsUI(int passed)
    {
        float yPosition = -90;

        Button keyboard = Instantiate(keyboardPrefab, Vector2.zero, Quaternion.identity);
        keyboard.transform.parent = settings.transform;
        keyboard.GetComponent<RectTransform>().localPosition = new Vector2(0, 90 + (yPosition * passed));
        keyboard.GetComponent<RectTransform>().localScale = Vector3.one;
        keyboard.GetComponent<KeyboardRemap>().InitiateButton(passed);
        string key = keyboard.GetComponent<KeyboardRemap>().keyName;
        keyboard.GetComponentInChildren<Text>().text = key;
        keyboardCodes.Add(key);


        Button xbox = Instantiate(xboxPrefab, Vector2.zero, Quaternion.identity);
        xbox.transform.parent = settings.transform;
        xbox.GetComponent<RectTransform>().localPosition = new Vector2(275, 90 + (yPosition * passed));
        xbox.GetComponent<RectTransform>().localScale = Vector3.one;
        xbox.GetComponent<XboxRemap>().InitiateButton(passed);
        xbox.interactable = false;
        string xb = xbox.GetComponent<XboxRemap>().keyName;
        xbox.GetComponentInChildren<Text>().text = xb;
        xboxCodes.Add(xb);
      
    }
}
