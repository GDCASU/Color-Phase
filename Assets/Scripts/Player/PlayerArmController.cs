using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArmController : MonoBehaviour {
    public static PlayerArmController singleton;
    [System.Serializable]
    public class SetableTransform {
        public Transform transform;
        public Vector3? position;
        public Vector3? rotation;
        public void SetPos (){
            if(position != null) transform.localPosition = (Vector3)position;
            if(rotation != null) transform.localRotation = Quaternion.Euler((Vector3)rotation);
        }
    }
    public SetableTransform rShoulder;
    public SetableTransform rUpperArm;
    public SetableTransform rArm;
    public SetableTransform rHand;

    public SetableTransform lShoulder;
    public SetableTransform lUpperArm;
    public SetableTransform lArm;
    public SetableTransform lHand;
	public void Awake()
    {
        // Delete any extra copies of script not attached to the GameObject with the GameManager
        if (singleton == null)
            singleton = this;
        else
        {
            Destroy(this);
            return;
        }
        this.enabled = false;
    }
	void LateUpdate () {
        rShoulder.SetPos();
        rUpperArm.SetPos();
        rArm.SetPos();
        rHand.SetPos();
        lShoulder.SetPos();
        lUpperArm.SetPos();
        lArm.SetPos();
        lHand.SetPos();
	}
}
