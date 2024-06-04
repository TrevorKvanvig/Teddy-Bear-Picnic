using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    GameObject cursor;//reference to cursor object


    // Start is called before the first frame update
    void Start()
    {
        cursor = GameObject.Find("Cursor");
    }

    // Update is called once per frame
    void Update()
    {
        //get position of cursor, lock y rotation
        Vector3 lookPos = new Vector3(cursor.transform.position.x, transform.position.y, cursor.transform.position.z);
        //point player toward cursor
        transform.LookAt(lookPos);
    }
}
