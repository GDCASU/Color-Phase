using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSwapPickup : MonoBehaviour {
	public GameColor colorValue;
    public bool destroyOnContact = false;
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Player")) Destroy(gameObject);
    }
	void OnDestroy() {
        // Create a new follower usually 
        if(PlayerColorController.singleton.GetComponent<QuickSwap>() == null ){
            GameObject follower = Instantiate(Resources.Load("Prefabs/Player/SwapFollower") as GameObject);
            follower.transform.position = this.transform.position;
            PlayerColorController.singleton.gameObject.AddComponent<QuickSwap>();

            PlayerColorController.singleton.GetComponent<QuickSwap>().followerState = follower.GetComponent<ColorState>();
        }
        // If for some reason we have levels with multiple of these we'll just set the color
        else {
            PlayerColorController.singleton.GetComponent<QuickSwap>().followerState.currentColor = colorValue;
        }
	}
}
