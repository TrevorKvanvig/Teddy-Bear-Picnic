using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreGUI : MonoBehaviour
{
    public TextMeshProUGUI gameText;
   
    public TextMeshProUGUI enemyText;
    public TextMeshProUGUI score;


    public int currentScore;
    private int remaining;
    public int currScore = 0;

    public UnityEngine.Events.UnityEvent trigger;
    bool killedAllEnemies = false;
    public int enemiesLeft = 0;


    // Start is called before the first frame update
    void Start()
    {
        currScore = 0;
        currentScore = 0;
        enemiesLeft = 3;
        remaining = 3;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;
        //enemyText.text = "Enemies Left: " + enemiesLeft.ToString();
        
        score.text = "Timer: " + currScore.ToString();
        currScore = currScore + 1;

        if (currentScore == 3)
        {
            
            
            gameText.text = "Nice!! Head to the Door!";

            enemyText.text = "";

            score.text = "";
        }
        else
        {

            gameText.text = "Items Collected: " + currentScore.ToString() + "\n" +
            "Remaining: " + remaining.ToString();

            enemyText.text = "Enemies Left: " + enemiesLeft.ToString();
   
            score.text = "Timer: " + currScore.ToString();
            currScore = currScore + 1;


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
