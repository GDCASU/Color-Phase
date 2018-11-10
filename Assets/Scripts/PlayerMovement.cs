using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;
using PlayerInput;

public class PlayerMovement : MonoBehaviour
{
    public GameObject cam;
    public float minimumY = -30f;
    public float lookSpeed;
    public float angleToSnap;
    private Collider playerCollider;
    private IInputPlayer player;
    private Rigidbody rb;
    private Vector3 ledgeMemory;
    private Animator animator;
    private PhysicMaterial physicsMaterial;
    private bool grounded;
    private static readonly float axisModifier = Mathf.Sqrt(2) / 2;
    private static readonly float pushModifier = 50f;

    #region Jump Parm
    bool jumpingDown = false;
    [Header("Jump Info")]
    public float hangTime = 1f;
    public float fallSpeedCap = 10;
    public float fallCoefficent = 1;
    public float jumpStrength = 20f;

    #endregion

    #region Move Param
    [Header("Move Info")]
    public float moveSpeedCap = 10;
    public float runSpeed = 2.0f;
    private Vector3 forceOld = Vector3.zero;
    private Vector3 force;
    
    #endregion
    private void Start()
    {
        player = GetComponent<IInputPlayer>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        physicsMaterial = GetComponent<PhysicMaterial>();
        playerCollider = GetComponent<Collider>();
    }

    private void Update()
    {
        Move();
        Animations();
    }
    
    private void Animations() 
    {
        animator.SetFloat("Speed", Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)));
        animator.SetBool("Grounded", grounded);
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

        # region Jump 
        if (Physics.Raycast(transform.position, Vector3.down, playerCollider.bounds.extents.y))
        {
            // TODO: Detect if it is a valid platform (Not a moving object)
            if(!grounded) jumpingDown = false;
            grounded = true;
            ledgeMemory = transform.position; // Remember the ledge position of the player
            if (InputManager.GetButtonDown(PlayerButton.Jump, player))
            {
                animator.SetTrigger("Jump");
                rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z);
                jumpingDown = true;
            }
        }

        else if(!Physics.Raycast(playerCollider.bounds.center, Vector3.down, playerCollider.bounds.extents.y + 1)){
            if (!InputManager.GetButton(PlayerButton.Jump, player) || rb.velocity.y < -hangTime) 
                jumpingDown = false;
            grounded = false; 
            if(!jumpingDown) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - fallCoefficent, rb.velocity.z);
        }
        //uncomment to prevent movement mid-air
        //if (grounded)
        {
            force = cam.transform.forward.normalized * zAxis * runSpeed + cam.transform.right.normalized * xAxis * runSpeed/2;
            force.y = 0;
        }
        # endregion

        rb.AddForce( (grounded) ? force : (force + forceOld) / 2 , ForceMode.Impulse);
        rotatePlayer(xAxis, zAxis);

        rb.velocity = clampVelocities(rb.velocity);

        if(grounded) forceOld = force;
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
        }
        else rb.freezeRotation = true;
        
        // Makes sure that the x and z rotations are 0
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);

    }

    /// <summary>
    /// This keeps the player from accelerating past set limits
    /// We can't use a straight vector clamp as we treat the axes separately 
    /// </summary>
    private Vector3 clampVelocities(Vector3 velocity) {
        Vector3 vOut = velocity;

        Vector3 moveSpeed = new Vector3(velocity.x, 0, velocity.z);

        // Clamp movement speed
        if(moveSpeed.magnitude > moveSpeedCap) 
            vOut = moveSpeed.normalized * moveSpeedCap;

        // Clamp falling speed
        float ySpeed = velocity.y;
        if(!grounded && ySpeed < -fallSpeedCap) 
            ySpeed = -fallSpeedCap;

        vOut = new Vector3(vOut.x, ySpeed, vOut.z);

        return vOut;
    }
}
