using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	public string currentScene;
	public string previousScene;
	public string nextScene;

	private void Start () 
	{
		// If we had a separate folder for build scenes, adding scenes to the build
		// settings could be automated
		PrintSceneInfo();

		currentScene = SceneManager.GetActiveScene().name;
		
		// Not sure whether LevelController should be persistent or not
		// If it is persistent, events can be used to make sure that currentScene stays accurate
	}

	private static void PrintSceneInfo()
	{
		print("~~Scene count: " + SceneManager.sceneCount);
		print("~~Scene count in build settings: " + SceneManager.sceneCountInBuildSettings);
		foreach (var scene in EditorBuildSettings.scenes)
		{
			print("~~Scene path: " + scene.path);
		}
	}

	private void Update()
	{
		// for testing
		if (Input.GetKeyDown(KeyCode.LeftAlt))
		{
			GoToScene("Old SampleScene");
		}
	}

	public void RestartScene()
	{
		GoToScene(currentScene);
	}

	// Should this be made a coroutine so that the caller is forced to be aware?
	// You can only load/go to a scene if it is in the build settings
	public static void GoToScene(string targetSceneName)
	{
		SceneManager.LoadSceneAsync(targetSceneName);
	}

	public void GoToNextScene()
	{
		GoToScene(nextScene);
	}

	public void GoToPreviousScene()
	{
		GoToScene(previousScene);
	}
}
