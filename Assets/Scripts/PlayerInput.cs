using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerInput
{
	private static IDictionary<PlayerAction, string> actionStrings = new Dictionary<PlayerAction, string>
	{
		{PlayerAction.Forward, "Forward"},
		{PlayerAction.Back, "Back"},
		{PlayerAction.Left, "Left"},
		{PlayerAction.Right, "Right"},
		{PlayerAction.Jump, "Jump"}
	};

	public static bool GetButton(PlayerAction action, IInputPlayer player)
	{
		return InputManager.GetButton(actionStrings[action], player);
	}

	public static float GetAxis(PlayerAction action, IInputPlayer player)
	{
		return InputManager.GetAxis(actionStrings[action], player);
	}
}

public enum PlayerAction
{
	Forward,
	Back,
	Left,
	Right,
	Jump
}
