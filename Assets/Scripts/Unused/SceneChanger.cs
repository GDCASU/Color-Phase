using UnityEngine;

public class SceneChanger : MonoBehaviour
{
    // targetScene is meant to be set by level designers
    public string targetScene;
    // sceneController will find an existing SceneController if you don't supply one
    public SceneController sceneController;

    private void Awake()
    {
        TryToPopulateController();
    }

    /// <summary>
    /// Goes to targetScene
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
            GameObject obj = GameObject.FindGameObjectWithTag("SceneController");
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
