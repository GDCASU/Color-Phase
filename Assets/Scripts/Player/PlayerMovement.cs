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
    private Rigidbody rb;
    private Vector3 ledgeMemory;
    private Animator animator;
    private static readonly float axisModifier = Mathf.Sqrt(2) / 2;

    #region Jump Parm
    public bool grounded = true;
    public bool jumpHeld = false;
    public bool stuck = false;
    public bool detached = false;
    private int hasJumped = 0;
    private int jumps = 1;
    public int jumpsAvailable = 0;
    private bool cooldown = false;
    float cooldownTime = 0.1f;

    [Header("Jump Info")]
    public float hangTime = 1f;
    public float fallSpeedCap = 10;
    public float fallCoefficent = 1;
    public float jumpStrength = 20f;
    public float jumpControl = 1;
    public float slopeSize = 0.05f;
    #endregion

    #region Move Param
    [Header("Move Info")]
    public float moveSpeedCap = 10;
    public float runSpeed = 2.0f;
    public float frictionCoefficient = 1.2f;
    private Vector3 force;

    #endregion

    #region Yellow Param
    [Header("Yellow Info")]
    public float yellowJumpHeightPercent = 75;
    public float yellowMassMultiplier = 2;
    public float yellowFallCapMultiplier = 2;
    public float yellowFallCoefficent = 1.3f;
    public float yellowRunForceMultiplyer = 1.5f;
    #endregion
    private bool inputJump = false;
    private void Start()
    {
        if(GetComponent<ColorState>().currentColor==GameColor.Blue)
        {
            jumps = 2;
        }
        GetComponent<ColorState>().onSwap += HandleColors;
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider>();
        GetComponent<ColorState>().onSwap += YellowProperties;
    }

    private void Update () {
        inputJump |= InputManager.GetButtonDown(PlayerButton.Jump);
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
        animator.SetFloat("Speed", 1 + Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) / moveSpeedCap);
        animator.SetBool("Grounded", grounded);
        animator.SetBool("Walking", xAxis != 0 || zAxis != 0);
    }

    void OnCollisionEnter(Collision collision) 
    {
        // TODO: Detect if it is a valid platform (Not a moving object)
        // Note: This can be accomplished by checking collision.other

        // Check if grounded and handle some other behavior that happens we we ground
        if (!jumpHeld && Vector3.Dot(collision.contacts[0].normal, Vector3.up) > slopeSize)
        {
            grounded = true;
            resetJumpInfo();
        }
        if (!Box.Holding) Stick(collision);

        if(Vector3.Dot(collision.contacts[0].normal, Vector3.up) > 0.9f) jumpHeld = false;
    }

    void OnCollisionStay(Collision collision)
    {
        if (Vector3.Dot(collision.contacts[0].normal, Vector3.up) > slopeSize)
        {
            grounded = true;
            if(rb.velocity.y <= 0) {
                resetJumpInfo();
                // Update the last on ledge position of the player
                if(collision.transform.GetComponent<Platform>()==null && collision.transform.tag != "NoRespawn") ledgeMemory = transform.position;
            }
        }
        if(stuck) checkDetatch(collision);
    }

    void OnCollisionExit(Collision collision) {
        if (!detached && collision.gameObject.tag=="StickableWall")
            detach();
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

        xAxis += InputManager.GetAxis(PlayerAxis.MoveHorizontal);
        zAxis += InputManager.GetAxis(PlayerAxis.MoveVertical);

        // If the player falls off of the map then set the player on the last ledge
        if (transform.position.y < minimumY)
        {
            var f = ledgeMemory - transform.position; f.y = 0;
            f.Normalize(); f.y = 20;
            rb.velocity = f;
            transform.position = ledgeMemory;
            resetJumpInfo();
        }
        if (grounded)
        {
            setGroundInfo();
        }


        #region Jump  
        // Handle a jump input
        if (inputJump && jumpsAvailable > 0 && hasJumped < 2 && !cooldown)
        {
            cooldown = true;
            StartCoroutine(endCooldown());
            jumpsAvailable--;
            animator.SetTrigger("Jump");
            rb.velocity = new Vector3(rb.velocity.x, jumpStrength / (grounded ? 1 : 1.1f), rb.velocity.z);
            rb.velocity /=  frictionCoefficient;
            jumpHeld = true;
            hasJumped++;
            grounded = false;

            setGroundInfo();
        }
        else if (!grounded)
        {
            if (!InputManager.GetButton(PlayerButton.Jump) || rb.velocity.y < -hangTime)
                jumpHeld = false;

            if (!jumpHeld) rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - fallCoefficent, rb.velocity.z);
        }

        //uncomment to prevent movement mid-air
        //if (grounded)
        {
            force = cam.transform.forward.normalized * zAxis * runSpeed + cam.transform.right.normalized * xAxis * runSpeed;
            force.y = 0;
        }
        #endregion

        // Calculate force from input, angle, and speed
        force = getDirFromInput(xAxis, zAxis);
        force *= runSpeed;
        force *= Mathf.Clamp(new Vector2(xAxis, zAxis).magnitude,0,1);
        force.y = 0;

        // Apply ground friction
        rb.velocity /= ((grounded&&!jumpHeld) ? frictionCoefficient : 1);

        // check if we are going faster then the cap, if not we don't add our foce (other things can still push the player faster)
        if (Mathf.Sqrt(Mathf.Pow(rb.velocity.x, 2) + Mathf.Pow(rb.velocity.z, 2)) < moveSpeedCap)
        {
            // While in the air our force is reduced to give the player less control and preserve momentum in the jump
            rb.AddForce((grounded) ? force * frictionCoefficient : (force * jumpControl) / frictionCoefficient, ForceMode.Impulse);
        }
        // If we're off the ground rotate to our jump direction
        if(!stuck)
        {
            rotatePlayer((grounded) ? xAxis : xAxisOld, (grounded) ? zAxis : zAxisOld);
        }

        // Yellow momentum 
        if(!grounded && GetComponent<ColorState>().currentColor == GameColor.Yellow) {
            var temp = rb.velocity;
            rb.velocity = rb.velocity /= 1.025f;
            rb.velocity = new Vector3(rb.velocity.x, temp.y, rb.velocity.z);
        }
        
        // reset jump cache
        inputJump = false;
    }

    private Vector3 getDirFromInput(float xAxis, float zAxis)
    {
        var inp = new Vector2(xAxis, zAxis);
        var direction = inp.normalized;
        var forward = cam.transform.forward; forward.y = 0;
        var right = cam.transform.right; right.y = 0;
        return forward.normalized * direction.y + right.normalized * direction.x;
    }

    //adds properties of yellow when you swap to yellow and returns to original values when you swap to a different color
    private void YellowProperties(GameColor prev, GameColor next)
    {
        if (next != GameColor.Yellow)
        {
            jumpStrength = 19f;
            rb.mass = 10;
            fallSpeedCap = 20;
            fallCoefficent = 1.05f;
            runSpeed = 18;
        }
        else
        {
            jumpStrength = 19f * (yellowJumpHeightPercent/100);
            rb.mass = 10 * yellowMassMultiplier;
            fallSpeedCap = 20 * yellowFallCapMultiplier;
            fallCoefficent = yellowFallCoefficent;
            runSpeed = 18 * yellowRunForceMultiplyer;
        }
    }
    /// <summary>
    /// This rotates the player according to
    /// the camera position and player input
    /// </summary>
    private void rotatePlayer(float xAxis, float zAxis)
    {
        // If statement only if input is received and the player is on the ground
        if ((xAxis != 0 || zAxis != 0))
        {
            rb.freezeRotation = false;

            // The y rotation of the player and the camera
            float playerRotation = transform.eulerAngles.y;

            // Find the rotation for our player input
            Vector2 camForward = new Vector2(cam.transform.forward.x, cam.transform.forward.z);
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

    /// <summary>
    /// This sets the info on last direction for when the player is in the air
    /// </summary>
    private void setGroundInfo() {
        // Store the previous force for jump momentum 
        xAxisOld = xAxis;
        zAxisOld = zAxis;
    }

    private void resetJumpInfo() {
        jumpsAvailable = jumps;
        stuck = false;
        detached = false;
        hasJumped = 0;
    }
    IEnumerator endCooldown()
    {
        yield return new WaitForSeconds(cooldownTime);
        cooldown = false;
    }

    private void HandleColors(GameColor previous, GameColor next)
    {
        if (previous == GameColor.Red && detached == false)
        {
            if (stuck)
            {
                transform.LookAt(transform.position - transform.forward);
            }
            detach();
        }
        if (previous == GameColor.Blue && jumpsAvailable > 0)
        {

            jumpsAvailable--;
           
        }
        if (next == GameColor.Blue)
        {
            jumpsAvailable++;
            jumps = 2;
            
        }
        else
        {
            jumps = 1;
        }
    }
    private void Stick(Collision collision)
    {
        Vector3 dir = collision.contacts[0].normal;
        
        if (GetComponent<ColorState>().currentColor == GameColor.Red 
            && !grounded && (!stuck || detached)
            && collision.gameObject.tag=="StickableWall"
            && Vector3.Dot(dir, -transform.forward) > .1)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
            stuck = true;
            detached = false;
            animator.SetBool("Detach",false);
            animator.SetTrigger("Stuck");
            
            transform.LookAt(new Vector3(transform.position.x - dir.x, transform.position.y, transform.position.z - dir.z));
        }

    }
    [SerializeField]
    private float wallJumpStrength = 10;
    private float wallJumpHeightStrength = 1.8f;
    private void checkDetatch(Collision collision)
    {
        Vector3 dir = collision.contacts[0].normal /*+ cam.transform.forward*/ ;
        dir +=   getDirFromInput(InputManager.GetAxis(PlayerAxis.MoveHorizontal), InputManager.GetAxis(PlayerAxis.MoveVertical)) / 2f;
        
        if(InputManager.GetButtonDown(PlayerButton.Jump) && !detached )
        {
            rb.constraints = RigidbodyConstraints.FreezeRotation;
            rb.velocity = new Vector3(dir.x * wallJumpStrength, wallJumpHeightStrength * jumpStrength, dir.z * wallJumpStrength);
            animator.SetTrigger("Jump");

            transform.LookAt(new Vector3(transform.position.x + dir.x, transform.position.y, transform.position.z+ dir.z));
        }
    }

    private void detach()
    {
        detached = true;
        rb.constraints = RigidbodyConstraints.FreezeRotation;
        animator.SetBool("Detach",true);
    }

}
