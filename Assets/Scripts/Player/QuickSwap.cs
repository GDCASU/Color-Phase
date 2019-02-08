using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerInput;
[RequireComponent(typeof(ColorState))]
[RequireComponent(typeof(PlayerColorController))]
public class QuickSwap : MonoBehaviour {
    /*
    Allows the player to swap between a stored color and their currect color
     */
    public GameObject PalletCurrentPrefab;
    public GameObject PalletBackupPrefab;
    private ColorState playerColor;
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
        
        // Add it to swap events
        playerColor.onSwap+=updatePalletUI;
    }
	
	void Update () {
		if(InputManager.GetButtonDown(PlayerButton.Swap, inputPlayer)) {
            GameColor temp = storedColor; 
            storedColor = playerColor.currentColor;
            playerColor.currentColor = temp;
        }
	}

    public void updatePalletUI (GameColor prev, GameColor next) {
         PalletCurrentRender.color = ColorState.RGBColors[next];
         PalletBackupRender.color = ColorState.RGBColors[storedColor];
    }
}
