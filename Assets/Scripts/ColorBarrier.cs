using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorBarrier : MonoBehaviour
{
    public int barrierColor = 0;

    // Needs to do a search for player Layers

    public GameObject player1;
    public ColorSwap player1Color;
    public GameObject player2;
    public ColorSwap player2Color;
    public GameObject player3;
    public ColorSwap player3Color;
    public GameObject player4;
    public ColorSwap player4Color;

    public Material redMaterial;
    public Material greenMaterial;
    public Material blueMaterial;
    public Material yellowMaterial;

    // Use this for initialization
    void Start()
    {
        setBarrierColor(barrierColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (player1Color.currentColor == barrierColor)
        {
            Physics.IgnoreCollision(player1.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        else
        {
            Physics.IgnoreCollision(player1.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }

        if (player2Color.currentColor == barrierColor)
        {
            Physics.IgnoreCollision(player2.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        else
        {
            Physics.IgnoreCollision(player2.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }

        if (player3Color.currentColor == barrierColor)
        {
            Physics.IgnoreCollision(player3.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        else
        {
            Physics.IgnoreCollision(player3.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }

        if (player4Color.currentColor == barrierColor)
        {
            Physics.IgnoreCollision(player4.GetComponent<Collider>(), GetComponent<Collider>(), true);
        }
        else
        {
            Physics.IgnoreCollision(player4.GetComponent<Collider>(), GetComponent<Collider>(), false);
        }
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
