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
    public Sprite GreenAbilitySprite;
    public Sprite RedAbilitySprite;
    public Sprite YellowAbilitySprite;
    public Sprite BlueAbilitySprite;
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
        playerColor = gameObject.transform.parent.transform.parent.GetComponentInChildren<ColorState>();
        playerMoevement = gameObject.transform.parent.transform.parent.GetComponentInChildren<PlayerMovement>();
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

        previousColor();

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
        Ability.sprite = GreenAbilitySprite;
        AbilityCoolDown.fillAmount = 0;
    }
    public void Red()
    {
        Ability.sprite = RedAbilitySprite;

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
        Ability.sprite = YellowAbilitySprite;
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
        Ability.sprite = BlueAbilitySprite;

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
    public void previousColor()
    {
        if(storedColor==GameColor.Green)
        {
            PreviousAbility.sprite = GreenSprite;
        }
        if (storedColor == GameColor.Red)
        {
            PreviousAbility.sprite = RedSprite;
        }
        if (storedColor == GameColor.Yellow)
        {
            PreviousAbility.sprite = YellowSprite;
        }
        if (storedColor == GameColor.Blue)
        {
            PreviousAbility.sprite = BlueSprite;
        }
    }


}
