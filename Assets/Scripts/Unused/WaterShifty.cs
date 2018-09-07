using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterShifty : MonoBehaviour
{
    private int count = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        count = count + 1;

        /*
        if (count >= 50)
        {
            transform.Translate(Vector3.right * -40f);
            count = count - 50;
        }
        */

        transform.Translate(Vector3.right * -0.08f);

        if (count >= 250)
        {
            transform.Translate(Vector3.right * 20f);
            count = count - 250;
        }
    }
}
