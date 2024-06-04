using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour {

	public int buttonWidth;
	public int buttonHeight;
	private int origin_x;
	private int origin_y;

	// Use this for initialization
	void Start () {
		buttonWidth = 500;
		buttonHeight = 125;
		origin_x = Screen.width / 2 - buttonWidth / 2;
		origin_y = Screen.height / 2 - buttonHeight * 2;
	}
	
	void OnGUI() {

		if(GUI.Button(new Rect(origin_x, origin_y, buttonWidth, buttonHeight), "Collect Only")) {

			SceneManager.LoadScene(2);
		}

		if(GUI.Button(new Rect(origin_x, origin_y + buttonHeight + 10, buttonWidth, buttonHeight), "w/ Slow Enemies")) {
			SceneManager.LoadScene(3);
		}

        if(GUI.Button(new Rect(origin_x, origin_y + buttonHeight + 20, buttonWidth, buttonHeight), "w/ AI Enemies")){
            SceneManager.LoadScene(4);
        }
    }
}










