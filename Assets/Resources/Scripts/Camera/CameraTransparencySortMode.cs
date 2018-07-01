using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraTransparencySortMode : MonoBehaviour {

    public Camera cam;

	// Use this for initialization
	void Start ()
    {
        //cam.transparencySortMode = TransparencySortMode.Orthographic;
        cam.transparencySortMode = TransparencySortMode.Perspective;
    }
	
}
