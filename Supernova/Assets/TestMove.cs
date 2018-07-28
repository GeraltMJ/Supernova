using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMove : MonoBehaviour {

	public float speed;
	private bool isMove = false;
	private Vector2 endPos;
	private float process = 0;

	void Start()
	{
	}

	void FixPosition()
	{
		Vector2 oldPosition = transform.position;
		transform.position = new Vector2(Mathf.RoundToInt(oldPosition.x), Mathf.RoundToInt(oldPosition.y));
	}
	void FixedUpdate () {
		Vector2 oldPosition = transform.position;
		if(!isMove)
		{
			if(Input.GetKeyDown(KeyCode.W))
			{
				endPos = new Vector2(transform.position.x, transform.position.y + 1);
				process = 0;
				isMove = true;
			}
			else if(Input.GetKeyDown(KeyCode.S))
			{
				endPos = new Vector2(transform.position.x, transform.position.y - 1);
				process = 0;
				isMove = true;
			}
			else if(Input.GetKeyDown(KeyCode.A))
			{
				endPos = new Vector2(transform.position.x - 1, transform.position.y);
				process = 0;
				isMove = true;
			}
			else if(Input.GetKeyDown(KeyCode.D))
			{
				endPos = new Vector2(transform.position.x + 1, transform.position.y);
				process = 0;
				isMove = true;
			}
		}
		if(isMove)
		{
			process += Time.deltaTime*2;
			if(process < 1)
			{
				transform.position = Vector2.Lerp(transform.position, endPos, process);
			}
			else
			{
				isMove = false;
				FixPosition();
			}
		}
	}
}
