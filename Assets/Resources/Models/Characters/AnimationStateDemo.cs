using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationStateDemo : MonoBehaviour
{
    // Blender Character To Unity part 1 of 2: https://www.youtube.com/watch?v=h8oI0n5kAIg

    // Bug Note: Some of the animations appear to have a slight delay in the transitions, it might be a problem with the animator
    // System Note: All animations involving character repositioning such as running, jumping, falling, and swinging need additional code for repositioning and transitioning animation states.

    private Animator anim;
    private int animationState;
    private int jumpCounter;
    private int damageFlyingCounter;
    private int actionPull;
    private int actionGrappleFly;
    private int actionSwing;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponentInChildren<Animator>();
        animationState = 0;
        jumpCounter = 0;
        damageFlyingCounter = 0;
        actionPull = 0;
        actionGrappleFly = 0;
        actionSwing = 0;
    }

    // Update is called once per frame
    void Update()
    {
        animationState = 0; // always default to idle

        // Note: Run and Walk animations may need to be programmed based on the velocity of the characters as well as detecting if the character is on solid ground
        if (Input.GetKey("up")) // Demo Running animation
        {
            animationState = 2;
        }

        if (Input.GetKey("down")) // Demo Walking animation
        {
            animationState = 1;
        }

        if (Input.GetKey("space")) // Demo Jump animation
        {
            animationState = 4;
            jumpCounter = 1; //starts jump counter
        }

        // Similar to the jump animation this animation is for when the player walks off of an edge and falls
        if (Input.GetKey("left")) // Demo Falling animation
        {
            animationState = 5;
            jumpCounter = 60; //starts jump counter
        }

        // For the jump and fall animations it may be better to calculate the animation counter/state transitions using the characters velocity
        if (jumpCounter > 0)
        {
            jumpCounter = jumpCounter + 1;
            if (jumpCounter >= 60)
            {
                animationState = 5; // falling animation
            }
            if (jumpCounter >= 120)
            {
                animationState = 6; // landing animation
            }
            if (jumpCounter >= 160)
            {
                jumpCounter = 0; // back to idle animation
            }
        }

        if (Input.GetKey("1")) // Demo waving arm animation
        {
            animationState = 3;
        }

        if (Input.GetKey("2")) // Demo hop animation - could be used for end of level, or when picking up an item
        {
            animationState = 7;
        }

        if (Input.GetKey("3")) // Demo no animation - it shakes head sideways
        {
            animationState = 8;
        }

        if (Input.GetKey("4")) // Demo yes animation
        {
            animationState = 9;
        }

        if (Input.GetKey("5")) // Demo Damaged 1
        {
            animationState = 15;
        }

        if (Input.GetKey("6")) // Demo Damaged 2
        {
            animationState = 16;
        }

        if (Input.GetKey("7")) // Demo Damaged 3 - freezy
        {
            animationState = 17;
        }

        if (Input.GetKey("8")) // Demo Damaged 4 - fire
        {
            animationState = 18;
        }

        if (Input.GetKey("9")) // Demo Damaged 5+6 - flying backwards
        {
            animationState = 19;
            damageFlyingCounter = 1;
        }

        if (Input.GetKey("0")) // Demo Damaged 7 - fall back
        {
            animationState = 21;
        }

        // For damageFlying animations 19-20 this may need to calculate environment collision for the character to stop.
        if (damageFlyingCounter > 0)
        {
            damageFlyingCounter = damageFlyingCounter + 1;
            if (damageFlyingCounter >= 120)
            {
                animationState = 20; // hit wall or stop flying animation
            }
            if (damageFlyingCounter >= 160)
            {
                damageFlyingCounter = 0; // back to idle animation
            }
        }

        if (Input.GetKey("q")) // Demo action punch
        {
            animationState = 10;
        }

        if (Input.GetKey("w")) // Demo action punch + Pull animation
        {
            animationState = 10;
            actionPull = 1;
        }

        if (actionPull >= 1)
        {
            animationState = 10;
            actionPull = actionPull + 1;
            if (actionPull >= 40)
            {
                animationState = 11; // action pull animation
            }
            if (actionPull >= 100)
            {
                actionPull = 0; // back to idle animation
            }
        }

        if (Input.GetKey("e")) // Demo action punch + Fly animation
        {
            animationState = 10;
            actionGrappleFly = 1;
        }

        if (actionGrappleFly >= 1)
        {
            animationState = 10;
            actionGrappleFly = actionGrappleFly + 1;
            if (actionGrappleFly >= 18)
            {
                animationState = 12; // action grapple/fly animation
            }
            if (actionGrappleFly >= 120)
            {
                actionGrappleFly = 0; // back to idle animation
            }
        }

        if (Input.GetKey("a")) // Demo action punch angled without swing animation
        {
            animationState = 13;
        }

        if (Input.GetKey("s")) // Demo action punch angled + swing animation
        {
            animationState = 13;
            actionSwing = 1;
        }

        // Note: the swing animation assumes that the angle and position of the character will be adjusted with code.
        if (actionSwing > 0)
        {
            animationState = 13;
            actionSwing = actionSwing + 1;
            if (actionSwing >= 18)
            {
                animationState = 14; // action swing animation
            }
            if (actionSwing >= 120)
            {
                actionSwing = 0; // back to idle animation
            }
        }

        // As of 8/22/2018
        // There are animation states ranging from -1 to 21
        if (animationState < -1)
        {
            animationState = 21;
        }
        if (animationState > 21)
        {
            animationState = -1;
        }

        anim.SetInteger("AnimPar", animationState); // this sets animation states and will transition based on the state chart
    }
}
