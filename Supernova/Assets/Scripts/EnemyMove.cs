using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
	private string currentCommand;
	public GameObject tcpHolder;
	private TcpClient tcp;

	public iTween.EaseType easeType;

	public float timer = 0.2f;

	public float nowTime = 0.0f;

	private float fixPositionX;
	private float fixPositionY;



	public bool isMoving = false;
	public bool faceUp = false;
	public bool faceDown = true;
	public bool faceLeft = false;
	public bool faceRight = false;

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

	void MoveUp()
	{
		isMoving = true;
		faceDown = false;
		faceUp = true;
		faceLeft = false;
		faceRight = false;

		iTween.MoveBy(this.gameObject, upArgs1);
		iTween.MoveBy(this.gameObject, upArgs2);
	}

	void MoveDown()
	{
		isMoving = true;
		faceDown = true;
		faceUp = false;
		faceLeft = false;
		faceRight = false;

		iTween.MoveBy(this.gameObject, downArgs1);
		iTween.MoveBy(this.gameObject, downArgs2);
	}
	void MoveLeft()
	{
		isMoving = true;
		faceDown = false;
		faceUp = false;
		faceLeft = true;
		faceRight = false;

		iTween.MoveBy(this.gameObject, leftArgs1);
		iTween.MoveBy(this.gameObject, leftArgs2);
	}
	void MoveRight()
	{
		isMoving = true;
		faceDown = false;
		faceUp = false;
		faceLeft = false;
		faceRight = true;

		iTween.MoveBy(this.gameObject, rightArgs1);
		iTween.MoveBy(this.gameObject, rightArgs2);
	}
	
	void FixPosition()
	{
		fixPositionX = Mathf.Floor(this.transform.position.x) + 0.5f;
		fixPositionY = Mathf.Floor(this.transform.position.y) + 0.5f;
		this.transform.position = new Vector2(fixPositionX, fixPositionY);
	}
	

	void StatusCheck()
	{
		nowTime += Time.deltaTime;
		if (nowTime >= timer)
		{
			nowTime = 0;
			isMoving = false;
		}
	}

	public void SetCurrentCommand(string command)
	{
		currentCommand = command;
	}

	void Start()
	{
		tcp = tcpHolder.GetComponent<TcpClient>();
	}


	// Update is called once per frame
	void Update()
	{
		if (!isMoving)
		{
			if (currentCommand == "W" && !isMoving)
			{
				MoveUp();
			}
			else if (currentCommand == "S" && !isMoving)
			{
				MoveDown();
			}
			else if (currentCommand == "A" && !isMoving)
			{
				MoveLeft();
			}
			else if (currentCommand == "D" && !isMoving)
			{
				MoveRight();
			}

			FixPosition();
		}
		else
		{
			StatusCheck();
		}
	}
}
