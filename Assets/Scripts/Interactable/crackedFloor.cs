using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class crackedFloor : MonoBehaviour {

    public GameObject objReplace;

    private void OnTriggerEnter(Collider other)
    {
        GameObject wreck = (GameObject)Instantiate(objReplace, transform.position, transform.rotation);
        Destroy(gameObject);
    }

}
