using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class flow : MonoBehaviour {

    private int count;
    private float deltaCount;

    void Start()
    {
        count = 0;
        deltaCount = 0f;
    }

    void Update()
    {

        deltaCount = deltaCount + Time.deltaTime;
        if (deltaCount > 0.04f)
        {
            count = count + 1;
            if (count > 999)
            {
                count = 0;
            }
            deltaCount = deltaCount - 0.04f;
        }

        if (count < 10)
        {
            GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Materials/Textures/Flow/Animation00" + count.ToString()) as Texture2D);
        }
        else
        {
            if (count < 100)
            {
                GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Materials/Textures/Flow/Animation0" + count.ToString()) as Texture2D);
            }
            else
            {
                GetComponent<Renderer>().material.SetTexture("_MainTex", Resources.Load("Materials/Textures/Flow/Animation" + count.ToString()) as Texture2D);
            }
        }

    }
}
