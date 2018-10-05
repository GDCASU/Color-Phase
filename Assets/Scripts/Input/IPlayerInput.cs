using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
	bool GetButton(PlayerAction action);
	float GetAxis(PlayerAction action);
}

public enum PlayerAction
{
	Forward,
	Back,
	Left,
	Right,
	Jump
}
