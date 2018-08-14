using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Level3 : MonoBehaviour {

	public PlayerCharacter_Level3 bulletType;
	public GameObject explosion, iceExlposion;
	public GameObject frozenCarryEffect;
	private GameObject explo;
	private TcpClient_Level2 tcpClient;
	private float dragonSpeed = 6.0f;
	private float assassinSpeed = 6.0f;
	private float magicSpeed = 4.0f;
	private float knightSpeed = 9.0f;
	private float bossSpeed = 8f;
	private float assassinTimeLimit = 1.5f;
	private float dragonTimeLimit = 3.5f;
	private float knightTimeLimit = 1f;
	private float magicTimeLimit = 5f;
	private float bossTimeLimit = 5f;
	public bool isIceBullet = false;
	
	public float timer = 0f;
	private GameObject player1, player2, player3;
	private PlayerStatus_Level3 ps1, ps2, ps3;

	void Start() {
		player1 = GameObject.FindWithTag("Player1");
		player2 = GameObject.FindWithTag("Player2");
		player3 = GameObject.FindWithTag("Player3");
		ps1 = player1.GetComponent<PlayerStatus_Level3>();
		ps2 = player2.GetComponent<PlayerStatus_Level3>();
		ps3 = player3.GetComponent<PlayerStatus_Level3>();
	}

	void Update () {

		switch(bulletType)
		{
			case PlayerCharacter_Level3.Dragon:
				DragonWeaponControl();
				break;
			case PlayerCharacter_Level3.Knight:
				KnightWeaponControl();
				break;
			case PlayerCharacter_Level3.Assassin:
				AssasinWeaponControl();
				break;
			case PlayerCharacter_Level3.Magic:
				MagicWeaponControl();
				break;
			case PlayerCharacter_Level3.Boss:
				BossWeaponControl();
				break;
		}
	}

	private void AssasinWeaponControl()
	{
		if(timer < assassinTimeLimit)
		{
			this.transform.Translate(Time.deltaTime * assassinSpeed * Vector3.right);
			timer += Time.deltaTime;
		}
		else if(timer > assassinTimeLimit && timer <= 2*assassinTimeLimit)
		{
			this.transform.Translate(Time.deltaTime * assassinSpeed * Vector3.left);
			timer += Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void KnightWeaponControl()
	{
		if (timer < knightTimeLimit)
		{
			this.transform.Translate(Time.deltaTime * knightSpeed * Vector3.right);
			timer += Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void MagicWeaponControl()
	{
		if (timer < magicTimeLimit)
		{
			this.transform.Translate(Time.deltaTime * magicSpeed * Vector3.right);
			timer += Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void DragonWeaponControl()
	{
		if (timer < dragonTimeLimit)
		{
			this.transform.Translate(Time.deltaTime * dragonSpeed * Vector3.right);
			timer += Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	private void BossWeaponControl()
	{
		if (timer < bossTimeLimit)
		{
			this.transform.Translate(Time.deltaTime * bossSpeed * Vector3.right);
			timer += Time.deltaTime;
		}
		else
		{
			Destroy(this.gameObject);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) 
	{
		
		if(collision.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}

		if(gameObject.CompareTag("Player1Bullet"))
		{
			if(collision.gameObject.CompareTag("Player2") || collision.gameObject.CompareTag("Player3"))
			{
				if(collision.gameObject.CompareTag("Player2") && PlayerStatusControl_Level3._instance.playerIdentity == 2)
				{
					ps2.Damage(ps1.attackAbility);
					TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity, -Mathf.RoundToInt(ps1.attackAbility));
				}
				if(collision.gameObject.CompareTag("Player3") && PlayerStatusControl_Level3._instance.playerIdentity == 3)
				{
					ps3.Damage(ps1.attackAbility);
					TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity, -Mathf.RoundToInt(ps1.attackAbility));
				}
				if(isIceBullet)
				{
					if(collision.gameObject.CompareTag("Player2"))
					{
						ps2.frozenSpeed = 0.5f;
						ps2.frozenRemain = 6;
						ps2.frozenCarryEffect = Instantiate(frozenCarryEffect, collision.gameObject.transform.position, Quaternion.identity);
						ps2.frozenCarryEffect.transform.parent = collision.gameObject.transform;
					}
					if(collision.gameObject.CompareTag("Player3"))
					{
						ps3.frozenSpeed = 0.5f;
						ps3.frozenRemain = 6;
						ps3.frozenCarryEffect = Instantiate(frozenCarryEffect, collision.gameObject.transform.position, Quaternion.identity);
						ps3.frozenCarryEffect.transform.parent = collision.gameObject.transform;
					}
					
					explo = (GameObject)Instantiate(iceExlposion, collision.transform.position, Quaternion.identity);
					Destroy(explo,1);
				}
				else
				{
					explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
					Destroy(explo,1);
				}
				Destroy(gameObject);
			}
		}
		else if(gameObject.CompareTag("Player2Bullet"))
		{
			if(collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player3"))
			{
				if(collision.gameObject.CompareTag("Player1") && PlayerStatusControl_Level3._instance.playerIdentity == 1)
				{
					ps1.Damage(ps2.attackAbility);
					TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity, -Mathf.RoundToInt(ps2.attackAbility));
				}
				if(collision.gameObject.CompareTag("Player3") && PlayerStatusControl_Level3._instance.playerIdentity == 3)
				{
					ps3.Damage(ps2.attackAbility);
					TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity, -Mathf.RoundToInt(ps2.attackAbility));
				}
				if(isIceBullet)
				{
					if(collision.gameObject.CompareTag("Player1"))
					{
						ps1.frozenSpeed = 0.5f;
						ps1.frozenRemain = 6;
						ps1.frozenCarryEffect = Instantiate(frozenCarryEffect, collision.gameObject.transform.position, Quaternion.identity);
						ps1.frozenCarryEffect.transform.parent = collision.gameObject.transform;
					}
					if(collision.gameObject.CompareTag("Player3"))
					{
						ps3.frozenSpeed = 0.5f;
						ps3.frozenRemain = 6;
						ps3.frozenCarryEffect = Instantiate(frozenCarryEffect, collision.gameObject.transform.position, Quaternion.identity);
						ps3.frozenCarryEffect.transform.parent = collision.gameObject.transform;
					}
					
					explo = (GameObject)Instantiate(iceExlposion, collision.transform.position, Quaternion.identity);
					Destroy(explo,1);
				}
				else
				{
					explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
					Destroy(explo,1);
				}
				Destroy(gameObject);
			}
		}
		else if(gameObject.CompareTag("Player3Bullet"))
		{
			if(collision.gameObject.CompareTag("Player1") || collision.gameObject.CompareTag("Player2"))
			{
				if(collision.gameObject.CompareTag("Player1") && PlayerStatusControl_Level3._instance.playerIdentity == 1)
				{
					ps1.Damage(ps3.attackAbility);
					TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity, -Mathf.RoundToInt(ps3.attackAbility));
				}
				if(collision.gameObject.CompareTag("Player2") && PlayerStatusControl_Level3._instance.playerIdentity == 2)
				{
					ps2.Damage(ps3.attackAbility);
					TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity, -Mathf.RoundToInt(ps3.attackAbility));
				}
				if(isIceBullet)
				{
					if(collision.gameObject.CompareTag("Player1"))
					{
						ps1.frozenSpeed = 0.5f;
						ps1.frozenRemain = 6;
						ps1.frozenCarryEffect = Instantiate(frozenCarryEffect, collision.gameObject.transform.position, Quaternion.identity);
						ps1.frozenCarryEffect.transform.parent = collision.gameObject.transform;
					}
					if(collision.gameObject.CompareTag("Player2"))
					{
						ps2.frozenSpeed = 0.5f;
						ps2.frozenRemain = 6;
						ps2.frozenCarryEffect = Instantiate(frozenCarryEffect, collision.gameObject.transform.position, Quaternion.identity);
						ps2.frozenCarryEffect.transform.parent = collision.gameObject.transform;
					}
					
					explo = (GameObject)Instantiate(iceExlposion, collision.transform.position, Quaternion.identity);
					Destroy(explo,1);
				}
				else
				{
					explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
					Destroy(explo,1);
				}
				Destroy(gameObject);
			}
		}



	}

}
