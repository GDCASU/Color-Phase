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
    IInputPlayer player;
    string scene;
    string[] scenes;
    public List<GameObject> panels;
    public List<string> keyboardCodes;
    public List<string> xboxCodes;
    public Button leftArrowPrefab;
    public Button rightArrowPrefab;
    public Button buttonPrefab;
    private Button button;
    public Button xboxPrefab;
    public Button keyboardPrefab;
    private int numberOfPanels;
    public int currentPanel;
    int index;
    int numberOfScenes;
    public GameObject settings;
    public GameObject selectLevel;
    private GameObject canvas;
    public GameObject main;

    [Header("Pause Menu")]
    #region Pause Menu
    UI playerUI;
    PlayerCamControl camControl;
    PlayerMovement playerMovement;
    public GameObject pauseMenu;
    public GameObject HUD;
    public GameObject UIPrefab;
    private bool isPaused = false;
    #endregion

    [Header("Title Screen")]
    #region Title Screen
    public GameObject first;
    public GameObject titleScreenCanvas;
    #endregion

    private void Start()
    {
        settings.GetComponentInChildren<Slider>().value = GameObject.Find("Managers").GetComponent<SoundManager>().MasterVolume;
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
        keyboardCodes.Add("W");
        keyboardCodes.Add("S");
        keyboardCodes.Add("D");
        keyboardCodes.Add("A ");
        if (SceneManager.GetActiveScene().name == "Title")
        {
            main = first;
            canvas = titleScreenCanvas;
            isPaused = true;
        }
        else
        {
            main = pauseMenu;
            playerUI = GetComponent<UI>();
            camControl = PlayerColorController.singleton.GetComponent<PlayerCamControl>();
            playerMovement = PlayerColorController.singleton.GetComponent<PlayerMovement>();
            canvas = UIPrefab;
        }
        while (numberOfPanels <= (numberOfScenes / 10))
        {
            int temp = 0;
            if ((numberOfScenes % 10) == 0)
            {
                numberOfPanels++;
            }
            while (temp < 10 && index < numberOfScenes)
            {
                BuildLevelsUI(temp);
                temp++;
                index++;
            }
            currentPanel++;
            numberOfPanels++;
        }
        for (int x = 0; x < 4; x++)
        {
            buildControllerRemapUI(x);
        }
        currentPanel = 0;
    }

    private void Update()
    {
        if (InputManager.GetButtonDown(PlayerInput.PlayerButton.Pause, player))
        {
            if (!isPaused && !main.activeInHierarchy)
            {
                Pause();
            }
            else if(panels[currentPanel].activeInHierarchy)
            {
                panels[currentPanel].SetActive(false);
                main.SetActive(true);
                currentPanel = 0;

            }
            else if(settings.activeInHierarchy)
            {
                settings.SetActive(false);
                main.SetActive(true);
            }
            else if(SceneManager.GetActiveScene().name != "Title")
            {
                ResumeGame();                
            }
            EventSystem.current.SetSelectedGameObject(main.transform.GetChild(0).gameObject);
        }
    }
    #region Title Methods
    public void StartGame()
    {
        var latest = GameManager.latestUnlocked;
        //SceneManager.LoadScene( (latest >= GameManager.totalLevels || latest < 1) ? 1 : GameManager.latestUnlocked);
        SceneManager.LoadScene(1); // START ON THE FIRST LEVEL FOR DEMO BUILD
    }
    #endregion

    #region Menu Methods
    public void Pause()
    {
        isPaused = true;
        if (SceneManager.GetActiveScene().name != "Title")
        {
            camControl.enabled = false;
            playerMovement.enabled = false;
            playerUI.enabled = false;
        }
        Time.timeScale = 0;
        HUD.SetActive(false);
        main.SetActive(true);
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

    }
    public void ResumeGame()
    {
        isPaused = false;
        main.SetActive(false);
        Time.timeScale = 1;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        if (SceneManager.GetActiveScene().name != "Title")
        {
            camControl.enabled = true;
            playerMovement.enabled = true;
            playerUI.enabled = true;
        }
        HUD.SetActive(true);
    }
    public void MainMenu()
    {
        SceneManager.LoadScene("Title");
    }
    #endregion

    #region Shared Methods
    public void ExitGame()
    {
        // any extra logic can go here
        // a fade would be nice
        Application.Quit();
    }
    public void LevelSelect()
    {
        main.SetActive(false);
        panels[currentPanel].SetActive(true);
        selectLevel.SetActive(true);
        EventSystem.current.SetSelectedGameObject(panels[currentPanel].transform.GetChild(1).gameObject);
    }
    public void Settings()
    {
        main.SetActive(false);
        settings.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settings.transform.GetChild(0).gameObject);
    }
    public void nextLevels()
    {
        PauseMenu temp = GameObject.Find("Player 1 Camera").GetComponentInChildren<PauseMenu>();
        temp.panels[temp.currentPanel].SetActive(false);
        temp.currentPanel++;
        temp.panels[temp.currentPanel].SetActive(true);
        EventSystem.current.SetSelectedGameObject(temp.panels[temp.currentPanel].transform.GetChild(1).gameObject);

    }
    public void previousLevles()
    {
        PauseMenu temp = GameObject.Find("Player 1 Camera").GetComponentInChildren<PauseMenu>();
        temp.panels[temp.currentPanel].SetActive(false);
        temp.currentPanel--;
        temp.panels[temp.currentPanel].SetActive(true);
        EventSystem.current.SetSelectedGameObject(temp.panels[temp.currentPanel].transform.GetChild(1).gameObject);
    }
    public void SetVolume(float passed)
    {
        GameObject.Find("Audio Source").GetComponent<AudioSource>().volume = passed;
    }
    public void BuildLevelsUI(int passed)
    {
        float xPosition = 160;
        float yPosition = -90;
        float canvasWidth = canvas.GetComponent<RectTransform>().rect.width;
        float canvasHeight = canvas.GetComponent<RectTransform>().rect.width;

        if (passed == 0)
        {
            GameObject panel = Instantiate(selectLevel, Vector2.zero, Quaternion.identity);
            panel.transform.parent = canvas.transform;
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
            if (index >= 10)
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
            button.GetComponent<RectTransform>().localPosition = new Vector2(-xPosition, 180 + (yPosition * passed));
        }
        else
        {
            button.GetComponent<RectTransform>().localPosition = new Vector2(xPosition, 180 + (yPosition * (passed - 5)));
        }
        button.GetComponent<RectTransform>().localScale = Vector3.one;
        button.GetComponent<ButtonProperties>().SetScene(scene, this);

        string name = scene.Substring(scene.LastIndexOf('/') + 1);
        name = name.Substring(0, name.Length - 6);
        button.GetComponentInChildren<Text>().text = name;

    }
    public void buildControllerRemapUI(int passed)
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
        xbox.interactable = false; // remove once the method is fixed in the XboxRemap Class
        string xb = xbox.GetComponent<XboxRemap>().keyName;
        xbox.GetComponentInChildren<Text>().text = xb;
        xboxCodes.Add(xb);

    }
    #endregion

}
