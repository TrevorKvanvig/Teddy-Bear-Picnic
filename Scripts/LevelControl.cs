using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelControl : MonoBehaviour
{
    private GameObject[] enemies;
    public int currentLevel;
    public int currentScore;
    public int cumulativeScore;
    public int scoreThreshold;
    public TextMeshProUGUI scoreText;

    public EnemySpawner waveManager;
    public PlayerController player;

    //currency variables
    public TextMeshProUGUI goldText;
    public int money;

    [SerializeField]
    public GameObject buyMenuRef;

    // Start is called before the first frame update
    void Start()
    {
        currentLevel = 0;
        scoreThreshold = 3;
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        player = GameObject.Find("Player").GetComponent<PlayerController>();
        waveManager = GameObject.Find("Wave Manager").GetComponent<EnemySpawner>();

        money = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentScore >= scoreThreshold)
        {
            currentScore = 0;
            currentLevel += 1;
            if (currentLevel > 10) {
                Debug.Log("You Win");
                //SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
            }

            StartCoroutine(waiter());
            player.hp = 5;
            scoreThreshold += 10;

        }
        
    }

    public void AddScore(int addS)
    {
        cumulativeScore += addS;
        currentScore += addS;
        Debug.Log(currentScore);

    }
    public void addMoney(int add) {
        money += add;
        goldText.text = "Wallet: $" + (money).ToString();
    }
    public void reduceMoney(int sub) {
        money -= sub;
        goldText.text = "Wallet: $" + (money).ToString();
    }

    //public void nextLevel()
    //{



    //}

    IEnumerator waiter()
    {
        //SceneManager.LoadScene("StartMenu", LoadSceneMode.Single);
        yield return new WaitForSecondsRealtime(1);
        scoreText.text = "Level: " + (currentLevel + 1).ToString();

        //player.upgradeWeapon();
        //player.state = PlayerController.State.BUILD;
        waveManager.GetComponent<EnemySpawner>().waveActive = false;//pause spawning for next wave
        var leftover = GameObject.FindGameObjectsWithTag("Enemy");
        player.GetComponent<PlayerController>().state = PlayerController.State.PAUSE;

        //  !!  remove later  !!
        //bandaid fix to despawn extra enemies
        foreach (GameObject go in leftover) {
            Destroy(go);
        }

        buyMenuRef.SetActive(true);
        yield return new WaitForSecondsRealtime(4);
        waveManager.incrementReps();

    }

}
