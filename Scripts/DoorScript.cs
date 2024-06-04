using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DoorScript : MonoBehaviour
{
    public Transform door;
    public Vector3 closedPosition = new Vector3(0f, 1f, 1f);
    public Vector3 openPosition = new Vector3(0f, 3f, 1f);
    public float openSpeed = 2;
    public UnityEngine.Events.UnityEvent trigger;
    public bool enabled1;

    private bool open = false;
    public GameObject manager;
    private GameObject[] enemies;
    public int scence;
    

    // Update is called once per frame
    void Update()
    {
        
        if (open)
        {
            door.position = Vector3.Lerp(door.position, openPosition, Time.deltaTime * openSpeed);
            

        }
        else
        {
            door.position = Vector3.Lerp(door.position, closedPosition, Time.deltaTime * openSpeed);

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        if (manager.GetComponent<ScoreLVL1>().currentScore == 3 && enemies.Length == 0)
        {
            enabled1 = true;
        }

        if (other.gameObject.name == "Player" && enabled1)
        {

            openDoor();
            trigger.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {

            closeDoor();
        }
    }

    public void closeDoor()
    {
        open = false;

    }

    public void openDoor()
    {
        open = true;

    }

}
