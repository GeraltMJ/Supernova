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
	ShieldSkill, GunSkill, IceSkill, Default, DoubleDmgSkill, HPSkill
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
	public float originAttack = 0.0f;
	public float attackAbility = 0.0f;
	private bool isDead = false;
	private PlayerAttack_Level3 attack;
	private PlayerMove_Level3 move;
	private AudioSource[] audioSources;
	private AudioSource audioSource;
	private Camera cam;

	public GameObject bloodEffect;

	private float attackBuffRemain = 10.0f;
	private float damageReflectRemain = 10.0f;
	private float overPoisonRemain = 10.0f;
	private float overAreaRemain = 10.0f;
	public float hpChange;
	
	// Use this for initialization
	void Awake () {
		cam = Camera.main;
		attack = GetComponent<PlayerAttack_Level3>();
		move = GetComponent<PlayerMove_Level3>();
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[2];
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

	void Update()
	{
		CheckAttackBuff();
		CheckDamageReflect();
		CheckOverPoison();
		CheckOverArea();
		CheckHpChange();
	}

	
	void Unfreeze()
	{
		attack.enabled = true;
		move.enabled = true;
	}

	void Freeze()
	{
		attack.enabled = false;
		move.enabled = false;
	}
	


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
		PlayerStatusControl_Level3._instance.player2Win = true;
		Debug.Log("Player1死了！！！");
	}
}
