using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamBehavior : MonoBehaviour {
    bool looking;
    bool found=false;


	// Use this for initialization
	void Start () {
        looking = true;
	}
	
	// Update is called once per frame
	void Update () {
        RaycastHit hit;
        if (looking)
        {
            if (Physics.Raycast(transform.position, transform.forward, out hit, 100))
            {
                if (hit.collider.tag.StartsWith("Player"))
                {
                    found = true;
                }
            }
        }
        if (found)
        {
            transform.LookAt(hit.transform);
        }
	}

}
