using UnityEngine;

public class SceneChanger : MonoBehaviour
{
	public string targetScene;
	public SceneController sceneController;

	/// <summary>
	/// Goes to targetScene
	/// </summary>
	public AsyncOperation ChangeScene()
	{
		return sceneController.GoToScene(targetScene);
	}
}
