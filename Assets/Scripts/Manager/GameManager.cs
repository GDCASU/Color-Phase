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

        Debug.GeneralLog("GameManager Awake");
    }

    void Start() {
        // This really should be somewhere else but I dont care at this point
        if(SceneManager.GetActiveScene().name == "Title") {
            // Start/Continue Button
            var btn = GameObject.Find("LoadGames").GetComponentInChildren<Text>();
            // If we've completed any levels then continue
            btn.text = levelCompletion.Any(level => level)
                ? "Continue"
                : "Start";
        }
    }

    static void updateSaveData (Scene scene, LoadSceneMode sceneMode) {
        int firstIncompleteLevel = 0;
        // if we're on the title screen or we've already completed this level
        if(scene.buildIndex == 0 || levelCompletion[scene.buildIndex]) {
            // check for the first incomplete level if we dont have a last opened
            for(int i = 0; i < totalLevels; i++) {
                if(levelCompletion[i]) {
                    firstIncompleteLevel = i + 1;
                    break;
                }
            }
        } else {
            // write the last scene opened for "continue" option
            firstIncompleteLevel = scene.buildIndex;
        }
        lastLoaded = firstIncompleteLevel;
        // write to save file
        SaveGame();
    }

    static bool SaveGame () {
        bool saved = true;
        FileStream fs = new FileStream(Application.persistentDataPath+"/"+saveName, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        try 
        {
            formatter.Serialize(fs, new SaveData(levelCompletion, lastLoaded, new PauseMenu.OptionsData()));
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
            }
            catch (SerializationException e)
            {
                Debug.Log("Failed to load save. Reason: " + e.Message);
                loaded = false;
            }
            finally
            {
                fs.Close();
            }
        }

        return loaded;
    }
}