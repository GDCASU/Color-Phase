using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleTarget : MonoBehaviour {
    public static List<GrappleTarget> targets = new List<GrappleTarget>(); 
    public bool neutral;
    public GameColor targetColor;
	public void Start () {
        targets.Add(this);
    }
    public void OnDestroy(){
        targets.Remove(this);
    }
}
