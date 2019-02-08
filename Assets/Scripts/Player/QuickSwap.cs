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
    ColorState playerColor;
    private InputPlayer inputPlayer;
    private GameObject PalletCurrent;
    private GameObject PalletBackup;
    private SpriteRenderer PalletCurrentRender;
    private SpriteRenderer PalletBackupRender;
    public GameColor storedColor;
    void Awake () {
        playerColor = GetComponent<ColorState>(); 
        inputPlayer = GetComponent<InputPlayer>();
    }
	void Start () { 
        // Set the stored color to whatever the player has at the time
        storedColor = playerColor.currentColor;

        // Set up the UI
        PalletCurrent = Instantiate(PalletCurrentPrefab);
        PalletBackup = Instantiate(PalletBackupPrefab);

        PalletCurrent.transform.parent = GetComponent<PlayerColorController>().playerCamera.transform;
        PalletCurrent.transform.localPosition = new Vector3 (0.29F, -0.18F, 0.4F); // These are hardcoded for now (no UI canvas)
        PalletCurrent.transform.localRotation = Quaternion.identity; 

        PalletBackup.transform.parent =  GetComponent<PlayerColorController>().playerCamera.transform;
        PalletBackup.transform.localPosition = new Vector3 (0.35F, -0.18F, 0.4F);
        PalletBackup.transform.localRotation = Quaternion.identity; 

        // Get references to sprite renderers for setting colors 
        PalletCurrentRender = PalletCurrent.GetComponent<SpriteRenderer>();
        PalletBackupRender = PalletBackup.GetComponent<SpriteRenderer>();

        // Set colors on the UI
        updatePalletUI(storedColor, playerColor.currentColor);
    }
	
	void Update () {
		if(InputManager.GetButtonDown(PlayerButton.Swap, inputPlayer)) {
            GameColor temp = playerColor.currentColor;
            playerColor.currentColor = storedColor;
            storedColor = temp;
        }
	}

    public void updatePalletUI (GameColor backup, GameColor cur) {
         PalletCurrentRender.color = ColorState.RGBColors[cur];
         PalletBackupRender.color = ColorState.RGBColors[backup];
    }
}
