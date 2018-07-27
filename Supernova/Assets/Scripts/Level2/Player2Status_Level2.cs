using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player2Status_Level2 : MonoBehaviour {
	public static Player2Status_Level2 _instance;

	//character
	public PlayerCharacter playerCharacter = PlayerCharacter.None;
	public PlayerPower playerPower = PlayerPower.None;

	//power

	public float hp = 3.0f;
	private bool isDead = false;
	//private PlayerAttack attack;
	//private PlayerMove move;
	private AudioSource audio;
	public GameObject blueHeart;
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
		heartRenderer = blueHeart.GetComponent<Renderer>();
		float heartWidth = heartRenderer.bounds.extents.x;
		float heartHeight = heartRenderer.bounds.extents.y;
		Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
		Vector3 targetwidth = cam.ScreenToWorldPoint(upperCorner);
		Vector3 heartPosition = new Vector3(targetwidth.x-heartWidth, targetwidth.y - heartWidth-0.5f, 0.0f);
		hearts = new GameObject[Mathf.FloorToInt(hp)];
		for(int i = 0; i < Mathf.FloorToInt(hp); i++)
		{
			hearts[i] = (GameObject)Instantiate(blueHeart, heartPosition, Quaternion.identity);
			heartPosition.x = heartPosition.x - 2*heartWidth - 0.1f;
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
			Debug.Log("Player2收到了" + damage + "点伤害");
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
		Debug.Log("Player2死了！！！");
	}
}
