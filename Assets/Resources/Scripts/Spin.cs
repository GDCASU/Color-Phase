using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour {

    public float speed = 10.0f;
    public int axis = 0;


	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
        switch (axis) {
            case 0:
                transform.Rotate(Vector3.forward * Time.deltaTime * speed);
                break;
            case 1:
                transform.Rotate(Vector3.right * Time.deltaTime * speed);
                break;
            case 2:
                transform.Rotate(Vector3.up * Time.deltaTime * speed);
                break;
        }
	}
}
