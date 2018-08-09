using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour {

	// Use this for initialization
	public void OnMatchButton()
	{
		SceneManager.LoadScene("00_RoomMenu", LoadSceneMode.Single);
	}
	public void OnExitButton()
	{
		Application.Quit();
	}
}
