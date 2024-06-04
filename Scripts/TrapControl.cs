using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapControl : MonoBehaviour {

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

    public int hp = 2;

    public Color startColor;

    // Start is called before the first frame update
    void Start() {
        currentState = State.PLACE;
        gameObject.GetComponent<Collider>().isTrigger = true;
        radius = 2f;
        searchRadius = 10f;
        startColor = gameObject.GetComponent<Renderer>().material.color;
        target = null;
        shotTime = shotTimer = 0.5f;
    }

    //trap active state script
    public void activeTrap() {
        //if bear collides, put them in stun state
        Collider[] hitTargets = Physics.OverlapSphere(transform.position, transform.localScale.x);
        foreach (var hitTarget in hitTargets) {
            if (hitTarget.tag == "Enemy") {
                if (hitTarget.gameObject.GetComponent<EnemyControl>().currentState != EnemyControl.State.SNARED)
                    hp--;
                hitTarget.gameObject.GetComponent<EnemyControl>().currentState = EnemyControl.State.SNARED;
                hitTarget.gameObject.GetComponent<CapsuleCollider>().isTrigger = true;
            }
        }
        if (hp <= 0) {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        switch (currentState) {
            case State.PLACE:
                transform.position = GameObject.Find("Cursor").transform.position;
                //if placement is allowed && mouse click -> place turret
                /*
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
                */
                if (Input.GetKeyDown(KeyCode.Space)) {
                    //place turret
                    currentState = State.ACTIVE;
                    gameObject.GetComponent<Collider>().isTrigger = false;
                    gameObject.GetComponent<Renderer>().material.SetColor("_Color", startColor);
                }
                break;
            case State.ACTIVE:
                activeTrap();

                break;
            case State.PAUSE:

                break;

            default:
                break;
        }
    }
}
