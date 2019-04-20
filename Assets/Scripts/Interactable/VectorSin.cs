using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VectorSin : MonoBehaviour {

	public Vector3 axis;
    public float speed;
    
    private Vector3 st;
    void Start () { 
        
        st = transform.position;
    }
    private float t;
	void FixedUpdate () {
        t+=Time.deltaTime;
        transform.position = Mathf.Sin(t*speed)*axis+st;
	}
}
