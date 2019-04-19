using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonProperties : MonoBehaviour
{
    public Scene scene;
    public bool available;
	// Use this for initialization
    
    public void SetScene(Scene passed)
    {
        scene = passed;
    }
    public void MakeAvailable()
    {
        available = true;
    }
    public void SwitchScene()
    {
        SceneManager.LoadScene(scene.name);
    }

}
