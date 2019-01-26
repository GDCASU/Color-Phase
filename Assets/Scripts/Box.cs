using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{

    //player is holding the box
    public bool onHand;

    //there should be a LayerMask that ignores the player so Raycast is only aimed at the boxes
    public LayerMask ignorePlayer;
    public GameObject player;

    //the hitbox is a separate object from the box so that the box can collide with objects
    public GameObject hitbox;

    public int colorValue = 0;

    // Use this for initialization
    void Start()
    {
        //ignores collisions between the hitbox and actual box
        Physics.IgnoreCollision(hitbox.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        onHand = false;
    }

    // Update is called once per frame
    void Update()
    {
        //drop the box when you stop holding left click or whatever button
        if (Input.GetMouseButtonUp(0) && onHand == true)
        {
            gameObject.transform.parent = null;
            gameObject.GetComponent<Rigidbody>().useGravity = true;
            gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            onHand = false;
            hitbox.SetActive(false);
            hitbox.transform.parent = gameObject.transform;
        }

        //pick up the box in front of the player when the button is pressed and held
        if (Input.GetMouseButtonDown(0) && onHand == false)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 5, ignorePlayer))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    //set box in front of player while they're holding it
                    gameObject.transform.position = player.transform.position + player.transform.forward;
                    gameObject.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
                    gameObject.transform.rotation = player.transform.rotation;
                    gameObject.transform.parent = player.transform;
                    gameObject.GetComponent<Rigidbody>().useGravity = false;
                    gameObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    onHand = true;
                    //same thing for its hitbox
                    hitbox.transform.position = player.transform.position + player.transform.forward;
                    hitbox.transform.position = new Vector3(gameObject.transform.position.x, 1, gameObject.transform.position.z);
                    hitbox.transform.rotation = player.transform.rotation;
                    hitbox.transform.parent = player.transform;
                    hitbox.SetActive(true);
                }
            }
        }        
    }

    //ignores collision between barriers and boxes of the same color or boxes of no color
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<ColorBarrier>() != null)
        {
            if (collision.gameObject.GetComponent<ColorBarrier>().barrierColor == colorValue || colorValue > 3)
            {
                Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), hitbox.GetComponent<Collider>());
            }
        }
    }
}
