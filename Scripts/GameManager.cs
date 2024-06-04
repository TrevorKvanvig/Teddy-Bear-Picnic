using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI gameText;
    public GameObject player;
    public int currentScore;
    private int scoreGoal;
    private int points;
    private GameObject[] enemies;

    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        points = 0;
        scoreGoal = 5;
    }

    // Update is called once per frame
    void Update()
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

        if (currentScore == scoreGoal && enemies.Length == 0)
        {
            gameText.fontSize = 100;
            gameText.text = "Nice! You Win!";
        }

        else if (player.GetComponent<FirstPersonCtrl>().hit == true)
        {
            gameText.fontSize = 100;
            gameText.text = "Hit! Game Over!";

        }
        else
        {

            gameText.text = "Enemies Remaining: " + enemies.Length + "\n" +
           "Items Collected: " + currentScore.ToString() + "\n" +
            "Current Score: " + points;
        }
    }

    public void AddScore(int addS)
    {
        currentScore += 1;
        points += (15 + enemies.Length - 5);

    }
}
