using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : IPlayerInput
{
	private static IDictionary<PlayerAction, string> actionStrings = new Dictionary<PlayerAction, string>
	{
		{PlayerAction.Forward, "Forward"},
		{PlayerAction.Back, "Back"},
		{PlayerAction.Left, "Left"},
		{PlayerAction.Right, "Right"},
		{PlayerAction.Jump, "Jump"}
	};
	
	public static readonly PlayerInput instance = new PlayerInput();

	public bool GetButton(PlayerAction action, IInputPlayer player)
	{
		return InputManager.GetButton(actionStrings[action], player);
	}

	public float GetAxis(PlayerAction action, IInputPlayer player)
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
