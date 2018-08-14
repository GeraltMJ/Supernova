using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerCharacter_Level3
{
	Dragon, Knight, Magic, Assassin, Boss, Default
}

public enum PlayerPower_Level3
{
	DragonPower1, DragonPower2, DragonPower3, KnightPower1, KnightPower2, MagicPower1, MagicPower2, MagicPower3, AssassinPower1, AssassinPower2, BossPower, Default
}

public enum PlayerSkill_Level3
{
	ShieldSkill, GunSkill, IceSkill, DoubleDmgSkill, Default
}
public class PlayerStatus_Level3 : MonoBehaviour {
	public PlayerCharacter_Level3 playerCharacter = PlayerCharacter_Level3.Default;
	public PlayerPower_Level3 playerPower = PlayerPower_Level3.Default;
	public PlayerSkill_Level3 playerSkill = PlayerSkill_Level3.Default;
	public int playerIdentity;
	public int skillRemain = 0;

	public bool overPoison = false;
	public bool overArea = false;
	public bool damageReflect = false;
	public bool attackBuff = false;
	private bool attackBuffFirst = true;
	public float maxHp = 5.0f;
	public float hp = 5.0f;
	public float updateHp = 5.0f;
	public float originAttack = 0.0f;
	public float attackAbility = 0.0f;
	private bool isDead = false;
	private PlayerAttack_Level3 playerAttack;
	private PlayerMove_Level3 playerMove;
	private AudioSource[] audioSources;
	private AudioSource audioSource;
	private Camera cam;

	public GameObject bloodEffect;

	private float attackBuffRemain = 10.0f;
	private float damageReflectRemain = 10.0f;
	private float overPoisonRemain = 10.0f;
	private float overAreaRemain = 10.0f;
	public float hpChange;

	public GameObject carrySkillEffect;
	public GameObject carryPowerEffect;

	public float frozenSpeed = 1f;
	public GameObject frozenCarryEffect;
	public float frozenRemain = 0;
	public bool isFire = false;
	public bool isTeleport = false;
	public bool gameStart = false;
	public Sprite tombSprite;
	
	// Use this for initialization
	void Awake () {
		cam = Camera.main;
		playerAttack = GetComponent<PlayerAttack_Level3>();
		playerMove = GetComponent<PlayerMove_Level3>();
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[2];
	}

	public void UpdateHp(float newHp)
	{
		updateHp  = newHp;
	}

	void CheckFrozenStatus()
	{
		if(frozenRemain > 0)
		{
			frozenRemain -= Time.deltaTime;
		}
		if(frozenRemain <= 0)
		{
			if(frozenCarryEffect)
			{
				Destroy(frozenCarryEffect);
			}
			frozenSpeed = 1f;
		}
	}

	void CheckAttackBuff()
	{
		if(attackBuff)
		{
			if(attackBuffFirst)
			{
				originAttack += 2;
				attackBuffFirst = false;
			}
			attackBuffRemain -= Time.deltaTime;
			if(attackBuffRemain < 0)
			{
				originAttack -= 2;
				attackBuff = false;
				attackBuffFirst = true;
				attackBuffRemain = 10.0f;
			}
		}
	}

	void CheckDamageReflect()
	{
		if(damageReflect)
		{
			damageReflectRemain -= Time.deltaTime;
			if(damageReflectRemain < 0)
			{
				damageReflect = false;
				damageReflectRemain = 10.0f;
			}
		}
	}

	void CheckOverPoison()
	{
		if(overPoison)
		{
			overPoisonRemain -= Time.deltaTime;
			if(overPoisonRemain < 0)
			{
				overPoison = false;
				overPoisonRemain = 10.0f;
			}
		}
	}

	void CheckOverArea()
	{
		if(overArea)
		{
			overAreaRemain -= Time.deltaTime;
			if(overAreaRemain < 0)
			{
				overArea = false;
				overAreaRemain = 10.0f;
			}
		}
	}

	void CheckHpChange()
	{
		if(hpChange < 0)
		{
			Damage(-hpChange);
		}
		else if(hpChange > 0)
		{
			Recover(hpChange);
		}
		hpChange = 0;
	}

	void CheckUpdateHp()
	{
		if(playerIdentity != PlayerStatusControl_Level3._instance.playerIdentity)
		{
			if(hp != updateHp)
			{
				if(updateHp > hp)
				{
					Recover(updateHp - hp);
				}
				else
				{
					Damage(hp - updateHp);
				}
				hp = updateHp;
			}
		}
	}

	void CheckSkillRemain()
	{
		if(skillRemain == 0)
		{
			playerSkill = PlayerSkill_Level3.Default;
			if(carrySkillEffect)
			{
				Destroy(carrySkillEffect);
			}
		}
	}

	void Update()
	{
		CheckAttackBuff();
		CheckDamageReflect();
		CheckOverPoison();
		CheckOverArea();
		CheckHpChange();
		CheckSkillRemain();
		CheckFrozenStatus();
		//CheckUpdateHp();
	}
	/*
	void FixedUpdate() {
		SendSelfInfo();	
	}

	void SendSelfInfo(){
		
		if(gameStart)
		{
			if(PlayerStatusControl_Level3._instance.playerIdentity == playerIdentity)
			{
				int fire = 0;
				int teleport = 0;
				if(isFire)
				{
					fire = 1;
					isFire = false;
				}
				if(isTeleport)
				{
					teleport = 1;
					isTeleport = false;
				}
				TcpClient_All._instance.SendPlayerAllInfo(PlayerStatusControl_Level3._instance.playerIdentity, transform.position, playerMove.dir,
															fire, transform.position, hp, teleport);
			}
		}
	}
	*/


	public void Damage(float damage)
	{
		if (!isDead)
		{
			audioSource.Play();
			hp -= damage;
			GameObject blood = (GameObject)Instantiate(bloodEffect, transform.position, Quaternion.identity);
			Destroy(blood.gameObject,1);
			Debug.Log("Player1收到了" + damage + "点伤害");
			if (hp <= 0)
			{
				Dead();
			}
		}
	}

	public void Recover(float recorvery)
	{
		if(!isDead)
		{
			hp += recorvery;
			hp = Mathf.Min(hp, maxHp);
			Debug.Log("Player1 回复" + recorvery + "点血");
		}
	}

	public void Dead()
	{
		isDead = true;
		if(gameObject.CompareTag("Player1"))
		{
			PlayerStatusControl_Level3._instance.player1Dead = true;
		}
		else if(gameObject.CompareTag("Player2"))
		{
			PlayerStatusControl_Level3._instance.player2Dead = true;
		}
		else if(gameObject.CompareTag("Player3"))
		{
			PlayerStatusControl_Level3._instance.player3Dead = true;
		}
		playerMove.enabled = false;
		GetComponent<SpriteRenderer>().sprite = tombSprite;
		GetComponent<Animator>().enabled = false;
		if(PlayerStatusControl_Level3._instance.playerIdentity == playerIdentity)
		{
			TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity, -20);
		}
	}
}
