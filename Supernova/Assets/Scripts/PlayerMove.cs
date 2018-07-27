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

	public GameObject networkManager;
	private TcpClient tcpClient;
	private string currentCommand;
	public Camera cam;
	private Animator camAnimator;

	public void SetCurrentCommand(string command)
	{
		currentCommand = command;
	}

	void CameraMove(string str)
	{
		if(gameObject.name == "Player1" && PlayerStatusControl._instance.isPlayer1)
		{
			camAnimator.SetTrigger(str);
		}else if(gameObject.name == "Player2" && !PlayerStatusControl._instance.isPlayer1)
		{
			camAnimator.SetTrigger(str);
		}
	}

	public void MoveRight()
	{	
		//CameraMove("RightMove");
		
		if(rightObstacle == 0)
		{
			anim.SetTrigger("RightWalk");
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
		
		//CameraMove("LeftMove");
		if(leftObstacle == 0)
		{
			anim.SetTrigger("LeftWalk");
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
		//CameraMove("UpMove");
		if(upObstacle == 0)
		{
			anim.SetTrigger("UpWalk");
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
		//CameraMove("DownMove");
		if(downObstacle == 0)
		{
			anim.SetTrigger("DownWalk");
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

	void ControlMove()
	{
		if ((Input.GetKey(up) || ETCInput.GetAxisPressedUp("Vertical")))
		{
			tcpClient.SendSelfCommand("W");
			dir = FaceDirection.Up;
		}
		else if(Input.GetKey(down) || ETCInput.GetAxisPressedDown("Vertical"))
		{
			tcpClient.SendSelfCommand("S");
			dir = FaceDirection.Down;
		}
		else if(Input.GetKey(left) || ETCInput.GetAxisPressedLeft("Horizontal"))
		{
			tcpClient.SendSelfCommand("A");
			dir = FaceDirection.Left;
		}
		else if(Input.GetKey(right) || ETCInput.GetAxisPressedRight("Horizontal"))
		{
			tcpClient.SendSelfCommand("D");
			dir = FaceDirection.Right;
		}
	}

	void EnemyMove()
	{
		if (currentCommand == "W")
		{
			dir = FaceDirection.Up;
		}
		else if(currentCommand == "S")
		{
			dir = FaceDirection.Down;
		}
		else if(currentCommand == "A")
		{
			dir = FaceDirection.Left;
		}
		else if(currentCommand == "D")
		{
			dir = FaceDirection.Right;
		}
		currentCommand = "";
	}

	void Start () 
	{	
		anim = GetComponent<Animator>();
		camAnimator = cam.GetComponent<Animator>();
		tcpClient = networkManager.GetComponent<TcpClient>();
	}

	private void FixedUpdate() 
	{	

		if(gameObject.name == "Player1")
		{
			if(PlayerStatusControl._instance.isPlayer1)
			{
				ControlMove();
			}
			else
			{
				EnemyMove();
			}
		}
		else if(gameObject.name == "Player2")
		{
			if(!PlayerStatusControl._instance.isPlayer1)
			{
				ControlMove();
			}
			else
			{
				EnemyMove();
			}
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
