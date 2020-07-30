using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateGameObject : MonoBehaviour {

	public List<GameObject> ActiveObjects = new List<GameObject>();
	public List<GameObject> InactiveObjects = new List<GameObject>();
    public ButtonToggle button;

    private bool internalState;
    void Start () {
        internalState = button.state;
    }
	void Update () {
		if(button.state != internalState) {
            ActiveObjects.ForEach(x => x.SetActive(button.state));
            InactiveObjects.ForEach(x => x.SetActive(!button.state));
        }
	}
}
