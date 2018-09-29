using UnityEngine;

namespace Scripts
{
	public class SceneChanger : MonoBehaviour
	{
		/// <summary>
		/// Set by level designers.
		/// </summary>
		public string targetScene;

		[SerializeField]
		private SceneController _sceneController;

		private ISceneController _iSceneController;

		/// <summary>
		/// Populates with an existing SceneController if you don't supply one.
		/// </summary>
		public ISceneController sceneController
		{
			private get { return _sceneController == null ? _iSceneController : _sceneController; }
			set
			{
				if (value.GetType() == typeof(SceneController)) _sceneController = (SceneController) value;
				else _iSceneController = value;
			}
		}

		private void Awake()
		{
			EnsureControllerIsPopulated();
		}

		/// <summary>
		/// Goes to targetScene. This could be tied to a collision or something like that.
		/// </summary>
		public AsyncOperation ChangeScene()
		{
			return sceneController.GoToScene(targetScene);
		}

		private void EnsureControllerIsPopulated()
		{
			if (sceneController == null) sceneController = SceneController.GetInstance();
		}
	}
}
