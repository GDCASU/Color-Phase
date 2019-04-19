using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreenController : MonoBehaviour {

	public static GameObject titleScreenCanvas;

    public void Awake() {
        titleScreenCanvas = this.gameObject;
    }

    
}
