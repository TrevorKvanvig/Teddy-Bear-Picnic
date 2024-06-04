using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    [SerializeField]
    GameObject hitboxPrefab;//reference to melee hitbox
    [SerializeField]
    GameObject projectilePrefab;//reference to bullet/projectile hitbox

    //possible weapon types
    public enum WeaponType {
        MELEE = 0,
        SEMI = 1,
        AUTO = 2,
        SHOT = 3,
        AUTOSHOT = 4,
    }

    [SerializeField]
    public WeaponType type = WeaponType.MELEE;//type of weapon to determine attack behavior


    public bool attacking;//bool set by PlayerController.cs if player presses attack button

    //full auto values
    public float autoTimer;//amount of time between full auto shots
    public float autoCounter;//counter to check if full auto is ready to trigger again

    //shotgun values
    public int numShot;//number of pellets
    public float shotAngle;//angle of shot cone

    // Start is called before the first frame update
    void Start()
    {
        attacking = false;
        autoTimer = 0.1f;
        autoCounter = autoTimer;
        numShot = 8;
        shotAngle = 20f;
    }

    // Update is called once per frame
    void Update()
    {
        //attacking bool set in PlayerController.cs
        if (attacking) {
            //GetComponent<AudioSource>().Play();
            switch (type) {
                case WeaponType.MELEE://melee attack
                    meleeAttack();
                    break;
                case WeaponType.SEMI://single fire projectile
                    semiAttack();
                    break;
                case WeaponType.AUTO://auto fire projectile
                    autoAttack();
                    break;
                case WeaponType.SHOT:
                    shotAttack();
                    break;
                case WeaponType.AUTOSHOT:
                    autoShotAttack();
                    break;
                default:
                    break;
            }
        }
    }
    

    //function for melee attacks
    //TODO: assign hitbox damage based on damage variable
    public void meleeAttack() {
        //create hitbox
        var hitbox = Instantiate(hitboxPrefab);
        hitbox.transform.position = transform.position;
        hitbox.transform.rotation = transform.rotation;
        hitbox.transform.parent = gameObject.transform;
        attacking = false;
    }

    //function for single fire attacks
    public void semiAttack() {
        //create projectile
        GetComponent<AudioSource>().Play();
        var projectile = Instantiate(projectilePrefab);
        projectile.transform.position = transform.position;
        projectile.transform.rotation = transform.rotation;
        projectile.GetComponent<ProjectileControl>().owner = "player";
        attacking = false;
    }

    //function for full auto attacks
    public void autoAttack() {
        //limit speed of full auto gun
        if (autoCounter >= autoTimer) {
            GetComponent<AudioSource>().Play();
            var projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.GetComponent<ProjectileControl>().owner = "player";
            autoCounter = 0;
        }
        if (Input.GetMouseButtonUp(0)) {
            attacking = false;
        }
        autoCounter += Time.deltaTime;
    }

    //function for shotgun attacks
    public void shotAttack() {
        float angle = -shotAngle / 2;//angle of individual bullet
        //create pellets based on number of shots
        GetComponent<AudioSource>().Play();
        for (int i = 0; i < numShot; i++) {
            var projectile = Instantiate(projectilePrefab);
            projectile.transform.position = transform.position;
            projectile.transform.rotation = transform.rotation;
            projectile.transform.Rotate(0f, angle, 0f);
            projectile.GetComponent<ProjectileControl>().owner = "player";
            angle += (shotAngle / numShot);//increment rotation
        }
        attacking = false;
    }

    public void autoShotAttack() {
        //limit speed of full auto gun
        float angle = -shotAngle / 2;//angle of individual bullet
        if (autoCounter >= autoTimer) {
            GetComponent<AudioSource>().Play();
            for (int i = 0; i < numShot; i++) {
                var projectile = Instantiate(projectilePrefab);
                projectile.transform.position = transform.position;
                projectile.transform.rotation = transform.rotation;
                projectile.transform.Rotate(0f, angle, 0f);
                projectile.GetComponent<ProjectileControl>().owner = "player";
                angle += (shotAngle / numShot);//increment rotation
            }
            autoCounter = 0;
        }
        if (Input.GetMouseButtonUp(0)) {
            attacking = false;
        }
        autoCounter += Time.deltaTime;
    }
}
