using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerInput;

public class UI : MonoBehaviour
{
    public Image AbilityCoolDown;
    public Image Ability;
    public Image PreviousAbility;
    public Image Grapple;
    public Image GrappleCooldown;
    public Sprite GreenSprite;
    public Sprite RedSprite;
    public Sprite YellowSprite;
    public Sprite BlueSprite;
    private InputPlayer inputPlayer;
    private ColorState playerColor;
    private PlayerMovement playerMoevement;

    public GameColor storedColor;

    public float coolDown = 5;

    void Awake()
    {
        playerColor = GetComponentInParent<ColorState>();
        playerMoevement = GetComponentInParent<PlayerMovement>();
    }

        // Use this for initialization
        void Start ()
    {
        Green();
    }
	

	void Update ()
    {
        if (InputManager.GetButtonDown(PlayerButton.Swap, inputPlayer))
        {
            GameColor temp = storedColor;
            storedColor = playerColor.currentColor;
            playerColor.currentColor = temp;
        }

        PreviousAbility.color = ColorState.RGBColors[storedColor];

        if (playerColor.currentColor == GameColor.Green)
        {
            Green();
        }
        else if (playerColor.currentColor == GameColor.Red)
        {
            Red();          
        }
        else if (playerColor.currentColor == GameColor.Yellow)
        {
            Yellow();
        }
        else if (playerColor.currentColor == GameColor.Blue)
        {
            Blue();
        }

    }
    public void Green()
    {
        AbilityCoolDown.sprite = GreenSprite;
        Ability.sprite = GreenSprite;
        AbilityCoolDown.fillAmount = 0;
    }
    public void Red()
    {
        AbilityCoolDown.sprite = RedSprite;
        Ability.sprite = RedSprite;

        if (playerMoevement.stuck)
        {
            AbilityCoolDown.fillAmount = 1;
        }
        else
        {
            AbilityCoolDown.fillAmount = 0;
        }
    }
    public void Yellow()
    {
        AbilityCoolDown.sprite = YellowSprite;
        Ability.sprite = YellowSprite;
        if(playerMoevement.grounded==false && playerMoevement.jumpHeld==false)
        {
            AbilityCoolDown.fillAmount = 1;
        }
        else
        {
            AbilityCoolDown.fillAmount = 0;
        }
    }
    public void Blue()
    {
        AbilityCoolDown.sprite = BlueSprite;
        Ability.sprite = BlueSprite;

        if (playerMoevement.jumpsAvailable == 1)
        {
            AbilityCoolDown.fillAmount = (float).5;
        }
        else if (playerMoevement.jumpsAvailable == 0)
        {
            AbilityCoolDown.fillAmount = 1;
        }
        else
        {
            AbilityCoolDown.fillAmount = 0;
        }
    }


}
