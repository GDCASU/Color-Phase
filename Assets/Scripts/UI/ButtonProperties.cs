using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonProperties : MonoBehaviour
{
    
    public string scene;
    public bool available;
    private PauseMenu pauseManager;
    // Use this for initialization
    public void SetScene(string passed, PauseMenu pause = null)
    {
        pauseManager = pause;
        scene = passed;
    }
    public void MakeAvailable()
    {
        available = true;
    }
    public void SwitchScene()
    {
        if(pauseManager != null) pauseManager.ResumeGame();
        SceneManager.LoadScene(scene);
    }


}
