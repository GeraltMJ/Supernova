using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour {
	public iTween.EaseType easeType;
	public KeyCode up;
	public KeyCode down;
	public KeyCode left;
	public KeyCode right;

	public float timer = 0.2f;

	public float nowTime = 0.0f;

	public bool isMoving = false;
	public bool faceUp = false;
	public bool faceDown = true;
	public bool faceLeft = false;
	public bool faceRight = false;

	//向上运动的参数
	Hashtable upArgs1 = iTween.Hash("x", 0, "y", 0.75, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic); //向上的第一段位移：x坐标不变，y坐标 +0.75
	Hashtable upArgs2 = iTween.Hash("x", 0, "delay", 0.1, "y", -0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic); //向上的第二段位移，x坐标不变，y坐标 -0.25

	//向下运动的参数
	Hashtable downArgs1 = iTween.Hash("x", 0, "y", 0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);
	Hashtable downArgs2 = iTween.Hash("x", 0, "delay", 0.1, "y", -0.75, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);

	//向左运动的参数
	Hashtable leftArgs1 = iTween.Hash("x", -0.25, "y", 0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);
	Hashtable leftArgs2 = iTween.Hash("x", -0.25, "delay", 0.1, "y", -0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);

	//向右运动的参数
	Hashtable rightArgs1 = iTween.Hash("x", 0.25, "y", 0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);
	Hashtable rightArgs2 = iTween.Hash("x", 0.25, "delay", 0.1, "y", -0.25, "time", 0.1, "easetype", iTween.EaseType.easeInOutCubic);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!isMoving)
		{
			if (Input.GetKey(up) && !isMoving)
			{
				isMoving = true;
				faceDown = false;
				faceUp = true;
				faceLeft = false;
				faceRight = false;
			    
				iTween.MoveBy(this.gameObject, upArgs1);
				iTween.MoveBy(this.gameObject, upArgs2);
			}
			if (Input.GetKey(down) && !isMoving)
			{
				isMoving = true;
				faceDown = true;
				faceUp = false;
				faceLeft = false;
				faceRight = false;

				iTween.MoveBy(this.gameObject, downArgs1);
				iTween.MoveBy(this.gameObject, downArgs2);
			}
			if (Input.GetKey(left) && !isMoving)
			{
				isMoving = true;
				faceDown = false;
				faceUp = false;
				faceLeft = true;
				faceRight = false;

				iTween.MoveBy(this.gameObject, leftArgs1);
				iTween.MoveBy(this.gameObject, leftArgs2);
			}
			if (Input.GetKey(right) && !isMoving)
			{
				isMoving = true;
				faceDown = false;
				faceUp = false;
				faceLeft = false;
				faceRight = true;

				iTween.MoveBy(this.gameObject, rightArgs1);
				iTween.MoveBy(this.gameObject, rightArgs2);
			}
		}
		else
		{
			nowTime += Time.deltaTime;
			if(nowTime >= timer)
			{
				nowTime = 0;
				isMoving = false;
			}
		}
	}
}
