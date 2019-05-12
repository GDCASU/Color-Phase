using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerEffects : MonoBehaviour {

	ColorState color;
    Rigidbody rb;
    PlayerMovement m;

    public ParticleSystem part;
	void Start () {
		color = GetComponent<ColorState>();
        rb = GetComponent<Rigidbody>();
        m = GetComponent<PlayerMovement>();
        color.onSwap += swapParticles;
	}
    float pY;
    void Update () {
        pY = rb.velocity.y;
    }
    
	void OnCollisionEnter(Collision c)  {
        if(color.currentColor == GameColor.Yellow && Vector3.Dot(c.contacts[0].normal, Vector3.up) >= 0.9f && pY < -16) {
            var g = Instantiate(Resources.Load("Dust Effect")) as GameObject;
            g.transform.position = c.contacts[0].point+Vector3.up*0.1f;
        }
	}

    void swapParticles(GameColor current, GameColor next) {
        part.startColor = ColorState.RGBColors[next];
        part.Play();
        part.time = 0;
    }
}
