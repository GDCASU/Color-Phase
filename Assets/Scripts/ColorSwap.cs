using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorSwap : MonoBehaviour
{
    public GameObject playerModel;
    public GameObject crossHair;
    public GameObject[] lights;

    public Material[] redMaterials;
    public Material[] greenMaterials;
    public Material[] blueMaterials;
    public Material[] yellowMaterials;

    public KeyCode colorChangeInput = KeyCode.Mouse0; // left mouse input = 134

    private int colorChangeInputToggle = 0;

    public int currentColor = 0; // The current color of the player

    // Use this for initialization
    void Start()
    {
        // swap materials for color
        // swap lights for color
        SetColor(currentColor);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(colorChangeInput))
        {
            if (colorChangeInputToggle == 0)
            {
                //swap colors and lights
                currentColor = currentColor + 1;
                if (currentColor > 3)
                {
                    currentColor = 0;
                }
                SetColor(currentColor);
                colorChangeInputToggle = 1;
            }
        }
        else
        {
            colorChangeInputToggle = 0; // allows input for the color toggle
        }
    }

    public void SetColor(int color)
    {
        playerModel.GetComponentInParent<Flyingcamera>().gameObject.layer = 20 + color;

        for (int i = 0; i < lights.Length; i++)
            lights[i].SetActive(i == color);
        switch (color)
        {
            case 0:
                playerModel.GetComponent<MeshRenderer>().materials = redMaterials;
                crossHair.GetComponent<SpriteRenderer>().color = new Color(0.5490196f, 0.2614016f, 0.2588235f);
                break;
            case 1:
                playerModel.GetComponent<MeshRenderer>().materials = greenMaterials;
                crossHair.GetComponent<SpriteRenderer>().color = new Color(0.2659206f, 0.5471698f, 0.26068f);
                break;
            case 2:
                playerModel.GetComponent<MeshRenderer>().materials = blueMaterials;
                crossHair.GetComponent<SpriteRenderer>().color = new Color(0.2588235f, 0.4211917f, 0.5490196f);
                break;
            case 3:
                playerModel.GetComponent<MeshRenderer>().materials = yellowMaterials;
                crossHair.GetComponent<SpriteRenderer>().color = new Color(0.5490196f, 0.5487493f, 0.2588235f);
                break;
        }
    }
}
