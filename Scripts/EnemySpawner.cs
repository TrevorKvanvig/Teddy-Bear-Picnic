using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    //adding to increase spawn radius/enemy amount
    int spawnReps;
    float spawnRadius;

    //bool to limit spawn between rounds
    public bool waveActive = true;

    [SerializeField]
    private GameObject TeddybearBasic;
    [SerializeField]
    private float TeddybearBasicTiming;
    // Start is called before the first frame update
    void Start()
    {
        //adding to increase spawn radius/enemy amount
        spawnReps = 1;
        spawnRadius = 20f;
        StartCoroutine(spawnTB(TeddybearBasicTiming, TeddybearBasic));
    }

   

    private IEnumerator spawnTB(float timing, GameObject TB)
    {
       
        yield return new WaitForSeconds(timing);
        if (waveActive) {
            for (int i = 0; i < spawnReps; i++) {
                GameObject newEnemy = Instantiate(TB, new Vector3(Random.Range(-5f, 5f), Random.Range(-5f, 5f), 0.1f), Quaternion.identity);
                var angle = Random.Range(0, 360);
                newEnemy.transform.position = new Vector3(spawnRadius * Mathf.Cos(angle), newEnemy.transform.position.y, spawnRadius * Mathf.Sin(angle));
            }
        }
        StartCoroutine(spawnTB(timing, TB));
    }

    // Just wondering, what is this for? - Eli
    public void incrementReps() {
       spawnReps++;
    }
}
