﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentRay : MonoBehaviour {


    private Stack<transparentObjectClass> transparentObjects=new Stack<transparentObjectClass>();
    public Material tranparent;
    public int resetCounter=0;

    private int layerMask=1<<2;
    // Use this for initialization
    void Start () {
        layerMask = ~(layerMask | (1 << 17));
	}
	
	// This code only works if the camera is centered on the player
    //Else add a seperate tag for the ground
	void Update () {
        RaycastHit hit;
        
        if (resetCounter == 0)
        {
            
            try
            //while (transparentObjects.Peek() != null)
            {
                transparentObjectClass reset=transparentObjects.Pop();
                reset.transparentObject.GetComponent<Renderer>().material = reset.originalMaterial;
                reset.transparentObject.layer = reset.originalLayer;
            }
            catch
            {

            }
        }
        
        if (Physics.Raycast(transform.position,transform.forward,out hit,100,layerMask))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.yellow);
            if (!hit.collider.tag.StartsWith("Player"))
            {
                GameObject G = hit.collider.gameObject;
                transparentObjects.Push(new transparentObjectClass(G, G.GetComponent<Renderer>().material,G.layer));
                if (!hit.collider.tag.StartsWith("PermamentWall"))
                {
                    G.GetComponent<Renderer>().material = tranparent;
                    G.layer = 2;
                }
                else
                {
                    G.layer = 17;
                }
            }
        }
        resetCounter= (resetCounter+1)%2000;

    }
    private class transparentObjectClass
    {
        public GameObject transparentObject;
        public Material originalMaterial;
        public int originalLayer;
        public transparentObjectClass(GameObject to, Material om,int ol)
        {
            transparentObject = to;
            originalMaterial = om;
            originalLayer = ol;
        }
    }
}
