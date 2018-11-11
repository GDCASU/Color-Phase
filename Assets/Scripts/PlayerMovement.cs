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
    private static readonly float axisModifier = Mathf.Sqrt(2) / 2;
    private static readonly float pushModifier = 50f;

    #region Jump Parm
    private bool grounded = true;
    private bool jumpHeld = false;

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

    private void FixedUpdate()
    {
        Move();
        Animations();

        // At the end of each frame we set grounded to false so that
        // OnCollisionStay needs to verify that we are still grounded
        // Obviously it would be better to use OnCollisionExit 
        // but we can't check the normal
        grounded = false;
    }
    
    private void Animations() 
    {
        animator.SetFloat("Speed", Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)));
        animator.SetBool("Grounded", grounded);
    }


    void OnCollisionEnter(Collision collision) 
    {
        // TODO: Detect if it is a valid platform (Not a moving object)
        // Note: This can be accomplished by checking collision.other

        // Check if grounded and handle some other behavior that happens we we ground
        if(collision.contacts[0].normal == Vector3.up ) {
            grounded = true;
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if(collision.contacts[0].normal == Vector3.up )  grounded = true;
    }

    float xAxisOld = 0;
    float zAxisOld = 0;

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
            if (!InputManager.GetButton(PlayerButton.Jump, player) || rb.velocity.y < -hangTime) 
                jumpHeld = false;
            if (!jumpHeld) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - fallCoefficent, rb.velocity.z);
        }
        //uncomment to prevent movement mid-air
        //if (grounded)
        {
            force = cam.transform.forward.normalized * zAxis * runSpeed + cam.transform.right.normalized * xAxis * runSpeed;
            force.y = 0;
        }
        # endregion

        // While in the air our force is an average of current input and force when we left the ground
        rb.AddForce( (grounded) ? force : (force + forceOld) / 2 , ForceMode.Impulse);
        // If we're off the ground rotate to our jump direction
        rotatePlayer( (grounded) ? xAxis : xAxisOld,(grounded) ? zAxis : zAxisOld);
        rb.velocity = clampVelocities(rb.velocity);

        // Store the previous force for jump momentum 
        if(grounded) {
            forceOld = force;
            xAxisOld = xAxis;
            zAxisOld = zAxis;
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
            else transform.rotation = inputLook;
        }
        rb.freezeRotation = true;
        
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
