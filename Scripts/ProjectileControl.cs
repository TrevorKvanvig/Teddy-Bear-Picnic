using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileControl : MonoBehaviour
{

    public float movespeed;//projectile speed
    float radius;
    float damage = 20;
    float displacement;
    float maxDispl = 20;

    public string owner = "";

    // Start is called before the first frame update
    void Start()
    {
        movespeed = 20f;
        radius = transform.localScale.x;
        displacement = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * (movespeed * Time.deltaTime));//move projectile forward
        displacement += movespeed * Time.deltaTime;
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);
        var size = 0;
        foreach (var hitObject in hitObjects) {
            if (hitObject.transform.root != transform && hitObject.tag == "Enemy") {
                size++;
                hitObject.GetComponent<EnemyControl>().health -= damage;
                if(owner == "player") {
                    hitObject.GetComponent<EnemyControl>().target = GameObject.Find("Player");
                }
            }
        }
        if (size > 0 || displacement >= maxDispl) {
            Destroy(gameObject);
        }
    }
}
