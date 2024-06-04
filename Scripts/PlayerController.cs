using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerController : MonoBehaviour
{
    public GameOverScript gameOver;
    public LevelControl currentKillCount;

    public GameObject currentWeapon;//reference to weapon object
    public GameObject turretPrefab;//prefab to test turret placement
    public GameObject trapPrefab;//prefab for trap placement
    public GameObject turret;
    public float horizontalBoundary;
    public float verticalBoundary;

    public string buildType;

    [SerializeField]
    public GameObject buyMenuRef;

    char inputType;//char for input type. k=keyboard, c=controller

    float moveSpeed;//scalar movement speed
    float moveX;//current x speed
    float moveZ;//current y speed

    // for player animation
    Animator anim;
    bool walking = false;

    //directional input check variables
    int up;
    int down;
    int left;
    int right;

    public int hp = 5;


    //weapons
    [SerializeField]
    public GameObject smg;
    [SerializeField]
    public GameObject shot;
    [SerializeField]
    public GameObject auto;


    //enum to control player state
    //
    //restrict movement in menu/pause states
    public enum State:int {
        NORMAL,
        MELEE,
        PAUSE,
        BUILD,
    }

    public State state;//current player state


    // Start is called before the first frame update
    void Start()
    {
        //initialize variables
        inputType = 'k';
        moveSpeed = 9f;
        moveX = 0f;
        moveZ = 0f;
        up = 0;
        down = 0;
        left = 0;
        right = 0;
        state = State.NORMAL;
        currentWeapon = GameObject.FindGameObjectWithTag("Weapon");
        turret = null;
        buildType = "turret";

        // Eli - for player boundary. Intended to prevent player from going offscreen.
        horizontalBoundary = 20.0f;
        verticalBoundary = 11.5f;

        // Eli - player animation
        anim = GetComponent<Animator>();

        //buyMenuRef = GameObject.Find("Buy Menu");
       
    }

    // Update is called once per frame
    void Update() {
//        
        switch (state) {
            case State.NORMAL:
                //
                //state for normal movement, attacking
                //

                if (inputType == 'k') {
                    //player bounadry 
                    if (transform.position.x >= horizontalBoundary) {
                        transform.position = new Vector3(horizontalBoundary, transform.position.y, transform.position.z);
                    }

                    if (transform.position.x <= -horizontalBoundary)   //|| transform.position.x <= -22)
                    {
                        transform.position = new Vector3(-horizontalBoundary, transform.position.y, transform.position.z);
                    }

                    if (transform.position.z >= verticalBoundary) {
                        transform.position = new Vector3(transform.position.x, transform.position.y, verticalBoundary);
                    }

                    if (transform.position.z <= -verticalBoundary - 2) {
                        transform.position = new Vector3(transform.position.x, transform.position.y, -verticalBoundary - 2);
                    }


                    //keyboard input
                    //keyboard inputs
                    up = Convert.ToInt32(Input.GetKey(KeyCode.W));
                    down = -Convert.ToInt32(Input.GetKey(KeyCode.S));
                    left = -Convert.ToInt32(Input.GetKey(KeyCode.A));
                    right = Convert.ToInt32(Input.GetKey(KeyCode.D));

                    //calculate x and y move directions
                    moveZ = (up + down) * moveSpeed;
                    moveX = (right + left) * moveSpeed;

                    //normalize diagonal speed
                    if (moveX != 0 && moveZ != 0) {
                        moveX *= Convert.ToSingle(Math.Sin(45));
                        moveZ *= Convert.ToSingle(Math.Sin(45));
                    }

                    if (Input.GetMouseButtonDown(0) && currentWeapon.gameObject.GetComponent<WeaponController>().attacking == false) {
                        currentWeapon.gameObject.GetComponent<WeaponController>().attacking = true;
                        //GetComponent<AudioSource>().Play();

                    }

                    //anim.SetBool("isWalking", false);
                    if (hp <= 0) {
                        Debug.Log("You Lose!");
                        state = State.PAUSE;
                        var turrets = GameObject.FindGameObjectsWithTag("Structure");
                        foreach (GameObject go in turrets) {
                            Destroy(go);
                        }
                        gameOver.Setup(currentKillCount.cumulativeScore); // set game over screen to active with score with points
                    }


                }

                Vector3 direction = new Vector3(moveX * Time.deltaTime, 0f, moveZ * Time.deltaTime);


                if (direction != Vector3.zero)
                {
                    transform.Translate(direction, Space.World);
                    anim.SetBool("isWalking", true);
                }
                else
                {
                    anim.SetBool("isWalking", false);

                }


                //2d movement


                break;
            case State.PAUSE:
                //do nothing
                break;
            case State.BUILD:
                //test turret placement
                if (turret == null) {
                    if (buildType == "turret") {
                        turret = Instantiate(turretPrefab);
                    } else if(buildType == "trap") {
                        turret = Instantiate(trapPrefab);
                    }
                } else {
                    if (turret.GetComponent<Collider>().isTrigger == false) {
                        buyMenuRef.transform.GetChild(0).gameObject.SetActive(true);
                        turret = null;
                        state = State.PAUSE;
                    }
                }
                break;
            default:
                break;
        }

    }

    public void upgradeWeapon() {
        var weapon = GetComponentInChildren<WeaponController>().type;
        if (weapon == WeaponController.WeaponType.MELEE) {
            //GetComponentInChildren<WeaponController>().type = WeaponController.WeaponType.SEMI;
            
        } else if (weapon == WeaponController.WeaponType.SEMI) {
            //GetComponentInChildren<WeaponController>().type = WeaponController.WeaponType.AUTO;
            currentWeapon.transform.parent.gameObject.SetActive(false);
            smg.SetActive(true);
            currentWeapon = smg.transform.GetChild(0).gameObject;
        } else if (weapon == WeaponController.WeaponType.AUTO) {
            //GetComponentInChildren<WeaponController>().type = WeaponController.WeaponType.SHOT;
            currentWeapon.transform.parent.gameObject.SetActive(false);
            shot.SetActive(true);
            currentWeapon = shot.transform.GetChild(0).gameObject;
        } else if (weapon == WeaponController.WeaponType.SHOT) {
            //GetComponentInChildren<WeaponController>().type = WeaponController.WeaponType.AUTOSHOT;
            currentWeapon.transform.parent.gameObject.SetActive(false);
            auto.SetActive(true);
            currentWeapon = auto.transform.GetChild(0).gameObject;
        }
    }
}
