
﻿using UnityEngine;
using PlayerInput;
using System.Linq;
using System.Collections;

// Author: Nick Arnieri
// Date: 11/2/2018
// Description: Grapple hook that has 4 different characteristics based on the current color.
//              Red: Pulls an object towards you
//              Green: Pushes an object away from you
//              Blue: Brings you towards another object
//              Yellow: Lets you swing from a fixed point
[RequireComponent(typeof(ColorState))]
public class Grapple : MonoBehaviour
{
    [Header("Grapple Attributes")]
    public float hookRange = 50f;
    public float grappleSpeed = 1f;

    [Header("Push/Pull Speed")]
    public float pullPlayerSpeed = 1f;
    public float pullObjectSpeed = 1f;
    public float pushObjectSpeed = 1000f;

    [Header("Swing Speed")]
    public float swingSpeed = 200f;
    public float swingStrafeSpeed = 200f;

    [Header("UI")]
    public GameObject reticle;

    public Transform handTransform;

    // Grapple hook states
    private float ropeLength;
    private bool isGrappled;
    private bool canGrapple;
    private bool swinging;
    private bool resetSwing;
    private bool grounded;

    // Object to use in calcualtions
    private Collider col;
    private Rigidbody rb;
    private ColorState state;
    private Transform hookAnchor;
    private Transform grappleAnchor;
    private LineRenderer line;
    private RaycastHit hit;
    private Vector3 v;
    private Vector3 minSwing;


    private GameObject target;
    private float swingXDirection;
    private Animator animator;
    private PlayerArmController arms;
    void Awake()
    {
        resetSwing = true;
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        hookAnchor = new GameObject().transform;
        grappleAnchor = new GameObject().transform;
        state = GetComponent<ColorState>();
        animator = GetComponent<Animator>();
        arms = GetComponent<PlayerArmController>();

        // set up grapple hook line and effect
        line = gameObject.AddComponent<LineRenderer>();
        line.enabled = false;
        line.materials[0].shader = Shader.Find("Unlit/GrappleEffect");
        line.materials[0].SetColor("_Color",ColorState.RGBColors[state.currentColor]);
        line.startWidth = 0.1f;
        line.endWidth = 0.15f;

        state.onSwap += (GameColor a, GameColor b) => line.materials[0].SetColor("_Color",ColorState.RGBColors[b]);;
    }
    public void Start()
    {
        state.onSwap += switchColors;
    }
    private bool OnScreen(Vector3 worldPos)
    {
        var vP = Camera.main.WorldToViewportPoint(worldPos);
        return vP.x > 0 && vP.x < 1 && vP.y > 0 && vP.y < 1;
    }
    public void LateUpdate()
    {
        RaycastHit r = new RaycastHit();
        if (!isGrappled && !canGrapple)
        {

            // This LINQ query filters for valid targets and then sorts by distance
            var t = GrappleTarget.targets
                .Where(x => (x.neutral == true || (x.PushPull && state.canGrappleBox && x.targetColor == state.currentColor) || x.targetColor == state.currentColor)   // Is it a valid target
                    && Vector3.Distance(x.transform.position, transform.position) <= hookRange                                  // Is the target in range
                    && Vector3.Dot(x.transform.position - Camera.main.transform.position, Camera.main.transform.forward) >= 0   // Is the target in front of the camera (filter out targets behind our view)
                    && OnScreen(x.transform.position))                                                                          // Is the target in screenspace
                .OrderBy(p => Vector2.Distance(Camera.main.WorldToViewportPoint(p.transform.position), new Vector2(0.5f, 0.5f)))// Now order the targets by how close they are to the center of the screen
                .FirstOrDefault();                                                                                              // Take the first of these
            // ADD THIS BACK TO ORDER QUERY LATER FOR SMOOTHING OVER DISTANCE
            //Vector3.Distance(p.transform.position,transform.position)+100*V

            var dir = (t != null) ? Vector3.Normalize(t.transform.position - transform.position) : Vector3.zero;
            if (t != null && Physics.Raycast(transform.position + dir/5+Vector3.up/2, dir, out r, hookRange) && r.transform == t.transform)
            {
                hit = r;
                target = t.gameObject;
            }
            else 
                target = null;
        }
        else {
            if(state.canGrappleBox || (canGrapple && !isGrappled)) {
                var a = (canGrapple && !isGrappled) ? grappleAnchor : hit.transform;
                var f = a.position; f.y = transform.position.y;
                transform.LookAt(f);
                var hand = PlayerArmController.singleton.rHand.transform;
                hand.parent.parent.localPosition = new Vector3(-1.83f, 0.25f, 0.13f);
                hand.parent.parent.localRotation = Quaternion.Euler(137.291f, -37.63199f, 43.62099f);
                hand.parent.localRotation = Quaternion.Euler(19.962f, 28.783f, 31.961f);
                Vector3 grappleDir = (a.position - hand.parent.parent.position).normalized;
                hand.parent.position = hand.parent.parent.position + grappleDir * 0.3f; 
                hand.position = hand.parent.parent.position + grappleDir* 0.5f;
            }
        }

        if (target != null && (isGrappled || canGrapple || (target != null && r.transform == target.transform))
            && Vector3.Dot(target.transform.position - Camera.main.transform.position, Camera.main.transform.forward) >= 0)
        {
            reticle.SetActive(true);
            reticle.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
        }
        else {
            reticle.SetActive(false);
        }
        UpdateAnimations();
    }
    

    void FixedUpdate()
    {
        if (grounded && !Physics.Raycast(GetComponent<Collider>().bounds.center, Vector3.down, GetComponent<Collider>().bounds.extents.y + 0.5f))
        {
            grounded = false;
        }
        else
        {
            grounded = true;
        }
        if (InputManager.GetButtonDown(PlayerButton.Grapple) && !Box.Holding)
        {
            if (target != null)
            {
                enableGrapple();
            }
        }

        // Handles when grapple is at the object it collided with and does actions based on color
        if (isGrappled)
        {
            swinging = state.currentColor == GameColor.Red;
            switch (state.currentColor)
            {
                case GameColor.Yellow:
                    GrapplePullObject();
                    break;
                case GameColor.Green:
                    GrapplePushObject();
                    break;
                case GameColor.Blue:
                    GrapplePullPlayer();
                    break;
                case GameColor.Red:
                    GrappleSwing();
                    break;
            }
        }
        // Handles the initial grapple movement towards the object it collided with
        else if (canGrapple)
        {
            grappleAnchor.position = Vector3.MoveTowards(grappleAnchor.position, hookAnchor.position, grappleSpeed);
            if (grappleAnchor.position == hookAnchor.position) {
                isGrappled = true;
                if(state.currentColor == GameColor.Red) animator.SetTrigger("StartGrappleSwing");
                if(state.currentColor == GameColor.Blue) animator.SetTrigger("StartGrappleHook");
            }
        }

        if (InputManager.GetButtonUp(PlayerButton.Grapple))
        {
            var s = swinging;
            disableGrapple();

            if (Box.Holding == true)
            {
                GetComponentInChildren<Box>().DropBox();
             }   
            if (s)
            {
                resetSwing = true;
                rb.velocity *= 1.5f;
            }
                GetComponent<PlayerMovement>().enabled = true;
                rb.useGravity = true;
        }
        // This method is only called once the rope has shortedned to a length where the player does not touch the ground
        if (swinging && !grounded)
        {
            // Dissables playermovement and set the transform of the palyer to be based from the transform of the camera
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y - 1, rb.velocity.z);
            GetComponentInParent<PlayerMovement>().enabled = false;
            transform.forward = gameObject.transform.parent.GetComponentInChildren<Camera>().transform.forward;
            // Gets the swinging direction by getting the cross product from the rope vector and the players transform
            Vector3 swingZDirection = Vector3.Cross(v, transform.right);
            Vector3 swingXDirection = Vector3.Cross(v, transform.forward);
            // Gets the input to see if the force applied will be forward or backwards
            float z = InputManager.GetAxis(PlayerAxis.MoveVertical);
            float x = InputManager.GetAxis(PlayerAxis.MoveHorizontal);

            // Notes about the swing: The player can only swing upwards until it hits the max height and can only 
            // swing upwards again after resetting by getting to the middle/bottom of the arch, the player will always be
            // allowed to add force if the player is going down and facing towards the other side of the arch

            // Checks if the player is at the bottom of the arch swing to reset the ability to swing upwards 
            if (transform.position.y < (hookAnchor.position.y - (ropeLength * .99)))
            {
                resetSwing = true;
            }
            // Checks if the player is going downwards on the swing
            else if (rb.velocity.y < 0)
            {
                // Checks if the player is looking towards the oposite side of the arch and pressing forwards
                if (InputManager.GetAxis(PlayerAxis.MoveVertical) == 1 && Vector3.Angle(transform.forward, v) > 90f)
                {
                    // Applies a force
                    applySwingForce(swingZDirection, swingXDirection,z,x);
                }
                // Prevents the player from swinging upward if its velocity is downwards 
                else
                {
                    resetSwing = false;
                }
            }
            // Checks if the player is within the allowed swinging range of the arch
            if ((transform.position.y < (hookAnchor.position.y - (ropeLength * .15))))
            {
                if (resetSwing)
                {
                    // Applies a force
                    applySwingForce(swingZDirection, swingXDirection, z, x);
                }
            }
            // Blocks the ability to swing upwards again until the player resets
            else
            {
                resetSwing = false;
            }
        }
        else
        {
            GetComponentInParent<PlayerMovement>().enabled = true;
        }
    }

    private void enableGrapple()
    {
        hookAnchor.position = hit.point;
        grappleAnchor.position = transform.position;
        ropeLength = hit.distance;
        line.enabled = true;
        canGrapple = true;
        isGrappled = false;
    }

    private void disableGrapple()
    {
        line.enabled = false;
        isGrappled = false;
        canGrapple = false;
        swinging = false;
    }

    // Pull the object the grapple collided with towards the player
    private void GrapplePullObject()
    {
        if (hit.rigidbody)
        {
            hookAnchor.position = Vector3.MoveTowards(hookAnchor.position, transform.position, pullObjectSpeed);
            hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, pullObjectSpeed);
            if (Vector3.Distance(hit.transform.position, transform.position) <= 2f)
            {
                if (target.tag == "Box")
                {
                    target.GetComponent<Box>().GrabBox();
                }
                disableGrapple();
                
                return;
            }
        }
    }

    // Pull the player towards the object the grapple collided with
    private void GrapplePullPlayer()
    {
        GetComponent<PlayerMovement>().enabled = false;
        rb.useGravity = false;
        transform.position = Vector3.MoveTowards(transform.position, hookAnchor.position, pullPlayerSpeed);

        if (Vector3.Distance(hit.transform.position, transform.position) <= 2f)
        {
            disableGrapple();
            rb.velocity = Vector3.zero;
            rb.useGravity = true;
        }
    }

    // When the grapple collides with an object it pushes it forwards
    private void GrapplePushObject()
    {
        if (hit.rigidbody)
        {
            hit.rigidbody.AddForce(Camera.main.transform.forward * pushObjectSpeed);
            disableGrapple();
        }
    }

    // Lets the user swing around from a fixed point
    private void GrappleSwing()
    {
        minSwing = transform.position;
        v = transform.position - hookAnchor.position;
        float distance = v.magnitude;

        // Draws an imaginary sphere around the player, simulating the rope length.
        // If the player is past the maximum distance of the rope, we move it back in
        // This allows the player to move toward the anchor point, but keeps him from going further out.
        if (distance > ropeLength)
        {
            Vector3 normal = v.normalized;
            v = Vector3.ClampMagnitude(v, ropeLength);
            transform.position = hookAnchor.position + v;
            float x = Vector3.Dot(normal, rb.velocity);
            normal *= x;
            rb.velocity -= normal;
        }

        // Checks the player's distance to the ground and shortens it will player will touch the floor.
        if (Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 1) &&  grounded)
        {
            ropeLength -= .15f;
        }
    }

    public void applySwingForce( Vector3 swingZDirection, Vector3 swingXDirection, float z, float x)
    {
        rb.AddForce(swingZDirection * z * swingSpeed);
        rb.AddForce(swingXDirection * -x * swingStrafeSpeed);
    }
    public void switchColors(GameColor a, GameColor b )
    {
        disableGrapple();
    }

    public void UpdateAnimations() {
        animator.SetBool("GrappleSwing",swinging);
        animator.SetBool("GrappleHook",isGrappled && state.currentColor == GameColor.Blue);

        if(swinging || (isGrappled && state.currentColor == GameColor.Blue)){
            // Force direction
            transform.LookAt(target.transform);
            transform.Rotate(new Vector3 (1, 0, 0), 45,Space.Self);
        }

        // Line render in late update makes it smoother
        if(isGrappled){
            line.SetPosition(0, handTransform.position);
            line.SetPosition(1, hookAnchor.transform.position);
        }
        else if (canGrapple)
        {
            line.SetPosition(0, handTransform.position);
            line.SetPosition(1, grappleAnchor.transform.position);
        }
    }
}

