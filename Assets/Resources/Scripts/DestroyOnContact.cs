using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour {

    public void OnTriggerEnter(Collider other)
    {
        Destroy(this.gameObject);
    }
}
