using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Scripts
{
	public class SceneController : MonoBehaviour
	{
		/// <summary>
		/// Updates each time a new scene is loaded
		/// </summary>
		public string currentScene;

		private void Awake()
		{
			DontDestroyOnLoad(gameObject);
		
			// If we had a separate folder for build scenes, adding scenes to the build
			// settings could be automated
			PrintSceneInfo();

			currentScene = SceneManager.GetActiveScene().name;
			SceneManager.sceneLoaded += OnSceneLoaded;
		}

		/// <summary>
		/// Loads scene with specified name. Scenes must be in build settings.
		/// </summary>
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
	
		private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
		{
			currentScene = SceneManager.GetActiveScene().name;
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
	}
}
