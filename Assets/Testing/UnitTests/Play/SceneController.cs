using UnityEngine;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using UnityEngine.SceneManagement;

namespace Testing.UnitTests.Play
{
	public class SceneController {

		[Test]
		public void SceneControllerSimplePasses() {
			// Use the Assert class to test conditions.
			var go = new GameObject("Test_Object");
			var sc = go.AddComponent<Resources.Scripts.SceneController>();
			string currentSceneName = SceneManager.GetActiveScene().name;
			Assert.AreEqual(currentSceneName, sc.currentScene);
		}

		// A UnityTest behaves like a coroutine in PlayMode
		// and allows you to yield null to skip a frame in EditMode
		[UnityTest]
		public IEnumerator SceneControllerWithEnumeratorPasses() {
			// Use the Assert class to test conditions.
			// yield to skip a frame
			yield return null;
		}
	}
}

