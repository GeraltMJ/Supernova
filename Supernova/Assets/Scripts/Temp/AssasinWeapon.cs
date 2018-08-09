using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssasinWeapon : MonoBehaviour {

	
	public float speed = 6.0f;
	public float timeLimit = 2.5f;
	public float timer = 0.0f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(timer < timeLimit)
		{
			this.transform.Translate(Time.deltaTime * speed * Vector3.right);
			timer += Time.deltaTime;
		}
		else
		{
			this.transform.Translate(Time.deltaTime * speed * Vector3.left);
		}
		
	}
}
