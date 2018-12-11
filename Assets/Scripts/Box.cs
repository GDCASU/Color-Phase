using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    //the position that the box will be relative to the player when picked up
    public Transform pickup;

    //player is holding the box
    public bool onHand;

    //there should be a LayerMask that ignores the player so Raycast is only aimed at the boxes
    public LayerMask ignorePlayer;
    public GameObject player;

    //the hitbox is a separate object from the box so that the box can collide with objects
    public GameObject hitbox;
    GameObject box;
    public int colorValue = 0;

    // Use this for initialization
    void Start()
    {
        //ignores collisions between the hitbox and actual box
        Physics.IgnoreCollision(hitbox.GetComponent<Collider>(), gameObject.GetComponent<Collider>());
        onHand = false;
        pickup.transform.parent = player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        //drop the box when you stop holding left click or whatever button
        if (Input.GetMouseButtonUp(0) && onHand == true)
        {
            box.transform.parent = null;
            box.GetComponent<Rigidbody>().useGravity = true;
            box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
            box = null;
            onHand = false;
            hitbox.SetActive(false);
            hitbox.transform.parent = gameObject.transform;
        }

        //pick up the box when the button is pressed and held
        if (Input.GetMouseButtonDown(0) && onHand == false)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 10, ignorePlayer))
            {
                if (hit.collider.gameObject == gameObject)
                {
                    box = hit.collider.gameObject;
                    box.transform.position = pickup.position;
                    box.transform.rotation = pickup.rotation;
                    box.transform.parent = player.transform;
                    box.GetComponent<Rigidbody>().useGravity = false;
                    box.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
                    onHand = true;
                    hitbox.transform.position = pickup.position;
                    hitbox.transform.rotation = pickup.rotation;
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
