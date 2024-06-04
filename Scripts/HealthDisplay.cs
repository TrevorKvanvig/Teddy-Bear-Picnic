using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HealthDisplay : MonoBehaviour
{
    [SerializeField]
    public GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<TextMeshProUGUI>().text = "HP: " + player.GetComponent<PlayerController>().hp.ToString() + " / 5";
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text = "HP: " + player.GetComponent<PlayerController>().hp.ToString() + " / 5";
    }
}
