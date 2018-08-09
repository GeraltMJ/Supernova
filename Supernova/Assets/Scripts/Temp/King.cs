using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class King : MonoBehaviour {

	public bool firstPress = false;
	public bool secondPress = false;
	public bool thirdPress = false;

	public float speed = 12.0f;

	public GameObject kingBullet;

	public GameObject bulletParent;
	private PlayerMove_Level3 move;
	

	// Use this for initialization
	void Start () {
		move = this.GetComponent<PlayerMove_Level3>();
	}
	
	// Update is called once per frame
	void Update () {
		

		if(firstPress && !secondPress && !thirdPress)
		{
			GenerateKingBullet(); // 在PlayerMove里面调用这个函数，走一步调用一次
		}
		else if(firstPress && secondPress && !thirdPress)
		{
			//按两下的时候什么也不做
		}
		else if(firstPress && secondPress && thirdPress)
		{
			KingAttack();
		}



	}

	public void GenerateKingBullet()
	{
		if (firstPress && !secondPress && !thirdPress)
		{
			GameObject bullet = GameObject.Instantiate(kingBullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 90), bulletParent.transform);
		}
	}
	

	private void KingAttack()
	{
		switch (move.dir)
		{
			case FaceDirection.Up:
				KingAttackUp();
				break;

			case FaceDirection.Down:
				KingAttackDown();
				break;

			case FaceDirection.Left:
				KingAttackLeft();
				break;

			case FaceDirection.Right:
				KingAttackRight();
				break;
		}
		firstPress = false;
		secondPress = false;
		thirdPress = false;
	}

	private void KingAttackUp()
	{
		bulletParent.transform.Translate(Time.deltaTime * speed * Vector3.up);
	}

	private void KingAttackDown()
	{
		bulletParent.transform.Translate(Time.deltaTime * speed * Vector3.down);
	}

	private void KingAttackLeft()
	{
		bulletParent.transform.Translate(Time.deltaTime * speed * Vector3.left);
	}

	private void KingAttackRight()
	{
		bulletParent.transform.Translate(Time.deltaTime * speed * Vector3.right);
	}

}
