using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayerStatusControl : MonoBehaviour {


	public static PlayerStatusControl _instance;
	public bool isPlayer1 = false;
	public GameObject player1;
	public GameObject player2;
	private PlayerMove player1Move;
	private PlayerMove player2Move;

	public Text text;
	private float remainSeconds;
	private float durationSeconds;
	public bool gameStart = false;
	private bool onGame = false;
	private bool first = true;
	private AudioSource audio;

	public bool player1Win = false;
	public bool player2Win = false;

	void CheckGameStart()
	{
		if(gameStart)
		{	
			if(first)
			{
				audio.Play();
				first = false;
			}
			if(remainSeconds > 0f)
			{	
				remainSeconds -= Time.deltaTime;
				if(remainSeconds > 2f)
				{
					text.text = "3!!!";
				}else if(remainSeconds > 1f)
				{
					text.text = "2!!!";
				}else
				{
					text.text = "1!!!";
				}
			}
			else
			{
				text.text = "Game Start!";
				player1Move.enabled = true;
				player2Move.enabled = true;
				onGame = true;
				durationSeconds = 2f;
			}
		}
	}
	void Awake()
	{
		_instance = this;
		player1Move = player1.GetComponent<PlayerMove>();
		player2Move = player2.GetComponent<PlayerMove>();
		player1Move.enabled = false;
		player2Move.enabled = false;
		remainSeconds = 3f;
		durationSeconds = 0;
		text.text = "Wait for another player";
		audio = GetComponent<AudioSource>();

	}
	void FixedUpdate()
	{	
		if(!onGame)
		{
			CheckGameStart();
		}else
		{
			if(durationSeconds > 0)
			{
				durationSeconds -= Time.deltaTime;
			}
			else
			{
				text.text = "";
			}
		}
	}
}
