﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PlayerCharacter
{
	Dragon, Magic, Assassin, Knight, Boss, None
}
public enum PlayerPower
{
	DragonPower, MagicPower, AssassinPower, KnightPower, BossPower, None
}

public class Player1Status_Level2 : MonoBehaviour {

	public static Player1Status_Level2 _instance;

	//character
	public bool isDragon = false;
	public bool isMagic = false;
	public bool isAssassin = false;
	public bool isKnight = false;
	public bool isBoss = false;

	//power
	public bool hasDragonPower = false;
	public bool hasMagicPower = false;
	public bool hasAssassinPower = false;
	public bool hasKnightPower = false;
	public bool hasBossPower = false;

	private PlayerCharacter playerCharacter;
	private PlayerPower playerPower;

	public float hp = 3.0f;
	private bool isDead = false;
	//private PlayerAttack attack;
	//private PlayerMove move;
	public Text text;
	private AudioSource audio;
	public GameObject redHeart;
	private Camera cam;
	private Renderer heartRenderer;
	private GameObject[] hearts;
	private int heartIndex = -1;
	private float timePause = 0.48f;
	private int count = 1;
	private int firstcount = 1;
	// Use this for initialization
	void Awake () {
		_instance = this;
		cam = Camera.main;
		heartRenderer = redHeart.GetComponent<Renderer>();
		float heartWidth = heartRenderer.bounds.extents.x;
		float heartHeight = heartRenderer.bounds.extents.y;
		Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
		Vector3 targetwidth = cam.ScreenToWorldPoint(upperCorner);
		Vector3 heartPosition = new Vector3(-targetwidth.x  + heartWidth, targetwidth.y - heartWidth-0.5f, 0.0f);
		hearts = new GameObject[Mathf.FloorToInt(hp)];
		for(int i = 0; i < Mathf.FloorToInt(hp); i++)
		{
			hearts[i] = (GameObject)Instantiate(redHeart, heartPosition, Quaternion.identity);
			heartPosition.x = heartPosition.x + 2*heartWidth+0.1f;
			heartIndex++;
		}
		//attack = GetComponent<PlayerAttack>();
		//move = GetComponent<PlayerMove>();
		audio = GetComponent<AudioSource>();
	}

	/* 
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
	*/


	public void Damage(float damage)
	{
		if (!isDead)
		{
			audio.Play();
			_instance.hp -= damage;
			int i = 0;
			while(i < damage && heartIndex >= 0)
			{
				Destroy(hearts[heartIndex--]);
				i++;
			}
			Debug.Log("Player1收到了" + damage + "点伤害");
			if (_instance.hp <= 0)
			{
				Dead();
			}
		}
	}

	public void Dead()
	{
		isDead = true;
		//attack.enabled = false;
		//move.enabled = false;
		text.gameObject.SetActive(true);
		Debug.Log("Player1死了！！！");
	}
}