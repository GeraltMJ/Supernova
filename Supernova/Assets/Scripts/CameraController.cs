using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	// Use this for initialization
	public GameObject player1;
	public GameObject player2;

	private bool first = true;
	void Update () {

		if(first)
		{
			if(PlayerStatusControl._instance.isPlayer1)
			{
				transform.position = new Vector3(-5,3,-10);
			}
			else
			{
				transform.position = new Vector3(3,-3,-10);
			}
			first = false;
		}
	}
}
