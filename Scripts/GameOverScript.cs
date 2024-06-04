using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    public Text pointsText;
    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = "You stopped " + score.ToString() + " Teddy Bears!";
    }

    // public method intended to allow a player to restart
    public void RestartButton()
    {
        SceneManager.LoadScene(1);

    }

    // public method intended to allow a player to return to main menu
    public void ExitButton()
    {
        SceneManager.LoadScene(0);
    }
}
