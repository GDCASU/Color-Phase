using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
	public GameObject cam;
	public IInputPlayer player;
	public float speed = 2.0F;
	
	private Rigidbody rb;
	private IDictionary<PlayerAction, string> actionStrings;
	private readonly float axisModifier = Mathf.Sqrt(2) / 2;
	private Vector3 ledgeMemory;
	private bool jumping;

	private void Start()
	{
		rb = GetComponent<Rigidbody>();
		actionStrings.Add(PlayerAction.Forward, "Forward");
		actionStrings.Add(PlayerAction.Back, "Back");
		actionStrings.Add(PlayerAction.Left, "Left");
		actionStrings.Add(PlayerAction.Right, "Right");
	}

	private void Update()
	{
		Move();
	}

	private void Move()
	{
		// Movement Input
		float xAxis = 0;
		float zAxis = 0;

		if (Trying(PlayerAction.Left)) xAxis--;
		if (Trying(PlayerAction.Right)) xAxis++;
		if (Trying(PlayerAction.Forward)) zAxis++;
		if (Trying(PlayerAction.Back)) zAxis--;

		xAxis *= axisModifier;
		zAxis *= axisModifier;

		// Old input style, I'm not really sure what this is but ideally we could get rid of it
		if (Input.GetKey(KeyCode.Q))
		{
			Cursor.visible = true;
			Cursor.lockState = CursorLockMode.None;
		}

		// If the player falls off of the map then set the player on the last ledge
		if (transform.position.y < -30)
		{
			//TODO: Remove health from player(s)
			rb.velocity = new Vector3(0, 1, 0);
			transform.position = ledgeMemory;
		}

		if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 1.05f))
		{
			// TODO: Detect if it is a valid platform (Not a moving object)
			ledgeMemory = transform.position; // Remember the ledge position of the player

			if (Trying(PlayerAction.Jump) && !jumping)
			{
				rb.velocity = new Vector3(rb.velocity.x, 20, rb.velocity.z);
				jumping = true;
			}
		}
		if (!Trying(PlayerAction.Jump))
		{
			jumping = false;
		}

		//uncomment to prevent movement mid-air
		//if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.81f))
		{
			Vector3 push = (cam.transform.forward * zAxis + cam.transform.right * xAxis) * speed * 50;
			rb.AddForce(push, ForceMode.Acceleration);
			transform.LookAt(transform.position + push);
			transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
		}
	}

	private bool Trying(PlayerAction action)
	{
		return InputManager.GetButton(actionStrings[action], player);
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
