using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SinusUV : MonoBehaviour
{
    MeshRenderer ObjectRender;
    Vector2 UV_Offset;

    //public float U_Speed = 0.0f;
    //public float V_Speed = 0.0f;

    public float radius = 0.05f;
    public float speed = 0.02f;
    private float count = 0.0f;

    // Use this for initialization
    void Start()
    {
        UV_Offset = new Vector2(0, 0);
        ObjectRender = transform.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        count = count + speed;
        UV_Offset = new Vector2(Mathf.Sin(count) * radius, Mathf.Cos(count) * radius);
        ObjectRender.material.SetTextureOffset("_MainTex", UV_Offset);
    }
}

