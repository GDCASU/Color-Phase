using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class PlayerMovement : MonoBehaviour
{
	public GameObject cam;
	public float speed = 2.0f;
	public float jumpStrength = 20f;
	public float minimumY = -30f;
    private float colliderHeight;
	private IInputPlayer player;
	private Rigidbody rb;
	private Vector3 ledgeMemory;
    private Animator animator;
    private PhysicMaterial physicsMaterial;
	private bool jumping;
	private static readonly float axisModifier = Mathf.Sqrt(2) / 2;
	private static readonly float pushModifier = 50f;

	private void Start()
	{
		player = GetComponent<IInputPlayer>();
		rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        physicsMaterial = GetComponent<PhysicMaterial>();
	}

	private void Update()
	{
		Move();
        Animations();
	}

    private void Animations() 
    {
        animator.SetFloat("Speed", Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)));
        animator.SetBool("Grounded", !jumping);
    }

	private void Move()
	{
		// Movement Input
		float xAxis = 0;
		float zAxis = 0;

        // This needs to use the enum abstraction once its sorted out
		xAxis = InputManager.GetAxis("LeftHorizontal", player);
        zAxis = InputManager.GetAxis("LeftVertical", player);

		// If the player falls off of the map then set the player on the last ledge
		if (transform.position.y < minimumY)
		{
			rb.velocity = new Vector3(0, 1, 0);
			transform.position = ledgeMemory;
		}

		if (Physics.Raycast(transform.position, Vector3.down, 0.5f))
		{
			// TODO: Detect if it is a valid platform (Not a moving object)
            jumping = false;
			ledgeMemory = transform.position; // Remember the ledge position of the player
            if (InputManager.GetButtonDown("A")) rb.AddForce(jumpStrength * Vector3.up, ForceMode.Acceleration);
		}
        else jumping = true;

		//uncomment to prevent movement mid-air
		//if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.81f))
		{
			transform.LookAt(cam.transform.position);
			transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y + 180,0);
            Vector3 force = transform.forward * zAxis * speed + transform.right * xAxis * speed/2;
            rb.AddForce(force, ForceMode.Acceleration);
		}
	}

	private bool Trying(PlayerAction action)
	{
		return PlayerInput.GetButton(action, player);
	}
}
