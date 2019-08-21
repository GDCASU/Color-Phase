using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[ExecuteInEditMode]
[RequireComponent(typeof(ColorState))]
public class MaterialSwap : MonoBehaviour {
    public GameObject rendererLocation;
    public bool applyChildren = false;
    private List<Renderer> childRenders;
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
        
        // Add to the swap events
        GetComponent<ColorState>().onSwap += swapMaterial;

        if(applyChildren) childRenders = GetComponentsInChildren<Renderer>().ToList();
        else
            // We can set a seperate game object where the render is on
            rend = (rendererLocation != null) ? rendererLocation.GetComponent<Renderer>() : GetComponent<Renderer>();
    }
    void swapMaterial(GameColor current, GameColor next) {
        if(applyChildren) foreach(Renderer rend in childRenders) rend.materials = materialColors[next];
        else rend.materials = materialColors[next];
    }
}
