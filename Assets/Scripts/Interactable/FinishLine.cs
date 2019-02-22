using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishLine : MonoBehaviour
{
    public string SceneName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag=="Player")
        {
                        DontDestroyOnLoad(GameObject.Find("Managers"));
            SceneManager.LoadScene(SceneName);
        }
    }
}
