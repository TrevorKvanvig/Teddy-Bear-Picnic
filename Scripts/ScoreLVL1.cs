using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreLVL1 : MonoBehaviour
{
    public TextMeshProUGUI gameText;
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI timerText;
    public int currScore = 0;
    public int currentScore;
    private int remaining;
    public UnityEngine.Events.UnityEvent trigger;
    private GameObject[] enemies;


    // Start is called before the first frame update
    void Start()
    {
        currentScore = 0;
        currScore = 0;
        remaining = 3;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");

    }

    // Update is called once per frame
    void Update()
    {

        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        currScore = currScore + 1;
        timerText.text = "Timer: " + currScore; 

        if (currentScore == 3 && enemies.Length == 0)
        {
            
            gameText.fontSize = 48;
            enemyText.text = "";
            gameText.text = "Nice!! Head to the Door!";
        }
        else
        {
            enemyText.text = "Enemies Remaining: " + enemies.Length.ToString();
            gameText.text = "Items Collected: " + currentScore.ToString() + "\n" +
            "Remaining: " + remaining.ToString();


        }
    }

    public void AddScore(int addS)
    {
        currentScore += 1;
        remaining -= 1;

        if (currentScore == 1)
        {
            trigger.Invoke();
        }


    }

}
