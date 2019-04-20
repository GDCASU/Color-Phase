using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(ColorState))]
public class Box : MonoBehaviour
{
    //distance from which the player can pick up the box
    public int pickupDistance = 10;

    //raycastLayerMask goes through player and blocks player from picking up box through wrong barriers
    public LayerMask raycastLayerMask;
    private GameObject player;

    //the hitbox is a separate object from the box so that the box can collide with objects
    public GameObject hitbox;

    //player is holding the box
    bool onHand;
    // static var to check if any boxes are held
    public static bool Holding;
    private ColorState color;

    public void Awake ()
    {
        color = GetComponent< ColorState >();
    }
    // Use this for initialization
    void Start()
    {
        //ignores collisions between the hitbox and actual box
        Physics.IgnoreCollision(hitbox.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        onHand = false;
        player = PlayerColorController.singleton.gameObject;

        //set raycastLayerMask based on box color
        switch (gameObject.layer)
        {
            case (20):
                raycastLayerMask = 1 << 20 | 1 << 26 | 1 << 27 | 1 << 28; break;
            case (21):
                raycastLayerMask = 1 << 21 | 1 << 25 | 1 << 27 | 1 << 28; break;
            case (22):
                raycastLayerMask = 1 << 22 | 1 << 25 | 1 << 26 | 1 << 28; break;
            case (23):
                raycastLayerMask = 1 << 23 | 1 << 25 | 1 << 26 | 1 << 27; break;
            default:
                raycastLayerMask = 1 << 24; break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //drop the box when you stop holding left click or whatever button
        if (InputManager.GetButtonUp(PlayerInput.PlayerButton.PickUp) && onHand)
        {
            DropBox();
        }

        //pick up the box in front of the player when the button is pressed and held
        if (InputManager.GetButtonDown(PlayerInput.PlayerButton.PickUp) && !onHand && !Holding)
        {
            RaycastHit hit;
            Ray ray = new Ray(player.transform.position+Vector3.up/2, player.transform.forward); //Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, pickupDistance, raycastLayerMask))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    GrabBox(); 
                }
            }
        }        
    }
    public void GrabBox()
    {
        Rigidbody box = gameObject.GetComponent<Rigidbody>();

        //set box in front of player while they're holding it
        gameObject.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.1f, player.transform.position.z) + player.transform.forward;
        gameObject.transform.rotation = player.transform.rotation;
        gameObject.transform.parent = player.transform;
        box.useGravity = false;
        box.constraints = RigidbodyConstraints.FreezeAll;
        onHand = true;
        Holding = true;
        //same thing for its hitbox
        hitbox.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + 1.1f, player.transform.position.z) + player.transform.forward;
        hitbox.transform.rotation = player.transform.rotation;
        hitbox.transform.parent = player.transform;
        hitbox.SetActive(true);
        hitbox.layer = gameObject.layer;

        // activate playerArm
        PlayerArmController.singleton.enabled=true;
        PlayerArmController.singleton.rUpperArm.position = new Vector3(-1.27f, 0.4f, 0.24f);
        PlayerArmController.singleton.rUpperArm.rotation = new Vector3(154.739f, -49.37799f, 21.28299f);
        PlayerArmController.singleton.rArm.position = new Vector3(-0.4598275f,-1.380015f,-0.27012f);
        PlayerArmController.singleton.rArm.rotation =  new Vector3(-9.030001f, 33.907f, 38.972f);
        PlayerArmController.singleton.rHand.position = new Vector3(-0.28f, -0.5f, 0.12f);
        PlayerArmController.singleton.rHand.rotation = new Vector3(61.819f, 0.719f, 3.061f);

        PlayerArmController.singleton.lUpperArm.position = new Vector3(-1.04f, -0.5f, 0.01f);
        PlayerArmController.singleton.lUpperArm.rotation = new Vector3(33.077f, 3.048f, 33.993f);
        PlayerArmController.singleton.lArm.position = new Vector3(-0.39f, -0.97f, 0.77f);
        PlayerArmController.singleton.lArm.rotation =  new Vector3(10.502f,21.617f, 67.705f);
        PlayerArmController.singleton.lHand.position = new Vector3(-0.25f, -0.11f, 0.32f);
        PlayerArmController.singleton.lHand.rotation = new Vector3(0f, -20f, 20f);
    }
    public void DropBox()
    {
        Rigidbody box = gameObject.GetComponent<Rigidbody>();

        gameObject.transform.parent = null;
        box.useGravity = true;
        box.constraints = RigidbodyConstraints.None;
        onHand = false;
        Holding = false;
        hitbox.SetActive(false);
        hitbox.transform.parent = gameObject.transform;
        hitbox.layer = 0;

        
        PlayerArmController.singleton.enabled=false;
    }
}
