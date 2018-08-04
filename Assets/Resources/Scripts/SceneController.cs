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
	public AsyncOperation GoToScene(string targetSceneName)
	{
		return SceneManager.LoadSceneAsync(targetSceneName, LoadSceneMode.Single);
	}
	
	/// <summary>
	/// Loads current scene
	/// </summary>
	public AsyncOperation RestartScene()
	{
		return GoToScene(currentScene);
	}

	/// <summary>
	/// Loads scene with name set in nextScene
	/// </summary>
	public AsyncOperation GoToNextScene()
	{
		return GoToScene(nextScene);
	}

	/// <summary>
	/// Loads scene with name set in nextScene
	/// </summary>
	public AsyncOperation GoToPreviousScene()
	{
		return GoToScene(previousScene);
	}
}
