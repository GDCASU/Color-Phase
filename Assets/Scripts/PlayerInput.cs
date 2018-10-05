using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour, IPlayerInput
{
	/// <summary>
	/// This is the central dictionary for player input mappings.
	/// </summary>
	public static IDictionary<PlayerAction, string> actionStrings = new Dictionary<PlayerAction, string>
	{
		{PlayerAction.Forward, "Forward"},
		{PlayerAction.Back, "Back"},
		{PlayerAction.Left, "Left"},
		{PlayerAction.Right, "Right"},
		{PlayerAction.Jump, "Jump"}
	};
	
	private IInputPlayer player;

	private void Start()
	{
		if (player == null) player = GetComponent<IInputPlayer>();
	}

	public bool GetButton(PlayerAction action)
	{
		return InputManager.GetButton(actionStrings[action], player);
	}

	public float GetAxis(PlayerAction action)
	{
		return InputManager.GetAxis(actionStrings[action], player);
	}
}
