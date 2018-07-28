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
	public float hp = 3.0f;
	public float attackAbility = 1.0f;
	private bool isDead = false;
	private PlayerAttack_Level2 attack;
	private PlayerMove_Level2 move;
	private AudioSource audio;
	private Camera cam;

	public Text heartText;
	public Text weaponText;

	
	// Use this for initialization
	void Awake () {
		_instance = this;
		cam = Camera.main;
		attack = GetComponent<PlayerAttack_Level2>();
		move = GetComponent<PlayerMove_Level2>();
		audio = GetComponent<AudioSource>();
	}

	void CheckHpAndHearts()
	{
		heartText.text = "x " + Mathf.RoundToInt(hp).ToString();
	}

	void CheckAttackAbility()
	{
		weaponText.text = "x " + Mathf.RoundToInt(attackAbility).ToString();
	}

	void Update()
	{
		CheckHpAndHearts();
		CheckAttackAbility();
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
			audio.Play();
			_instance.hp -= damage;
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
			_instance.hp += recorvery;
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
