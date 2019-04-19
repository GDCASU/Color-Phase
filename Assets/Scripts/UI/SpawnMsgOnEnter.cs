using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class SpawnMsgOnEnter : MonoBehaviour
{
    [SerializeField]
    public List<string> helpMessages = new List<string>();

    public bool triggerOnce;

    void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            HintTextUI.singleton.helpMessages.AddRange(helpMessages);
            HintTextUI.singleton.startPush();

            if (triggerOnce)
            {
                Destroy(this);
            }
        }
    }
}