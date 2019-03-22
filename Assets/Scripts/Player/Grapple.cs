using UnityEngine;
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

    // Grapple hook states
    private float ropeLength;
    private bool isGrappled;
    private bool canGrapple;
    private bool swinging;

    // Object to use in calcualtions
    private Collider col;
    private Rigidbody rb;
    private ColorState state;
    private Transform hookAnchor;
    private Transform grappleAnchor;
    private LineRenderer line;
    private RaycastHit hit;
    
    private GameObject target;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
        line = gameObject.AddComponent<LineRenderer>();
        line.enabled = false;
        hookAnchor = new GameObject().transform;
        grappleAnchor = new GameObject().transform;
        state = GetComponent<ColorState>();
    }

    void Update()
    {
        if (InputManager.GetButtonDown(PlayerButton.Grapple))
        {
            if (target != null)
            {
                enableGrapple();
            }
        }

        // Handles when grapple is at the object it collided with and does actions based on color
        if (isGrappled)
        {
            GameColor color = col.GetComponent<ColorState>().currentColor;
            swinging = color == GameColor.Yellow;
            switch (color)
            {
                case GameColor.Red:
                    GrapplePullObject();
                    break;
                case GameColor.Green: 
                    GrapplePushObject();
                    break;
                case GameColor.Blue:
                    GrapplePullPlayer();
                    break;
                case GameColor.Yellow:
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

        if (InputManager.GetButtonUp(PlayerButton.Grapple))
        {
            disableGrapple();
        }
    }
    private bool OnScreen(Vector3 worldPos){
        var vP = Camera.main.WorldToViewportPoint(worldPos);
        return vP.x>0 && vP.x<1 && vP.y>0 &&vP.y <1;
    }
    public void LateUpdate () {
        var t = GrappleTarget.targets.Where(x=> (x.neutral==true || x.targetColor==state.currentColor) 
                                            && Vector3.Distance(x.transform.position,transform.position) <= hookRange 
                                            && Vector3.Dot(x.transform.position - Camera.main.transform.position, Camera.main.transform.forward) >= 0
                                            && OnScreen(x.transform.position))
            .OrderBy (p => Vector3.Distance(p.transform.position,transform.position)*Vector2.Distance(Camera.main.WorldToScreenPoint(p.transform.position), Vector2.zero))
            .FirstOrDefault();
            

        if(t != null) {
            RaycastHit r;
            if(Physics.Raycast(transform.position, t.transform.position - transform.position, out r, hookRange)) {
                if(r.transform == t.transform) hit = r;
                target = t.gameObject;
            }
        }
        else {
            target = null;
        }

        if(target != null) {
            reticle.SetActive(true);
            reticle.transform.position = Camera.main.WorldToScreenPoint( target.transform.position );
        }
        else 
            reticle.SetActive(false);

    }

    void FixedUpdate()
    {
        // Only let player add force if they are below a certain point in relation to the hook anchor point
        if (swinging && transform.position.y < hookAnchor.position.y - 10)
        {
            // Get orthogonal vector to player
            Vector3 player = transform.forward;
            Vector3 rope = hookAnchor.position - transform.position;
            Vector3.OrthoNormalize(ref rope, ref player);

            float x = InputManager.GetAxis(PlayerAxis.MoveHorizontal);
            float z = InputManager.GetAxis(PlayerAxis.MoveVertical);

            rb.AddForce(transform.right * x * swingStrafeSpeed);
            rb.AddForce(player * z * swingSpeed);
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
        {
            hit.rigidbody.AddForce(Camera.main.transform.forward * pushObjectSpeed);
            disableGrapple();
        }
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