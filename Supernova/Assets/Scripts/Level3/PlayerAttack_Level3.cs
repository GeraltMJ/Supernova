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
	public GameObject iceDragonBullet, iceKnightBullet, iceMagicBullet, iceAssassinBullet, iceBossBullet;
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
					playerStatus.skillRemain -= 1;
					Fire(true);
					break;
				case PlayerSkill_Level3.Default:
					Fire(false);
					break;
			}
		}
	}
	void ShieldSkillFire()
	{
		playerStatus.skillRemain -= 1;
		GameObject go = (GameObject)Instantiate(shieldSkillBullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
		go.transform.parent = gameObject.transform;
		SetSkillTag(go);
		Destroy(go,1);
	}
	void GunSkillFire()
	{
		playerStatus.skillRemain -= 1;
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
		playerStatus.skillRemain -= 1;
		faceDir = gameObject.GetComponent<PlayerMove_Level3>().dir;
		SelectAccordingToPower(playerStatus.playerPower, false);
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
					playerStatus.skillRemain -= 1;
					EnemyFire(true);
					break;
				case PlayerSkill_Level3.Default:
					EnemyFire(false);
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

	void SetBulletIce(GameObject bullet, bool isIce)
	{
		Bullet_Level3 bl = bullet.GetComponent<Bullet_Level3>();
		bl.isIceBullet = isIce;
	}

	void SelectAccordingToPower(PlayerPower_Level3 power, bool isIce)
	{
		switch(power)
		{
			case PlayerPower_Level3.DragonPower1:
				bullet = isIce? iceDragonBullet : dragonBullet;
				audioSource.clip = dragonAttack;
				break;
			case PlayerPower_Level3.DragonPower2:
				bullet = isIce? iceDragonBullet : dragonBullet;
				audioSource.clip = dragonAttack;
				break;
			case PlayerPower_Level3.DragonPower3:
				bullet = isIce? iceDragonBullet : dragonBullet;
				audioSource.clip = dragonAttack;
				break;
			case PlayerPower_Level3.KnightPower1:
				bullet = isIce? iceKnightBullet : knightBullet;
				audioSource.clip = knightAttack;
				break;
			case PlayerPower_Level3.KnightPower2:
				bullet = isIce? iceKnightBullet : knightBullet;
				audioSource.clip = knightAttack;
				break;
			case PlayerPower_Level3.MagicPower1:
				bullet = isIce? iceMagicBullet : magicBullet;
				audioSource.clip = magicAttack;
				break;
			case PlayerPower_Level3.MagicPower2:
				bullet = isIce? iceMagicBullet : magicBullet;
				audioSource.clip = magicAttack;
				break;
			case PlayerPower_Level3.MagicPower3:
				bullet = isIce? iceMagicBullet : magicBullet;
				audioSource.clip = magicAttack;
				break;
			case PlayerPower_Level3.AssassinPower1:
				bullet = isIce? iceAssassinBullet : assassinBullet;
				audioSource.clip = assassinAttack;
				break;
			case PlayerPower_Level3.AssassinPower2:
				bullet = isIce? iceAssassinBullet : assassinBullet;
				audioSource.clip = assassinAttack;
				break;
			case PlayerPower_Level3.BossPower:
				bullet = isIce? iceBossBullet : bossBullet;
				audioSource.clip = bossAttack;
				break;
			case PlayerPower_Level3.Default:
				bullet = null;
				audioSource.clip = null;
				break;
		}
	}

	void Fire(bool isIce)
	{
		faceDir = gameObject.GetComponent<PlayerMove_Level3>().dir;
		Debug.Log(bullet);
		SelectAccordingToPower(playerStatus.playerPower, isIce);
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
						UpAttack(isIce);
						LeftAttack(isIce);
						RightAttack(isIce);
					}
					else
					{
						UpAttack(isIce);
					}
				}
				break;

			case FaceDirection.Down:
				anim.SetTrigger("DownAttack");
				if(bullet)
				{	
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						DownAttack(isIce);
						LeftAttack(isIce);
						RightAttack(isIce);
					}
					else
					{
						DownAttack(isIce);
					}
				}
				break;

			case FaceDirection.Left:
				anim.SetTrigger("LeftAttack");
				if(bullet)
				{
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						LeftAttack(isIce);
						UpAttack(isIce);
						DownAttack(isIce);
					}
					else
					{
						LeftAttack(isIce);
					}
				}
				break;

			case FaceDirection.Right:
				anim.SetTrigger("RightAttack");
				if(bullet)
				{
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						RightAttack(isIce);
						UpAttack(isIce);
						DownAttack(isIce);
					}
					else
					{
						RightAttack(isIce);
					}
				}
				break;
		}
		
	}

	void EnemyFire(bool isIce)
	{
		faceDir = gameObject.GetComponent<PlayerMove_Level3>().dir;
		SelectAccordingToPower(playerStatus.playerPower,isIce);
		audioSource.Play();
		switch (faceDir)
		{
			case FaceDirection.Up:
				anim.SetTrigger("UpAttack");
				if(bullet)
				{	
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						EnemyUpAttack(isIce);
						EnemyLeftAttack(isIce);
						EnemyRightAttack(isIce);
					}
					else
					{
						EnemyUpAttack(isIce);
					}
				}
				break;

			case FaceDirection.Down:
				anim.SetTrigger("DownAttack");
				if(bullet)
				{	
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						EnemyDownAttack(isIce);
						EnemyLeftAttack(isIce);
						EnemyRightAttack(isIce);
					}
					else
					{
						EnemyDownAttack(isIce);
					}
				}
				break;

			case FaceDirection.Left:
				anim.SetTrigger("LeftAttack");
				if(bullet)
				{
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						EnemyLeftAttack(isIce);
						EnemyUpAttack(isIce);
						EnemyDownAttack(isIce);
					}
					else
					{
						EnemyLeftAttack(isIce);
					}
				}
				break;

			case FaceDirection.Right:
				anim.SetTrigger("RightAttack");
				if(bullet)
				{
					if(playerStatus.playerPower == PlayerPower_Level3.BossPower)
					{
						EnemyRightAttack(isIce);
						EnemyUpAttack(isIce);
						EnemyDownAttack(isIce);
					}
					else
					{
						EnemyRightAttack(isIce);
					}
				}
				break;
		}
		
	}

	void UpAttack(bool isIce)
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f,0f,90f));
		SetBulletTag(go);
		SetBulletType(go);
		SetBulletIce(go,isIce);
	}

	void DownAttack(bool isIce)
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f,0f,-90f));
		SetBulletTag(go);
		SetBulletType(go);
		SetBulletIce(go,isIce);
	}

	void LeftAttack(bool isIce)
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0f,0f,-180f));
		SetBulletTag(go);
		SetBulletType(go);
		SetBulletIce(go,isIce);
	}

	void RightAttack(bool isIce)
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity);
		SetBulletTag(go);
		SetBulletType(go);
		SetBulletIce(go,isIce);
	}
	void EnemyUpAttack(bool isIce)
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector2(firePosition.x, firePosition.y), Quaternion.Euler(0f,0f,90f));
		SetBulletTag(go);
		SetBulletType(go);
		SetBulletIce(go,isIce);
	}

	void EnemyDownAttack(bool isIce)
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector2(firePosition.x, firePosition.y), Quaternion.Euler(0f,0f,-90f));
		SetBulletTag(go);
		SetBulletType(go);
		SetBulletIce(go,isIce);
	}

	void EnemyLeftAttack(bool isIce)
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector2(firePosition.x, firePosition.y), Quaternion.Euler(0f,0f,-180f));
		SetBulletTag(go);
		SetBulletType(go);
		SetBulletIce(go,isIce);
	}

	void EnemyRightAttack(bool isIce)
	{
		GameObject go = (GameObject)Instantiate(bullet, new Vector2(firePosition.x, firePosition.y), Quaternion.identity);
		SetBulletTag(go);
		SetBulletType(go);
		SetBulletIce(go,isIce);
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


			
