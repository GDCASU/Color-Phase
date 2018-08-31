using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarrier : MonoBehaviour
{
    public int barrierColor = 0;

    public ColorSwap[] players;

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
        foreach (ColorSwap ply in players)
            Physics.IgnoreCollision(ply.GetComponent<Collider>(), col, ply.currentColor == barrierColor);
    }

    private void setBarrierColor(int color)
    {
        switch (color)
        {
            case 0:
                GetComponent<MeshRenderer>().material = redMaterial;
                break;
            case 1:
                GetComponent<MeshRenderer>().material = greenMaterial;
                break;
            case 2:
                GetComponent<MeshRenderer>().material = blueMaterial;
                break;
            case 3:
                GetComponent<MeshRenderer>().material = yellowMaterial;
                break;
        }
    }
}
