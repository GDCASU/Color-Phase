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
            DontDestroyOnLoad(GameObject.Find("Managers"));
             // Mark completed
            GameManager.levelCompletion[SceneManager.GetActiveScene().buildIndex-1] = true;
            StartCoroutine(GameManager.LoadScene(sceneName));
        }
    }
}
