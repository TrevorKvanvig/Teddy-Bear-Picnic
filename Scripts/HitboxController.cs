using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitboxController : MonoBehaviour
{
    public float ttl;//1 second time to live
    public float counter;//counter for destroy timer
    public float damage;
    float radius;

    // Start is called before the first frame update
    void Start()
    {
        ttl = 0.1f;
        counter = 0f;
        damage = 0.5f;
        radius = transform.localScale.x / 4;
    }

    // Update is called once per frame
    void Update()
    {
        counter += Time.deltaTime;//increment destroy timer
        //destroy object if ttl is reached
        if (counter > ttl) {
            Destroy(gameObject);
        }
        Collider[] hitObjects = Physics.OverlapSphere(transform.position, radius);
        foreach (var hitObject in hitObjects) {
            if (hitObject.transform.root != transform && hitObject.tag == "Enemy") {
                hitObject.GetComponent<EnemyControl>().health -= damage;
            }
        }
    }
}
