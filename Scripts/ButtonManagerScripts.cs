using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManagerScripts : MonoBehaviour
{

	public void beginGame()
	{
		SceneManager.LoadScene(1);
	}

}
