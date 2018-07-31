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
	private AudioSource[] audioSources;
	private AudioSource audioSource;
	public AudioClip stepSound;

	public void SetNextPosition(Vector2 pos)
	{
		nextPosition = pos;
	}

	public void SetDir(int number)
	{
		switch(number)
		{
			case 0:
				dir = FaceDirection.Up;
				break;
			case 1:
				dir = FaceDirection.Down;
				break;
			case 2:
				dir = FaceDirection.Left;
				break;
			case 3:
				dir = FaceDirection.Right;
				break;
		}
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
		if ((Input.GetKeyDown(KeyCode.W) || ETCInput.GetButton("UpButton")))
		{
			dir = FaceDirection.Up;
			anim.SetTrigger("UpWalk");
		}
		else if(Input.GetKeyDown(KeyCode.S) || ETCInput.GetButton("DownButton"))
		{
			dir = FaceDirection.Down;
			anim.SetTrigger("DownWalk");
		}
		else if(Input.GetKeyDown(KeyCode.A) || ETCInput.GetButton("LeftButton"))
		{
			dir = FaceDirection.Left;
			anim.SetTrigger("LeftWalk");
		}
		else if(Input.GetKeyDown(KeyCode.D) || ETCInput.GetButton("RightButton"))
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
				//transform.position = new Vector2(nextPosition.x, nextPosition.y + 0.1f);
				break;
			case FaceDirection.Down:
				anim.SetTrigger("DownWalk");
				//transform.position = new Vector2(nextPosition.x, nextPosition.y - 0.1f);
				break;
			case FaceDirection.Left:
				anim.SetTrigger("LeftWalk");
				//transform.position = new Vector2(nextPosition.x - 0.1f, nextPosition.y);
				break;
			case FaceDirection.Right:
				anim.SetTrigger("RightWalk");
				//transform.position = new Vector2(nextPosition.x + 0.1f, nextPosition.y);
				break;
		}
	}
	
	
	
	 /* 
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
	*/

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
			//tcpClient.SendCurrentInfo(endPosition, dir);
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
				//audioSource.clip = stepSound;
				//audioSource.Play();
			}
		}
	}

	void Start () 
	{	
		anim = GetComponent<Animator>();
		//camAnimator = cam.GetComponent<Animator>();
		tcpClient = networkManager.GetComponent<TcpClient_Level2>();
		nextPosition = new Vector2(transform.position.x, transform.position.y - 1);
		nextDir = FaceDirection.Down;
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[1];

	}

	void FixCameraPosition()
	{
		cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}
	
	void FixedUpdate()
	{
		if((gameObject.CompareTag("Player1") && PlayerStatusControl_Level2._instance.isPlayer1) || (gameObject.CompareTag("Player2") && !PlayerStatusControl_Level2._instance.isPlayer1))
		{
			tcpClient.SendCurrentInfo(transform.position, dir);
			ControlMove();
			NextMove();
		}
		else
		{
			EnemyMove();
		} 
	}

	
	void MoveAccordingToServer()
	{

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
			process = 0;
			isMoving = true;
			endPosition = nextPosition;
		}
		if(isMoving)
		{
			process += Time.deltaTime*speed;
			if(process < 1)
			{
				transform.position = Vector2.Lerp(transform.position, endPosition, process);
				if((gameObject.CompareTag("Player1") && PlayerStatusControl_Level2._instance.isPlayer1) || (gameObject.CompareTag("Player2") && !PlayerStatusControl_Level2._instance.isPlayer1))
				{
					FixCameraPosition();
				}
				//Debug.Log(process);
				//tcpClient.SendCurrentInfo(transform.position, dir);
			}
			else
			{
				FixPosition();
				Vector2 predictPosition = new Vector2(0f,0f);
				switch(dir)
				{
					case FaceDirection.Up:
						predictPosition = new Vector2(transform.position.x, transform.position.y + 1);
						break;
					case FaceDirection.Down:
						predictPosition = new Vector2(transform.position.x, transform.position.y - 1);
						break;
					case FaceDirection.Left:
						predictPosition = new Vector2(transform.position.x - 1, transform.position.y);
						break;
					case FaceDirection.Right:
						predictPosition = new Vector2(transform.position.x + 1, transform.position.y);
						break;
				}
				
				if((gameObject.CompareTag("Player1") && PlayerStatusControl_Level2._instance.isPlayer1))
				{
					tcpClient.SendPlayerCurrentInfo(predictPosition, dir,1);
				}
				else if((gameObject.CompareTag("Player2") && !PlayerStatusControl_Level2._instance.isPlayer1))
				{
					tcpClient.SendPlayerCurrentInfo(predictPosition, dir,2);
				}
				isMoving = false;
			}
		}
	}

	void ControlMoveSyn()
	{	
		if ((Input.GetKeyDown(KeyCode.W) || ETCInput.GetButton("UpButton")))
		{
			dir = FaceDirection.Up;
			anim.SetTrigger("UpWalk");
		}
		else if(Input.GetKeyDown(KeyCode.S) || ETCInput.GetButton("DownButton"))
		{
			dir = FaceDirection.Down;
			anim.SetTrigger("DownWalk");
		}
		else if(Input.GetKeyDown(KeyCode.A) || ETCInput.GetButton("LeftButton"))
		{
			dir = FaceDirection.Left;
			anim.SetTrigger("LeftWalk");
		}
		else if(Input.GetKeyDown(KeyCode.D) || ETCInput.GetButton("RightButton"))
		{
			dir = FaceDirection.Right;
			anim.SetTrigger("RightWalk");
		}

		
	}

	
	/*
	private void Update()
	{
		if((gameObject.CompareTag("Player1") && PlayerStatusControl_Level2._instance.isPlayer1) || (gameObject.CompareTag("Player2") && !PlayerStatusControl_Level2._instance.isPlayer1))
		{
			ControlMoveSyn();
		}
		MoveAccordingToServer();
	}
	*/
	
	
}
