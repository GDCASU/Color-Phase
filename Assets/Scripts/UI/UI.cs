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
    public Dictionary<GameColor, Sprite> SpriteColors;
    public Dictionary<GameColor, Sprite> SpriteAbilityColors;
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
    private PlayerMovement playerMovement;
    private bool hasQuickswap=false;
    private QuickSwap quickSwap;

    public GameColor storedColor;

    public float coolDown = 5;

    void Awake()
    {
        SpriteColors = new Dictionary<GameColor, Sprite>
        {
            {GameColor.Red, RedSprite},
            {GameColor.Green, GreenSprite },
            {GameColor.Blue, BlueSprite },
            {GameColor.Yellow, YellowSprite },
        };
        SpriteAbilityColors = new Dictionary<GameColor, Sprite>
        {
            {GameColor.Red, RedAbilitySprite},
            {GameColor.Green, GreenAbilitySprite },
            {GameColor.Blue, BlueAbilitySprite },
            {GameColor.Yellow, YellowAbilitySprite },
        };
        playerColor = gameObject.transform.parent.transform.parent.GetComponentInChildren<ColorState>();
        playerMovement = gameObject.transform.parent.transform.parent.GetComponentInChildren<PlayerMovement>();
    }

        // Use this for initialization
        void Start ()
    {
        if (gameObject.transform.parent.transform.parent.GetComponentInChildren<QuickSwap>().isActiveAndEnabled)
        {
            hasQuickswap = true;
            quickSwap = gameObject.transform.parent.transform.parent.GetComponentInChildren<QuickSwap>();
            var tempColor = PreviousAbility.color;
            tempColor.a = 1;
            PreviousAbility.color = tempColor;
        }
        else
        {
            var tempColor = PreviousAbility.color;
            tempColor.a = 0;
            PreviousAbility.color = tempColor;
        }

        Green();
    }


    void Update()
    {
        if(hasQuickswap)
        {
            storedColor = quickSwap.storedColor;
            previousColor();
        }
        Ability.sprite = SpriteAbilityColors[playerColor.currentColor];
        currentColor();

    }
    public void currentColor()
    {
        switch (playerColor.currentColor)
        {
            case GameColor.Red:
                Red();
                break;
            case GameColor.Yellow:
                Yellow();
                break;
            case GameColor.Blue:
                Blue();
                break;
            default:
                Green();
                break;
        }
    }
    public void Green()
    {
        AbilityCoolDown.fillAmount = 0;
    }
    public void Red()
    {

        if (playerMovement.stuck && !playerMovement.detached)
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
        if(playerMovement.grounded==false && playerMovement.jumpHeld==false)
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

        if (playerMovement.jumpsAvailable == 1)
        {
            AbilityCoolDown.fillAmount = (float).5;
        }
        else if (playerMovement.jumpsAvailable == 0)
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
        PreviousAbility.sprite = SpriteColors[storedColor];
    }
}
