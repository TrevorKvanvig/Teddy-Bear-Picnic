using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyControl2 : MonoBehaviour
{
    public enum State
    {
        BEGIN,
        WALK,
        CHASE
    }

    // health variables 
    public float health;
    private float maxHealth;
    private Text healText;
    private Image healBar;

    // state variables
    private LevelControl levelManager; // for adding score
    public State currentState;//currently executing state

    // assets 
    [SerializeField]
    public GameObject player;//reference to player
    public GameObject explostion;
    private NavMeshAgent agent;
    Animator anim;


    private void Start()
    {
        //initializing enemy health and associated GUI
        health = 100.0f;
        maxHealth = 100.0f;
        healText = transform.Find("EnemyCanvas").Find("HealthBarText").GetComponent<Text>();
        healBar = transform.Find("EnemyCanvas").Find("MaxHealthBar").Find("HealthBar").GetComponent<Image>();

        //initializing  AI variables and associated components 
        player = GameObject.Find("Picnic");
        levelManager = GameObject.Find("Level Manager").GetComponent<LevelControl>();
        agent = GetComponent<NavMeshAgent>();
        currentState = State.CHASE;//initial state
        anim = GetComponent<Animator>();

        //nodeIndex = 0;//initialize current path node
        ////agent.SetDestination(path.GetComponent<PathData>().pathNodes[nodeIndex].transform.position);//initialize target
        //stareDistance = 30f;
        //chaseDistance = 15f;

    }

    // Update is called once per frame
    void Update()
    {

        if (levelManager.currentLevel == 1) // every other level increase speed by 1
        {
            agent.speed = 100.0f;
            anim.SetBool("isRunning", true);
        }

        healText.text = health.ToString();
        healBar.fillAmount = health / maxHealth;

        //universal state change to manic state when majority of coins are collected
        //if (numCoins <= maxCoins / 2) {
        //    if (currentState != State.MANIC) {
        //        currentState = State.MANIC;
        //        GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 0f, 0f, 1f));
        //    }
        //}

        switch (currentState)
        {
            //Beginning State
            //case State.PATROL:
            //
            //    //transition to stare state if the player is close
            //    if (Vector3.Distance(transform.position, player.transform.position) < stareDistance) {
            //        agent.ResetPath();//stop moving along path
            //        currentState = State.STARE;
            //    }
            //
            //    break;
            ////Stare State
            //case State.STARE:
            //    transform.LookAt(player.transform.position);
            //
            //    //transition to chase state if the player is close
            //    if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance) {
            //        agent.SetDestination(player.transform.position);//target player
            //        currentState = State.CHASE;
            //    }
            //    //transition to patrol state if the player is far
            //    if (Vector3.Distance(transform.position, player.transform.position) >= stareDistance) {
            //        agent.SetDestination(path.GetComponent<PathData>().pathNodes[nodeIndex].transform.position);//initialize target
            //        currentState = State.PATROL;
            //    }
            //
            //    break;
            ////Chase State
            case State.CHASE:
                //target player
                agent.SetDestination(player.transform.position);





                //transition to stare state if the player is far
                //if (Vector3.Distance(transform.position, player.transform.position) >= chaseDistance) {
                //    agent.ResetPath();//stop moving along path
                //    currentState = State.STARE;
                //}

                break;
            //Manic State
            //case State.MANIC:
            //    agent.SetDestination(player.transform.position);//target player
            //
            //    break;
            default:
                //error state

                break;
        }
        if (health <= 1) {
            // Play Audio
            GetComponent<AudioSource>().Play();
            levelManager.AddScore(1);
            levelManager.addMoney(1);
            Instantiate(explostion, transform.position, transform.rotation);

            Destroy(explostion);
            Destroy(this.gameObject);

        }
    }
}

//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;
//using UnityEngine.UI;

//public class EnemyControl : MonoBehaviour
//{
//    public enum State
//    {
//        PATROL,
//        STARE,
//        CHASE,
//        MANIC
//    }



//    //copied from chasing
//    public float health;
//    private float maxHealth;
//    public GameObject explostion;
//    private Text healText;
//    private Image healBar;
//    private LevelControl levelManager; // for adding score

//    public State currentState;//currently executing state
//    [SerializeField]
//    public GameObject player;//reference to player

//    //pathing variables
//    [SerializeField]
//    //public GameObject path;//reference to path for patrol state
//    public NavMeshAgent agent;
//    Animator anim;

//    //public int nodeIndex;//index of current node along path

//    //public float stareDistance;//maximum distance for stare state
//    //public float chaseDistance;//maximum distance for chase state
//    //
//    //public int maxCoins;//total number of coins
//    //public int numCoins;//current number of coins

//    private void Start()
//    {
//        //copied from chasing.cs
//        health = 100.0f;
//        maxHealth = 100.0f;
//        healText = transform.Find("EnemyCanvas").Find("HealthBarText").GetComponent<Text>();
//        healBar = transform.Find("EnemyCanvas").Find("MaxHealthBar").Find("HealthBar").GetComponent<Image>();

//        //init AI vars
//        player = GameObject.Find("Picnic");
//        levelManager = GameObject.Find("Level Manager").GetComponent<LevelControl>();

//        agent = GetComponent<NavMeshAgent>();
//        currentState = State.CHASE;//initial state

//        // Eli - enemy animation
//        anim = GetComponent<Animator>();

//        //nodeIndex = 0;//initialize current path node
//        ////agent.SetDestination(path.GetComponent<PathData>().pathNodes[nodeIndex].transform.position);//initialize target
//        //stareDistance = 30f;
//        //chaseDistance = 15f;

//    }

//    // Update is called once per frame
//    void Update()
//    {
//        if (levelManager.currentLevel >= 0)
//        {
//            Debug.Log(levelManager.currentLevel);
//            anim.SetBool("isRunning", true);
//        }

//        //numCoins = GameObject.Find("Coins").GetComponent<Transform>().childCount;//get current coins

//        healText.text = health.ToString();
//        healBar.fillAmount = health / maxHealth;

//        //universal state change to manic state when majority of coins are collected
//        //if (numCoins <= maxCoins / 2) {
//        //    if (currentState != State.MANIC) {
//        //        currentState = State.MANIC;
//        //        GetComponent<Renderer>().material.SetColor("_Color", new Color(1f, 0f, 0f, 1f));
//        //    }
//        //}

//        switch (currentState)
//        {
//            //Patrol State
//            //case State.PATROL:
//            //
//            //    //transition to stare state if the player is close
//            //    if (Vector3.Distance(transform.position, player.transform.position) < stareDistance) {
//            //        agent.ResetPath();//stop moving along path
//            //        currentState = State.STARE;
//            //    }
//            //
//            //    break;
//            ////Stare State
//            //case State.STARE:
//            //    transform.LookAt(player.transform.position);
//            //
//            //    //transition to chase state if the player is close
//            //    if (Vector3.Distance(transform.position, player.transform.position) < chaseDistance) {
//            //        agent.SetDestination(player.transform.position);//target player
//            //        currentState = State.CHASE;
//            //    }
//            //    //transition to patrol state if the player is far
//            //    if (Vector3.Distance(transform.position, player.transform.position) >= stareDistance) {
//            //        agent.SetDestination(path.GetComponent<PathData>().pathNodes[nodeIndex].transform.position);//initialize target
//            //        currentState = State.PATROL;
//            //    }
//            //
//            //    break;
//            ////Chase State
//            case State.CHASE:
//                agent.SetDestination(player.transform.position);//target player

//                //transition to stare state if the player is far
//                //if (Vector3.Distance(transform.position, player.transform.position) >= chaseDistance) {
//                //    agent.ResetPath();//stop moving along path
//                //    currentState = State.STARE;
//                //}

//                break;
//            //Manic State
//            //case State.MANIC:
//            //    agent.SetDestination(player.transform.position);//target player
//            //
//            //    break;
//            default:
//                //error state

//                break;
//        }
//        if (health <= 1)
//        {
//            // Play Audio
//            GetComponent<AudioSource>().Play();
//            levelManager.AddScore(1);
//            levelManager.addMoney(1);
//            Instantiate(explostion, transform.position, transform.rotation);

//            Destroy(this.gameObject);

//        }
//    }

//    //IEnumerator destroyEvent(GameObject enemy) {
//    //    //Play Audio
//    //    GetComponent<AudioSource>().Play();

//    //    //Wait until it's done playing
//    //    while (GetComponent<AudioSource>().isPlaying)
//    //        yield return null;


//    //    levelManager.AddScore(1);
//    //    Instantiate(explostion, transform.position, transform.rotation);

//    //    Destroy(enemy);
//    //}
//}