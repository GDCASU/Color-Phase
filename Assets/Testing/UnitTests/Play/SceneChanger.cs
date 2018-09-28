using NSubstitute;
using NUnit.Framework;
using Scripts;
using UnityEngine;

namespace Testing.UnitTests.Play
{
	public class SceneChanger {
		[Test]
		public void ChangeScene_CallsGoToSceneWithTarget()
		{
			GameObject go = new GameObject("TestObject");
			Scripts.SceneChanger sut = go.AddComponent<Scripts.SceneChanger>();
			sut.targetScene = "TestScene";
			ISceneController fakeController = Substitute.For<ISceneController>();
			sut.sceneController = fakeController;
		
			sut.ChangeScene();
		
			fakeController.Received(1).GoToScene("TestScene");
		}
	}
}
