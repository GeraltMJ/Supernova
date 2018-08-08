using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatusControl_Level3 : MonoBehaviour {


	public static PlayerStatusControl_Level3 _instance;
	public int playerIdentity;
	public bool isPlayer1 = false;
	public GameObject player1, player2, player3;
	private PlayerMove_Level3 player1Move, player2Move, player3Move;
	private PlayerStatus_Level3 player1Status, player2Status, player3Status;

	public Text text;
	private float remainSeconds;
	private float durationSeconds;
	public bool gameStart = false;
	private bool onGame = false;
	private bool first = true;
	private AudioSource audioSource;
	public Image win_lose_Image;
	public Sprite p1winPic, p2winPic, p3winPic;
	public Image win_lose_black;

	public bool player1Win = false;
	public bool player2Win = false;
	public bool player3Win = false;

	public GameObject restartButton, startButton;
	private bool gameOver = false;
	public bool twoReady = false;
	private TcpClient_Level3 tcpClient;
	public bool selfCheck = false;
	public bool enemyCheck = false;

	public Text playerHpText, enemy1HpText, enemy2HpText;
	public Text weaponText;
	public Image weaponImage;
	public Sprite defaultWeapon, dragonWeapon, knightWeapon, magicWeapon, assassinWeapon, bossWeapon;


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
		player1Move = player1.GetComponent<PlayerMove_Level3>();
		player2Move = player2.GetComponent<PlayerMove_Level3>();
		player3Move = player3.GetComponent<PlayerMove_Level3>();
		player1Status = player1.GetComponent<PlayerStatus_Level3>();
		player2Status = player2.GetComponent<PlayerStatus_Level3>();
		player3Status = player3.GetComponent<PlayerStatus_Level3>();

		//player1Move.enabled = false;
		//player2Move.enabled = false;
		//player3Move.enabled = false;
		remainSeconds = 3f;
		durationSeconds = 0;
		text.text = "Wait for another player";
		audioSource = GetComponent<AudioSource>();
		win_lose_Image.enabled = false;
		win_lose_black.enabled = false;
		tcpClient = GetComponent<TcpClient_Level3>();
		

	}

	void CheckGameOver()
	{
		if(!gameOver)
		{
			if(player1Win)
			{
				win_lose_Image.sprite = p1winPic;
				win_lose_Image.enabled = true;
				win_lose_black.enabled = true;
				player1Move.enabled = false;
				player2Move.enabled = false;
				restartButton.SetActive(true);
				gameOver = true;
			}
			else if(player2Win)
			{
				win_lose_Image.sprite = p2winPic;
				win_lose_Image.enabled = true;
				win_lose_black.enabled = true;
				player1Move.enabled = false;
				player2Move.enabled = false;
				restartButton.SetActive(true);
				gameOver = true;
			}
			else if(player3Win)
			{
				win_lose_Image.sprite = p3winPic;
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
				SceneManager.LoadScene("03_GamePlay",LoadSceneMode.Single);
			}
		}
	}

	void CheckPlayerHp()
	{
		switch(playerIdentity)
		{
			case 1:
				playerHpText.text = player1Status.hp.ToString() + "/" + player1Status.maxHp.ToString();
				enemy1HpText.text = player2Status.hp.ToString() + "/" + player2Status.maxHp.ToString();
				enemy2HpText.text = player3Status.hp.ToString() + "/" + player3Status.maxHp.ToString();
				break;
			case 2:
				playerHpText.text = player2Status.hp.ToString() + "/" + player2Status.maxHp.ToString();
				enemy1HpText.text = player1Status.hp.ToString() + "/" + player1Status.maxHp.ToString();
				enemy2HpText.text = player3Status.hp.ToString() + "/" + player3Status.maxHp.ToString();
				break;
			case 3:
				playerHpText.text = player3Status.hp.ToString() + "/" + player3Status.maxHp.ToString();
				enemy1HpText.text = player1Status.hp.ToString() + "/" + player1Status.maxHp.ToString();
				enemy2HpText.text = player2Status.hp.ToString() + "/" + player2Status.maxHp.ToString();
				break;
		}
	}

	void WeaponImageSelect(PlayerPower_Level3 playerPower)
	{
		switch(playerPower)
		{
			case PlayerPower_Level3.DragonPower1:
				weaponImage.sprite = dragonWeapon;
				break;
			case PlayerPower_Level3.DragonPower2:
				weaponImage.sprite = dragonWeapon;
				break;
			case PlayerPower_Level3.DragonPower3:
				weaponImage.sprite = dragonWeapon;
				break;
			case PlayerPower_Level3.KnightPower1:
				weaponImage.sprite = knightWeapon;
				break;
			case PlayerPower_Level3.KnightPower2:
				weaponImage.sprite = knightWeapon;
				break;
			case PlayerPower_Level3.MagicPower1:
				weaponImage.sprite = magicWeapon;
				break;
			case PlayerPower_Level3.MagicPower2:
				weaponImage.sprite = magicWeapon;
				break;
			case PlayerPower_Level3.MagicPower3:
				weaponImage.sprite = magicWeapon;
				break;
			case PlayerPower_Level3.AssassinPower1:
				weaponImage.sprite = assassinWeapon;
				break;
			case PlayerPower_Level3.AssassinPower2:
				weaponImage.sprite = assassinWeapon;
				break;
			case PlayerPower_Level3.BossPower:
				weaponImage.sprite = bossWeapon;
				break;
			case PlayerPower_Level3.Default:
				weaponImage.sprite = defaultWeapon;
				break;
		}
	}

	void CheckPlayerWeaponImage()
	{
		switch(playerIdentity)
		{
			case 1:
			
				break;
			case 2:
				break;
			case 3:
				break;
		}
	}

	void CheckPlayerWeaponText()
	{

	}

	void Update()
	{
		CheckGameOver();
		CheckPlayerHp();
		CheckPlayerWeaponImage();
		CheckPlayerWeaponText();
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
				Debug.Log("check");
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
