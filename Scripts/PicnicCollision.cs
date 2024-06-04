using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PicnicCollision : MonoBehaviour
{

    public GameOverScript gameOver;
    public LevelControl currentKillCount;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            Debug.Log("You Lose!");
            GameObject.Find("Player").GetComponent<PlayerController>().state = PlayerController.State.PAUSE;
            var turrets = GameObject.FindGameObjectsWithTag("Structure");
            foreach(GameObject go in turrets) {
                Destroy(go);
            }
            gameOver.Setup(currentKillCount.cumulativeScore); // set game over screen to active with score with points
        }
    }
}
