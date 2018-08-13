using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMove_Level3 : MonoBehaviour 
{


	public bool leftCubeActive = false;
	public bool rightCubeActive = false;
	public bool upCubeActive = false;
	public bool downCubeActive = false;
	private Animator anim;
	public float speed = 2.1f;
	public float speedUp = 1f;
	public float timer = 0.2f;
	public float nowTime = 0.0f;
	public FaceDirection dir = FaceDirection.Down;
	private bool isMoving = false;
	private bool moveCD = false;

	public GameObject smokeEffect;
	//public GameObject networkManager;
	//private TcpClient_Level3 tcpClient;
	public Camera cam;
	private Animator camAnimator;

	public Vector2 endPosition;
	private float process;

	private Vector2 nextPosition;
	private FaceDirection nextDir;
	private AudioSource[] audioSources;
	private AudioSource audioSource;
	public AudioClip stepSound;
	private PlayerStatus_Level3 playerStatus;
	private PlayerAttack_Level3 playerAttack;

	private bool isInStar = false;
	public bool starTeleport = false;
	public bool canAttackInStar = false;
	public float starRemain = 0;

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

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3"))
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

	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3"))
		{
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
		if ((Input.GetKeyDown(KeyCode.W) || ETCInput.GetButton("UpButton")) && !upCubeActive)
		{
			dir = FaceDirection.Up;
			anim.SetTrigger("UpWalk");
		}
		else if((Input.GetKeyDown(KeyCode.S) || ETCInput.GetButton("DownButton")) && !downCubeActive)
		{
			dir = FaceDirection.Down;
			anim.SetTrigger("DownWalk");
		}
		else if((Input.GetKeyDown(KeyCode.A) || ETCInput.GetButton("LeftButton")) && !leftCubeActive)
		{
			dir = FaceDirection.Left;
			anim.SetTrigger("LeftWalk");
		}
		else if((Input.GetKeyDown(KeyCode.D) || ETCInput.GetButton("RightButton")) && !rightCubeActive)
		{
			dir = FaceDirection.Right;
			anim.SetTrigger("RightWalk");
		}
		
	}
	
	void CheckFaceDirection()
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
	

	void NextMove()
	{
		if(isInStar)
		{	
			if(starRemain >= 0)
			{
				starRemain -= Time.deltaTime;
			}
			else
			{
				transform.position = new Vector2(transform.position.x + 50, transform.position.y);
				isMoving = false;
				isInStar = false;
				speedUp = 1f;
			}
		}
		if(starTeleport)
		{
			transform.position = new Vector2(transform.position.x - 50, transform.position.y);
			isMoving = false;
			starTeleport = false;
			isInStar = true;
		}
		else
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
				process += Time.deltaTime * speed * speedUp * playerStatus.frozenSpeed;
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
	}

	void Start () 
	{	
		anim = GetComponent<Animator>();
		nextPosition = new Vector2(transform.position.x, transform.position.y - 1);
		nextDir = FaceDirection.Down;
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[1];
		playerStatus = GetComponent<PlayerStatus_Level3>();
		playerAttack = GetComponent<PlayerAttack_Level3>();
	}

	void FixCameraPosition()
	{
		cam.transform.position = new Vector3(transform.position.x, transform.position.y, -10);
	}
	
	void FixedUpdate()
	{
		if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
		{
			//tcpClient.SendPlayerCurrentInfo(transform.position, dir, PlayerStatusControl_Level3._instance.playerIdentity);
			TcpClient_All._instance.SendPlayerCurrentInfo(transform.position, dir, PlayerStatusControl_Level3._instance.playerIdentity);
			ControlMove();
			NextMove();
		}
		else
		{
			EnemyMove();
		}
		CheckFaceDirection();
	}

	
	
}
