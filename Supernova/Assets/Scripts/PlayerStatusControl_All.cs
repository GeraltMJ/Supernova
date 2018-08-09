using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatusControl_All : MonoBehaviour {

	// Use this for initialization
	public static PlayerStatusControl_All _instance;
	public int playerIndex;
	void Start () {
		_instance = this;
		DontDestroyOnLoad(this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
