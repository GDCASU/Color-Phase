using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenSceneOnTriggerEnter : MonoBehaviour
{

    public string sceneName;

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneName); // https://docs.unity3d.com/ScriptReference/SceneManagement.SceneManager.LoadScene.html
        }
    }
}
