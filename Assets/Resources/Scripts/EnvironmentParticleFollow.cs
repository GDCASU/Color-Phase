using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentParticleFollow : MonoBehaviour {

    public GameObject player;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {

        if (player.transform.position.y > 40)
        {
            transform.SetPositionAndRotation(new Vector3(player.transform.position.x, player.transform.position.y + 10, player.transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        }
        else
        {
            transform.SetPositionAndRotation(new Vector3(player.transform.position.x, 50, player.transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        }

    }
}
