using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Treebound1 : MonoBehaviour
{
    public float North1;
    public float South1;
    public float East1;
    public float West1;
    public GameObject player;


    // Start is called before the first frame update
    void Start()
    {
    North1 = 5f;
    South1 = -5f;
    East1 = 5f;
    West1 = -5f;
    player = GameObject.FindWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
    //NorthWest
    if (player.transform.position.z >= player.transform.position.x + North1)
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.x + North1);
    }
    //SouthEast
    if (player.transform.position.z <= player.transform.position.x + South1)
    {
        player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.x + South1);
    }
    //NorthEast
    if (player.transform.position.x >= -player.transform.position.z + East1)
    {
        player.transform.position = new Vector3(-player.transform.position.z + East1, player.transform.position.y, player.transform.position.z);
    }
    //SouthWest
    if (player.transform.position.x <= -player.transform.position.z + West1)
    {
        player.transform.position = new Vector3(-player.transform.position.z + West1, player.transform.position.y, player.transform.position.z);
    }
   
       
    }
}
