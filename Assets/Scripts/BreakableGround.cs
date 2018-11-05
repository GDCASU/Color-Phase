using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableGround : MonoBehaviour {

    public GameObject replacement;
    public float breakingSpeed;
    public bool destroyObject;

    /**
     * IMPORTANT NOTICE:
     * This script utilizes a trigger collider to work. The game object
     * associated with this script must have a collider dedicated to being the trigger
     * 
     * Summary:
     * This will either destroy or replace the game object
     * when the player collides with it at a certain speed
     */
    private void OnTriggerEnter(Collider other)
    {
        //Reads that the object collided with a player
        if(other.gameObject.tag == "Player")
        {
            //Gets the players rigidbody
            Rigidbody player = other.gameObject.GetComponent<Rigidbody>();

            //Debug.Log("Player Colliding");
            //Debug.Log("Player velocity: " + player.velocity.y);

            //If the player is going at the specific breaking speed
            if (player.velocity.y < breakingSpeed)
            {
                //Debug.Log("Player going faster than breaking speed");

                //If the object is being set to destroy itself...
                if(destroyObject == true)
                {
                    //Debug.Log("Destroying Object");

                    //Destroy itself
                    Destroy(this.gameObject);
                }
                //If the object is not set to destroy itself
                else
                {
                    //Debug.Log("Replacing Object");

                    //Instantiate the new game object and destroy itself
                    Instantiate(replacement, this.transform.position, this.transform.rotation);
                    Destroy(this.gameObject);
                }
            }
        }
    }
}
