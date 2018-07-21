using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Status : MonoBehaviour {

	public static Player1Status _instance;

	public float hp = 3.0f;
	private bool isDead = false;
	public PlayerAttack attack;
	public PlayerMove move;
	public Text text;
	public AudioSource audio;

	private void Start()
	{
		_instance = this;
		attack = GetComponent<PlayerAttack>();
		move = GetComponent<PlayerMove>();
		audio = GetComponent<AudioSource>();
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

	public void Dead()
	{
		isDead = true;
		attack.enabled = false;
		move.enabled = false;
		text.gameObject.SetActive(true);
		Debug.Log("Player1死了！！！");
	}

}
