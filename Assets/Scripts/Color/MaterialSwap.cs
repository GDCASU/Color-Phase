using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(ColorState))]
public class MaterialSwap : MonoBehaviour {
    public Dictionary<GameColor, Material[] > materialColors;
    public Material[] redMaterials;
    public Material[] greenMaterials;
    public Material[] blueMaterials;
    public Material[] yellowMaterials;
    private Renderer rend;
    void Awake() {
        // Set up material dictionary 
        materialColors = new Dictionary <GameColor, Material[]> {
            {GameColor.Red, redMaterials },
            {GameColor.Green, greenMaterials },
            {GameColor.Blue, blueMaterials },
            {GameColor.Yellow, yellowMaterials },
        };

        rend = GetComponent<Renderer>();
        
        // Add to the swap events
        GetComponent<ColorState>().onSwap += swapMaterial;
    }
    void swapMaterial(GameColor current, GameColor next) {
        rend.materials = materialColors[next];
    }
}
