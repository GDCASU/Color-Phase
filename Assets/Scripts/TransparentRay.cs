using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransparentRay : MonoBehaviour
{
    private Stack<transparentObjectClass> transparentObjects = new Stack<transparentObjectClass>();
    public Material oringinalTransparent;
    public GameObject[] players;
    private Material[] tranparent;
    public int resetCounter = 0;
    

    private int layerMask = 1 << 2;
    // Use this for initialization
    void Start()
    {
        layerMask = ~(layerMask | (1 << 17));
        tranparent = new Material[players.Length];
        for(int x=0;x<players.Length;x++)
        {
            tranparent[x] = new Material(oringinalTransparent);
        }
    }

    // This code only works if the camera is centered on the player
    //Else add a seperate tag for the ground
    void Update()
    {
        RaycastHit hit;
        //resets things to retry to make things better
        if (resetCounter == 0)
        {
            //zooming- zooms in incase the players are close together
            transform.Translate(Vector3.forward * 10);

            while (transparentObjects.Count != 0)
            {
                transparentObjectClass reset = transparentObjects.Pop();
                reset.transparentObject.layer = reset.originalLayer;
                reset.transparentObject.GetComponent<Renderer>().material = reset.originalMaterial;
            }
        }
        //zooming
        float xSum = 0, zSum = 0;
        for (int x = 0; x < players.Length; x++)
        {
            xSum += players[x].transform.position.x;
            zSum += players[x].transform.position.z;
            bool onScreen = false;
            int s = 0;
            while (!onScreen)
            {
                Vector3 screenPoint = GetComponent<Camera>().WorldToViewportPoint(players[x].transform.position);
                onScreen = screenPoint.z > 0 && screenPoint.x > .05 && screenPoint.x < .95 && screenPoint.y > .05 && screenPoint.y < .95;
                if (!onScreen)
                {
                    transform.Translate(Vector3.back); //if everyone is not on screen, ove back a bit and try again
                }
                s++;
                if (s > 10)
                {
                    Debug.Log("overflowing");
                    break;
                }
            }
        }
        //calculates the average player position and centers the camera on it
        transform.position = new Vector3(xSum / players.Length, transform.position.y, zSum / players.Length);

        //transparent raycasting
        for (int x = 0; x < players.Length; x++)
        {
            bool keepGoing = true;
            while (keepGoing)
            {
                if (Physics.Raycast(transform.position, /*transform.forward*/ players[x].transform.position - transform.position, out hit, 100, layerMask))
                {
                    //Debug.DrawRay(transform.position, transform.TransformDirection(transform.forward/*players[x].transform.position-transform.position*/) * hit.distance, Color.yellow);
                    if (!hit.collider.tag.StartsWith("Player"))
                    {
                        GameObject G = hit.collider.gameObject;
                        transparentObjects.Push(new transparentObjectClass(G, G.GetComponent<Renderer>().material, G.layer));
                        if (!hit.collider.tag.StartsWith("PermamentWall"))
                        {
                            //G.GetComponent<Renderer>().material = tranparent;
                            StartCoroutine("Fade", G.GetComponent<Renderer>());
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
        resetCounter = (resetCounter + 1) % 20;
    }

    public IEnumerator Fade(Renderer r)
    {
        for (int f = 0; f < tranparent.Length; f++)
        {
            r.material = tranparent[f];
            yield return new WaitForSeconds(.1f);
        }
    }

    private class transparentObjectClass
    {
        public GameObject transparentObject;
        public Material originalMaterial;
        public int originalLayer;
        public transparentObjectClass(GameObject to, Material om, int ol)
        {
            transparentObject = to;
            originalMaterial = om;
            originalLayer = ol;
        }
    }
}
