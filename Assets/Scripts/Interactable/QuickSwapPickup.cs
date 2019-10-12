using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuickSwapPickup : MonoBehaviour {
	public GameColor colorValue;
    private UI ui;
    public void Start()
    {
        ui = GameObject.Find("PlayerUI").GetComponent<UI>();
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.tag.StartsWith("Player")) Destroy(gameObject);
    }
	void OnDestroy() {
        // Create a new follower usually 
        if(PlayerColorController.singleton.GetComponent<QuickSwap>() == null ){
            GameObject follower = Instantiate(Resources.Load("Prefabs/Player/SwapFollower") as GameObject);
            var followerColor = follower.GetComponent<ColorState>();
            followerColor.currentColor = colorValue;
            follower.transform.position = this.transform.position;
            PlayerColorController.singleton.gameObject.AddComponent<QuickSwap>();

            var quickSwapColor = PlayerColorController.singleton.GetComponent<QuickSwap>();
            quickSwapColor.followerState = followerColor;
            quickSwapColor.storedColor = followerColor.currentColor;

            ui.hasQuickswap = true;
            ui.quickSwap = quickSwapColor;
            var tempColor = ui.PreviousAbility.color;
            tempColor.a = 1;
            ui.PreviousAbility.color = tempColor;
        }
        // If for some reason we have levels with multiple of these we'll just set the color
        else {
            PlayerColorController.singleton.GetComponent<QuickSwap>().followerState.currentColor = colorValue;
        }
	}
}
