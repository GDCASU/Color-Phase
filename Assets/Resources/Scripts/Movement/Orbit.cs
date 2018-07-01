using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour {

    public float speed = 1;
    private float count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count = count + 0.025f;
        transform.Translate(Vector3.right * Mathf.Sin(count)* speed * Time.deltaTime);
        transform.Translate(Vector3.up * Mathf.Cos(count) * speed * Time.deltaTime);
    }
}
