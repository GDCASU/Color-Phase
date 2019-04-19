using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreenController : MonoBehaviour {

	public static GameObject titleScreenCanvas;

    public void Awake() {
        titleScreenCanvas = this.gameObject;
    }

    public void StartGame () {
        var latest = GameManager.latestUnlocked;
        SceneManager.LoadScene( (latest >= GameManager.totalLevels || latest < 1) ? 1 : GameManager.latestUnlocked);
    }

    public void LevelSelect () {
        var latest = GameManager.latestUnlocked;
        // let the player scrole between scenes between 1 and latest or total
    }

    public void Settings () {

    }

    public void ExitGame () {
        // any extra logic can go here
        // a fade would be nice
        Application.Quit();
    }
}
