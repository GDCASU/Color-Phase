using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Testing.UnitTests.Play
{
	public class SceneController {
		[Test]
		public void GetsSceneNameInitially() {
			var go = new GameObject("Test_Object");
			var sc = go.AddComponent<Scripts.SceneController>();
			string currentSceneName = SceneManager.GetActiveScene().name;
			Assert.AreEqual(currentSceneName, sc.currentScene);
		}

		[UnityTest]
		public IEnumerator GetsSceneNameAfterSceneChange()
		{
			var go = new GameObject("Test_Object");
			var sc = go.AddComponent<Scripts.SceneController>();
			var sceneLoaded = SceneManager.LoadSceneAsync("TestScene1", LoadSceneMode.Single);
			while (!sceneLoaded.isDone) yield return null;
			Assert.AreEqual("TestScene1", sc.currentScene);
		}
	}
}

