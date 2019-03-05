using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    void OnCollisionEnter(Collision c){
        var r = c.rigidbody;
        if(r!=null){
            r.transform.parent = this.transform;
        }
    }
    void OnCollisionExit(Collision c){
        var r = c.rigidbody;
        if(r!=null){
            r.transform.parent = null;
        }
    }
}
