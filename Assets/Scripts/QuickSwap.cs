using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;

public class QuickSwap : MonoBehaviour {
    /*
    Allows the player to swap between a stored color and their currect color
     */
    public GameObject PalletCurrentPrefab;
    public GameObject PalletBackupPrefab;
    ColorSwap colorSwap;
    private InputPlayer inputPlayer;
    private GameObject PalletCurrent;
    private GameObject PalletBackup;
    private SpriteRenderer PalletCurrentRender;
    private SpriteRenderer PalletBackupRender;
    public SwapColor storedColor;
    void Awake () {
        colorSwap = GetComponent<ColorSwap>(); 
        inputPlayer = GetComponent<InputPlayer>();
    }
	void Start () { 
        // Set the stored color to whatever the player has at the time
        storedColor = colorSwap.currentColor;

        // Set up the UI
        PalletCurrent = Instantiate(PalletCurrentPrefab);
        PalletBackup = Instantiate(PalletBackupPrefab);

        PalletCurrent.transform.parent = colorSwap.playerCamera.transform;
        PalletCurrent.transform.localPosition = new Vector3 (0.29F, -0.18F, 0.4F); // These are hardcoded for now (no UI canvas)

        PalletBackup.transform.parent = colorSwap.playerCamera.transform;
        PalletBackup.transform.localPosition = new Vector3 (0.35F, -0.18F, 0.4F);

        // Get references to sprite renderers for setting colors 
        PalletCurrentRender = PalletCurrent.GetComponent<SpriteRenderer>();
        PalletBackupRender = PalletBackup.GetComponent<SpriteRenderer>();

        // Set colors on the UI
        updatePalletUI();
    }
	
	void Update () {
        // I've changed this from X because of the Input remapping.
        // This will need to be chnaged later once inputs are defined
		if(InputManager.GetButtonDown(PlayerButton.Jump, inputPlayer)) {
            SwapColor temp = colorSwap.currentColor;
            colorSwap.SetColor(storedColor);
            storedColor = temp;
        }
        // We don't really want to call this every update
        updatePalletUI();
	}

    public void updatePalletUI () {
        Color c;
        if(colorSwap.spriteColors.TryGetValue(colorSwap.currentColor, out c)) PalletCurrentRender.color = c;
        if(colorSwap.spriteColors.TryGetValue(storedColor, out c)) PalletBackupRender.color = c;
    }
}
