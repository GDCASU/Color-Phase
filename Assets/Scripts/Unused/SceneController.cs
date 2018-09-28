using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

namespace Scripts
{
	public interface ISceneController
	{
		/// <summary>
		/// Loads scene with specified name. Scenes must be in build settings.
		/// </summary>
		AsyncOperation GoToScene(string targetSceneName);

		/// <summary>
		/// Loads current scene
		/// </summary>
		AsyncOperation RestartScene();
	}

	public class SceneController : MonoBehaviour, ISceneController
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
		/// Tries progressively slower methods to find an existing scene controller
		/// </summary>
		public static ISceneController GetInstance()
		{
			GameObject obj = GameObject.FindGameObjectWithTag("SceneController");
			if (obj != null) return obj.GetComponent<SceneController>();
			obj = GameObject.Find("SceneController");
			return obj == null ? FindObjectOfType<SceneController>() : obj.GetComponent<SceneController>();
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
