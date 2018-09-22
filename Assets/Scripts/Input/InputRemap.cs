using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * Author:      Zachary Schmalz
 * Version:     1.0.0
 * Date:        September 19, 2018
 */

/// <summary>
/// The class handles the display and remapping of input controls
/// </summary>
public class InputRemap : MonoBehaviour
{
    // Public class variables
    public GameObject controlPrefab;
    public Text playerText;
    public Text inputText;
    public GameObject axisPanel;
    public GameObject buttonPanel;
    public Color neutralColor;
    public Color selectedColor;

    // Private class variables
    private InputManager input;
    private List<GameObject> controls;
    private int currentSelected;
    private int currentPlayer;
    private bool isRemapping;
    private float leftVertical, leftHorizontal;
    private bool up, right, down, left;
    private InputManager.InputMethod previousMethod;

    // Private class properties
    /// <summary>
    /// Returns the current selected control in the controls list
    /// </summary>
    private int CurrentSelected { get { return currentSelected; } set { currentSelected = Mathf.Clamp(value, 0, controls.Count - 1); } }
    /// <summary>
    /// Returns the Player instance class of the current player
    /// </summary>
    private Player CurrentPlayer { get { return input.players[currentPlayer].GetComponent<Player>(); } }

    void Start ()
    {
        input = InputManager.singleton;
        controls = new List<GameObject>() { playerText.gameObject, inputText.gameObject };
        currentPlayer = 0;
        UpdateText();
        previousMethod = CurrentPlayer.InputMethod;
        isRemapping = false;
        CurrentSelected = 0;
        Select();
    }
	
	void Update ()
    {
        // Update the text in the event of an input change
        if (previousMethod != CurrentPlayer.InputMethod)
        {
            UpdateText();
            previousMethod = CurrentPlayer.InputMethod;
        }

        UpdateInput();

        // Do not update anything while controls are being remapped
        if (!isRemapping)
        {
            // Current player text
            if (CurrentSelected == 0)
            {
                if (leftHorizontal != 0 || leftVertical != 0 || right || down)
                {
                    Deselect();
                    if (leftHorizontal == 1 || right)
                        CurrentSelected++;
                    else if (leftVertical == -1 || down)
                        CurrentSelected += 2;
                    Select();
                }
            }

            // Input method text
            else if (CurrentSelected == 1)
            {
                if (leftHorizontal != 0 || leftVertical != 0 || left || down)
                {
                    Deselect();
                    if (leftHorizontal == -1 || left)
                        CurrentSelected--;
                    else if (leftVertical == -1 || down)
                        CurrentSelected++;
                    Select();
                }
            }

            // Control text
            else
            {
                if (leftVertical != 0 || down || up)
                {
                    Deselect();
                    if (leftVertical == -1 || down)
                        CurrentSelected++;
                    else if (leftVertical == 1 || up)
                        CurrentSelected--;
                    Select();
                }
            }

            // When the player selects a player, input method, or control to change, start the respective coroutine
            if (input.GetButtonDown("A", CurrentPlayer))
            {
                input.ResetButton("A", CurrentPlayer);

                if (CurrentSelected == 0)
                    StartCoroutine(SwitchPlayer());
                else if (CurrentSelected == 1)
                    StartCoroutine(SwitchInput());
                else
                    StartCoroutine(RemapControl());
            }
        }
	}

    // Change the text color of the currently selected control
    private void Select()
    {
        foreach (Text t in controls[CurrentSelected].GetComponentsInChildren<Text>())
            t.color = selectedColor;
    }

    // Change the text color of the deselected control
    private void Deselect()
    {
        foreach (Text t in controls[CurrentSelected].GetComponentsInChildren<Text>())
            t.color = neutralColor;
    }

    // Coroutine for remapping controls
    IEnumerator RemapControl()
    {
        Text[] text = controls[CurrentSelected].GetComponentsInChildren<Text>();

        isRemapping = true;
        Color c = controls[CurrentSelected].GetComponentInChildren<Image>().color;
        XboxController.XboxButton button = XboxController.XboxButton.None;
        KeyCode key = KeyCode.None;
        text[1].text = "";

        Player player = CurrentPlayer;
        if (player.InputMethod == InputManager.InputMethod.Keyboard)
        {
            while ((key = input.GetNextKeyboardButton()) == KeyCode.None)
            {
                c.a = Mathf.Abs(Mathf.Sin(Time.time * 3));
                controls[CurrentSelected].GetComponentInChildren<Image>().color = c;
                yield return new WaitForEndOfFrame();
            }
            if (key != KeyCode.None)
            {
                input.RemapKeyboardButton(text[0].text, key, player);
                text[1].text = key.ToString();
            }
        }

        else if (player.InputMethod == InputManager.InputMethod.XboxController)
        {
            while ((button = input.GetNextXboxButton()) == XboxController.XboxButton.None)
            {
                c.a = Mathf.Abs(Mathf.Sin(Time.time * 3));
                controls[CurrentSelected].GetComponentInChildren<Image>().color = c;
                yield return new WaitForEndOfFrame();
            }
            if (button != XboxController.XboxButton.None)
            {
                input.RemapXboxButton(text[0].text, button, player);
                text[1].text = button.ToString();
            }
        }

        input.ResetButton(button, player);
        c.a = 0;
        controls[CurrentSelected].GetComponentInChildren<Image>().color = c;
        isRemapping = false;
    }

    // Coroutine for selecting the current players input mapping
    IEnumerator SwitchPlayer()
    {
        isRemapping = true;
        Color color = playerText.color;
        color.a = 0;

        Player player = CurrentPlayer;
        int previousPlayer = currentPlayer;

        while(input.GetButton("A", player) == false)
        {
            color.a = Mathf.Abs(Mathf.Sin(Time.time * 3));
            playerText.color = color;

            if (leftHorizontal == -1 || left)
            {
                if (currentPlayer == 0)
                    currentPlayer = input.players.Count - 1;
                else
                    currentPlayer--;
                playerText.text = "Player " + (currentPlayer + 1);
            }

            else if (leftHorizontal == 1 || right)
            {
                if (currentPlayer == input.players.Count - 1)
                    currentPlayer = 0;
                else
                    currentPlayer++;
                playerText.text = "Player " + (currentPlayer + 1);
            }

            yield return new WaitForEndOfFrame();
        }

        color.a = 1;
        playerText.color = color;
        isRemapping = false;
        input.ResetButton("A", player);

        if (previousPlayer != currentPlayer)
            UpdateText();
    }

    // Coroutine for switching the input method
    IEnumerator SwitchInput()
    {
        isRemapping = true;
        Color color = inputText.color;
        color.a = 0;

        Player player = CurrentPlayer;
        int previousMethod = (int)player.InputMethod;
        int newMethod = previousMethod;

        while (input.GetButton("A", player) == false)
        {
            color.a = Mathf.Abs(Mathf.Sin(Time.time * 3));
            inputText.color = color;

            if (previousMethod == (int)InputManager.InputMethod.Keyboard && input.ControllersConnected == 0) { }
            else
            {
                if (leftHorizontal == -1 || left)
                    newMethod--;
                else if (leftHorizontal == 1 || right)
                    newMethod++;
            }

            newMethod = Mathf.Clamp(newMethod, 0, (int)Enum.GetValues(typeof(InputManager.InputMethod)).Cast<InputManager.InputMethod>().Max());
            inputText.text = "Input Type: " + ((InputManager.InputMethod)newMethod).ToString();
            yield return new WaitForEndOfFrame();
        }

        color.a = 1;
        inputText.color = color;
        isRemapping = false;
        input.ResetButton("A", player);

        if (previousMethod != newMethod)
        {
            player.InputMethod = (InputManager.InputMethod)newMethod;
            UpdateText();
        }
    }

    // Updates canvas text
    private void UpdateText()
    {
        foreach(GameObject o in controls)
            if (o != playerText.gameObject && o != inputText.gameObject)
                Destroy(o);

        controls.Clear();
        controls.Add(playerText.gameObject);
        controls.Add(inputText.gameObject);

        playerText.text = "Player " + (currentPlayer + 1);
        inputText.text = "Input Type: " + CurrentPlayer.InputMethod.ToString();

        Player player = CurrentPlayer;

        if (player.InputMethod == InputManager.InputMethod.Keyboard)
            UpdateKeyboardControlText();
        else if (player.InputMethod == InputManager.InputMethod.XboxController)
            UpdateXboxControlText();
    }

    // Updates the text on the canvas with the axis and button keys/values of the current player keyboard input mapping
    private void UpdateKeyboardControlText()
    {
        List<Dictionary<string, KeyCode>> keyboardDictionary = input.GetKeyboardDictionary(currentPlayer + 1);
        int j = 0;
        for (int i = 0; i < keyboardDictionary.Count; i++)
        {
            foreach(KeyValuePair<string, KeyCode> kp in keyboardDictionary[i])
            {
                controls.Add(Instantiate(controlPrefab, i == 0 ? axisPanel.transform : buttonPanel.transform));
                Text[] text = controls[j + 2].GetComponentsInChildren<Text>();
                for (int k = 0; k < text.Length; k++)
                {
                    if (k == 0)
                        text[k].text = kp.Key.ToString();

                    else if (k == 1)
                        text[k].text = kp.Value.ToString();

                    text[k].color = neutralColor;
                }
                j++;
            }
        }
    }

    // Updates the text on the canvas with the axis and button keys/values of the current player xbox controller input mapping 
    private void UpdateXboxControlText()
    {
        Dictionary<string, XboxController.XboxAxis> xboxAxisDictionary = input.GetXboxAxisDictionary(currentPlayer + 1);
        Dictionary<string, XboxController.XboxButton> xboxButtonDictionary = input.GetXboxButtonDictionary(currentPlayer + 1);

        int i = 0;
        foreach(KeyValuePair<string, XboxController.XboxAxis> kp in xboxAxisDictionary)
        {
            controls.Add(Instantiate(controlPrefab, axisPanel.transform));
            Text[] text = controls[i + 2].GetComponentsInChildren<Text>();
            for (int j = 0; j < text.Length; j++)
            {
                if (j == 0)
                    text[j].text = kp.Key.ToString();
                else if (j == 1)
                    text[j].text = kp.Value.ToString();

                text[j].color = neutralColor;
            }
            i++;
        }

        foreach (KeyValuePair<string, XboxController.XboxButton> kp in xboxButtonDictionary)
        {
            controls.Add(Instantiate(controlPrefab, buttonPanel.transform));
            Text[] text = controls[i + 2].GetComponentsInChildren<Text>();
            for (int j = 0; j < text.Length; j++)
            {
                if (j == 0)
                    text[j].text = kp.Key.ToString();
                else if (j == 1)
                    text[j].text = kp.Value.ToString();

                text[j].color = neutralColor;
            }
            i++;
        }
    }

    // Update input values from the current player
    private void UpdateInput()
    {
        Player player = CurrentPlayer;

        leftHorizontal = input.GetAxisDown("LeftHorizontal", player);
        leftVertical = input.GetAxisDown("LeftVertical", player);
        up = input.GetButtonDown("Up", player);
        right = input.GetButtonDown("Right", player);
        down = input.GetButtonDown("Down", player);
        left = input.GetButtonDown("Left", player);
    }
}