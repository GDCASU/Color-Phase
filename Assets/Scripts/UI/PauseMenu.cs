using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using System;
using System.Linq;
using UnityEngine.UI;
using System.Runtime.Serialization;

public class PauseMenu : MonoBehaviour
{
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
    public GameObject controlSettings;
    public GameObject generalSettings;
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

    // Options stuff
    [Serializable]
    public struct OptionsData {
        public bool fullscreen;
        public int resolution_x;
        public int resolution_y;
    }
    public List<AudioSource> sfx = new List<AudioSource>();
    private AudioSource music;
    public static PauseMenu singleton;
    private void Awake() {
        if (singleton == null)
            singleton = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        
        //settings.GetComponentInChildren<Slider>().value = GameObject.Find("Managers").GetComponent<SoundManager>().MusicMasterVolume;
        music = GameObject.Find("Audio Source").GetComponent<AudioSource>();
        panels = new List<GameObject>();
        keyboardCodes = new List<string>();
        xboxCodes = new List<string>();
        currentPanel = 0;
        index = 1;
        numberOfPanels = 0;
        numberOfScenes = UnityEngine.SceneManagement.SceneManager.sceneCountInBuildSettings;
        scenes = new string[numberOfScenes];
        for(int i = 0; i < numberOfScenes; i++) scenes[i]=UnityEngine.SceneManagement.SceneUtility.GetScenePathByBuildIndex(i);

        keyboardCodes.Add("W");
        keyboardCodes.Add("S");
        keyboardCodes.Add("D");
        keyboardCodes.Add("A");
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

        // setup the main setting menu
        var resolutions = Screen.resolutions;

        var resDropdown = generalSettings.transform.Find("ResolutionDropdown").GetComponent<Dropdown>();

        resDropdown.ClearOptions();

        var curRes = Screen.currentResolution;
        var options = resolutions.Select((r, i) => {
            string o = r.width + " x " + r.height;
            if(r.width == curRes.width && r.height == curRes.height) resDropdown.value = i;
            return o;
        }).ToList();

        resDropdown.AddOptions(options);
        resDropdown.RefreshShownValue();

        //
        var qualityDropdown = generalSettings.transform.Find("QualityDropdown");
    }

    private void Update()
    {
        if (InputManager.GetButtonDown(PlayerInput.PlayerButton.Pause))
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
            else if(controlSettings.activeInHierarchy)
            {
                controlSettings.SetActive(false);
                main.SetActive(true);
            }
            else if(generalSettings.activeInHierarchy)
            {
                generalSettings.SetActive(false);
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
        var latest = GameManager.lastLoaded;
        //SceneManager.LoadScene( (latest >= GameManager.totalLevels || latest < 1) ? 1 : GameManager.latestUnlocked);
        SceneManager.LoadScene(latest);
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
        
        //if(player!=null && player.InputMethod!=InputManager.InputMethod.XboxController) Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
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
    // Straight up fucking spagetti i will not fix this ass class 
    public void Back() {
        if(panels[currentPanel].activeInHierarchy)
        {
            panels[currentPanel].SetActive(false);
            main.SetActive(true);
            currentPanel = 0;

        }
        else if(controlSettings.activeInHierarchy)
        {
            controlSettings.SetActive(false);
            main.SetActive(true);
        }
        else if(generalSettings.activeInHierarchy)
        {
            generalSettings.SetActive(false);
            main.SetActive(true);
        }
        EventSystem.current.SetSelectedGameObject(main.transform.GetChild(0).gameObject);
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
    public void ControlSettings()
    {
        main.SetActive(false);
        controlSettings.SetActive(true);
        EventSystem.current.SetSelectedGameObject(controlSettings.transform.GetChild(0).gameObject);
    }

    public void GeneralSettings()
    {
        main.SetActive(false);
        generalSettings.SetActive(true);
        EventSystem.current.SetSelectedGameObject(generalSettings.transform.GetChild(0).gameObject);
    }

    public void SwitchControllerType(Dropdown change) {
        InputManager.inputMode = (InputManager.InputMode) change.value;
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
    public void SetMusicVolume(float passed)
    {
        music.volume = passed;
    }
    public void SetEffectsVolume(float passed)
    {
        for(int i = 0; i < sfx.Count(); i++) {
            sfx[i].volume = passed;
        }
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
            panel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(Back);
            panels.Add(panel);
            if ((numberOfScenes - index) > 10)
            {
                Button temp = Instantiate(rightArrowPrefab, Vector2.zero, Quaternion.identity);
                temp.transform.SetParent(panels[currentPanel].transform);
                temp.GetComponent<RectTransform>().localPosition = new Vector2(360, 0);
                temp.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 180);
                temp.transform.localScale = Vector3.one * 2;
            }
            if (index >= 10)
            {
                Button temp = Instantiate(leftArrowPrefab, Vector2.zero, Quaternion.identity);
                temp.transform.SetParent(panels[currentPanel].transform);
                temp.GetComponent<RectTransform>().localPosition = new Vector2(-360, 0);
                temp.GetComponent<RectTransform>().localRotation = Quaternion.Euler(0, 0, 0);
                temp.transform.localScale = Vector3.one * 2;
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

        string name = scene.Substring(scene.LastIndexOf('/') + 3);
        name = name.Substring(0, name.Length - 6);
        button.GetComponentInChildren<Text>().text = name;
        // Has the level been completed
        button.transform.GetChild(1).gameObject.SetActive(GameManager.levelCompletion[index-1]);
    }
    public void buildControllerRemapUI(int passed)
    {
        float yPosition = -60;

        Button keyboard = Instantiate(keyboardPrefab, Vector2.zero, Quaternion.identity);
        keyboard.transform.parent = controlSettings.transform;
        keyboard.GetComponent<RectTransform>().localPosition = new Vector2(-70, 20 + (yPosition * passed));
        keyboard.GetComponent<RectTransform>().localScale = Vector3.one * 0.729927f;
        keyboard.GetComponent<KeyboardRemap>().InitiateButton(passed);
        string key = keyboard.GetComponent<KeyboardRemap>().keyName;
        keyboard.GetComponentInChildren<Text>().text = key;
        keyboardCodes.Add(key);


        Button xbox = Instantiate(xboxPrefab, Vector2.zero, Quaternion.identity);
        xbox.transform.parent = controlSettings.transform;
        xbox.GetComponent<RectTransform>().localPosition = new Vector2(220, 20 + (yPosition * passed));
        xbox.GetComponent<RectTransform>().localScale = Vector3.one * 0.729927f;
        xbox.GetComponent<XboxRemap>().InitiateButton(passed);
        string xb = xbox.GetComponent<XboxRemap>().keyName;
        xbox.GetComponentInChildren<Text>().text = xb;
        xboxCodes.Add(xb);

    }

    public void SetQuality(int value) {
        QualitySettings.SetQualityLevel(value);
    }

    public void SetFullScreen(bool f) {
        Screen.fullScreen = f;
    }

    public void SetResolution(int i) {
        var res = Screen.resolutions[i];
        Screen.SetResolution(res.width, res.height, Screen.fullScreen);
    }

    #endregion

}
