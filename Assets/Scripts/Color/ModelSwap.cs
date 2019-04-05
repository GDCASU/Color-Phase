using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(ColorState))]
public class ModelSwap : MonoBehaviour {
    public GameObject rendererLocation;
    public Dictionary<GameColor, Mesh > meshColors;
    public Mesh redMesh;
    public Mesh greenMesh;
    public Mesh blueMesh;
    public Mesh yellowMesh;
    private SkinnedMeshRenderer rend;
    void Awake() {
        // Set up mesh dictionary 
        meshColors = new Dictionary <GameColor, Mesh> {
            {GameColor.Red, redMesh },
            {GameColor.Green, greenMesh },
            {GameColor.Blue, blueMesh },
            {GameColor.Yellow, yellowMesh },
        };
        
        // We can set a seperate game object where the render is on
        rend = (rendererLocation != null) ? rendererLocation.GetComponent<SkinnedMeshRenderer>() : GetComponent<SkinnedMeshRenderer>();
        
        // Add to the swap events
        GetComponent<ColorState>().onSwap += swapMesh;
    }
    void swapMesh(GameColor current, GameColor next) {
        rend.sharedMesh = meshColors[next];
    }
}
