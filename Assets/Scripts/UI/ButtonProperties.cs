using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonProperties : MonoBehaviour
{
    
    public string scene;
    public bool available;
    // Use this for initialization
    public void SetScene(string passed)
    {
        scene = passed;
    }
    public void MakeAvailable()
    {
        available = true;
    }
    public void SwitchScene()
    {
        SceneManager.LoadScene(scene);
    }


}
