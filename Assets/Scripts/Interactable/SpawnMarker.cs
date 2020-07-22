using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMarker : MonoBehaviour {
    public GameObject marker;
    private PlayerMovement player;
	void Start () {
        player = PlayerColorController.singleton.GetComponent<PlayerMovement>();
	}
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player") {
            player.spawnLoc = marker.transform.position;
        }
    }
}
