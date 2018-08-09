using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove_Sudden : MonoBehaviour 
{
	private Animator anim;
	public float timer = 0.2f;
	public float nowTime = 0.0f;
	public FaceDirection dir = FaceDirection.Down;
	public bool isMoving = false;

	public GameObject smokeEffect;
	public GameObject networkManager;
	private TcpClient tcpClient;
	public Camera cam;
	private Animator camAnimator;

	private Vector3 nextPosition;
	private FaceDirection nextDir;

	public void SetNextPosition(Vector3 pos)
	{
		nextPosition = pos;
	}

	public void SetDirection(int number)
	{
		switch(number)
		{
			case 0:
				nextDir = FaceDirection.Up;
				break;
			case 1:
				nextDir = FaceDirection.Down;
				break;
			case 2:
				nextDir = FaceDirection.Left;
				break;
			case 3:
				nextDir = FaceDirection.Right;
				break;
		}
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

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
		{
			return;
		}

		bool passed = false;

		if(other.gameObject.CompareTag("HolePic"))
		{
			if(gameObject.CompareTag("Player1"))
			{
				if(Player1Status_V2._instance.playerPower == PlayerPower_V2.MousePower)
				{
					other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
					passed = true;
				}

			}
			else if(gameObject.CompareTag("Player2"))
			{
				if(Player1Status_V2._instance.playerPower == PlayerPower_V2.MousePower)
				{
					other.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
					passed = true;
				}
			}
		}

		if(passed)
		{
			return;
		}


		switch(dir)
		{
			case FaceDirection.Down:
				dir = FaceDirection.Up;
				anim.SetTrigger("UpWalk");
				break;
			case FaceDirection.Up:
				dir = FaceDirection.Down;
				anim.SetTrigger("DownWalk");
				break;
			case FaceDirection.Left:
				dir = FaceDirection.Right;
				anim.SetTrigger("RightWalk");
				break;
			case FaceDirection.Right:
				dir = FaceDirection.Left;
				anim.SetTrigger("LeftWalk");
				break;
		}
	}
	void SmokeEffect()
	{
		GameObject sm = (GameObject)Instantiate(smokeEffect, transform.position, Quaternion.identity);
		Destroy(sm,0.5f);
	}

	void FixPosition()
	{
		Vector3 oldPosition = transform.position;
		oldPosition.x = Mathf.RoundToInt(oldPosition.x);
		oldPosition.y = Mathf.RoundToInt(oldPosition.y);
		transform.position = oldPosition;
	}
	public void MoveRight()
	{	
		//CameraMove("RightMove");
		
		SmokeEffect();
		Vector3 oldPosition = transform.position;
		oldPosition.x += 1;
		transform.position = oldPosition;

		isMoving = true;
		
	}

	public void MoveLeft()
	{	
		
		//CameraMove("LeftMove");
		
		SmokeEffect();
		Vector3 oldPosition = transform.position;
		oldPosition.x -= 1;
		transform.position = oldPosition;
		
		isMoving = true;
		
	}

	public void MoveUp()
	{	
		//CameraMove("UpMove");
		
		SmokeEffect();
		Vector3 oldPosition = transform.position;
		oldPosition.y += 1;
		transform.position = oldPosition;
		
		isMoving = true;
		
	}

	public void MoveDown()
	{
		//CameraMove("DownMove");
		
		SmokeEffect();
		Vector3 oldPosition = transform.position;
		oldPosition.y -= 1;
		transform.position = oldPosition;
		
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
		if ((Input.GetKeyDown(KeyCode.W) || ETCInput.GetAxisPressedUp("Vertical")))
		{
			dir = FaceDirection.Up;
			anim.SetTrigger("UpWalk");
		}
		else if(Input.GetKeyDown(KeyCode.S) || ETCInput.GetAxisPressedDown("Vertical"))
		{
			dir = FaceDirection.Down;
			anim.SetTrigger("DownWalk");
		}
		else if(Input.GetKeyDown(KeyCode.A) || ETCInput.GetAxisPressedLeft("Horizontal"))
		{
			dir = FaceDirection.Left;
			anim.SetTrigger("LeftWalk");
		}
		else if(Input.GetKeyDown(KeyCode.D) || ETCInput.GetAxisPressedRight("Horizontal"))
		{
			dir = FaceDirection.Right;
			anim.SetTrigger("RightWalk");
		}
	}

	void EnemyMove()
	{
		transform.position = nextPosition;
		dir = nextDir;
		switch(dir)
		{
			case FaceDirection.Up:
				anim.SetTrigger("UpWalk");
				break;
			case FaceDirection.Down:
			anim.SetTrigger("DownWalk");
			break;
			case FaceDirection.Left:
			anim.SetTrigger("LeftWalk");
			break;
			case FaceDirection.Right:
			anim.SetTrigger("RightWalk");
			break;
		}
	}

	void NextMove()
	{
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
			tcpClient.SendCurrentInfo(transform.position, dir);
		}
		else
		{
			TimeCheck();
		}
		FixPosition();
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
				NextMove();
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
				NextMove();
			}
			else
			{
				EnemyMove();
			}
		}
		
	}
}
