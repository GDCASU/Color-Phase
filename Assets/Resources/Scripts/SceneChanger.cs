using UnityEngine;

public class SceneChanger : MonoBehaviour
{
	// targetScene is meant to be set by level designers
	public string targetScene;
	// sceneController will find an existing SceneController if you don't supply one
	public SceneController sceneController;

	private void Start()
	{
		if (!sceneController)
		{
			sceneController = GameObject.Find("SceneController").GetComponent<SceneController>();	
		}
	}

	/// <summary>
	/// Goes to targetScene
	/// </summary>
	public AsyncOperation ChangeScene()
	{
		return sceneController.GoToScene(targetScene);
	}
}
