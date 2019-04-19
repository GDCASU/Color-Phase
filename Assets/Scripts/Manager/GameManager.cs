using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

/* Author:      Zachary Schmalz & Jacob Hann
 * Version:     1.1.0
 * Date:        April 4, 2019
 */

[RequireComponent(typeof(Debug))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(SoundManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager singleton;
    private static int latestUnlocked;
    const string saveName = "ColorPhase.dat";
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

        SceneManager.sceneLoaded += updateSaveData;

        Debug.GeneralLog("GameManager Awake");
    }
    void Start ()
    {
		// Get the save file
        LoadGame ();
	}
    
    static void updateSaveData (Scene scene, LoadSceneMode sceneMode) {
        if(scene.buildIndex > latestUnlocked) latestUnlocked = scene.buildIndex;
        // write to save file
        SaveGame ();
    }

    static bool SaveGame () {
        bool saved = true;
        FileStream fs = new FileStream(Application.persistentDataPath+saveName, FileMode.Create);
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
        FileStream fs = new FileStream(Application.persistentDataPath+saveName, FileMode.Open);
        try 
        {
            BinaryFormatter formatter = new BinaryFormatter();
            latestUnlocked = (int) formatter.Deserialize(fs);
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
        return loaded;
    }
}