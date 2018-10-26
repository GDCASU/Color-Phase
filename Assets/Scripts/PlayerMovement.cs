using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using PlayerInput;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cam;
    public float speed = 2.0f;
    public float jumpStrength = 20f;
    public float minimumY = -30f;
    public float lookSpeed;
    public float angleToSnap;
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

        xAxis += InputManager.GetAxis(PlayerAxis.MoveHorizontal, player);
        zAxis += InputManager.GetAxis(PlayerAxis.MoveVertical, player);

        xAxis *= axisModifier;
        zAxis *= axisModifier;

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
            if (InputManager.GetButtonDown(PlayerButton.Jump, player))
            {
                animator.SetTrigger("Jump");
                rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z);
            }
        }
        else if(!Physics.Raycast(transform.position, Vector3.down, 1f)) jumping = true;

        //uncomment to prevent movement mid-air
        //if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.down), 0.81f))
        {
            Vector3 force = cam.transform.forward.normalized * zAxis * speed + cam.transform.right.normalized * xAxis * speed/2;
            force.y = 0;
            rb.AddForce(force, ForceMode.Acceleration);
        }
        rotatePlayer(xAxis, zAxis);
    }
    
    /// <summary>
    /// This rotates the player according to
    /// the camera position
    /// </summary>
    private void rotatePlayer (float xAxis, float zAxis) {
        // If statement only if input is received
        if (xAxis != 0 || zAxis != 0)
        {
            rb.freezeRotation = false;

            // The y rotation of the player and the camera
            float playerRotation = transform.eulerAngles.y;
            float cameraRotation = cam.transform.eulerAngles.y;

            // Rotate gently until the snap threshold
            if (Mathf.Abs(playerRotation - cameraRotation) > angleToSnap)
                transform.rotation = Quaternion.Lerp(this.transform.rotation, cam.transform.rotation, lookSpeed * Time.deltaTime);
            else transform.rotation = cam.transform.rotation;

            // Makes sure that the x and z rotations are 0
            transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

        }
        else rb.freezeRotation = true;
    }
}
