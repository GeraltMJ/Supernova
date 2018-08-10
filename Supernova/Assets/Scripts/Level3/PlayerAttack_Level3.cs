using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack_Level3 : MonoBehaviour {

	public KeyCode shootKey;
	private Animator anim;
	private bool isShoot = false; //表示是否射击
	private float shootTimer = 0.0f;// 射击的计时器
	public float shootTime = 0.47f;// 表示射击的CD时间
	private FaceDirection faceDir;
	private bool fireCommand;

	public GameObject dragonBullet, knightBullet, magicBullet, assassinBullet, bossBullet;
	private GameObject bullet;

	public GameObject shieldSkillBullet, gunSkillBullet;

	//public GameObject networkManager;
	//private TcpClient_Level3 tcpClient;

	public AudioClip dragonAttack, knightAttack, magicAttack, assassinAttack, bossAttack;
	private AudioSource audioSource;
	private AudioSource[] audioSources;
	private Vector2 firePosition;
	private PlayerStatus_Level3 playerStatus;
	public void SetFireCommand(Vector2 pos)
	{
		fireCommand = true;
		firePosition = pos;
	}

	void Start()
	{
		anim = GetComponent<Animator>();
		//tcpClient = networkManager.GetComponent<TcpClient_Level3>();
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[0];
		firePosition = transform.position;
		playerStatus = GetComponent<PlayerStatus_Level3>();
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
		if (playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
		{
			ControlShoot();
		}
		else
		{
			EnemyShoot();
		}
	}

	void ControlShoot()
	{
		if(ETCInput.GetButton("Button_Level3") || Input.GetKeyDown(shootKey))
		{
			isShoot = true;
			//tcpClient.SendFireCommand(transform.position, PlayerStatusControl_Level3._instance.playerIdentity);
			TcpClient_All._instance.SendFireCommand(transform.position, PlayerStatusControl_Level3._instance.playerIdentity);
			switch(playerStatus.playerSkill)
			{
				case PlayerSkill_Level3.ShieldSkill:
					ShieldSkillFire();
					break;
				case PlayerSkill_Level3.GunSkill:
					GunSkillFire();
					break;
				case PlayerSkill_Level3.DoubleDmgSkill:
					DoubleDmgSkillFire();
					break;
				case PlayerSkill_Level3.IceSkill:
					IceSkillFire();
					break;
				case PlayerSkill_Level3.Default:
					Fire();
					break;
			}
		}
	}
	void ShieldSkillFire()
	{
		GameObject go = (GameObject)Instantiate(shieldSkillBullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
		go.transform.parent = gameObject.transform;
		SetSkillTag(go);
		Destroy(go,1);
	}
	void GunSkillFire()
	{
		faceDir = gameObject.GetComponent<PlayerMove_Level3>().dir;
		switch(faceDir)
		{
			case FaceDirection.Down:
				GameObject go = (GameObject)Instantiate(gunSkillBullet, new Vector3(this.transform.position.x, this.transform.position.y-15, this.transform.position.z), Quaternion.Euler(0f,0f,90f));
				go.transform.parent = gameObject.transform;
				SetSkillTag(go);
				Destroy(go,1);
				break;
			case FaceDirection.Up:
				go = (GameObject)Instantiate(gunSkillBullet, new Vector3(this.transform.position.x, this.transform.position.y+15, this.transform.position.z), Quaternion.Euler(0f,0f,-90f));
				go.transform.parent = gameObject.transform;
				SetSkillTag(go);
				Destroy(go,1);
				break;
			case FaceDirection.Left:
				go = (GameObject)Instantiate(gunSkillBullet, new Vector3(this.transform.position.x-15, this.transform.position.y, this.transform.position.z), Quaternion.identity);
				go.transform.parent = gameObject.transform;
				SetSkillTag(go);
				Destroy(go,1);
				break;
			case FaceDirection.Right:
				go = (GameObject)Instantiate(gunSkillBullet, new Vector3(this.transform.position.x+15, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f,0f,-180f));
				go.transform.parent = gameObject.transform;
				SetSkillTag(go);
				Destroy(go,1);
				break;
		}
	}
	void DoubleDmgSkillFire()
	{
		faceDir = gameObject.GetComponent<PlayerMove_Level3>().dir;
		SelectAccordingToPower(playerStatus.playerPower);
		audioSource.Play();
		switch(faceDir)
		{
			case FaceDirection.Up:
				UpDoubleAttack();
				break;
			case FaceDirection.Down:
				DownDoubleAttack();
				break;
			case FaceDirection.Left:
				LeftDoubleAttack();
				break;
			case FaceDirection.Right:
				RightDoubleAttack();
				break;
		}
	}
	void IceSkillFire()
	{

	}

	void EnemyShoot()
	{
		if(fireCommand)
		{
			isShoot = true;
			switch(playerStatus.playerSkill)
			{
				case PlayerSkill_Level3.ShieldSkill:
					ShieldSkillFire();
					break;
				case PlayerSkill_Level3.GunSkill:
					GunSkillFire();
					break;
				case PlayerSkill_Level3.DoubleDmgSkill:
					DoubleDmgSkillFire();
					break;
				case PlayerSkill_Level3.IceSkill:
					IceSkillFire();
					break;
				case PlayerSkill_Level3.Default:
					EnemyFire();
					break;
			}
			fireCommand = false;
			firePosition = transform.position;
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
		else if(gameObject.CompareTag("Player3"))
		{
			bullet.tag = "Player3Bullet";
		}
	}

	void SetSkillTag(GameObject bullet)
	{
		if(gameObject.CompareTag("Player1"))
		{
			bullet.tag = "Player1Skill";
		}
		else if(gameObject.CompareTag("Player2"))
		{
			bullet.tag = "Player2Skill";
		}
		else if(gameObject.CompareTag("Player3"))
		{
			bullet.tag = "Player3Skill";
		}
	}
	void SetBulletType(GameObject bullet)
	{
		Bullet_Level3 bl = bullet.GetComponent<Bullet_Level3>();
		bl.bulletType = playerStatus.playerCharacter;
	}

	void SelectAccordingToPower(PlayerPower_Level3 power)
	{
		switch(power)
		{
			case PlayerPower_Level3.DragonPower1:
				bullet = dragonBullet;
				audioSource.clip = dragonAttack;
				break;
			case PlayerPower_Level3.DragonPower2:
				bullet = dragonBullet;
				audioSource.clip = dragonAttack;
				break;
			case PlayerPower_Level3.DragonPower3:
				bullet = dragonBullet;
				audioSource.clip = dragonAttack;
				break;
			case PlayerPower_Level3.KnightPower1:
				bullet = knightBullet;
				audioSource.clip = knightAttack;
				break;
			case PlayerPower_Level3.KnightPower2:
				bullet = knightBullet;
				audioSource.clip = knightAttack;
				break;
			case PlayerPower_Level3.MagicPower1:
				bullet = magicBullet;
				audioSource.clip = magicAttack;
				break;
			case PlayerPower_Level3.MagicPower2:
				bullet = magicBullet;
				audioSource.clip = magicAttack;
				break;
			case PlayerPower_Level3.MagicPower3:
				bullet = magicBullet;
				audioSource.clip = magicAttack;
				break;
			case PlayerPower_Level3.AssassinPower1:
				bullet = assassinBullet;
				audioSource.clip = assassinAttack;
				break;
			case PlayerPower_Level3.AssassinPower2:
				bullet = assassinBullet;
				audioSource.clip = assassinAttack;
				break;
			case PlayerPower_Level3.BossPower:
				bullet = bossBullet;
				audioSource.clip = bossAttack;
				break;
			case PlayerPower_Level3.Default:
				bullet = null;
				audioSource.clip = null;
				break;
		}
	}

	void Fire()
	{
		faceDir = gameObject.GetComponent<PlayerMove_Level3>().dir;
		Debug.Log(bullet);
		SelectAccordingToPower(playerStatus.playerPower);
		audioSource.Play();
		Debug.Log(bullet);
		switch (faceDir)
		{
			case FaceDirection.Up:
				anim.SetTrigger("UpAttack");
				if(bullet)
				{	
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
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
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
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
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
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
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
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

	void EnemyFire()
	{
		faceDir = gameObject.GetComponent<PlayerMove_Level3>().dir;
		SelectAccordingToPower(playerStatus.playerPower);
		audioSource.Play();
		switch (faceDir)
		{
			case FaceDirection.Up:
				anim.SetTrigger("UpAttack");
				if(bullet)
				{	
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						EnemyUpAttack();
						EnemyLeftAttack();
						EnemyRightAttack();
					}
					else
					{
						EnemyUpAttack();
					}
				}
				break;

			case FaceDirection.Down:
				anim.SetTrigger("DownAttack");
				if(bullet)
				{	
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						EnemyDownAttack();
						EnemyLeftAttack();
						EnemyRightAttack();
					}
					else
					{
						EnemyDownAttack();
					}
				}
				break;

			case FaceDirection.Left:
				anim.SetTrigger("LeftAttack");
				if(bullet)
				{
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						EnemyLeftAttack();
						EnemyUpAttack();
						EnemyDownAttack();
					}
					else
					{
						EnemyLeftAttack();
					}
				}
				break;

			case FaceDirection.Right:
				anim.SetTrigger("RightAttack");
				if(bullet)
				{
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						EnemyRightAttack();
						EnemyUpAttack();
						EnemyDownAttack();
					}
					else
					{
						EnemyRightAttack();
					}
				}
				break;
		}
		
	}

	void UpAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f,0f,90f));
		SetBulletTag(go);
		SetBulletType(go);
	}

	void DownAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f,0f,-90f));
		SetBulletTag(go);
		SetBulletType(go);
	}

	void LeftAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f,0f,-180f));
		SetBulletTag(go);
		SetBulletType(go);
	}

	void RightAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
		SetBulletTag(go);
		SetBulletType(go);
	}
	void EnemyUpAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector2(firePosition.x, firePosition.y), Quaternion.Euler(0f,0f,90f));
		SetBulletTag(go);
		SetBulletType(go);
	}

	void EnemyDownAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector2(firePosition.x, firePosition.y), Quaternion.Euler(0f,0f,-90f));
		SetBulletTag(go);
		SetBulletType(go);
	}

	void EnemyLeftAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector2(firePosition.x, firePosition.y), Quaternion.Euler(0f,0f,-180f));
		SetBulletTag(go);
		SetBulletType(go);
	}

	void EnemyRightAttack()
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector2(firePosition.x, firePosition.y), Quaternion.identity);
		SetBulletTag(go);
		SetBulletType(go);
	}

	void DownDoubleAttack()
	{
		GameObject b1 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -45), this.transform);
		GameObject b2 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -90), this.transform);
		GameObject b3 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -135), this.transform);
		SetBulletTag(b1);
		SetBulletType(b1);
		SetBulletTag(b2);
		SetBulletType(b2);
		SetBulletTag(b3);
		SetBulletType(b3);
	}

	void UpDoubleAttack()
	{
		GameObject b1 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 45), this.transform);
		GameObject b2 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 90), this.transform);
		GameObject b3 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 135), this.transform);
		SetBulletTag(b1);
		SetBulletType(b1);
		SetBulletTag(b2);
		SetBulletType(b2);
		SetBulletTag(b3);
		SetBulletType(b3);
	}

	void LeftDoubleAttack()
	{
		GameObject b1 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -135), this.transform);
		GameObject b2 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -180), this.transform);
		GameObject b3 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 135), this.transform);
		SetBulletTag(b1);
		SetBulletType(b1);
		SetBulletTag(b2);
		SetBulletType(b2);
		SetBulletTag(b3);
		SetBulletType(b3);
	}

	void RightDoubleAttack()
	{
		GameObject b1 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 45), this.transform);
		GameObject b2 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity, this.transform);
		GameObject b3 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -45), this.transform);
		SetBulletTag(b1);
		SetBulletType(b1);
		SetBulletTag(b2);
		SetBulletType(b2);
		SetBulletTag(b3);
		SetBulletType(b3);
	}
}


			
