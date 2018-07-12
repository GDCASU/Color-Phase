using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
	// currentScene populates in Start, previous and next need to be set manually
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

	/// <summary>
	/// Loads scene with specified name. Scene must be in build settings.
	/// </summary>
	/// <param name="targetSceneName"></param>
	public static void GoToScene(string targetSceneName)
	{
		// Should this funtion be made a coroutine so that the caller is forced to be aware of the asynchronicity?
		SceneManager.LoadSceneAsync(targetSceneName);
	}
	
	/// <summary>
	/// Loads current scene
	/// </summary>
	public void RestartScene()
	{
		GoToScene(currentScene);
	}

	/// <summary>
	/// Loads scene with name set in nextScene
	/// </summary>
	public void GoToNextScene()
	{
		GoToScene(nextScene);
	}

	/// <summary>
	/// Loads scene with name set in nextScene
	/// </summary>
	public void GoToPreviousScene()
	{
		GoToScene(previousScene);
	}
}
