using UnityEngine;

public class SceneChanger : MonoBehaviour
{
	/// <summary>
	/// Set by level designers.
	/// </summary>
	public string targetScene;
	/// <summary>
	/// Populates with an existing SceneController if you don't supply one.
	/// </summary>
	public SceneController sceneController;

	private void Awake()
	{
		TryToPopulateController();
	}

	/// <summary>
	/// Goes to targetScene. This could be tied to a collision or something like that.
	/// </summary>
	public AsyncOperation ChangeScene()
	{
		return sceneController.GoToScene(targetScene);
	}

	private void TryToPopulateController()
	{
		// Tries progressively slower methods to find sceneController;
		if (sceneController == null)
		{
			var obj = GameObject.FindGameObjectWithTag("SceneController");
			if (obj == null)
			{
				obj = GameObject.Find("SceneController");
				if (obj == null)
				{
					sceneController = FindObjectOfType<SceneController>();
					return;
				}
			}

			sceneController = obj.GetComponent<SceneController>();
		}
	}
}
