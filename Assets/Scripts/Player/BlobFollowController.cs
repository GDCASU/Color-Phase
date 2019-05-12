using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlobFollowController : MonoBehaviour {
    private GameObject playerChar;
    private Rigidbody rb;
	void Start () {
		playerChar = PlayerColorController.singleton.gameObject;
        rb = GetComponent<Rigidbody>();
	}
    public float maxFollow;
    public float minFollow;

    public float yOffset;
    public float yMax;
	void FixedUpdate () {
        Vector3 p2d = playerChar.transform.position;
        Vector3 t2d = transform.position;
        p2d.y = 0;
        t2d.y = 0;

        float pDist = Vector3.Distance(p2d, t2d);
		if(pDist > maxFollow) 
            rb.AddForce( (p2d - t2d)*20 );
        if(pDist < minFollow) 
            rb.AddForce( (t2d - p2d).normalized*20 * (1/pDist) );
        if(pDist < minFollow+1 && pDist > maxFollow -1)
            rb.velocity= new Vector3(rb.velocity.x/2, rb.velocity.y, rb.velocity.z/2);
        
        
        float py = playerChar.transform.position.y + yOffset;
        float ty = transform.position.y;
        
        float yDist = Mathf.Abs(py+ty);
		if(yDist > yMax) {
            rb.AddForce( new Vector3(0, (py - ty) * 40 ,0) );
        }
        rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y/2f, rb.velocity.z);
	}
}