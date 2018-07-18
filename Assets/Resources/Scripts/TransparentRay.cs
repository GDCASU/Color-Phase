using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentRay : MonoBehaviour {


    private Stack<transparentObjectClass> transparentObjects=new Stack<transparentObjectClass>();
    public Material tranparent;
    public int resetCounter=0;
    public GameObject[] players;

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
            //zooming
            transform.Translate(transform.forward * 10);

            try
            {
                while (transparentObjects.Peek() != null)
                {
                    transparentObjectClass reset = transparentObjects.Pop();
                    reset.transparentObject.GetComponent<Renderer>().material = reset.originalMaterial;
                    reset.transparentObject.layer = reset.originalLayer;
                }
            }
            catch
            {

            }
        }
        //zooming
        for (int x = 0; x < players.Length; x++)
        {
            bool onScreen = false;
            while (!onScreen)
            {
                Vector3 screenPoint = GetComponent<Camera>().WorldToViewportPoint(players[x].transform.position);
                onScreen = screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
                if (!onScreen)
                {
                    transform.Translate(transform.forward * -1);
                }
                
            }
        }
        //transparent raycasting
        for (int x = 0; x < players.Length; x++)
        {
            bool keepGoing= true;
            while (keepGoing)
            {
                if (Physics.Raycast(transform.position, /*transform.forward*/ players[x].transform.position-transform.position, out hit, 100, layerMask))
                {
                    //Debug.DrawRay(transform.position, transform.TransformDirection(transform.forward/*players[x].transform.position-transform.position*/) * hit.distance, Color.yellow);
                    if (!hit.collider.tag.StartsWith("Player"))
                    {
                        GameObject G = hit.collider.gameObject;
                        transparentObjects.Push(new transparentObjectClass(G, G.GetComponent<Renderer>().material, G.layer));
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
                    else keepGoing = false;
                }
                else keepGoing = false;
            }
        }
        resetCounter= (resetCounter+1)%20;

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
