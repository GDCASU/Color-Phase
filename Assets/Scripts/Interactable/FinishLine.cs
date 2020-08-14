using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public string SceneName;
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
            DontDestroyOnLoad(GameObject.Find("Managers"));
            // Mark completed
            GameManager.levelCompletion[SceneManager.GetActiveScene().buildIndex] = true;
            Debug.Log("lets try this now i guess");
            Debug.Log(GameManager.levelCompletion[SceneManager.GetActiveScene().buildIndex]);
            SceneManager.LoadScene(SceneName);
        }
    }
}
