using UnityEngine;
using UnityEngine.SceneManagement;

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
	}
    
    static void updateSaveData (Scene scene, LoadSceneMode sceneMode) {
        if(scene.buildIndex > latestUnlocked) latestUnlocked = scene.buildIndex;
        // write to save file
    }
}