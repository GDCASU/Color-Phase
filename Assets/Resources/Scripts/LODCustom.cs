using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LODCustom : MonoBehaviour
{
    public GameObject cam;
    public GameObject[] objects;
    public float[] range;
    //public float CullRange;
    //public int ObjectAmount = 0;

    private int toggle = -1;
    private int LODState = -1;
    private float distance = 0f;

    // Use this for initialization
    void Start()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            objects[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {


        // Get the distance from the camera and the object
        //distance = getDistance(this.transform, cam.transform);

        /*
        float numX = (obj1.position.x - obj2.position.x);
        float numY = (obj1.position.y - obj2.position.y);
        float numZ = (obj1.position.z - obj2.position.z);

        distance = Mathf.Sqrt(numX * numX + numY * numY + numZ * numZ);


        // Check distance an range comparison of the models
        for (int i = 0; i < objects.Length; i++)
        {
            if (i == 0)
            {
                if (distance < range[i] && LODState != 0)
                {
                    toggle == 0;
                }
            } else
            {
                if (distance < range[i] && distance >= range[i-1] && LODState != i)
                {
                    toggle == i;
                }
            }
            
        }

        if (toggle == -1)
        {
            // Cull Object
            if (distance >= range[objects.Length] && LODState != objects.Length)
            {
                LODState = objects.Length;
                for (int i = 0; i < objects.Length; i++)
                {
                    objects[i].SetActive(false);
                }
            }
        } else {
            // Swap LOD model
            LODState = toggle;
            for (int i = 0; i < objects.Length; i++)
            {
                objects[i].SetActive(false);
            }
            objects[LODState].SetActive(true);
            toggle = -1;
        }
        */
    }


    float getDistance(Transform obj1, Transform obj2)
    {
        float numX = (obj1.position.x - obj2.position.x);
        float numY = (obj1.position.y - obj2.position.y);
        float numZ = (obj1.position.z - obj2.position.z);

        return Mathf.Sqrt(numX * numX + numY * numY + numZ * numZ);
    }
}
