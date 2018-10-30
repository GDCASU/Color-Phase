using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarrier : MonoBehaviour
{
    public SwapColor barrierColor = 0;

    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;

    private Collider col;

    // Use this for initialization
    void Start()
    {
        setBarrierColor(barrierColor);
        col = GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (ColorSwap ply in ColorSwap.players)
            Physics.IgnoreCollision(ply.GetComponent<Collider>(), col, ply.currentColor == barrierColor);
    }

    private void setBarrierColor(SwapColor color)
    {
        switch (color)
        {
            case SwapColor.Red:
                GetComponent<MeshRenderer>().material = redMaterial;
                break;
            case SwapColor.Green:
                GetComponent<MeshRenderer>().material = greenMaterial;
                break;
            case SwapColor.Blue:
                GetComponent<MeshRenderer>().material = blueMaterial;
                break;
            case SwapColor.Yellow:
                GetComponent<MeshRenderer>().material = yellowMaterial;
                break;
        }
    }
}
