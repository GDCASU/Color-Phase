using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour {

	ColorState color;
    Rigidbody rb;
    PlayerMovement m;
	void Start () {
		color = GetComponent<ColorState>();
        rb = GetComponent<Rigidbody>();
        m = GetComponent<PlayerMovement>();
	}
    
	void OnCollisionEnter(Collision c)  {
        if(color.currentColor == GameColor.Yellow && Vector3.Dot(c.contacts[0].normal, Vector3.up) > 0) {
            var g = Instantiate(Resources.Load("Dust Effect")) as GameObject;
            g.transform.position = c.contacts[0].point+Vector3.up*0.1f;
        }
	}
}
