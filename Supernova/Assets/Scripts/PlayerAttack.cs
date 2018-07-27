using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

	public KeyCode shootKey;
	private Animator anim;
	public GameObject[] bullet;
	private bool isShoot = false; //表示是否射击
	private float shootTimer = 0.0f;// 射击的计时器
	public float shootTime = 0.47f;// 表示射击的CD时间
	private FaceDirection faceDir;
	private string currentCommand;

	public GameObject networkManager;
	private TcpClient tcpClient;

	public bool autoAttack;

	public void SetCurrentCommand(string str)
	{
		currentCommand = str;
	}

	void Start()
	{
		anim = GetComponent<Animator>();
		tcpClient = networkManager.GetComponent<TcpClient>();
	}

	void FixedUpdate()
	{
		if (isShoot)
		{
			shootTimer += Time.deltaTime;
			if(shootTimer >= shootTime)
			{
				shootTimer = 0.0f;
				isShoot = false;
			}
		}
		else
		{
			Shoot();
		}
	}

	void Shoot()
	{
		if(gameObject.CompareTag("Player1"))
		{
			if(PlayerStatusControl._instance.isPlayer1 == true)
			{
				ControlShoot();
			}else
			{
				EnemyShoot();
			}
		}
		else if(gameObject.CompareTag("Player2"))
		{
			if(PlayerStatusControl._instance.isPlayer1 == false)
			{
				ControlShoot();
			}else
			{
				EnemyShoot();
			}
		}
	}

	void ControlShoot()
	{
		if(ETCInput.GetButton("Button") || Input.GetKeyDown(shootKey))
		{
			tcpClient.SendSelfCommand("F");
			Fire();
		}
	}

	void EnemyShoot()
	{
		if(currentCommand == "F")
		{
			Fire();
			currentCommand = "";
		}
	}


	void Fire()
	{
		isShoot = true;
		faceDir = gameObject.GetComponent<PlayerMove_Sudden>().dir;
		switch (faceDir)
		{
			case FaceDirection.Up:
				anim.SetTrigger("UpAttack");
				GameObject go1 = GameManager.Instantiate(bullet[0], new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), Quaternion.identity);
				go1.GetComponent<Bullet_V2>().dir = FaceDirection.Up;
				break;

			case FaceDirection.Down:
				anim.SetTrigger("DownAttack");
				GameObject go2 = GameManager.Instantiate(bullet[1], new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
				go2.GetComponent<Bullet_V2>().dir = FaceDirection.Down;
				break;

			case FaceDirection.Left:
				anim.SetTrigger("LeftAttack");
				GameObject go3 = GameManager.Instantiate(bullet[2], new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
				go3.GetComponent<Bullet_V2>().dir = FaceDirection.Left;
				break;

			case FaceDirection.Right:
				anim.SetTrigger("RightAttack");
				GameObject go4 = GameManager.Instantiate(bullet[3], new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
				go4.GetComponent<Bullet_V2>().dir = FaceDirection.Right;
				break;
		}
		
	}
}


			
