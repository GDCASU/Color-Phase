using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroytimer : MonoBehaviour {

    public int countdown = 10;

	// Use this for initialization
	void Start () {
        Destroy(gameObject, countdown);
    }
	
	// Update is called once per frame
	void Update () {
        //countdown = countdown - 1;
        //if (countdown <= 0) { Destroy(this); }
	}
}
