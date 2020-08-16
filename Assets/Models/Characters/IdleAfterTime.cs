using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleAfterTime : MonoBehaviour {
    public float timeToIdle = 5f;
    private Animator anim;
    private PlayerMovement player;
	void Start () {
		anim = gameObject.GetComponentInChildren<Animator>();
        player = PlayerColorController.singleton.GetComponent<PlayerMovement>();
	}
	
	private float t;
	void LateUpdate () {
        if(player.isStill()) {
         t+=Time.deltaTime;
           if( t > timeToIdle && !anim.GetCurrentAnimatorStateInfo(0).IsName("SpecialIdle")) {
               anim.Play("SpecialIdle");
           }
        } else {
            if(anim.GetCurrentAnimatorStateInfo(0).IsName("SpecialIdle")) {
                anim.Play("Idle");
            }
            t = 0;
        }
	}

    public void stopLongIdle () {
        anim.Play("Idle");
        t = 0;
    }
}
