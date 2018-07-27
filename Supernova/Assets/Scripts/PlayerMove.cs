using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove : MonoBehaviour 
{
	private Animator anim;
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	public float timer = 0.2f;
	public float nowTime = 0.0f;
	public FaceDirection dir = FaceDirection.Down;
	public bool isMoving = false;
	public int rightObstacle = 0;
	public int leftObstacle = 0;
	public int upObstacle = 0;
	public int downObstacle = 0;
	public Camera cam;
	private Animator camAnimator;

	public void MoveRight()
	{	
		camAnimator.SetTrigger("RightMove");
		
		dir = FaceDirection.Right;
		if(rightObstacle == 0)
		{
			anim.SetTrigger("RightWalk");
			dir = FaceDirection.Right;
		}
		else
		{
			anim.SetTrigger("Jump");
			dir = FaceDirection.Down;
		}
		isMoving = true;
		
	}

	public void MoveLeft()
	{	
		
		camAnimator.SetTrigger("LeftMove");
		dir = FaceDirection.Left;
		if(leftObstacle == 0)
		{
			anim.SetTrigger("LeftWalk");
			dir = FaceDirection.Left;
		}
		else
		{
			anim.SetTrigger("Jump");
			dir = FaceDirection.Down;
		}
		isMoving = true;
		
	}

	public void MoveUp()
	{	
		camAnimator.SetTrigger("UpMove");
		dir = FaceDirection.Up;
		if(upObstacle == 0)
		{
			anim.SetTrigger("UpWalk");
			dir = FaceDirection.Up;
		}
		else
		{
			anim.SetTrigger("Jump");
			dir = FaceDirection.Down;
		}
		isMoving = true;
		
	}

	public void MoveDown()
	{
		camAnimator.SetTrigger("DownMove");
		dir = FaceDirection.Down;
		if(downObstacle == 0)
		{
			anim.SetTrigger("DownWalk");
			dir = FaceDirection.Down;
		}
		else
		{
			anim.SetTrigger("Jump");
			dir = FaceDirection.Down;
		}
		isMoving = true;
	}

	public void TimeCheck()
	{
		nowTime += Time.deltaTime;
		if (nowTime >= timer)
		{
			nowTime = 0;
			isMoving = false;
		}
	}

	void Start () 
	{	
		anim = GetComponent<Animator>();
		camAnimator = cam.GetComponent<Animator>();
	}

	private void FixedUpdate() 
	{	

		if ((Input.GetKey(up) || ETCInput.GetAxisPressedUp("Vertical")))
		{
			dir = FaceDirection.Up;
		}
		else if(Input.GetKey(down) || ETCInput.GetAxisPressedDown("Vertical"))
		{
			dir = FaceDirection.Down;
		}
		else if(Input.GetKey(left) || ETCInput.GetAxisPressedLeft("Horizontal"))
		{
			dir = FaceDirection.Left;
		}
		else if(Input.GetKey(right) || ETCInput.GetAxisPressedRight("Horizontal"))
		{
			dir = FaceDirection.Right;
		}

		if(!isMoving)
		{
			switch(dir)
			{
				case FaceDirection.Up:
					MoveUp();
					break;
				case FaceDirection.Down:
					MoveDown();
					break;
				case FaceDirection.Left:
					MoveLeft();
					break;
				case FaceDirection.Right:
					MoveRight();
					break;
			}
		}
		else
		{
			TimeCheck();
		}
	}
}

public enum FaceDirection
{
	Up, Down, Left, Right
}
