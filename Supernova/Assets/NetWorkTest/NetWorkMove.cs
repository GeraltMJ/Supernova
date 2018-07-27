using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkMove : MonoBehaviour {
	
	// Update is called once per frame
	void MoveDown()
	{
		Vector3 oldPosition = transform.position;
		oldPosition.y -= 1;
		transform.position = oldPosition;
	}
	void MoveLeft()
	{
		Vector3 oldPosition = transform.position;
		oldPosition.x -= 1;
		transform.position = oldPosition;
	}
	void MoveRight()
	{
		Vector3 oldPosition = transform.position;
		oldPosition.x += 1;
		transform.position = oldPosition;
	}
	void MoveUp()
	{
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
