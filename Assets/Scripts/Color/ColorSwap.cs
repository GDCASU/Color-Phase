using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ColorSwap : MonoBehaviour
{
    public GameObject playerModel;
    public GameObject playerCamera;
    public GameObject crossHair;
    public GameObject[] lights;
    private SkinnedMeshRenderer playerMesh;
    private SpriteRenderer crossHairRender;
    public Dictionary<GameColor, Material[] > materialColors;
    public Material[] redMaterials;
    public Material[] greenMaterials;
    public Material[] blueMaterials;
    public Material[] yellowMaterials;

    void Awake() {
        // Set up material dictionary 
        materialColors = new Dictionary <PlayerColor, Material[]> {
            {PlayerColor.Red, redMaterials },
            {PlayerColor.Green, greenMaterials },
            {PlayerColor.Blue, blueMaterials },
            {PlayerColor.Yellow, yellowMaterials },
        };

        // Get component references
        playerMesh = playerModel.GetComponent<SkinnedMeshRenderer>(); 
        crossHairRender = crossHair.GetComponent<SpriteRenderer>();
        SceneManager.activeSceneChanged += clearColors;
    }
    void clearColors(Scene current,Scene next)
    {
        players.Clear();
    }
    void Start()
    {
        
        players.Add(this);
        // swap materials for color
        // swap lights for color
        SetColor(currentColor);
    }

    public void SetColor(int color)
    {
        gameObject.layer = 20 + color;

        for (int i = 0; i < lights.Length; i++)
            lights[i].SetActive(i == (int)color);
        
        Material[] m;
        if (materialColors.TryGetValue((PlayerColor)color, out m)) playerMesh.materials = m;

        Color c;
        if(spriteColors.TryGetValue((PlayerColor)color, out c)) crossHairRender.color = c;

        currentColor = color;

        // This isn't very efficent but it means we can decide if this is a given ability 
        QuickSwap q = GetComponent<QuickSwap>();
        if(q != null ) q.updatePalletUI();
    }
}
