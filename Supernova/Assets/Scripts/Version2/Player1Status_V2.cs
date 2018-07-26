﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Status_V2 : MonoBehaviour {

	public bool isCat = false;
	public bool isMouse = false;
	public bool hasCatAbility = false;
	public bool hasMouseAbility = false;
	public static Player1Status_V2 _instance;

	public float maxHp = 5.0f;
	public float hp = 3.0f;
	private bool isDead = false;
	private PlayerAttack attack;
	private PlayerMove move;
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
	private Vector3 heartPosition; 
	private float heartWidth;
	// Use this for initialization
	void Awake () {
		_instance = this;
		cam = Camera.main;
		heartRenderer = redHeart.GetComponent<Renderer>();
		heartWidth = heartRenderer.bounds.extents.x;
		float heartHeight = heartRenderer.bounds.extents.y;
		Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
		Vector3 targetwidth = cam.ScreenToWorldPoint(upperCorner);
		heartPosition = new Vector3(-targetwidth.x  + heartWidth, targetwidth.y - heartWidth-0.5f, 0.0f);
		hearts = new GameObject[Mathf.FloorToInt(maxHp)];
		for(int i = 0; i < Mathf.FloorToInt(hp); i++)
		{
			hearts[i] = (GameObject)Instantiate(redHeart, heartPosition, Quaternion.identity);
			heartPosition.x = heartPosition.x + 2*heartWidth+0.1f;
			heartIndex++;
		}
		attack = GetComponent<PlayerAttack>();
		move = GetComponent<PlayerMove>();
		audio = GetComponent<AudioSource>();
	}

	void CheckHpAndHearts()
	{	
		if(heartIndex+1 < Mathf.FloorToInt(hp))
		{
			for(int i = heartIndex+1; i < Mathf.FloorToInt(hp); i++)
			{
				hearts[i] = (GameObject)Instantiate(redHeart, heartPosition, Quaternion.identity);
				heartPosition.x = heartPosition.x + 2*heartWidth+0.1f;
				heartIndex++;
			}
		}
		else
		{
			while(heartIndex + 1 > Mathf.FloorToInt(hp))
			{
				Destroy(hearts[heartIndex--]);
				heartPosition.x = heartPosition.x - 2*heartWidth+0.1f;
			}
		}
	}

	void Update()
	{
		CheckHpAndHearts();		
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
			/* 
			int i = 0;
			while(i < damage && heartIndex >= 0)
			{
				Destroy(hearts[heartIndex--]);
				heartPosition.x = heartPosition.x - 2*heartWidth+0.1f;
				i++;
			}
			*/
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
		attack.enabled = false;
		move.enabled = false;
		text.gameObject.SetActive(true);
		Debug.Log("Player1死了！！！");
	}
}