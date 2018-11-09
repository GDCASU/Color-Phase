using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarrier : MonoBehaviour
{
    private ChangeableColor color;
    private Collider col;

    // Use this for initialization
    void Start()
    {
        col = GetComponent<Collider>();
        color = GetComponent<ChangeableColor>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ColorSwap ply in ColorSwap.players)
            Physics.IgnoreCollision(ply.GetComponent<Collider>(), col, ply.currentColor == color.CurrentColor);
    }
}
