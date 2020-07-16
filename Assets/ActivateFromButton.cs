using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateFromButton : MonoBehaviour {

	public List<MonoBehaviour> ActiveBehaviours = new List<MonoBehaviour>();
    public ButtonToggle button;

    private bool internalState;
    void Start () {
        internalState = button.state;
    }
	void Update () {
		if(button.state != internalState) {
            ActiveBehaviours.ForEach(x => x.enabled = button.state);
        }
	}
}
