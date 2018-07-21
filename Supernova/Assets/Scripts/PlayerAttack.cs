using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	public GameObject item;
	public KeyCode player1Attack;
	public KeyCode player2Attack;
	private FaceDirection player1Dir;
	private FaceDirection player2Dir;

	RaycastHit2D hit;
	private Vector2 offset;
	private GameObject player1;
	private GameObject player2;

	private void Awake()
	{
		player1 = GameObject.FindGameObjectWithTag("Player1");
		player2 = GameObject.FindGameObjectWithTag("Player2");
	}

	private void Update()
	{
		Player1Attack();
		Player2Attack();
	}

	private void Player1Attack()
	{
		if (Input.GetKeyDown(player1Attack))
		{
			Debug.Log("Player1按下了攻击按钮");
			offset = transform.position - player2.transform.position;
			if(offset.magnitude < 1.1f)
			{
				Debug.Log("Player2处于攻击范围内");
				switch (player1.GetComponent<PlayerMove>().dir)
				{
					case FaceDirection.Down:
						if(player2.transform.position.y < this.transform.position.y)
						{
							Debug.Log("Player2位于Player1下方");
							Player2Status._instance.Damage(0.5f);
						}
						break;

					case FaceDirection.Up:
						if(player2.transform.position.y > this.transform.position.y)
						{
							Debug.Log("Player2位于Player1上方");
							Player2Status._instance.Damage(0.5f);
						}
						break;

					case FaceDirection.Left:
						if(player2.transform.position.x < this.transform.position.x)
						{
							Debug.Log("Player2位于Player1左边");
							Player2Status._instance.Damage(0.5f);
						}
						break;

					case FaceDirection.Right:
						if(player2.transform.position.x > this.transform.position.x)
						{
							Debug.Log("Player2位于Player1右边");
							Player2Status._instance.Damage(0.5f);
						}
						break;
				}
			}
		}
	}

	private void Player2Attack()
	{
		if (Input.GetKeyDown(player2Attack))
		{
			Debug.Log("Player2按下了攻击按钮");
			offset = transform.position - player1.transform.position;
			if (offset.magnitude < 1.1f)
			{
				Debug.Log("Player1处于攻击范围内");
				switch (player2.GetComponent<PlayerMove>().dir)
				{
					case FaceDirection.Down:
						if (player1.transform.position.y < this.transform.position.y)
						{
							Debug.Log("Player1位于Player2下方");
							Player1Status._instance.Damage(0.5f);
						}
						break;

					case FaceDirection.Up:
						if (player1.transform.position.y > this.transform.position.y)
						{
							Debug.Log("Player1位于Player2上方");
							Player1Status._instance.Damage(0.5f);
						}
						break;

					case FaceDirection.Left:
						if (player1.transform.position.x < this.transform.position.x)
						{
							Debug.Log("Player1位于Player2左边");
							Player1Status._instance.Damage(0.5f);
						}
						break;

					case FaceDirection.Right:
						if (player1.transform.position.x > this.transform.position.x)
						{
							Debug.Log("Player1位于Player2右边");
							Player1Status._instance.Damage(0.5f);
						}
						break;
				}
			}
		}
	}


	/*
	private void Player1Attack()
	{
		//当Player1按下攻击按钮时：
		if (Input.GetKeyDown(player1Attack))
		{
			Debug.Log("Player1进行了攻击！");
			player1Dir = this.gameObject.GetComponent<PlayerMove>().dir;
			//根据玩家的方向发射射线检测是否能够攻击到Player2，如果可以就调用Player2Status的Damage()方法
			switch (player1Dir)
			{
				case FaceDirection.Down:
					Debug.Log(player1Dir);
					hit = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y - 1.1f));
					if (hit.transform.gameObject.tag == "Player2")
					{
						Debug.Log("Player1攻击到了Player2!!!");
						Player2Status._instance.Damage(0.5f);
					}
					break;

				case FaceDirection.Up:
					Debug.Log(player1Dir);
					hit = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y + 1.1f));
					if (hit.transform.gameObject.tag == "Player2")
					{
						Debug.Log("Player1攻击到了Player2!!!");
						Player2Status._instance.Damage(0.5f);
					}
					break;

				case FaceDirection.Left:
					Debug.Log(player1Dir);
					hit = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x - 1.1f, this.transform.position.y));
					if (hit.transform.gameObject.tag == "Player2")
					{
						Debug.Log("Player1攻击到了Player2!!!");
						Player2Status._instance.Damage(0.5f);
					}
					break;

				case FaceDirection.Right:
					Debug.Log(player1Dir);
					hit = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + 1.1f, this.transform.position.y));
					if (hit.transform.gameObject.tag == "Player2")
					{
						Debug.Log("Player1攻击到了Player2!!!");
						Player2Status._instance.Damage(0.5f);
					}
					break;

			}
		}
	}

	
	private void Player2Attack()
	{
		//当Player2按下攻击按钮时：
		if (Input.GetKeyDown(player2Attack))
		{
			Debug.Log("Player2进行了攻击！！");
			player2Dir = this.gameObject.GetComponent<PlayerMove>().dir;
			//根据玩家的方向发射射线检测是否能够攻击到Player1，如果可以就调用Player1Status的Damage()方法
			switch (player2Dir)
			{
				case FaceDirection.Down:
					Debug.Log(player2Dir);
					hit = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y - 1.1f));
					if (hit.transform.gameObject.tag == "Player1")
					{
						Debug.Log("Player2攻击到了Player1!!!");
						Player2Status._instance.Damage(0.5f);
					}
					break;

				case FaceDirection.Up:
					Debug.Log(player2Dir);
					hit = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x, this.transform.position.y + 1.1f));
					if (hit.transform.gameObject.tag == "Player1")
					{
						Debug.Log("Player2攻击到了Player1!!!");
						Player2Status._instance.Damage(0.5f);
					}
					break;

				case FaceDirection.Left:
					Debug.Log(player2Dir);
					hit = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x - 1.1f, this.transform.position.y));
					if (hit.transform.gameObject.tag == "Player1")
					{
						Debug.Log("Player2攻击到了Player1!!!");
						Player2Status._instance.Damage(0.5f);
					}
					break;

				case FaceDirection.Right:
					Debug.Log(player2Dir);
					hit = Physics2D.Linecast(new Vector2(this.transform.position.x, this.transform.position.y), new Vector2(this.transform.position.x + 1.1f, this.transform.position.y));
					if (hit.transform.gameObject.tag == "Player1")
					{
						Debug.Log("Player2攻击到了Player1!!!");
						Player2Status._instance.Damage(0.5f);
					}
					break;

			}
		}
	}
	*/

}