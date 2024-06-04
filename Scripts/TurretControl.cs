using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretControl : MonoBehaviour
{
    [SerializeField]
    GameObject projectilePrefab;

    public GameObject target;

    public enum State {
        PLACE,
        ACTIVE,
        PAUSE,
    }

    public State currentState;
    public State previousState;

    float radius;
    float searchRadius;

    float shotTimer;
    float shotTime;

    public Color startColor;

    // Start is called before the first frame update
    void Start()
    {
        currentState = State.PLACE;
        gameObject.GetComponent<Collider>().isTrigger = true;
        radius = 2f;
        searchRadius = 10f;
        startColor = gameObject.GetComponent<Renderer>().material.color;
        target = null;
        shotTime = shotTimer = 0.5f;
    }

    //turret active state script
    public void activeTurret() {
        if (target == null) {
            //search for target
            Collider[] hitTargets = Physics.OverlapSphere(transform.position, searchRadius);
            foreach (var hitTarget in hitTargets) {
                if (hitTarget.tag == "Enemy") {
                    target = hitTarget.gameObject;
                }
            }
        } else {
            //attack target
            Vector3 lookPos = new Vector3(target.transform.position.x, transform.position.y, target.transform.position.z);
            transform.LookAt(lookPos);
            if (shotTimer >= shotTime) {
                var projectile = Instantiate(projectilePrefab);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                shotTimer = 0;
            }

            shotTimer += Time.deltaTime;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (currentState) {
            case State.PLACE:
                transform.position = GameObject.Find("Cursor").transform.position;
                //if placement is allowed && mouse click -> place turret
                Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);
                bool canPlace = true;
                foreach (var hitObject in hitObjects) {
                    if (hitObject.transform.root != transform && hitObject.tag == "Structure") {
                        canPlace = false;
                    }
                }
                if (canPlace) {
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
                } else {
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                if(canPlace && Input.GetKeyDown(KeyCode.Space)) {
                    //place turret
                    currentState = State.ACTIVE;
                    gameObject.GetComponent<Collider>().isTrigger = false;
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", startColor);
                }
                break;
            case State.ACTIVE:
                activeTurret();

                break;
            case State.PAUSE:

                break;

            default:
                break;
        }
    }
}

