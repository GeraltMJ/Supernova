using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Status_Level2 : MonoBehaviour {

	public static Player2Status_Level2 _instance;

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

	private float attackBuffRemain = 10.0f;
	private float damageReflectRemain = 10.0f;
	private float overPoisonRemain = 10.0f;
	private float overAreaRemain = 10.0f;
	private PlayerAttack_Level2 attack;
	private PlayerMove_Level2 move;
	private AudioSource audioSource;
	private AudioSource[] audioSources;
	private Camera cam;
	// Use this for initialization

	public Text heartText;
	public Text weaponText;
	public Image weaponImage;

	public Sprite defaultWeapon, dragonWeapon, knightWeapon, magicWeapon, assassinWeapon, bossWeapon;

	public GameObject bloodEffect;
	public Text damageReflectText, attackBuffText, overPoisonText, overAreaText;
	public float hpChange;
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

	void CheckHpAndHearts()
	{
		heartText.text = Mathf.RoundToInt(hp).ToString() + " x";
	}

	void CheckAttackAbility()
	{
		weaponText.text = Mathf.RoundToInt(attackAbility).ToString() + " x";
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
				attackBuffText.enabled = false;
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


	public void Damage(float damage)
	{
		if (!isDead)
		{
			audioSource.Play();
			GameObject blood = (GameObject)Instantiate(bloodEffect, transform.position, Quaternion.identity);
			Destroy(blood.gameObject,1);
			_instance.hp -= damage;
			Debug.Log("Player2收到了" + damage + "点伤害");
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
			hp = Mathf.Min(hp,maxHp);
			Debug.Log("Player1 回复" + recorvery + "点血");
		}
	}

	public void Dead()
	{
		isDead = true;
		PlayerStatusControl_Level2._instance.player1Win = true;
	}
}