using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkMove : MonoBehaviour {
	
	// Update is called once per frame
	public GameObject smoke;

	void SmokeEffect()
	{
		GameObject sm = (GameObject)Instantiate(smoke, transform.position, Quaternion.identity);
		Destroy(sm,0.5f);
	}
	void MoveDown()
	{
		SmokeEffect();
		Vector3 oldPosition = transform.position;
		oldPosition.y -= 1;
		transform.position = oldPosition;
	}
	void MoveLeft()
	{
		SmokeEffect();
		Vector3 oldPosition = transform.position;
		oldPosition.x -= 1;
		transform.position = oldPosition;
	}
	void MoveRight()
	{
		SmokeEffect();
		Vector3 oldPosition = transform.position;
		oldPosition.x += 1;
		transform.position = oldPosition;
	}
	void MoveUp()
	{
		SmokeEffect();
		Vector3 oldPosition = transform.position;
		oldPosition.y += 1;
		transform.position = oldPosition;
	}

	
	void FixedUpdate()
	{
		if(Input.GetKeyDown(KeyCode.W))
		{
			MoveUp();
		}
		if(Input.GetKeyDown(KeyCode.S))
		{
			MoveDown();
		}
		if(Input.GetKeyDown(KeyCode.A))
		{
			MoveLeft();
		}
		if(Input.GetKeyDown(KeyCode.D))
		{
			MoveRight();
		}
	}
}
