using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    //player is holding the box
    public bool onHand;

    //raycastLayerMask goes through player and blocks player from picking up box through wrong barriers
    public LayerMask raycastLayerMask;
    private GameObject player;

    //the hitbox is a separate object from the box so that the box can collide with objects
    public GameObject hitbox;

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
        if (InputManager.GetButtonUp(PlayerInput.PlayerButton.PickUp) && onHand == true)
        {
            Rigidbody box = gameObject.GetComponent<Rigidbody>();

            gameObject.transform.parent = null;
            box.useGravity = true;
            box.constraints = RigidbodyConstraints.None;
            onHand = false;
            hitbox.SetActive(false);
            hitbox.transform.parent = gameObject.transform;
            hitbox.layer = 0;
        }

        //pick up the box in front of the player when the button is pressed and held
        if (InputManager.GetButtonDown(PlayerInput.PlayerButton.PickUp) && onHand == false)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 100, raycastLayerMask))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    Rigidbody box = gameObject.GetComponent<Rigidbody>();

                    //set box in front of player while they're holding it
                    gameObject.transform.position = player.transform.position + player.transform.forward;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
                    gameObject.transform.rotation = player.transform.rotation;
                    gameObject.transform.parent = player.transform;
                    box.useGravity = false;
                    box.constraints = RigidbodyConstraints.FreezeAll;
                    onHand = true;
                    //same thing for its hitbox
                    hitbox.transform.position = player.transform.position + player.transform.forward;
                    hitbox.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
                    hitbox.transform.rotation = player.transform.rotation;
                    hitbox.transform.parent = player.transform;
                    hitbox.SetActive(true);
                    hitbox.layer = gameObject.layer;
                }
            }
        }        
    }
}
