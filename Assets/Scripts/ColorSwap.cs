using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ColorSwap : MonoBehaviour
{
    public static List<ColorSwap> players = new List<ColorSwap>(); // Easy access for puzzle objects
    public GameObject playerModel;
    public GameObject playerCamera;
    public GameObject crossHair;
    public GameObject[] lights;

    public Material[] redMaterials;
    public Material[] greenMaterials;
    public Material[] blueMaterials;
    public Material[] yellowMaterials;

    public int PlayerColorsCount = Enum.GetNames(typeof(SwapColor)).Length;

    public Dictionary<SwapColor, Color > spriteColors = new Dictionary <SwapColor, Color> {
        {SwapColor.Red, new Color(0.5490196f, 0.2614016f, 0.2588235f) },
        {SwapColor.Green, new Color(0.2659206f, 0.5471698f, 0.26068f) },
        {SwapColor.Blue, new Color(0.2588235f, 0.4211917f, 0.5490196f)},
        {SwapColor.Yellow, new Color(0.5490196f, 0.5487493f, 0.2588235f)},
    };
    public Dictionary<SwapColor, Material[] > materialColors;
    public SwapColor currentColor = 0; // The current color of the player

    public KeyCode colorChangeInput = KeyCode.Mouse0; // left mouse input = 134
    private int colorChangeInputToggle = 0;


    private SkinnedMeshRenderer playerMesh;
    private SpriteRenderer crossHairRender;

    void Awake() {
        // Set up material dictionary 
        materialColors = new Dictionary <SwapColor, Material[]> {
            {SwapColor.Red, redMaterials },
            {SwapColor.Green, greenMaterials },
            {SwapColor.Blue, blueMaterials },
            {SwapColor.Yellow, yellowMaterials },
        };

        // Get component references
        playerMesh = playerModel.GetComponent<SkinnedMeshRenderer>(); 
        crossHairRender = crossHair.GetComponent<SpriteRenderer>(); 

        // Add self to singleton
        players.Add(this);
    }

    void Start()
    {
        // swap materials for color
        // swap lights for color
        SetColor(currentColor);
    }

    void Update()
    {
        // Some test code that I'll leave for now
        if (Input.GetKey(colorChangeInput))
        {
            if (colorChangeInputToggle == 0)
            {
                //swap colors and lights
                currentColor++;
                if ((int)currentColor > PlayerColorsCount) currentColor = 0;
                SetColor(currentColor);
                colorChangeInputToggle = 1;
            }
        }
        else
        {
            colorChangeInputToggle = 0; // allows input for the color toggle
        }
    }

    public void SetColor(SwapColor color)
    {
        for (int i = 0; i < lights.Length; i++)
            lights[i].SetActive(i == (int)color);
        
        Material[] m;
        if (materialColors.TryGetValue(color, out m)) playerMesh.materials = m;

        Color c;
        if(spriteColors.TryGetValue(color, out c)) crossHairRender.color = c;

        currentColor = color;

        // This isn't very efficent but it means we can decide if this is a given ability 
        QuickSwap q = GetComponent<QuickSwap>();
        if(q != null ) q.updatePalletUI();
    }
}
