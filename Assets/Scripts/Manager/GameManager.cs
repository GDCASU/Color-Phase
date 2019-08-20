﻿using UnityEngine;
using UnityEngine.SceneManagement;

using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;
using UnityEngine.UI;

/* Author:      Zachary Schmalz & Jacob Hann
 * Version:     1.1.2
 * Date:        April 4, 2019
 * 
 * This manager handles game state, saving, and ensures that the other managers are avalible
 */

[RequireComponent(typeof(Debug))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(SoundManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    public static int latestUnlocked;
    const string saveName = "ColorPhase.dat";
    public const int totalLevels = 12; // This needs to be updated with total levels (not scenes) in build 
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

    static void updateSaveData (Scene scene, LoadSceneMode sceneMode) {
        // ensure that we dont write numbers for the extra scenes (not levels)
        if(scene.buildIndex > latestUnlocked && scene.buildIndex <= totalLevels) latestUnlocked = scene.buildIndex;
        // write to save file
        SaveGame ();
        Debug.Log("Saved\n"+latestUnlocked);
    }

    static bool SaveGame () {
        bool saved = true;
        FileStream fs = new FileStream(Application.persistentDataPath+"/"+saveName, FileMode.Create);
        BinaryFormatter formatter = new BinaryFormatter();
        try 
        {
            formatter.Serialize(fs, latestUnlocked);
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
            GameObject.Find("LoadGames").GetComponentInChildren<Text>().text = "Continue";
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                latestUnlocked = (int)formatter.Deserialize(fs);
                Debug.Log(latestUnlocked);
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
        else
        {
            GameObject.Find("LoadGames").GetComponentInChildren<Text>().text = "Start";
        }
        
        return loaded;
    }
}