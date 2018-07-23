using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{

	
	public KeyCode player1Attack;
	public KeyCode player2Attack;
	private FaceDirection player1Dir;
	private FaceDirection player2Dir;
	public KeyCode player1Shoot;
	public KeyCode player2Shoot;

	public Animator animator;

	public GameObject[] player1Bullet;//Player1使用的攻击道具
	public GameObject[] player2Bullet;//Player2使用的攻击道具

	RaycastHit2D hit;
	private Vector2 offset;
	private GameObject player1;
	private GameObject player2;



	private bool attacked; // 表示是否按下了攻击键
	private float attackTimer = 0.0f; // 肉搏攻击的计时器
	private float attackTime = 0.5f; // 表示攻击的CD时间


	private bool shoot; //表示是否射击
	private float shootTimer = 0.0f;// 射击的计时器
	private float shootTime = 0.4f;// 表示射击的CD时间


	private void Awake()
	{
		player1 = GameObject.FindGameObjectWithTag("Player1");
		player2 = GameObject.FindGameObjectWithTag("Player2");
		animator = this.GetComponent<Animator>();
	}

	private void Update()
	{
		if (attacked)
		{
			attackTimer += Time.deltaTime;
			if(attackTimer >= attackTime)
			{
				attackTimer = 0.0f;
				attacked = false;
			}
		}
		else
		{
			Player1Attack();
			Player2Attack();
		}

		if (shoot)
		{
			shootTimer += Time.deltaTime;
			if(shootTimer >= shootTime)
			{
				shootTimer = 0.0f;
				shoot = false;
			}
		}
		else
		{
			Player1Shoot();
			Player2Shoot();
		}
	}

	private void Player1Attack()
	{
		if (Input.GetKeyDown(player1Attack))
		{
			attacked = true;
			Debug.Log("Player1按下了攻击按钮");
			offset = transform.position - player2.transform.position;
			player1Dir = this.GetComponent<PlayerMove>().dir;
			switch (player1Dir)
			{
				case FaceDirection.Up:
					animator.SetTrigger("UpAttack");
					break;
				case FaceDirection.Down:
					animator.SetTrigger("DownAttack");
					break;
				case FaceDirection.Left:
					animator.SetTrigger("LeftAttack");
					break;
				case FaceDirection.Right:
					animator.SetTrigger("RightAttack");
					break;
			}

			if(offset.magnitude < 1.1f)
			{
				Debug.Log("Player2处于攻击范围内");
				switch (player1.GetComponent<PlayerMove>().dir)
				{
					case FaceDirection.Down:
						
						if(player2.transform.position.y < this.transform.position.y)
						{
							Debug.Log("Player2位于Player1下方");
							Player2Status._instance.Damage(1f);
						}
						break;

					case FaceDirection.Up:
						
						if (player2.transform.position.y > this.transform.position.y)
						{
							Debug.Log("Player2位于Player1上方");
							Player2Status._instance.Damage(1f);
						}
						break;

					case FaceDirection.Left:
						
						if (player2.transform.position.x < this.transform.position.x)
						{
							Debug.Log("Player2位于Player1左边");
							Player2Status._instance.Damage(1f);
						}
						break;

					case FaceDirection.Right:
						
						if (player2.transform.position.x > this.transform.position.x)
						{
							Debug.Log("Player2位于Player1右边");
							Player2Status._instance.Damage(1f);
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
			attacked = true;
			Debug.Log("Player2按下了攻击按钮");
			offset = transform.position - player1.transform.position;
			player2Dir = this.gameObject.GetComponent<PlayerMove>().dir;
			switch (player2Dir)
			{
				case FaceDirection.Up:
					animator.SetTrigger("UpAttack");
					break;
				case FaceDirection.Down:
					animator.SetTrigger("DownAttack");
					break;
				case FaceDirection.Left:
					animator.SetTrigger("LeftAttack");
					break;
				case FaceDirection.Right:
					animator.SetTrigger("RightAttack");
					break;
			}

			if (offset.magnitude < 1.1f)
			{
				Debug.Log("Player1处于攻击范围内");
				switch (player2.GetComponent<PlayerMove>().dir)
				{
					case FaceDirection.Down:
						
						if (player1.transform.position.y < this.transform.position.y)
						{
							Debug.Log("Player1位于Player2下方");
							Player1Status._instance.Damage(1f);
						}
						break;

					case FaceDirection.Up:
						
						if (player1.transform.position.y > this.transform.position.y)
						{
							Debug.Log("Player1位于Player2上方");
							Player1Status._instance.Damage(1f);
						}
						break;

					case FaceDirection.Left:
						
						if (player1.transform.position.x < this.transform.position.x)
						{
							Debug.Log("Player1位于Player2左边");
							Player1Status._instance.Damage(1f);
						}
						break;

					case FaceDirection.Right:
						
						if (player1.transform.position.x > this.transform.position.x)
						{
							Debug.Log("Player1位于Player2右边");
							Player1Status._instance.Damage(1f);
						}
						break;
				}
			}
		}
	}

	private void Player1Shoot()
	{
		if (Input.GetKeyDown(player1Shoot))
		{
			shoot = true;
			player1Dir = this.gameObject.GetComponent<PlayerMove>().dir;
			switch (player1Dir)
			{
				case FaceDirection.Up:
					GameObject go1 = GameManager.Instantiate(player1Bullet[0], new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
					go1.GetComponent<Bullet>().dir = FaceDirection.Up;
					break;

				case FaceDirection.Down:
					GameObject go2 = GameManager.Instantiate(player1Bullet[1], new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
					go2.GetComponent<Bullet>().dir = FaceDirection.Down;
					break;

				case FaceDirection.Left:
					GameObject go3 = GameManager.Instantiate(player1Bullet[2], new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z), Quaternion.identity);
					go3.GetComponent<Bullet>().dir = FaceDirection.Left;
					break;

				case FaceDirection.Right:
					GameObject go4 = GameManager.Instantiate(player1Bullet[3], new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z), Quaternion.identity);
					go4.GetComponent<Bullet>().dir = FaceDirection.Right;
					break;
			}
		}
	}

	private void Player2Shoot()
	{
		if (Input.GetKeyDown(player2Shoot))
		{
			shoot = true;
			player2Dir = this.gameObject.GetComponent<PlayerMove>().dir;
			switch (player2Dir)
			{
				case FaceDirection.Up:
					GameObject go1 = GameManager.Instantiate(player2Bullet[0], new Vector3(this.transform.position.x, this.transform.position.y + 1, this.transform.position.z), Quaternion.identity);
					go1.GetComponent<Bullet>().dir = FaceDirection.Up;
					break;

				case FaceDirection.Down:
					GameObject go2 = GameManager.Instantiate(player2Bullet[1], new Vector3(this.transform.position.x, this.transform.position.y - 1, this.transform.position.z), Quaternion.identity);
					go2.GetComponent<Bullet>().dir = FaceDirection.Down;
					break;

				case FaceDirection.Left:
					GameObject go3 = GameManager.Instantiate(player2Bullet[2], new Vector3(this.transform.position.x - 1, this.transform.position.y, this.transform.position.z), Quaternion.identity);
					go3.GetComponent<Bullet>().dir = FaceDirection.Left;
					break;

				case FaceDirection.Right:
					GameObject go4 = GameManager.Instantiate(player2Bullet[2], new Vector3(this.transform.position.x + 1, this.transform.position.y, this.transform.position.z), Quaternion.identity);
					go4.GetComponent<Bullet>().dir = FaceDirection.Right;
					break;
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