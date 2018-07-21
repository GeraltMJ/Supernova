using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{

	//public GameObject tcpHolder;
	//private TcpClient tcp;

	public iTween.EaseType easeType;
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;
	public Animator animator;

	public float timer = 0.2f;

	public float nowTime = 0.0f;

	private float fixPositionX;
	private float fixPositionY;

	public FaceDirection dir = FaceDirection.Down;

	public bool isMoving = false;

	//向上运动的参数
	Hashtable upArgs1 = iTween.Hash("x", 0, "y", 1.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic); //向上的第一段位移：x坐标不变，y坐标 +0.75
	Hashtable upArgs2 = iTween.Hash("x", 0, "delay", 0.1, "y", -0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic); //向上的第二段位移，x坐标不变，y坐标 -0.25

	//向下运动的参数
	Hashtable downArgs1 = iTween.Hash("x", 0, "y", 0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);
	Hashtable downArgs2 = iTween.Hash("x", 0, "delay", 0.1, "y", -1.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);

	//向左运动的参数
	Hashtable leftArgs1 = iTween.Hash("x", -0.5, "y", 0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);
	Hashtable leftArgs2 = iTween.Hash("x", -0.5, "delay", 0.1, "y", -0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);

	//向右运动的参数
	Hashtable rightArgs1 = iTween.Hash("x", 0.5, "y", 0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);
	Hashtable rightArgs2 = iTween.Hash("x", 0.5, "delay", 0.1, "y", -0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);

	// Use this for initialization

	public void MoveUp()
	{
		isMoving = true;
		dir = FaceDirection.Up;

		iTween.MoveBy(this.gameObject, upArgs1);
		iTween.MoveBy(this.gameObject, upArgs2);
	}

	public void MoveDown()
	{
		isMoving = true;
		dir = FaceDirection.Down;

		iTween.MoveBy(this.gameObject, downArgs1);
		iTween.MoveBy(this.gameObject, downArgs2);
	}
	public void MoveLeft()
	{
		isMoving = true;
		dir = FaceDirection.Left;

		iTween.MoveBy(this.gameObject, leftArgs1);
		iTween.MoveBy(this.gameObject, leftArgs2);
	}
	public void MoveRight()
	{
		isMoving = true;
		dir = FaceDirection.Right;

		iTween.MoveBy(this.gameObject, rightArgs1);
		iTween.MoveBy(this.gameObject, rightArgs2);
	}

	
	public void FixPosition()
	{
		fixPositionX = Mathf.Floor(this.transform.position.x) + 0.5f;
		fixPositionY = Mathf.Floor(this.transform.position.y) + 0.5f;
		this.transform.position = new Vector2(fixPositionX, fixPositionY);
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

	void Start()
	{
		//tcp = tcpHolder.GetComponent<TcpClient>();
		animator = GetComponent<Animator>();

	}

	


	// Update is called once per frame
	void FixedUpdate()
	{
		if (!isMoving)
		{
			if (Input.GetKey(up) && !isMoving)
			{
				MoveUp();
				animator.SetTrigger("UpWalk");
				//tcp.SendSelfCommand("W");
			}
			else if (Input.GetKey(down) && !isMoving)
			{
				MoveDown();
				animator.SetTrigger("DownWalk");
				//tcp.SendSelfCommand("S");
			}
			else if (Input.GetKey(left) && !isMoving)
			{
				MoveLeft();
				animator.SetTrigger("LeftWalk");
				//tcp.SendSelfCommand("A");
			}
			else if (Input.GetKey(right) && !isMoving)
			{
				MoveRight();
				animator.SetTrigger("RightWalk");
				//tcp.SendSelfCommand("D");
			}
			else 
			{	
				//tcp.SendSelfCommand(" ");
			}

			FixPosition();
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