using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerCharacter_Level2
{
	Dragon, Knight, Magic, Assassin, Boss, Default
}

public enum PlayerPower_Level2
{
	DragonPower, KnightPower, MagicPower, AssassinPower, BossPower, Default
}
public class Player1Status_Level2 : MonoBehaviour {

	public static Player1Status_Level2 _instance;

	public PlayerCharacter_Level2 playerCharacter = PlayerCharacter_Level2.Default;
	public PlayerPower_Level2 playerPower = PlayerPower_Level2.Default;

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
	private PlayerAttack_Level2 attack;
	private PlayerMove_Level2 move;
	private AudioSource[] audioSources;
	private AudioSource audioSource;
	private Camera cam;

	public Text heartText;
	public Text weaponText;
	public Image weaponImage;

	public Sprite defaultWeapon, dragonWeapon, knightWeapon, magicWeapon, assassinWeapon, bossWeapon;

	public GameObject bloodEffect;

	private float attackBuffRemain = 10.0f;
	private float damageReflectRemain = 10.0f;
	private float overPoisonRemain = 10.0f;
	private float overAreaRemain = 10.0f;
	public float hpChange;

	public Text damageReflectText, attackBuffText, overPoisonText, overAreaText;

	
	// Use this for initialization
	void Awake () {
		_instance = this;
		cam = Camera.main;
		attack = GetComponent<PlayerAttack_Level2>();
		move = GetComponent<PlayerMove_Level2>();
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[2];
		damageReflectText.enabled = false;
		attackBuffText.enabled = false;
		overPoisonText.enabled = false;
		overAreaText.enabled = false;
	}

	void CheckHpAndHearts()
	{
		heartText.text = "x " + Mathf.RoundToInt(hp).ToString();
	}

	void CheckAttackAbility()
	{
		weaponText.text = "x " + Mathf.RoundToInt(attackAbility).ToString();
	}

	void CheckWeaponImage()
	{
		switch(playerPower)
		{
			case PlayerPower_Level2.DragonPower:
				weaponImage.sprite = dragonWeapon;
				break;
			case PlayerPower_Level2.KnightPower:
				weaponImage.sprite = knightWeapon;
				break;
			case PlayerPower_Level2.MagicPower:
				weaponImage.sprite = magicWeapon;
				break;
			case PlayerPower_Level2.AssassinPower:
				weaponImage.sprite = assassinWeapon;
				break;
			case PlayerPower_Level2.BossPower:
				weaponImage.sprite = bossWeapon;
				break;
			case PlayerPower_Level2.Default:
				weaponImage.sprite = defaultWeapon;
				break;
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
				attackBuffText.enabled = true;
			}
			attackBuffRemain -= Time.deltaTime;
			if(attackBuffRemain < 0)
			{
				originAttack -= 2;
				attackBuff = false;
				attackBuffFirst = true;
				attackBuffText.enabled = false;
				attackBuffRemain = 10.0f;
			}
		}
	}

	void CheckDamageReflect()
	{
		if(damageReflect)
		{
			damageReflectText.enabled = true;
			damageReflectRemain -= Time.deltaTime;
			if(damageReflectRemain < 0)
			{
				damageReflect = false;
				damageReflectText.enabled = false;
				damageReflectRemain = 10.0f;
			}
		}
	}

	void CheckOverPoison()
	{
		if(overPoison)
		{
			overPoisonText.enabled = true;
			overPoisonRemain -= Time.deltaTime;
			if(overPoisonRemain < 0)
			{
				overPoison = false;
				overPoisonText.enabled = false;
				overPoisonRemain = 10.0f;
			}
		}
	}

	void CheckOverArea()
	{
		if(overArea)
		{
			overAreaText.enabled = true;
			overAreaRemain -= Time.deltaTime;
			if(overAreaRemain < 0)
			{
				overArea = false;
				overAreaText.enabled = false;
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
		CheckHpAndHearts();
		CheckAttackAbility();
		CheckWeaponImage();
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
			_instance.hp -= damage;
			GameObject blood = (GameObject)Instantiate(bloodEffect, transform.position, Quaternion.identity);
			Destroy(blood.gameObject,1);
			Debug.Log("Player1收到了" + damage + "点伤害");
			if (_instance.hp <= 0)
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
		PlayerStatusControl_Level2._instance.player2Win = true;
		Debug.Log("Player1死了！！！");
	}
}
