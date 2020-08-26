using Unity;
using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine.UI;
using System.Linq;
using System.Collections;

/* Authors:      Zachary Schmalz, Jacob Hann, Christian Gonzalez
 * Version:     1.1.2
 * Date:        August 13, 2020
 * 
 * This manager handles game state, saving, and ensures that the other managers are avalible
 */

[RequireComponent(typeof(Debug))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(SoundManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    const string saveName = "ColorPhase.dat";
    public const int totalLevels = 23; // This needs to be updated with total levels (not scenes) in build 
    public static bool [] levelCompletion = new bool[totalLevels];
    public static int lastLoaded = 1;
    public static UnityEngine.SceneManagement.Scene activeScene { get { return SceneManager.GetActiveScene(); } }
    // Version 3.0
    [Serializable]
    public struct SaveData {
        public bool [] levelCompletion;
        public int lastLoaded;
        [OptionalField(VersionAdded=2)]
        public PauseMenu.OptionsData options;
        public SaveData(bool [] levelCompletion, int lastLoaded, PauseMenu.OptionsData options) {
            this.levelCompletion = levelCompletion;
            this.lastLoaded = lastLoaded;
            this.options = options;
        }
    }

    void Awake()
    {
        if (singleton == null)
            singleton = this;

        else if(singleton != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        // Get the save file
        LoadGame ();

        SceneManager.sceneLoaded += updateSaveData;
        SceneManager.sceneLoaded += fixStupidFuckingPauseMenu;

        Debug.GeneralLog("GameManager Awake");
    }

    static void fixStupidFuckingPauseMenu (Scene scene, LoadSceneMode sceneMode) { 
        Time.timeScale = 1;
        LoadingScene = false;
    }

    void Start() {
        // This really should be somewhere else but I dont care at this point
        if(activeScene.name == "Title") {
            // Start/Continue Button
            var btn = GameObject.Find("LoadGame").GetComponent<Button>();
            // If we've completed any levels then continue
            if(!levelCompletion.Any(level => level)) {
                btn.image.sprite = Resources.LoadAll("Sprites/main menu spread")[4] as Sprite;
            }
        }
    }
    // We want to set last loaded to the current level UNLESS its beaten or is the title screen
    // Otherwise 
    public static void updateLastloaded(Scene scene) {
        int firstIncompleteLevel = 0;
        // if we're on the title screen or we've already completed this level
        if(scene.buildIndex == 0 && levelCompletion[lastLoaded] || scene.buildIndex > 0 && levelCompletion[scene.buildIndex-1]) {
            // check for the first incomplete level if we dont have a last opened
            for(int i = 0; i < totalLevels; i++) {
                if(!levelCompletion[i]) {
                    firstIncompleteLevel = i + 1;
                    break;
                }
            } 
        } else {
            // write the last scene opened for "continue" option
            // if its title screen write the last loaded
            firstIncompleteLevel = scene.buildIndex == 0 ? lastLoaded : scene.buildIndex;
        }
        // god this is just me giving up on thinking
        if(scene.buildIndex == 0) {
            // if we made it to the end 
            lastLoaded = firstIncompleteLevel > 0 ? firstIncompleteLevel : lastLoaded;
        } else {
            lastLoaded = firstIncompleteLevel > 0 ? firstIncompleteLevel : scene.buildIndex;
        }
    }
    static void updateSaveData (Scene scene, LoadSceneMode sceneMode) {
        updateLastloaded(scene);
        // write to save file
        SaveGame();
    }

    public static bool SaveGame () {
        bool saved = true;
        FileStream fs = new FileStream(Application.persistentDataPath+"/"+saveName, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        try 
        {
            options = GetOptionData();
            formatter.Serialize(fs, new SaveData(levelCompletion, lastLoaded, options));
        }
        catch (SerializationException e) 
        {
            Debug.Log("Saving Failed. Reason: " + e.Message);
            saved = false;
        }
        finally 
        {
            fs.Close();
        }
        return saved;
    }

    static bool LoadGame () {
        bool loaded = true;
        if (File.Exists(Application.persistentDataPath + "/" + saveName))
        {
            FileStream fs = File.Open(Application.persistentDataPath + "/" + saveName, FileMode.Open);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                var loadedData = (SaveData)formatter.Deserialize(fs);
                lastLoaded = loadedData.lastLoaded;
                levelCompletion = loadedData.levelCompletion;
                LoadOptionData(loadedData.options);
                options = loadedData.options;
            }
            catch (Exception e)
            {
                options = GetOptionData();
                Debug.Log("Failed to load save. Reason: " + e.Message);
                loaded = false;
            }
            finally
            {
                fs.Close();
            }
        } else {
            options = GetOptionData();
        }

        return loaded;
    }

    public static PauseMenu.OptionsData options;

    public static PauseMenu.OptionsData GetOptionData() {
        var op = new PauseMenu.OptionsData();
        op.fullscreen = Screen.fullScreen; 
        op.musicVolume = PauseMenu.musicVolume;
        op.quality = QualitySettings.GetQualityLevel();
        op.resolution_x = Screen.currentResolution.width;
        op.resolution_y = Screen.currentResolution.height;
        op.sfxVolume = PauseMenu.sfxVolume;
        op.controlType = (int)InputManager.inputMode;

        // Load options with keycodes
        if(InputManager.playerButtons != null && InputManager.playerButtons.Count() == 7) {
            op.playerControls = InputManager.playerButtons;
        } else {
            InputManager.ResetKeycodes();
            op.playerControls = InputManager.playerButtons;
            Debug.Log(InputManager.playerButtons.Count());
        }

        return op;
    }

    public static void LoadOptionData(PauseMenu.OptionsData data) {
        PauseMenu.musicVolume = data.musicVolume;
        PauseMenu.sfxVolume = data.sfxVolume;
        QualitySettings.SetQualityLevel(data.quality);
        Screen.SetResolution(data.resolution_x, data.resolution_y, data.fullscreen);
        InputManager.inputMode = (InputManager.InputMode)data.controlType;
        InputManager.playerButtons = data.playerControls;
    }

    public static bool LoadingScene = false;

    public static GameObject GetLoadingCanvas () {
        GameObject loadingCanvas;

        Transform t = Camera.main.transform.Find("loadingCanvas");

        if(t == null) {
            // create new
            loadingCanvas = Instantiate(Resources.Load("loadingCanvas"), Vector2.zero, Quaternion.identity) as GameObject;
            loadingCanvas.transform.SetParent(Camera.main.transform);
        } else {
            loadingCanvas = t.gameObject;
            loadingCanvas.SetActive(true);
        }

        return loadingCanvas;
    }

    public static IEnumerator FadeIn() {
        // Hook into the UI
        GameObject loadingCanvas = GetLoadingCanvas();

        var background = loadingCanvas.transform.GetChild(0).GetComponent<Image>();
        background.color = new Color(0,0,0,1);
        var text = loadingCanvas.transform.GetChild(1).GetComponent<Text>();

        var t = text.text.Length;
        // fade
        for(float i = 1.0f; i > 0.1f; i -= (Time.deltaTime / (i*i*i*5f))) {
            background.color = new Color(0,0,0, i);
            var c = (2*(i*i)) - 1f; // scale to last half of fade
            if (c >= 0) {
                text.text = text.text.Substring(0, (int)Math.Floor(c * t));
            }
            yield return new WaitForEndOfFrame();
        }
        loadingCanvas.SetActive(false);
    }

    public static IEnumerator LoadScene(string SceneName) {
        if(LoadingScene) yield break;
        Debug.Log("Loading...");
        LoadingScene = true;

        // Hook into the UI
        GameObject loadingCanvas = GetLoadingCanvas();

        var background = loadingCanvas.transform.GetChild(0).GetComponent<Image>();
        background.color = new Color(0,0,0,0);

        var text = loadingCanvas.transform.GetChild(1).GetComponent<Text>();
        text.text = "";

        // this might cause problems 
        if (SceneManager.GetActiveScene().name != "Title")
        {
            PlayerColorController.singleton.GetComponent<PlayerCamControl>().enabled = false;
            PlayerColorController.singleton.GetComponent<PlayerMovement>().enabled = false;
        }
        Time.timeScale = 0;

        // fade 
        for(float i = 0; i < 1.1; i += Time.unscaledDeltaTime*1.5f) {
            background.color = new Color(0,0,0, i);
            yield return null;
        }

        text.text = "Loading... ";

        AsyncOperation loading = SceneManager.LoadSceneAsync(SceneName);

        while(!loading.isDone) {
            float current = Mathf.Clamp01(loading.progress / 0.9f);
            text.text = "Loading... " + current + "%";

            yield return null;
        }
    }
}