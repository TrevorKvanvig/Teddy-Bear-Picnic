using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation : MonoBehaviour
{
    private Vector3 rotate;
    public GameObject points;

    // Start is called before the first frame update
    void Start()
    {
        rotate = new Vector3(0, 2, 0);
        //points = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(rotate);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            points.GetComponent<ScoreLVL1>().AddScore(1);
            Destroy(this.gameObject);
        }
    }
}
