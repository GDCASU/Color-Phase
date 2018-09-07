using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentWaterFollow : MonoBehaviour
{ 
    public GameObject player;
    private int count;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count++;

        if (count % 200 == 0)
        {
            transform.SetPositionAndRotation(new Vector3(player.transform.position.x, transform.position.y, player.transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        }
        //else
        //{
        //transform.SetPositionAndRotation(new Vector3(player.transform.position.x, 50, player.transform.position.z), new Quaternion(transform.rotation.x, transform.rotation.y, transform.rotation.z, transform.rotation.w));
        //}
    }
}
