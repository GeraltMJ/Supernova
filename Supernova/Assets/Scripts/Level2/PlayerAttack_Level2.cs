using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack_Level2 : MonoBehaviour {

	public KeyCode shootKey;
	private Animator anim;
	private bool isShoot = false; //表示是否射击
	private float shootTimer = 0.0f;// 射击的计时器
	public float shootTime = 0.47f;// 表示射击的CD时间
	private FaceDirection faceDir;
	private string currentCommand;

	public GameObject dragonBullet, knightBullet, magicBullet, assassinBullet, bossBullet;
	private GameObject bullet;

	public GameObject networkManager;
	private TcpClient_Level2 tcpClient;

	public AudioClip dragonAttack, knightAttack, magicAttack, assassinAttack, bossAttack;
	private AudioSource audioSource;
	private AudioSource[] audioSources;

	public bool autoAttack;

	public void SetCurrentCommand(string str)
	{
		currentCommand = str;
	}

	void Start()
	{
		anim = GetComponent<Animator>();
		tcpClient = networkManager.GetComponent<TcpClient_Level2>();
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[0];
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
			if(PlayerStatusControl_Level2._instance.isPlayer1 == true)
			{
				ControlShoot();
			}else
			{
				EnemyShoot();
			}
		}
		else if(gameObject.CompareTag("Player2"))
		{
			if(PlayerStatusControl_Level2._instance.isPlayer1 == false)
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
		if(ETCInput.GetButton("Button_Level2") || Input.GetKeyDown(shootKey))
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

	void SetBulletTag(GameObject bullet)
	{
		if(gameObject.CompareTag("Player1"))
		{
			bullet.tag = "Player1Bullet";
		}
		else if(gameObject.CompareTag("Player2"))
		{
			bullet.tag = "Player2Bullet";
		}
	}


	void Fire()
	{
		isShoot = true;
		faceDir = gameObject.GetComponent<PlayerMove_Level2>().dir;

		if(gameObject.CompareTag("Player1"))
		{
			switch(Player1Status_Level2._instance.playerPower)
			{
				case PlayerPower_Level2.DragonPower:
					bullet = dragonBullet;
					audioSource.clip = dragonAttack;
					break;
				case PlayerPower_Level2.KnightPower:
					bullet = knightBullet;
					audioSource.clip = knightAttack;
					break;
				case PlayerPower_Level2.MagicPower:
					bullet = magicBullet;
					audioSource.clip = magicAttack;
					break;
				case PlayerPower_Level2.AssassinPower:
					bullet = assassinBullet;
					audioSource.clip = assassinAttack;
					break;
				case PlayerPower_Level2.BossPower:
					bullet = bossBullet;
					audioSource.clip = bossAttack;
					break;
				case PlayerPower_Level2.Default:
					bullet = null;
					audioSource.clip = null;
					break;
			}
		}
		else if(gameObject.CompareTag("Player2"))
		{
			switch(Player2Status_Level2._instance.playerPower)
			{
				case PlayerPower_Level2.DragonPower:
					bullet = dragonBullet;
					audioSource.clip = dragonAttack;
					break;
				case PlayerPower_Level2.KnightPower:
					bullet = knightBullet;
					audioSource.clip = knightAttack;
					break;
				case PlayerPower_Level2.MagicPower:
					bullet = magicBullet;
					audioSource.clip = magicAttack;
					break;
				case PlayerPower_Level2.AssassinPower:
					bullet = assassinBullet;
					audioSource.clip = assassinAttack;
					break;
				case PlayerPower_Level2.BossPower:
					bullet = bossBullet;
					audioSource.clip = bossAttack;
					break;
				case PlayerPower_Level2.Default:
					bullet = null;
					audioSource.clip = null;
					break;
			}
		}
		audioSource.Play();
		switch (faceDir)
		{
			case FaceDirection.Up:
				anim.SetTrigger("UpAttack");
				if(bullet)
				{	
					if((gameObject.CompareTag("Player1") && Player1Status_Level2._instance.playerPower == PlayerPower_Level2.BossPower) ||
						(gameObject.CompareTag("Player2") && Player2Status_Level2._instance.playerPower == PlayerPower_Level2.BossPower))
					{
						UpAttack();
						LeftAttack();
						RightAttack();
					}
					else
					{
						UpAttack();
					}
				}
				break;

			case FaceDirection.Down:
				anim.SetTrigger("DownAttack");
				if(bullet)
				{	
					if((gameObject.CompareTag("Player1") && Player1Status_Level2._instance.playerPower == PlayerPower_Level2.BossPower) ||
						(gameObject.CompareTag("Player2") && Player2Status_Level2._instance.playerPower == PlayerPower_Level2.BossPower))
					{
						DownAttack();
						LeftAttack();
						RightAttack();
					}
					else
					{
						DownAttack();
					}
				}
				break;

			case FaceDirection.Left:
				anim.SetTrigger("LeftAttack");
				if(bullet)
				{
					if((gameObject.CompareTag("Player1") && Player1Status_Level2._instance.playerPower == PlayerPower_Level2.BossPower) ||
						(gameObject.CompareTag("Player2") && Player2Status_Level2._instance.playerPower == PlayerPower_Level2.BossPower))
					{
						LeftAttack();
						UpAttack();
						DownAttack();
					}
					else
					{
						LeftAttack();
					}
				}
				break;

			case FaceDirection.Right:
				anim.SetTrigger("RightAttack");
				if(bullet)
				{
					if((gameObject.CompareTag("Player1") && Player1Status_Level2._instance.playerPower == PlayerPower_Level2.BossPower) ||
						(gameObject.CompareTag("Player2") && Player2Status_Level2._instance.playerPower == PlayerPower_Level2.BossPower))
					{
						RightAttack();
						UpAttack();
						DownAttack();
					}
					else
					{
						RightAttack();
					}
				}
				break;
		}
		
	}

	void UpAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), Quaternion.Euler(0f,0f,90f));
		SetBulletTag(go);
	}

	void DownAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.Euler(0f,0f,-90f));
		SetBulletTag(go);
	}

	void LeftAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f,0f,-180f));
		SetBulletTag(go);
	}

	void RightAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
		SetBulletTag(go);
	}
}


			
