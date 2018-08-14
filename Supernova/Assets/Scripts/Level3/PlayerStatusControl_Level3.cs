using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerStatusControl_Level3 : MonoBehaviour {


	public static PlayerStatusControl_Level3 _instance;
	public int playerIdentity;
	public GameObject player1, player2, player3;
	private PlayerMove_Level3 player1Move, player2Move, player3Move;
	private PlayerStatus_Level3 player1Status, player2Status, player3Status;

	private float remainSeconds;
	private float durationSeconds;
	public bool gameStart = false;
	private bool onGame = false;
	private bool first = true;
	private AudioSource audioSource;
	public Image win_lose_black;

	public bool player1Win = false;
	public bool player2Win = false;
	public bool player3Win = false;
	public bool player1Dead = false;
	public bool player2Dead = false;
	public bool player3Dead = false;

	public GameObject restartButton, startButton;
	private bool gameOver = false;

	public bool[] playerReady;
	private bool allPlayerReady = false;

	public Text playerCurrentHpText, playerMaxHpText, enemy1CurrentHpText, enemy1MaxHpText, enemy2CurrentHpText, enemy2MaxHpText;
	public Slider playerHpSlider, enemy1HpSlider, enemy2HpSlider;
	public Text weaponText, skillText;
	public Image playerJob, enemy1Job, enemy2Job;
	public Sprite[] playerSprites;
	public Sprite[] jobSprites;
	public Sprite player1WinSprite, player2WinSprite, player3WinSprite;
	private bool selfDead = false;
	public Camera cam;
	private MapController mc;


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
		playerIdentity = PlayerStatusControl_All._instance.playerIndex + 1;
		player1Move = player1.GetComponent<PlayerMove_Level3>();
		player2Move = player2.GetComponent<PlayerMove_Level3>();
		player3Move = player3.GetComponent<PlayerMove_Level3>();
		player1Status = player1.GetComponent<PlayerStatus_Level3>();
		player2Status = player2.GetComponent<PlayerStatus_Level3>();
		player3Status = player3.GetComponent<PlayerStatus_Level3>();
		mc = GetComponent<MapController>();

		win_lose_black.enabled = false;

		player1Move.enabled = false;
		player2Move.enabled = false;
		player3Move.enabled = false;
		remainSeconds = 3f;
		durationSeconds = 0;
		audioSource = GetComponent<AudioSource>();
		win_lose_black.enabled = false;
		//tcpClient = GetComponent<TcpClient_Level3>();
		playerReady = new bool[3];
		for(int i = 0; i < 3; i++)
		{
			playerReady[i] = false;
		}
		

	}

	void CheckWhoWin()
	{
		if(!gameOver)
		{
			if(player1Dead && player2Dead)
			{
				player3Win = true;
			}
			else if(player1Dead && player3Dead)
			{
				player2Win = true;
			}
			else if(player2Dead && player3Dead)
			{
				player1Win = true;
			}

		}
	}

	void CheckGameOver()
	{
		if(!gameOver)
		{
			if(player1Win)
			{
				win_lose_black.sprite = player1WinSprite;
				win_lose_black.enabled = true;
				player1Move.enabled = false;
				player2Move.enabled = false;
				player3Move.enabled = false;
				restartButton.SetActive(true);
				gameOver = true;
			}
			else if(player2Win)
			{
				win_lose_black.sprite = player2WinSprite;
				win_lose_black.enabled = true;
				player1Move.enabled = false;
				player2Move.enabled = false;
				player3Move.enabled = false;
				restartButton.SetActive(true);
				gameOver = true;
			}
			else if(player3Win)
			{
				win_lose_black.sprite = player3WinSprite;
				win_lose_black.enabled = true;
				player1Move.enabled = false;
				player2Move.enabled = false;
				player3Move.enabled = false;
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
				playerCurrentHpText.text = Mathf.RoundToInt(player1Status.hp).ToString();
				playerMaxHpText.text = Mathf.RoundToInt(player1Status.maxHp).ToString();
				enemy1CurrentHpText.text = Mathf.RoundToInt(player2Status.hp).ToString();
				enemy1MaxHpText.text = Mathf.RoundToInt(player2Status.maxHp).ToString();
				enemy2CurrentHpText.text = Mathf.RoundToInt(player3Status.hp).ToString();
				enemy2MaxHpText.text = Mathf.RoundToInt(player3Status.maxHp).ToString();
				playerHpSlider.value = player1Status.hp / player1Status.maxHp;
				enemy1HpSlider.value = player2Status.hp / player2Status.maxHp;
				enemy2HpSlider.value = player3Status.hp / player3Status.maxHp;
				break;
			case 2:
				playerCurrentHpText.text = Mathf.RoundToInt(player2Status.hp).ToString();
				playerMaxHpText.text = Mathf.RoundToInt(player2Status.maxHp).ToString();
				enemy1CurrentHpText.text = Mathf.RoundToInt(player1Status.hp).ToString();
				enemy1MaxHpText.text = Mathf.RoundToInt(player1Status.maxHp).ToString();
				enemy2CurrentHpText.text = Mathf.RoundToInt(player3Status.hp).ToString();
				enemy2MaxHpText.text = Mathf.RoundToInt(player3Status.maxHp).ToString();
				playerHpSlider.value = player2Status.hp / player2Status.maxHp;
				enemy1HpSlider.value = player1Status.hp / player1Status.maxHp;
				enemy2HpSlider.value = player3Status.hp / player3Status.maxHp;
				break;
			case 3:
				playerCurrentHpText.text = Mathf.RoundToInt(player3Status.hp).ToString();
				playerMaxHpText.text = Mathf.RoundToInt(player3Status.maxHp).ToString();
				enemy1CurrentHpText.text = Mathf.RoundToInt(player1Status.hp).ToString();
				enemy1MaxHpText.text = Mathf.RoundToInt(player1Status.maxHp).ToString();
				enemy2CurrentHpText.text = Mathf.RoundToInt(player2Status.hp).ToString();
				enemy2MaxHpText.text = Mathf.RoundToInt(player2Status.maxHp).ToString();
				playerHpSlider.value = player3Status.hp / player3Status.maxHp;
				enemy1HpSlider.value = player1Status.hp / player1Status.maxHp;
				enemy2HpSlider.value = player2Status.hp / player2Status.maxHp;
				break;
		}
	}
	void CheckPlayerWeaponText()
	{
		switch(playerIdentity)
		{
			case 1:
				weaponText.text = Mathf.RoundToInt(player1Status.attackAbility).ToString();
				break;
			case 2:
				weaponText.text = Mathf.RoundToInt(player2Status.attackAbility).ToString();
				break;
			case 3:
				weaponText.text = Mathf.RoundToInt(player3Status.attackAbility).ToString();
				break;
		}
	}

	void CheckPlayerSkillText()
	{
		switch(playerIdentity)
		{
			case 1:
				skillText.text = Mathf.RoundToInt(player1Status.skillRemain).ToString();
				break;
			case 2:
				skillText.text = Mathf.RoundToInt(player2Status.skillRemain).ToString();
				break;
			case 3:
				skillText.text = Mathf.RoundToInt(player3Status.skillRemain).ToString();
				break;
		}
	}

	Sprite SelectSpriteAccordingToJob(PlayerCharacter_Level3 playerCharacter, Sprite defaultSprite)
	{
		switch(playerCharacter)
		{
			case PlayerCharacter_Level3.Dragon:
				return jobSprites[0];
			case PlayerCharacter_Level3.Knight:
				return jobSprites[1];
			case PlayerCharacter_Level3.Magic:
				return jobSprites[2];
			case PlayerCharacter_Level3.Assassin:
				return jobSprites[3];
			case PlayerCharacter_Level3.Boss:
				return jobSprites[4];
			case PlayerCharacter_Level3.Default:
				return defaultSprite;
		}
		return null;
	}

	void CheckPlayerJob()
	{
		switch(playerIdentity)
		{
			case 1:
				playerJob.sprite = SelectSpriteAccordingToJob(player1Status.playerCharacter, playerSprites[0]);
				enemy1Job.sprite = SelectSpriteAccordingToJob(player2Status.playerCharacter, playerSprites[1]);
				enemy2Job.sprite = SelectSpriteAccordingToJob(player3Status.playerCharacter, playerSprites[2]);
				break;
			case 2:
				playerJob.sprite = SelectSpriteAccordingToJob(player2Status.playerCharacter, playerSprites[1]);
				enemy1Job.sprite = SelectSpriteAccordingToJob(player1Status.playerCharacter, playerSprites[0]);
				enemy2Job.sprite = SelectSpriteAccordingToJob(player3Status.playerCharacter, playerSprites[2]);
				break;
			case 3:
				playerJob.sprite = SelectSpriteAccordingToJob(player3Status.playerCharacter, playerSprites[2]);
				enemy1Job.sprite = SelectSpriteAccordingToJob(player1Status.playerCharacter, playerSprites[0]);
				enemy2Job.sprite = SelectSpriteAccordingToJob(player2Status.playerCharacter, playerSprites[1]);
				break;
		}
	}
	void CheckSelfDead()
	{
		switch(playerIdentity)
		{
			case 1:
				if(player1Dead)
				{
					selfDead = true;
				}
				break;
			case 2:
				if(player2Dead)
				{
					selfDead = true;
				}
				break;
			case 3:
				if(player3Dead)
				{
					selfDead = true;
				}
				break;
		}
	}

	void CameraMoveAfterSelfDead()
	{
		if(selfDead)
		{
			switch(playerIdentity)
			{
				case 1:
					cam.transform.position = new Vector3(player2.gameObject.transform.position.x, player2.gameObject.transform.position.y, -10);
					break;
				case 2:
					cam.transform.position = new Vector3(player3.gameObject.transform.position.x, player3.gameObject.transform.position.y, -10);
					break;
				case 3:
					cam.transform.position = new Vector3(player1.gameObject.transform.position.x, player1.gameObject.transform.position.y, -10);
					break;
			}
		}
	}

	

	void Update()
	{
		CheckWhoWin();
		CheckGameOver();
		CheckPlayerHp();
		CheckPlayerWeaponText();
		CheckPlayerSkillText();
		CheckPlayerJob();
		CheckSelfDead();
		CameraMoveAfterSelfDead();
	}
	void WaitForStart()
	{	
		if(playerIdentity >= 1 && !playerReady[playerIdentity-1])
		{
			if(ETCInput.GetButton("StartButton"))
			{
				//tcpClient.SendSelfCommand("G");
				//TcpClient_All._instance.SendSelfCommand("G");
				TcpClient_All._instance.SendRoundStartCommand(playerIdentity);
				startButton.SetActive(false);
				playerReady[playerIdentity-1] = true;
				Debug.Log("check");
			}
		}
	}

	void CheckPlayerReady()
	{
		bool allready = true;
		for(int i = 0; i < 3; i++)
		{
			if(playerReady[i] == false)
			{
				allready = false;
			}
		}
		allPlayerReady = allready;
	}

	void CheckGameStart()
	{
		if(allPlayerReady)
		{	
			if(first)
			{
				audioSource.Play();
				first = false;
				player1Move.enabled = true;
				player2Move.enabled = true;
				player3Move.enabled = true;
				player1Status.gameStart = true;
				player2Status.gameStart = true;
				player3Status.gameStart = true;
				mc.gameStart = true;
			}
		}
	}
	void FixedUpdate()
	{	
		WaitForStart();
		CheckPlayerReady();
		CheckGameStart();	
	}
}
