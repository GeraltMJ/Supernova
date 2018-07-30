using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatusControl_Level2 : MonoBehaviour {


	public static PlayerStatusControl_Level2 _instance;
	public bool isPlayer1 = false;
	public GameObject player1;
	public GameObject player2;
	private PlayerMove_Level2 player1Move;
	private PlayerMove_Level2 player2Move;

	public Text text;
	private float remainSeconds;
	private float durationSeconds;
	public bool gameStart = false;
	private bool onGame = false;
	private bool first = true;
	private AudioSource audioSource;
	public Image win_lose_Image;
	public Sprite winPic, losePic;
	public Image win_lose_black;

	public bool player1Win = false;
	public bool player2Win = false;

	public GameObject restartButton, startButton;
	private bool gameOver = false;
	public bool twoReady = false;
	private TcpClient_Level2 tcpClient;
	public bool selfCheck = false;
	public bool enemyCheck = false;


/*
	void CheckGameStart()
	{
		if(gameStart)
		{	
			if(first)
			{
				audioSource.Play();
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
	*/
	void Awake()
	{
		_instance = this;
		player1Move = player1.GetComponent<PlayerMove_Level2>();
		player2Move = player2.GetComponent<PlayerMove_Level2>();
		player1Move.enabled = false;
		player2Move.enabled = false;
		remainSeconds = 3f;
		durationSeconds = 0;
		text.text = "Wait for another player";
		audioSource = GetComponent<AudioSource>();
		win_lose_Image.enabled = false;
		win_lose_black.enabled = false;
		tcpClient = GetComponent<TcpClient_Level2>();
		

	}

	void Update()
	{
		if(!gameOver)
		{
			if(player1Win)
			{
				if(isPlayer1)
				{
					win_lose_Image.sprite = winPic;
				}else
				{
					win_lose_Image.sprite = losePic;
				}
				win_lose_Image.enabled = true;
				win_lose_black.enabled = true;
				player1Move.enabled = false;
				player2Move.enabled = false;
				restartButton.SetActive(true);
				gameOver = true;
			}
			else if(player2Win)
			{
				if(!isPlayer1)
				{
					win_lose_Image.sprite = winPic;
				}else
				{
					win_lose_Image.sprite = losePic;
				}
				win_lose_Image.enabled = true;
				win_lose_black.enabled = true;
				player1Move.enabled = false;
				player2Move.enabled = false;
				restartButton.SetActive(true);
				gameOver = true;
			}
		}
		else
		{
			if(ETCInput.GetButton("RestartButton"))
			{
				SceneManager.LoadScene("02_GamePlay",LoadSceneMode.Single);
			}
		}
	}
	void WaitForStart()
	{
		if(!selfCheck)
		{
			if(ETCInput.GetButton("StartButton"))
			{
				tcpClient.SendSelfCommand("G");
				startButton.SetActive(false);
				selfCheck = true;
			}
		}
	}
	void FixedUpdate()
	{	
		if(twoReady)
		{
			WaitForStart();
		}
		if(selfCheck && enemyCheck)
		{
			if(first)
			{
				audioSource.Play();
				first = false;
				text.text = "";
				player1Move.enabled = true;
				player2Move.enabled = true;
			}
		}
	}
}
