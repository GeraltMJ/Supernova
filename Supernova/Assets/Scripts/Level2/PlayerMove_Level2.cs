using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove_Level2 : MonoBehaviour 
{
	private Animator anim;
	public float speed = 2;
	public float timer = 0.2f;
	public float nowTime = 0.0f;
	public FaceDirection dir = FaceDirection.Down;
	private bool isMoving = false;
	private bool moveCD = false;

	public GameObject smokeEffect;
	public GameObject networkManager;
	private TcpClient_Level2 tcpClient;
	public Camera cam;
	private Animator camAnimator;

	private Vector2 endPosition;
	private float process;

	private Vector2 nextPosition;
	private FaceDirection nextDir;
	private FaceDirection dirToSend;
	private Vector2 positionToSend;

	private Rigidbody2D rb2d;

	public void SetNextPosition(Vector2 pos)
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

	/* 
	void CameraMove(string str)
	{
		if(gameObject.name == "Player1" && PlayerStatusControl_Level2._instance.isPlayer1)
		{
			camAnimator.SetTrigger(str);
		}else if(gameObject.name == "Player2" && !PlayerStatusControl_Level2._instance.isPlayer1)
		{
			camAnimator.SetTrigger(str);
		}
	}
	*/

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2"))
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
			moveCD = false;
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
	
	
	/* 
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
	*/
	
	
	 
	void EnemyMove()
	{
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
		if(!isMoving)
		{
			endPosition = nextPosition;
			process = 0;
			isMoving = true;
		}
		if(isMoving)
		{
			process += Time.deltaTime*speed;
			if(process < 1)
			{
				transform.position = Vector2.Lerp(transform.position, endPosition, process);
			}
			else
			{
				isMoving = false;
				FixPosition();
			}
		}
	}
	

	void NextMove()
	{
		if(!isMoving)
		{	
			switch(dir)
			{
				case FaceDirection.Up:
					endPosition = new Vector2(transform.position.x, transform.position.y + 1);
					break;
				case FaceDirection.Down:
					endPosition = new Vector2(transform.position.x, transform.position.y - 1);
					break;
				case FaceDirection.Left:
					endPosition = new Vector2(transform.position.x - 1, transform.position.y);
					break;
				case FaceDirection.Right:
					endPosition = new Vector2(transform.position.x+1, transform.position.y);
					break;
			}
			process = 0;
			isMoving = true;
			tcpClient.SendCurrentInfo(endPosition, dir);
		}
		if(isMoving)
		{
			process += Time.deltaTime*speed;
			if(process < 1)
			{
				transform.position = Vector2.Lerp(transform.position, endPosition, process);
				FixCameraPosition();
				//tcpClient.SendCurrentInfo(transform.position, dir);
			}
			else
			{
				isMoving = false;
				FixPosition();
			}
		}
	}

	void Start () 
	{	
		anim = GetComponent<Animator>();
		//camAnimator = cam.GetComponent<Animator>();
		tcpClient = networkManager.GetComponent<TcpClient_Level2>();
		nextPosition = transform.position;
		nextDir = FaceDirection.Down;
		rb2d = GetComponent<Rigidbody2D>();
	}

	void FixCameraPosition()
	{
		cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}

	
	private void FixedUpdate() 
	{	
		if(gameObject.name == "Player1")
		{
			if(PlayerStatusControl_Level2._instance.isPlayer1)
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
			if(!PlayerStatusControl_Level2._instance.isPlayer1)
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
	
	/* 
	void PlayerControlMove()
	{
		if((gameObject.CompareTag("Player1") && PlayerStatusControl_Level2._instance.isPlayer1 ) || (gameObject.CompareTag("Player2") && !PlayerStatusControl_Level2._instance.isPlayer1))
		{
			if ((Input.GetKeyDown(KeyCode.W) || ETCInput.GetAxisPressedUp("Vertical")))
			{
				dir = FaceDirection.Up;
			}
			else if(Input.GetKeyDown(KeyCode.S) || ETCInput.GetAxisPressedDown("Vertical"))
			{
				dir = FaceDirection.Down;
			}
			else if(Input.GetKeyDown(KeyCode.A) || ETCInput.GetAxisPressedLeft("Horizontal"))
			{
				dir = FaceDirection.Left;
			}
			else if(Input.GetKeyDown(KeyCode.D) || ETCInput.GetAxisPressedRight("Horizontal"))
			{
				dir = FaceDirection.Right;
			}
		}
		
		switch(dirToSend)
		{
			case FaceDirection.Up:
				positionToSend = new Vector2(transform.position.x, transform.position.y + 1);
				break;
			case FaceDirection.Down:
				positionToSend = new Vector2(transform.position.x, transform.position.y - 1);
				break;
			case FaceDirection.Left:
				positionToSend = new Vector2(transform.position.x - 1, transform.position.y);
				break;
			case FaceDirection.Right:
				positionToSend = new Vector2(transform.position.x+1, transform.position.y);
				break;
		}
		
	}
	void MoveAccordingToNext()
	{
		if(!isMoving)
		{
			switch(dir)
			{
				case FaceDirection.Up:
					anim.SetTrigger("UpWalk");
					endPosition = new Vector2(transform.position.x, transform.position.y + 1);
					break;
				case FaceDirection.Down:
					anim.SetTrigger("DownWalk");
					endPosition = new Vector2(transform.position.x, transform.position.y - 1);
					break;
				case FaceDirection.Left:
					anim.SetTrigger("LeftWalk");
					endPosition = new Vector2(transform.position.x - 1, transform.position.y);
					break;
				case FaceDirection.Right:
					anim.SetTrigger("RightWalk");
					endPosition = new Vector2(transform.position.x+1, transform.position.y);
					break;
			}
			process = 0;
			isMoving = true;
		}
		if(isMoving)
		{
			process += Time.deltaTime*speed;
			if(process < 1)
			{
				transform.position = Vector2.Lerp(transform.position, endPosition, process);
				if((gameObject.CompareTag("Player1") && PlayerStatusControl_Level2._instance.isPlayer1 ) || (gameObject.CompareTag("Player2") && !PlayerStatusControl_Level2._instance.isPlayer1))
				{
					FixCameraPosition();
				}
			}
			else
			{
				isMoving = false;
				FixPosition();
			}
		}
	}
	void FixedUpdate()
	{	
		PlayerControlMove();
		MoveAccordingToNext();
	}
	*/
}
