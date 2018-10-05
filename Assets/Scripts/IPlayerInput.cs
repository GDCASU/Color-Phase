using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerInput
{
	bool GetButton(PlayerAction action, IInputPlayer player);
	float GetAxis(PlayerAction action, IInputPlayer player);
}
