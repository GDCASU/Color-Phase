using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Testing.UnitTests.Play
{
	public class SceneController {
		[Test]
		public void currentSceneName_PopulatesInitially() {
			var go = new GameObject("Test_Object");
			var sc = go.AddComponent<Scripts.SceneController>();
			string currentSceneName = SceneManager.GetActiveScene().name;
			Assert.AreEqual(currentSceneName, sc.currentScene);
		}

		[UnityTest]
		public IEnumerator currentSceneName_PopulatesAfterSceneChange()
		{
			GameObject go = new GameObject("Test_Object");
			Scripts.SceneController sc = go.AddComponent<Scripts.SceneController>();
			AsyncOperation sceneLoaded = SceneManager.LoadSceneAsync("TestScene1", LoadSceneMode.Single);
			while (!sceneLoaded.isDone) yield return null;
			Assert.AreEqual("TestScene1", sc.currentScene);
		}

		[UnityTest]
		public IEnumerator GetInstance_FindsInstance()
		{
			GameObject taggedOnlyObject = new GameObject("SomeObject") {tag = "SceneController"};
			Scripts.SceneController taggedOnlyComponent = taggedOnlyObject.AddComponent<Scripts.SceneController>();
			GameObject notTaggedButNamedObject = new GameObject("SceneController");
			Scripts.SceneController notTaggedButNamedComponent =
				notTaggedButNamedObject.AddComponent<Scripts.SceneController>();
			GameObject neitherTaggedNorNamedObject = new GameObject("SomeOtherObject");
			Scripts.SceneController neitherTaggedNorNamedComponent =
				neitherTaggedNorNamedObject.AddComponent<Scripts.SceneController>();
			yield return null;
	
			Assert.AreEqual(taggedOnlyComponent, Scripts.SceneController.GetInstance());
		
			Object.Destroy(taggedOnlyObject);
			yield return null;

			Assert.AreEqual(notTaggedButNamedComponent, Scripts.SceneController.GetInstance());

			Object.Destroy(notTaggedButNamedObject);
			yield return null;

			Assert.AreEqual(neitherTaggedNorNamedComponent, Scripts.SceneController.GetInstance());
		}
	}
}

