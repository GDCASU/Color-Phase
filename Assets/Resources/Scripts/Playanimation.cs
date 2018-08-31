using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Playanimation : MonoBehaviour
{

    public Animation anim;
    //public bool Play(PlayMode mode = PlayMode.StopSameLayer);

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animation>();
        foreach (AnimationState state in anim)
        {
            state.speed = 2.5F;
        }
        //anim.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //GetComponent<Animation>().Play();
        //anim.
    }
}
