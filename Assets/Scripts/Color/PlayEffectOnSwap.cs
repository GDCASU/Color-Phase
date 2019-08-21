using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayEffectOnSwap : MonoBehaviour {

	ColorState color;

    public ParticleSystem part;
	void Start () {
		color = GetComponent<ColorState>();
        color.onSwap += swapParticles;
	}
	void swapParticles(GameColor current, GameColor next) {
        part.startColor = ColorState.RGBColors[next];
        part.Play();
        part.time = 0;
    }
}
