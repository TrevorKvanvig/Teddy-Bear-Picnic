using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RoundEnd : MonoBehaviour
{

    public float downTime;//3 seconds between rounds
    public float counter;

    [SerializeField]
    public GameObject buyMenuRef;

    public GameObject levelControlRef;
    // Start is called before the first frame update
    void Start()
    {
        levelControlRef = GameObject.Find("Level Manager");
        downTime = 1;
        counter = 0;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponentInChildren<TextMeshProUGUI>().text = "Cleared Wave " + levelControlRef.GetComponent<LevelControl>().currentLevel.ToString();
        counter += Time.deltaTime;
        if (counter > downTime) {
            counter = 0;
            buyMenuRef.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
