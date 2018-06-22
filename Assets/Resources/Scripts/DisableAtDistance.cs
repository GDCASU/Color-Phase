using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAtDistance : MonoBehaviour {

    public float range = 15.0f;
    
    public GameObject player, obj;
    private float distance = 0.0f;
    private int count = 0;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        count++;

        if (count % 4 == 0)
        {
            distance = Vector3.Distance(player.transform.position, obj.transform.position);

            if (distance > range)
            {
                obj.SetActive(false);
            }
            else
            {
                obj.SetActive(true);
            }
        }
		
	}
}
