using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;
using System;
using TMPro;

public class ControlsManager : MonoBehaviour
{
    public TMP_Text moveRightButtonText;
    public TMP_Text moveLeftButtonText;
    public TMP_Text dropFruitButtonText;
    public TMP_Text saveFruitButtonText;
    public TMP_Text useSaveFruitButtonText;
    //public TMP_InputField moveRightInputField;

    void Start()
    {
        moveRightButtonText.text = GameData.moveRight.ToString();
        if(GameData.moveLeft.ToString() == "A") {
            moveLeftButtonText.text = "Q";
        } else {
            moveLeftButtonText.text = GameData.moveLeft.ToString();
        }
        dropFruitButtonText.text = GameData.dropFruit.ToString();
        saveFruitButtonText.text = GameData.saveFruit.ToString();
        useSaveFruitButtonText.text = GameData.useSaveFruit.ToString();
    }

    public void SwitchToGame()
    {
        SceneManager.LoadScene("Game");
    }

    /*public void ChangeMoveRightKey()
    {
        KeyCode newMoveRightKey;
        if (Enum.TryParse(moveRightInputField.text, out newMoveRightKey))
        {
            // update key in GameData
            GameData.moveRight = newMoveRightKey;
            moveRightButtonText.text = GameData.moveRight.ToString();
        }
        else
        {
            // print error message if unvalid key
            Debug.LogError("Erreur : Touche invalide");
        }
    }*/

    
}
