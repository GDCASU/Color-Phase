using UnityEngine;

/* Author:      Zachary Schmalz
 * Version:     1.0.0
 * Date:        September 28, 2018
 */

[RequireComponent(typeof(Debug))]
[RequireComponent(typeof(InputManager))]
[RequireComponent(typeof(SoundManager))]
public class GameManager : MonoBehaviour
{
    private static GameManager singleton;

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

        Debug.GeneralLog("GameManager Awake");
    }
    void Start ()
    {
		
	}

	void Update ()
    {
		
	}
}