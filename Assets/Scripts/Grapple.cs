using UnityEngine;
using PlayerInput;

// Author: Nick Arnieri
// Date: 11/2/2018
// Description: Grapple hook that has 4 different characteristics based on the current color.
//              Red: Pulls an object towards you
//              Green: Pushes an object away from you
//              Blue: Brings you towards another object
//              Yellow: Lets you swing from a fixed point

public class Grapple : MonoBehaviour
{
    [Header("Grapple Attributes")]
    public float hookRange = 50f;
    public float grappleSpeed = 1f;

    [Header("Push/Pull Speed")]
    public float pullPlayerSpeed = 1f;
    public float pullObjectSpeed = 1f;
    public float pushObjectSpeed = 100f;

    [Header("Swing Speed")]
    public float swingSpeed = 200f;
    public float swingStrafeSpeed = 200f;

    // Grapple hook states
    private float ropeLength;
    private bool isGrappled;
    private bool canGrapple;
    private bool swinging;

    // Object to use in calcualtions
    private Collider col;
    private Rigidbody rb;
    private Transform hookAnchor;
    private Transform grappleAnchor;
    private LineRenderer line;
    private RaycastHit hit;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        line = gameObject.AddComponent<LineRenderer>();
        line.enabled = false;
        hookAnchor = new GameObject().transform;
        grappleAnchor = new GameObject().transform;
    }

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(gameObject.transform.position, Camera.main.transform.forward, out hit, hookRange, 9))
            {
                if (hit.collider)
                {
                    hookAnchor.position = hit.point;
                    grappleAnchor.position = transform.position;
                    ropeLength = hit.distance;
                    line.enabled = true;
                    canGrapple = true;
                    isGrappled = false;
                }
            }
        }

        // Handles when grapple is at the object it collided with and does actions based on color
        if (isGrappled)
        {
            int color = col.GetComponent<ColorSwap>().currentColor;
            swinging = color == 3;
            switch (color)
            {
                case 0: // Red
                    GrapplePullObject();
                    break;
                case 1: // Green
                    GrapplePushObject();
                    break;
                case 2: // Blue
                    GrapplePullPlayer();
                    break;
                case 3: // Yellow
                    GrappleSwing();
                    break;
            }

            line.SetPosition(0, col.transform.position);
            line.SetPosition(1, hookAnchor.transform.position);
        }
        // Handles the initial grapple movement towards the object it collided with
        else if (canGrapple)
        {
            grappleAnchor.position = Vector3.MoveTowards(grappleAnchor.position, hookAnchor.position, grappleSpeed);
            if (grappleAnchor.position == hookAnchor.position)
                isGrappled = true;

            line.SetPosition(0, col.transform.position);
            line.SetPosition(1, grappleAnchor.transform.position);
        }

        if (Input.GetButtonUp("Fire1"))
        {
            line.enabled = false;
            isGrappled = false;
            canGrapple = false;
            swinging = false;
        }
    }

    void FixedUpdate()
    {
        // Lets player add force when swinging
        if (swinging && transform.position.y < hookAnchor.position.y)
        {
            float x = InputManager.GetAxis(PlayerAxis.MoveHorizontal);
            float z = InputManager.GetAxis(PlayerAxis.MoveVertical);
            rb.AddForce(transform.forward * z * swingSpeed);
            rb.AddForce(transform.right * x * swingStrafeSpeed);
        }
    }

    // Pull the object the grapple collided with towards the player
    private void GrapplePullObject()
    {
        if (hit.rigidbody)
        {
            hookAnchor.position = Vector3.MoveTowards(hookAnchor.position, transform.position, pullObjectSpeed);
            hit.transform.position = Vector3.MoveTowards(hit.transform.position, transform.position, pullObjectSpeed);
        }
    }

    // Pull the player towards the object the grapple collided with
    private void GrapplePullPlayer()
    {
        transform.position = Vector3.MoveTowards(transform.position, hookAnchor.position, pullPlayerSpeed);
    }

    // When the grapple collides with an object it pushes it forwards
    private void GrapplePushObject()
    {
        if (hit.rigidbody)
            hit.rigidbody.AddForce(Camera.main.transform.forward * pushObjectSpeed);
    }

    // Lets the user swing around from a fixed point
    private void GrappleSwing()
    {
        Vector3 v = transform.position - hookAnchor.position;
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
        if (Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 1))
        {
            ropeLength -= .15f;
        }
    }
}