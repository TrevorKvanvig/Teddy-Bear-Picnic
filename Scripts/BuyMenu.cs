using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuyMenu : MonoBehaviour
{
    public List<GameObject> buildings;
    public List<GameObject> upgrades;
    public List<GameObject> actions;
    public List<GameObject> options;

    [SerializeField]
    GameObject buttonPrefab;

    GameObject buyCanvas;

    GameObject playerRef;

    GameObject levelControlRef;

    GameObject waveManagerRef;

    float initXpos = Screen.width / 2;
    float initYpos = Screen.height / 3;

    public int turretCost = 3;
    public int trapCost = 1;
    public int wallCost = 1;
    public int upgradeCost = 18;
    public int treeCost = 10;

    public int treeStage = 0;

    // Start is called before the first frame update
    void Start()
    {
        buyCanvas = GameObject.Find("BuyCanvas");

        playerRef = GameObject.Find("Player");

        levelControlRef = GameObject.Find("Level Manager");

        waveManagerRef = GameObject.Find("Wave Manager");

        //TODO: make a function to instantiate buttons
        float shift = 70f;
            //initialize buttons
        //turret button
        var button = Instantiate(buttonPrefab);
        button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Buy Turret: $" + turretCost.ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate { placeTurret(); });
        button.transform.position = new Vector3((Screen.width / 3)+ shift, buyCanvas.transform.position.y + 75f, buyCanvas.transform.position.z);
        button.transform.SetParent(buyCanvas.transform);
        buildings.Add(button);
        //trap button
        button = Instantiate(buttonPrefab);
        button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Buy Trap: $" + trapCost.ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate { placeTrap(); });
        button.transform.position = new Vector3((Screen.width / 3)+ shift, buyCanvas.transform.position.y + -60f, buyCanvas.transform.position.z);
        button.transform.SetParent(buyCanvas.transform);
        buildings.Add(button);
        //wall button
        /*
        button = Instantiate(buttonPrefab);
        button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Buy Wall: $" + wallCost.ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate { placeWall(); });
        button.transform.position = new Vector3((Screen.width / 3)+ shift, buyCanvas.transform.position.y - 200f, buyCanvas.transform.position.z);
        button.transform.SetParent(buyCanvas.transform);
        buildings.Add(button);
        */
        //upgrade weapon button
        button = Instantiate(buttonPrefab);
        button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade Weapon: $" + upgradeCost.ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate { upgradeWeapon(); });
        button.transform.position = new Vector3(2*(Screen.width / 3) - shift, buyCanvas.transform.position.y + 75f, buyCanvas.transform.position.z);
        button.transform.SetParent(buyCanvas.transform);
        upgrades.Add(button);
        //clear forest button
        button = Instantiate(buttonPrefab);
        button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Cut Trees: $" + treeCost.ToString();
        button.GetComponent<Button>().onClick.AddListener(delegate { cutTrees(); });
        button.transform.position = new Vector3(2*(Screen.width / 3) - shift, buyCanvas.transform.position.y + -60f, buyCanvas.transform.position.z);
        button.transform.SetParent(buyCanvas.transform);
        actions.Add(button);
        //start next round button
        button = Instantiate(buttonPrefab);
        button.GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Begin Next Round";
        button.GetComponent<Button>().onClick.AddListener(delegate { startNextRound(); });
        button.transform.position = new Vector3((Screen.width / 2), buyCanvas.transform.position.y - 200f, buyCanvas.transform.position.z);
        button.transform.SetParent(buyCanvas.transform);
        options.Add(button);

        //TODO: handle x axis spreading of buttons based on number of buttons in list
        /*
        int i = 1;
        foreach(GameObject go in buildings) {
            //go.transform.position = new Vector3(buyCanvas.transform.position.x, buyCanvas.transform.position.y + 150f, buyCanvas.transform.position.z);
            go.transform.position = new Vector3((Screen.width/(buildings.Count+1))*i, buyCanvas.transform.position.y + 150f, buyCanvas.transform.position.z);
            go.transform.localScale = new Vector3(.75f, 1, 1);
            i++;
        }
        foreach(GameObject go in upgrades) {
            go.transform.position = new Vector3(buyCanvas.transform.position.x, buyCanvas.transform.position.y + 75, buyCanvas.transform.position.z);
        }
        foreach (GameObject go in actions) {
            go.transform.position = new Vector3(buyCanvas.transform.position.x, buyCanvas.transform.position.y - 75f, buyCanvas.transform.position.z);
        }
        foreach (GameObject go in options) {
            go.transform.position = new Vector3(buyCanvas.transform.position.x, buyCanvas.transform.position.y - 150f, buyCanvas.transform.position.z);
        }
        */

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //TODO.. generalized building function which takes unique build info as parameters
    public void placeTurret() {
        if (playerRef.GetComponent<PlayerController>().state != PlayerController.State.BUILD && levelControlRef.GetComponent<LevelControl>().money >= turretCost) {
            levelControlRef.GetComponent<LevelControl>().reduceMoney(turretCost);
            playerRef.GetComponent<PlayerController>().buildType = "turret";
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            playerRef.GetComponent<PlayerController>().state = PlayerController.State.BUILD;
        }
    }
    public void placeTrap() {
        if (playerRef.GetComponent<PlayerController>().state != PlayerController.State.BUILD && levelControlRef.GetComponent<LevelControl>().money >= trapCost) {
            levelControlRef.GetComponent<LevelControl>().reduceMoney(trapCost);
            playerRef.GetComponent<PlayerController>().buildType = "trap";
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            playerRef.GetComponent<PlayerController>().state = PlayerController.State.BUILD;
        }
    }

    public void placeWall() {
        if (playerRef.GetComponent<PlayerController>().state != PlayerController.State.BUILD && levelControlRef.GetComponent<LevelControl>().money >= wallCost) {
            levelControlRef.GetComponent<LevelControl>().reduceMoney(wallCost);
            playerRef.GetComponent<PlayerController>().buildType = "wall";
            gameObject.transform.GetChild(0).gameObject.SetActive(false);
            playerRef.GetComponent<PlayerController>().state = PlayerController.State.BUILD;
        }
    }

    public void upgradeWeapon() {
        if (playerRef.GetComponent<PlayerController>().GetComponentInChildren<WeaponController>().type != WeaponController.WeaponType.AUTOSHOT && 
            playerRef.GetComponent<PlayerController>().state != PlayerController.State.BUILD && 
            levelControlRef.GetComponent<LevelControl>().money >= upgradeCost) {

            levelControlRef.GetComponent<LevelControl>().reduceMoney(upgradeCost);
            playerRef.GetComponent<PlayerController>().upgradeWeapon();
            if (playerRef.GetComponent<PlayerController>().GetComponentInChildren<WeaponController>().type != WeaponController.WeaponType.AUTOSHOT) {
                upgradeCost *= 2;
                upgrades[0].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Upgrade Weapon: $" + upgradeCost.ToString();
            } else {
                upgrades[0].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Max Weapon Reached";
            }
        }
    }

    public void cutTrees() {
        if (levelControlRef.GetComponent<LevelControl>().money >= treeCost) {
            if (treeStage <= 3) {
                levelControlRef.GetComponent<LevelControl>().reduceMoney(treeCost);
                if (treeStage == 0) {
                    Destroy(GameObject.Find("TreeBorder1"));
                    treeCost = 25;
                } else if (treeStage == 1) {
                    treeCost = 50;
                    Destroy(GameObject.Find("TreeBorder2"));
                } else if (treeStage == 2) {
                    treeCost = 75;
                    Destroy(GameObject.Find("Treeborder3"));
                } else if (treeStage == 3) {
                    
                    Destroy(GameObject.Find("Treeborder4"));
                }
                actions[0].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Cut Trees: $" + treeCost;
                treeStage++;
            }
            if(treeStage > 3) {
                actions[0].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Max Deforestation Reached";
            }
        }
    }

    public void startNextRound() {
        if (playerRef.GetComponent<PlayerController>().state != PlayerController.State.BUILD) {
            waveManagerRef.GetComponent<EnemySpawner>().waveActive = true;
            playerRef.GetComponent<PlayerController>().state = PlayerController.State.NORMAL;
            if (levelControlRef.GetComponent<LevelControl>().currentLevel < 10) {
                turretCost += 3;
            } else {
                turretCost += 20;
            }
            buildings[0].GetComponent<Button>().GetComponentInChildren<TextMeshProUGUI>().text = "Buy Turret: $" + turretCost.ToString();
            gameObject.SetActive(false);
        }
    }

}
