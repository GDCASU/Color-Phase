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
    private static readonly float axisModifier = Mathf.Sqrt(2) / 2;

    #region Jump Parm
    private bool grounded = true;
    private bool jumpHeld = false;

    [Header("Jump Info")]
    public float hangTime = 1f;
    public float fallSpeedCap = 10;
    public float fallCoefficent = 1;
    public float jumpStrength = 20f;
    public float jumpControl = 1;
    public float yellowJumpHeightPercent = 75;
    public float yellowMassMultiplier = 2;

    #endregion

    #region Move Param
    [Header("Move Info")]
    public float moveSpeedCap = 10;
    public float runSpeed = 2.0f;
    public float frictionCoefficient = 1.2f;
    private Vector3 force;
    
    #endregion
    private void Start()
    {
        player = GetComponent<IInputPlayer>();
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();
        GetComponent<ColorState>().onSwap += YellowProperties;
    }

    private void FixedUpdate()
    {
        Move();
        Animations();

        // At the end of each frame we set grounded to false so that
        // OnCollisionStay needs to verify that we are still grounded
        // Obviously it would be better to use OnCollisionExit 
        // but we can't check the normal
        if (grounded && !Physics.Raycast(playerCollider.bounds.center, Vector3.down, playerCollider.bounds.extents.y + 0.5f)) grounded = false;
    }
    
    private void Animations() 
    {
        animator.SetFloat("Speed", 1 + Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) / moveSpeedCap );
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Walking", xAxis != 0 || zAxis != 0);
    }

    void OnCollisionEnter(Collision collision) 
    {
        // TODO: Detect if it is a valid platform (Not a moving object)
        // Note: This can be accomplished by checking collision.other

        // Check if grounded and handle some other behavior that happens we we ground
        if(!jumpHeld && Vector3.Dot(collision.contacts[0].normal, Vector3.up ) > 0 ) {
            grounded = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if( Vector3.Dot(collision.contacts[0].normal, Vector3.up ) > 0 ) {
            jumpHeld = false;
            grounded = true;
        } 
    }

    float xAxisOld = 0;
    float zAxisOld = 0;
    float xAxis = 0;
    float zAxis = 0;

    /// <summary>
    /// This controls the basic aspects of the players ground and jump movement
    /// </summary>
    private void Move()
    {
        // Movement Input
        xAxis = 0;
        zAxis = 0;

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
        if (grounded) {
            // Update the last on ledge position of the player
            ledgeMemory = transform.position; 
            // Handle a jump input
            if (InputManager.GetButtonDown(PlayerButton.Jump, player))
            {
                animator.SetTrigger("Jump");
                rb.velocity = new Vector3(rb.velocity.x, jumpStrength, rb.velocity.z);
                jumpHeld = true;
            }
        }
        else {
            // Check if the player is still holding jump from the button and from the hang time
            jumpHeld = !( (!InputManager.GetButton(PlayerButton.Jump, player) || rb.velocity.y < -hangTime) );
            // Only use the fall coefficent if we're less then the max fall speed 
            float ySpeed = rb.velocity.y  - fallCoefficent;
            if (!jumpHeld && ySpeed > -fallSpeedCap) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - fallCoefficent, rb.velocity.z);
        }
    # endregion

        // Calculate force from input, angle, and speed
        force = cam.transform.forward.normalized * zAxis * runSpeed + cam.transform.right.normalized * xAxis * runSpeed;
        force.y = 0;

        // Apply ground friction
        rb.velocity  /= ((grounded) ? frictionCoefficient : 1);
 
        // check if we are going faster then the cap, if not we don't add our foce (other things can still push the player faster)
        if(Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) < moveSpeedCap) {
            // While in the air our force is reduced to give the player less control and preserve momentum in the jump
            rb.AddForce( (grounded) ? force * frictionCoefficient : (force * jumpControl)/frictionCoefficient, ForceMode.Impulse );
        }
        // If we're off the ground rotate to our jump direction
        rotatePlayer( (grounded) ? xAxis : xAxisOld, (grounded) ? zAxis : zAxisOld);
        
        // Store the previous force for jump momentum 
        if(grounded) {
            xAxisOld = xAxis;
            zAxisOld = zAxis;
        }
    }

    //adds properties of yellow when you swap to yellow and returns to original values when you swap to a different color
    private void YellowProperties(GameColor prev, GameColor next)
    {
        if (next != GameColor.Yellow)
        {
            jumpStrength = 20f;
            rb.mass = 10;
        }
        else
        {
            jumpStrength = jumpStrength * (yellowJumpHeightPercent/100);
            rb.mass = rb.mass * yellowMassMultiplier;
        }
    }
    
    /// <summary>
    /// This rotates the player according to
    /// the camera position and player input
    /// </summary>
    private void rotatePlayer (float xAxis, float zAxis) {
        // If statement only if input is received and the player is on the ground
        if ((xAxis != 0 || zAxis != 0))
        {
            rb.freezeRotation = false;

            // The y rotation of the player and the camera
            float playerRotation = transform.eulerAngles.y;
            
            // Find the rotation for our player input
            Vector2 camForward = new Vector2 (cam.transform.forward.x, cam.transform.forward.z);
            float inputRotation = Vector2.SignedAngle(camForward, new Vector2(-xAxis, zAxis));

            Quaternion inputLook = Quaternion.AngleAxis(inputRotation, Vector3.up);

            // Rotate gently until the snap threshold
            if (Mathf.Abs(playerRotation - inputRotation) > angleToSnap)
                transform.rotation = Quaternion.Lerp(this.transform.rotation, inputLook, lookSpeed * Time.deltaTime);
            else 
                transform.rotation = inputLook;
        }
        rb.freezeRotation = true;
        
        // Makes sure that the x and z rotations are 0
        transform.rotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
    }
}
