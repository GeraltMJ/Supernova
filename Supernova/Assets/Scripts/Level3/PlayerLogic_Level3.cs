using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic_Level3 : MonoBehaviour {

	private int dragonPower1Count = 9;
	private int dragonPower2Count = 9;
	private int dragonPower3Count = 11;
	private int knightPower1Count = 10;
	private int knightPower2Count = 10;
	private int magicPower1Count = 11;
	private int magicPower2Count = 11;
	private int magicPower3Count = 9;

	private int assassinPower1Count = 10;
	private int assassinPower2Count = 12;

	private int bossPowerCount = 31;


	private int powerIndex = 0;
	private int addOneCount = 3;
	private int addTwoCount = 6;
	private int overPoisonCount = 5;
	private int overAreaCount = 5;
	private int attackBuffCount = 8;
	private int damageReflectCount = 6;
	private int shieldSkillCount = 6;
	private int gunSkillCount = 6;
	private int changeHpSkillCount = 8;
	private int iceSkillCount = 12;
	private TcpClient_Level3 tcpClient;

	public Sprite poisonReplace;
	public Sprite areaReplace;
	public Sprite windReplace;
	

	public GameObject smokeEffect;
	public GameObject redCross;
	private SpriteRenderer spriteRender;
	public Sprite[] sprite;
	public RuntimeAnimatorController dragonController, knightController, magicController, assassinController, bossController;
	private Animator animator;
	private int deleteIndex = 0;

	private GameObject[] powerRedCrossToDelete;
	private GameObject characterRedCross;

	private float dragonHp1 = 18f;
	private float dragonHp2 = 16f;
	private float dragonAttackAbility = 2f;
	private float knightHp = 12f;
	private float knightAttackAbility = 2f;
	private float magicHp = 12;
	private float magicAttackAbility1 = 2f;
	private float magicAttackAbility2 = 3f;
	private float assassinHp1 = 8f;
	private float assassinHp2 = 10f;
	private float assassinAttackAbility = 4f;
	private float bossHp = 20f;
	private float bossAttackAbility = 2f;

	public AudioClip recoverBuffClip, dragonAttackClip, knightAttackClip, magicAttackClip, assassinAttackClip, bossAttackClip, enhanceBuffClip, overBuffClip;
	private AudioSource audioSource;
	private AudioSource[] audioSources;

	private PlayerMove_Level3 playerMove;
	public GameObject networkManager;

	void UpdateFace(FaceDirection direction)
	{
		switch(direction)
		{
			case FaceDirection.Up:
				animator.SetTrigger("UpWalk");
				break;
			case FaceDirection.Down:
				animator.SetTrigger("DownWalk");
				break;
			case FaceDirection.Left:
				animator.SetTrigger("LeftWalk");
				break;
			case FaceDirection.Right:
				animator.SetTrigger("RightWalk");
				break;
		}
	}

	void Awake() {
		spriteRender = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();
		playerMove = GetComponent<PlayerMove_Level3>();
	}

	void Start()
	{
		powerRedCrossToDelete = new GameObject[50];
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[0];
		tcpClient  = networkManager.GetComponent<TcpClient_Level3>();
	}

	void ChangeToArea(Collider2D collision)
	{
		collision.gameObject.GetComponent<SpriteRenderer>().sprite = areaReplace;
		collision.gameObject.tag = "AreaPic";
	}

	void ChangeToPoison(Collider2D collision)
	{
		collision.gameObject.GetComponent<SpriteRenderer>().sprite = poisonReplace;
		collision.gameObject.tag = "PoisonPic";
	}

	void ChangeToWind(Collider2D collision)
	{
		collision.gameObject.GetComponent<SpriteRenderer>().sprite = windReplace;
		collision.gameObject.tag = "WindPic";
	}

	void BecomeInvisible()
	{
		spriteRender.sortingLayerName = "Background";
	}
	
	void BecomeVisible()
	{
		spriteRender.sortingLayerName = "Player";
	}

	void RecoverEffect()
	{

	}

	void RemovePowerRedCrossOnMap()
	{
		for(int i = 0; i < deleteIndex; i++)
		{
			if(powerRedCrossToDelete[i])
			{
				Destroy(powerRedCrossToDelete[i]);
			}
		}
		deleteIndex = 0;
	}

	void AddPowerRedCrossToList(GameObject rc)
	{
		powerRedCrossToDelete[deleteIndex] = rc;
		deleteIndex++;
	}

	void RemoveCharacterRedCross()
	{
		if(characterRedCross)
		{
			Destroy(characterRedCross);
		}
	}

	

	void CheckPlayer1Trigger(Collider2D collision)
	{
		bool visible = true;
		bool attackBuffOnArea = false;
		bool speedUpOnWind = false;
		if (collision.gameObject.CompareTag("DragonPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon)
			{	
				Player1Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Dragon;
				Player1Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player1Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = dragonController;
				audioSource.clip = dragonAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player1 turn into Dragon");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("KnightPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight)
			{	
				Player1Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Knight;
				Player1Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player1Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = knightController;
				audioSource.clip = knightAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player1 turn into Knight");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("MagicPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic)
			{	
				Player1Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Magic;
				Player1Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player1Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[2];
				animator.runtimeAnimatorController = magicController;
				audioSource.clip = magicAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player1 turn into Magic");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("AssassinPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin)
			{	
				Player1Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Assassin;
				Player1Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player1Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[3];
				animator.runtimeAnimatorController = assassinController;
				audioSource.clip = assassinAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player1 turn into Assassin");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("BossPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss)
			{	
				Player1Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Boss;
				Player1Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player1Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[4];
				animator.runtimeAnimatorController = bossController;
				audioSource.clip = bossAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player1 turn into Boss");
			}
			powerIndex = 0;
		}
		else if(collision.gameObject.CompareTag("DragonPowerPic"))
		{
			if(Player1Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Dragon)
			{
				if(collision.gameObject.name == "DragonPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower2Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower3Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == dragonPower1Count && collision.gameObject.name == "DragonPower1Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower1;
					Player1Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp1);
					Player1Status_Level3._instance.maxHp = dragonHp1;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Dragon Power1");
					TurnEffect();
				}
				else if(powerIndex == dragonPower2Count && collision.gameObject.name == "DragonPower2Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower2;
					Player1Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp1);
					Player1Status_Level3._instance.maxHp = dragonHp1;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Dragon Power2");
					TurnEffect();
				}
				else if(powerIndex == dragonPower3Count && collision.gameObject.name == "DragonPower3Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower3;
					Player1Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp2);
					Player1Status_Level3._instance.maxHp = dragonHp2;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Dragon Power3");
					TurnEffect();
				}
				
			}
		}
		else if(collision.gameObject.CompareTag("KnightPowerPic"))
		{
			if(Player1Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Knight)
			{
				if(collision.gameObject.name == "KnightPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "KnightPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "KnightPower2Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == knightPower1Count && collision.gameObject.name == "KnightPower1Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.KnightPower1;
					Player1Status_Level3._instance.originAttack = knightAttackAbility;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * knightHp);
					Player1Status_Level3._instance.maxHp = knightHp;

					audioSource.clip = knightAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Knight Power1");
					TurnEffect();
				}
				else if(powerIndex == knightPower2Count && collision.gameObject.name == "KnightPower2Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.KnightPower2;
					Player1Status_Level3._instance.originAttack = knightAttackAbility;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * knightHp);
					Player1Status_Level3._instance.maxHp = knightHp;

					audioSource.clip = knightAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Knight Power2");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("MagicPowerPic"))
		{
			if(Player1Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Magic)
			{
				if(collision.gameObject.name == "MagicPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower2Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower3Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == magicPower1Count && collision.gameObject.name == "MagicPower1Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower1;
					Player1Status_Level3._instance.originAttack = magicAttackAbility1;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player1Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Magic Power1");
					TurnEffect();
				}
				else if(powerIndex == magicPower2Count && collision.gameObject.name == "MagicPower2Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower2;
					Player1Status_Level3._instance.originAttack = magicAttackAbility1;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player1Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Magic Power2");
					TurnEffect();
				}
				else if(powerIndex == magicPower3Count && collision.gameObject.name == "MagicPower3Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower3;
					Player1Status_Level3._instance.originAttack = magicAttackAbility2;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player1Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Magic Power3");
					TurnEffect();
				}
				
			}
		}
		else if(collision.gameObject.CompareTag("AssassinPowerPic"))
		{
			if(Player1Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Assassin)
			{
				if(collision.gameObject.name == "AssassinPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "AssassinPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "AssassinPower2Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == assassinPower1Count && collision.gameObject.name == "AssassinPower1Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.AssassinPower1;
					Player1Status_Level3._instance.originAttack = assassinAttackAbility;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * assassinHp1);
					Player1Status_Level3._instance.maxHp = assassinHp1;

					audioSource.clip = assassinAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Assassin Power1");
					TurnEffect();
				}
				else if(powerIndex == assassinPower2Count && collision.gameObject.name == "AssassinPower2Pic" + (powerIndex-1).ToString())
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.AssassinPower2;
					Player1Status_Level3._instance.originAttack = assassinAttackAbility;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * assassinHp2);
					Player1Status_Level3._instance.maxHp = assassinHp2;

					audioSource.clip = assassinAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Knight Power2");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("BossPowerPic"))
		{
			if(Player1Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Boss)
			{
				if(collision.gameObject.name == "BossPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == bossPowerCount)
				{
					Player1Status_Level3._instance.playerPower = PlayerPower_Level3.BossPower;
					Player1Status_Level3._instance.originAttack = bossAttackAbility;

					float percent = Player1Status_Level3._instance.hp / Player1Status_Level3._instance.maxHp;
					Player1Status_Level3._instance.hp = Mathf.RoundToInt(percent * bossHp);
					Player1Status_Level3._instance.maxHp = bossHp;

					audioSource.clip = bossAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Boss Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("PoisonPic"))
		{
			powerIndex = 0;
			switch(Player1Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					break;
				case PlayerPower_Level3.DragonPower2:
					break;
				case PlayerPower_Level3.DragonPower3:
					break;
				case PlayerPower_Level3.KnightPower1:
					if(!Player1Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
						{
							Player1Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
				case PlayerPower_Level3.KnightPower2:
					if(!Player1Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
						{
							Player1Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
				case PlayerPower_Level3.MagicPower1:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level3.MagicPower2:
					if(!Player1Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
						{
							Player1Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
				case PlayerPower_Level3.MagicPower3:
					if(!Player1Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
						{
							Player1Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
				case PlayerPower_Level3.AssassinPower1:
					if(PlayerStatusControl_Level3._instance.playerIdentity == 2 || PlayerStatusControl_Level3._instance.playerIdentity == 3)
					{
						BecomeInvisible();
						visible = false;
					}
					break;
				case PlayerPower_Level3.AssassinPower2:
					if(PlayerStatusControl_Level3._instance.playerIdentity == 2 || PlayerStatusControl_Level3._instance.playerIdentity == 3)
					{
						BecomeInvisible();
						visible = false;
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					if(!Player1Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
						{
							Player1Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AreaPic"))
		{
			powerIndex = 0;
			switch(Player1Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					ChangeToWind(collision);
					break;
				case PlayerPower_Level3.DragonPower2:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.DragonPower3:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.KnightPower1:
					Player1Status_Level3._instance.attackAbility = Player1Status_Level3._instance.originAttack + 2;
					attackBuffOnArea = true;
					break;
				case PlayerPower_Level3.KnightPower2:
					break;
				case PlayerPower_Level3.MagicPower1:
					break;
				case PlayerPower_Level3.MagicPower2:
					break;
				case PlayerPower_Level3.MagicPower3:
					break;
				case PlayerPower_Level3.AssassinPower1:
					if(!Player1Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
						{
							Player1Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
				case PlayerPower_Level3.AssassinPower2:
					if(!Player1Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
						{
							Player1Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					if(!Player1Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
						{
							Player1Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
			}
		}
		else if(collision.gameObject.CompareTag("WindPic"))
		{
			powerIndex = 0;
			switch(Player1Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.DragonPower2:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.DragonPower3:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.KnightPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.KnightPower2:
					break;
				case PlayerPower_Level3.MagicPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.MagicPower2:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level3.MagicPower3:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.AssassinPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.AssassinPower2:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
					{
						Player1Status_Level3._instance.Damage(1);
						tcpClient.SendHpChange(1,-1);
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AddOnePic"))
		{
			if(collision.gameObject.name == "AddOnePic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == addOneCount)
			{
				if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
				{
					Player1Status_Level3._instance.Recover(2);
					tcpClient.SendHpChange(1,2);
				}
				audioSource.clip = recoverBuffClip;
				audioSource.Play();
				RecoverEffect();
				Debug.Log("Player1 recorve 2 hp");
			}
		}
		else if(collision.gameObject.CompareTag("AddTwoPic"))
		{
			if(collision.gameObject.name == "AddTwoPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == addTwoCount)
			{
				if(PlayerStatusControl_Level3._instance.playerIdentity == 1)
				{
					Player1Status_Level3._instance.Recover(4);
					tcpClient.SendHpChange(1,4);
				}
				audioSource.clip = recoverBuffClip;
				audioSource.Play();
				RecoverEffect();
				Debug.Log("Player1 recorve 4 hp");
			}
		}
		else if(collision.gameObject.CompareTag("OverPoisonPic"))
		{
			if(collision.gameObject.name == "OverPoisonPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == overPoisonCount)
			{
				Player1Status_Level3._instance.overPoison = true;
				audioSource.clip = overBuffClip;
				audioSource.Play();
				Debug.Log("Player1 is poison no effect");
			}
		}
		else if(collision.gameObject.CompareTag("OverAreaPic"))
		{
			if(collision.gameObject.name == "OverAreaPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == overAreaCount)
			{
				Player1Status_Level3._instance.overArea = true;
				audioSource.clip = overBuffClip;
				audioSource.Play();
				Debug.Log("Player1 is area no effect");
			}
		}
		else if(collision.gameObject.CompareTag("DamageReflectPic"))
		{
			if(collision.gameObject.name == "DamageReflectPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == damageReflectCount)
			{
				Player1Status_Level3._instance.damageReflect = true;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player1 is damage Reflect");
			}
		}
		else if(collision.gameObject.CompareTag("AttackBuffPic"))
		{
			if(collision.gameObject.name == "AttackBuffPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player1Status_Level3._instance.attackBuff = true;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player1 is attack buff");
			}
		}
		else if(collision.gameObject.CompareTag("ShieldSkillPic"))
		{
			if(collision.gameObject.name == "ShieldSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == shieldSkillCount)
			{
				Player1Status_Level3._instance.playerSkill = PlayerSkill_Level3.ShieldSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player1 has shield skill");
			}
		}
		else if(collision.gameObject.CompareTag("GunSkillPic"))
		{
			if(collision.gameObject.name == "GunSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player1Status_Level3._instance.playerSkill = PlayerSkill_Level3.GunSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player1 has gun skill");
			}
		}
		else if(collision.gameObject.CompareTag("IceSkillPic"))
		{
			if(collision.gameObject.name == "IceSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player1Status_Level3._instance.playerSkill = PlayerSkill_Level3.IceSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player1 has ice skill");
			}
		}
		else if(collision.gameObject.CompareTag("Player1Bullet") || collision.gameObject.CompareTag("Player2Bullet") || collision.gameObject.CompareTag("Player3Bullet")
				|| collision.gameObject.CompareTag("PlayerCube"))
		{

		}
		else
		{
			RemovePowerRedCrossOnMap();
			powerIndex = 0;
		}
		if(visible || ( PlayerStatusControl_Level3._instance.playerIdentity == 2 && 
													(Player2Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower1 ||
													Player2Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower2 ||
													Player2Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower3))
				   || ( PlayerStatusControl_Level3._instance.playerIdentity == 3 && 
													(Player3Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower1 ||
													Player3Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower2 ||
													Player3Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower3)) )
		{
			BecomeVisible();
		}
		if(!attackBuffOnArea)
		{
			Player1Status_Level3._instance.attackAbility = Player1Status_Level3._instance.originAttack;
		}
		if(!speedUpOnWind)
		{
			playerMove.speedUp = 1f;
		}
	}

	void CheckPlayer2Trigger(Collider2D collision)
	{
		bool visible = true;
		bool attackBuffOnArea = false;
		bool speedUpOnWind = false;
		if (collision.gameObject.CompareTag("DragonPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon)
			{	
				Player2Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Dragon;
				Player2Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player2Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = dragonController;
				audioSource.clip = dragonAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player2 turn into Dragon");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("KnightPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight)
			{	
				Player2Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Knight;
				Player2Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player2Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = knightController;
				audioSource.clip = knightAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player2 turn into Knight");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("MagicPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic)
			{	
				Player2Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Magic;
				Player2Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player2Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[2];
				animator.runtimeAnimatorController = magicController;
				audioSource.clip = magicAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player2 turn into Magic");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("AssassinPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin)
			{	
				Player2Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Assassin;
				Player2Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player2Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[3];
				animator.runtimeAnimatorController = assassinController;
				audioSource.clip = assassinAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player2 turn into Assassin");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("BossPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss)
			{	
				Player2Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Boss;
				Player2Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player2Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[4];
				animator.runtimeAnimatorController = bossController;
				audioSource.clip = bossAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player2 turn into Boss");
			}
			powerIndex = 0;
		}
		else if(collision.gameObject.CompareTag("DragonPowerPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Dragon)
			{
				if(collision.gameObject.name == "DragonPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower2Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower3Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == dragonPower1Count && collision.gameObject.name == "DragonPower1Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower1;
					Player2Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp1);
					Player2Status_Level3._instance.maxHp = dragonHp1;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Dragon Power1");
					TurnEffect();
				}
				else if(powerIndex == dragonPower2Count && collision.gameObject.name == "DragonPower2Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower2;
					Player2Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp1);
					Player2Status_Level3._instance.maxHp = dragonHp1;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Dragon Power2");
					TurnEffect();
				}
				else if(powerIndex == dragonPower3Count && collision.gameObject.name == "DragonPower3Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower3;
					Player2Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp2);
					Player2Status_Level3._instance.maxHp = dragonHp2;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Dragon Power3");
					TurnEffect();
				}
				
			}
		}
		else if(collision.gameObject.CompareTag("KnightPowerPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Knight)
			{
				if(collision.gameObject.name == "KnightPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "KnightPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "KnightPower2Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == knightPower1Count && collision.gameObject.name == "KnightPower1Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.KnightPower1;
					Player2Status_Level3._instance.originAttack = knightAttackAbility;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * knightHp);
					Player2Status_Level3._instance.maxHp = knightHp;

					audioSource.clip = knightAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Knight Power1");
					TurnEffect();
				}
				else if(powerIndex == knightPower2Count && collision.gameObject.name == "KnightPower2Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.KnightPower2;
					Player2Status_Level3._instance.originAttack = knightAttackAbility;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * knightHp);
					Player2Status_Level3._instance.maxHp = knightHp;

					audioSource.clip = knightAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Knight Power2");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("MagicPowerPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Magic)
			{
				if(collision.gameObject.name == "MagicPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower2Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower3Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == magicPower1Count && collision.gameObject.name == "MagicPower1Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower1;
					Player2Status_Level3._instance.originAttack = magicAttackAbility1;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player2Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Magic Power1");
					TurnEffect();
				}
				else if(powerIndex == magicPower2Count && collision.gameObject.name == "MagicPower2Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower2;
					Player2Status_Level3._instance.originAttack = magicAttackAbility1;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player2Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Magic Power2");
					TurnEffect();
				}
				else if(powerIndex == magicPower3Count && collision.gameObject.name == "MagicPower3Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower3;
					Player2Status_Level3._instance.originAttack = magicAttackAbility2;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player2Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Magic Power3");
					TurnEffect();
				}
				
			}
		}
		else if(collision.gameObject.CompareTag("AssassinPowerPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Assassin)
			{
				if(collision.gameObject.name == "AssassinPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "AssassinPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "AssassinPower2Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == assassinPower1Count && collision.gameObject.name == "AssassinPower1Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.AssassinPower1;
					Player2Status_Level3._instance.originAttack = assassinAttackAbility;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * assassinHp1);
					Player2Status_Level3._instance.maxHp = assassinHp1;

					audioSource.clip = assassinAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Assassin Power1");
					TurnEffect();
				}
				else if(powerIndex == assassinPower2Count && collision.gameObject.name == "AssassinPower2Pic" + (powerIndex-1).ToString())
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.AssassinPower2;
					Player2Status_Level3._instance.originAttack = assassinAttackAbility;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * assassinHp2);
					Player2Status_Level3._instance.maxHp = assassinHp2;

					audioSource.clip = assassinAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Knight Power2");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("BossPowerPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Boss)
			{
				if(collision.gameObject.name == "BossPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == bossPowerCount)
				{
					Player2Status_Level3._instance.playerPower = PlayerPower_Level3.BossPower;
					Player2Status_Level3._instance.originAttack = bossAttackAbility;

					float percent = Player2Status_Level3._instance.hp / Player2Status_Level3._instance.maxHp;
					Player2Status_Level3._instance.hp = Mathf.RoundToInt(percent * bossHp);
					Player2Status_Level3._instance.maxHp = bossHp;

					audioSource.clip = bossAttackClip;
					audioSource.Play();

					Debug.Log("Player2 has Boss Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("PoisonPic"))
		{
			powerIndex = 0;
			switch(Player2Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					break;
				case PlayerPower_Level3.DragonPower2:
					break;
				case PlayerPower_Level3.DragonPower3:
					break;
				case PlayerPower_Level3.KnightPower1:
					if(!Player2Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
						{
							Player2Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(2,-1);
						}
					}
					break;
				case PlayerPower_Level3.KnightPower2:
					if(!Player2Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
						{
							Player2Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(2,-1);
						}
					}
					break;
				case PlayerPower_Level3.MagicPower1:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level3.MagicPower2:
					if(!Player2Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
						{
							Player2Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(2,-1);
						}
					}
					break;
				case PlayerPower_Level3.MagicPower3:
					if(!Player2Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
						{
							Player2Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(2,-1);
						}
					}
					break;
				case PlayerPower_Level3.AssassinPower1:
					if(PlayerStatusControl_Level3._instance.playerIdentity == 1 || PlayerStatusControl_Level3._instance.playerIdentity == 3)
					{
						BecomeInvisible();
						visible = false;
					}
					break;
				case PlayerPower_Level3.AssassinPower2:
					if(PlayerStatusControl_Level3._instance.playerIdentity == 1 || PlayerStatusControl_Level3._instance.playerIdentity == 3)
					{
						BecomeInvisible();
						visible = false;
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					if(!Player2Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
						{
							Player2Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(2,-1);
						}
					}
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AreaPic"))
		{
			powerIndex = 0;
			switch(Player2Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					ChangeToWind(collision);
					break;
				case PlayerPower_Level3.DragonPower2:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.DragonPower3:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.KnightPower1:
					Player2Status_Level3._instance.attackAbility = Player2Status_Level3._instance.originAttack + 2;
					attackBuffOnArea = true;
					break;
				case PlayerPower_Level3.KnightPower2:
					break;
				case PlayerPower_Level3.MagicPower1:
					break;
				case PlayerPower_Level3.MagicPower2:
					break;
				case PlayerPower_Level3.MagicPower3:
					break;
				case PlayerPower_Level3.AssassinPower1:
					if(!Player2Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
						{
							Player2Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(1,-1);
						}
					}
					break;
				case PlayerPower_Level3.AssassinPower2:
					if(!Player2Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
						{
							Player2Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(2,-1);
						}
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					if(!Player2Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
						{
							Player2Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(2,-1);
						}
					}
					break;
			}
		}
		else if(collision.gameObject.CompareTag("WindPic"))
		{
			powerIndex = 0;
			switch(Player2Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.DragonPower2:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.DragonPower3:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.KnightPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.KnightPower2:
					break;
				case PlayerPower_Level3.MagicPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.MagicPower2:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level3.MagicPower3:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.AssassinPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.AssassinPower2:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
					{
						Player2Status_Level3._instance.Damage(1);
						tcpClient.SendHpChange(1,-1);
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AddOnePic"))
		{
			if(collision.gameObject.name == "AddOnePic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == addOneCount)
			{
				if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
				{
					Player2Status_Level3._instance.Recover(2);
					tcpClient.SendHpChange(2,2);
				}
				audioSource.clip = recoverBuffClip;
				audioSource.Play();
				RecoverEffect();
				Debug.Log("Player2 recorve 2 hp");
			}
		}
		else if(collision.gameObject.CompareTag("AddTwoPic"))
		{
			if(collision.gameObject.name == "AddTwoPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == addTwoCount)
			{
				if(PlayerStatusControl_Level3._instance.playerIdentity == 2)
				{
					Player2Status_Level3._instance.Recover(4);
					tcpClient.SendHpChange(2,4);
				}
				audioSource.clip = recoverBuffClip;
				audioSource.Play();
				RecoverEffect();
				Debug.Log("Player2 recorve 4 hp");
			}
		}
		else if(collision.gameObject.CompareTag("OverPoisonPic"))
		{
			if(collision.gameObject.name == "OverPoisonPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == overPoisonCount)
			{
				Player2Status_Level3._instance.overPoison = true;
				audioSource.clip = overBuffClip;
				audioSource.Play();
				Debug.Log("Player2 is poison no effect");
			}
		}
		else if(collision.gameObject.CompareTag("OverAreaPic"))
		{
			if(collision.gameObject.name == "OverAreaPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == overAreaCount)
			{
				Player2Status_Level3._instance.overArea = true;
				audioSource.clip = overBuffClip;
				audioSource.Play();
				Debug.Log("Player2 is area no effect");
			}
		}
		else if(collision.gameObject.CompareTag("DamageReflectPic"))
		{
			if(collision.gameObject.name == "DamageReflectPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == damageReflectCount)
			{
				Player2Status_Level3._instance.damageReflect = true;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player2 is damage Reflect");
			}
		}
		else if(collision.gameObject.CompareTag("AttackBuffPic"))
		{
			if(collision.gameObject.name == "AttackBuffPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player2Status_Level3._instance.attackBuff = true;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player2 is attack buff");
			}
		}
		else if(collision.gameObject.CompareTag("ShieldSkillPic"))
		{
			if(collision.gameObject.name == "ShieldSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == shieldSkillCount)
			{
				Player2Status_Level3._instance.playerSkill = PlayerSkill_Level3.ShieldSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player2 has shield skill");
			}
		}
		else if(collision.gameObject.CompareTag("GunSkillPic"))
		{
			if(collision.gameObject.name == "GunSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player2Status_Level3._instance.playerSkill = PlayerSkill_Level3.GunSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player2 has gun skill");
			}
		}
		else if(collision.gameObject.CompareTag("IceSkillPic"))
		{
			if(collision.gameObject.name == "IceSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player2Status_Level3._instance.playerSkill = PlayerSkill_Level3.IceSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player2 has ice skill");
			}
		}
		else if(collision.gameObject.CompareTag("Player1Bullet") || collision.gameObject.CompareTag("Player2Bullet") || collision.gameObject.CompareTag("Player3Bullet")
				|| collision.gameObject.CompareTag("PlayerCube"))
		{

		}
		else
		{
			RemovePowerRedCrossOnMap();
			powerIndex = 0;
		}
		if(visible || ( PlayerStatusControl_Level3._instance.playerIdentity == 1 && 
													(Player1Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower1 ||
													Player1Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower2 ||
													Player1Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower3))
				   || ( PlayerStatusControl_Level3._instance.playerIdentity == 3 && 
													(Player3Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower1 ||
													Player3Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower2 ||
													Player3Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower3)) )
		{
			BecomeVisible();
		}
		if(!attackBuffOnArea)
		{
			Player2Status_Level3._instance.attackAbility = Player2Status_Level3._instance.originAttack;
		}
		if(!speedUpOnWind)
		{
			playerMove.speedUp = 1f;
		}
	}


	void CheckPlayer3Trigger(Collider2D collision)
	{
		bool visible = true;
		bool attackBuffOnArea = false;
		bool speedUpOnWind = false;
		if (collision.gameObject.CompareTag("DragonPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Dragon)
			{	
				Player3Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Dragon;
				Player3Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player3Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = dragonController;
				audioSource.clip = dragonAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player3 turn into Dragon");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("KnightPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Knight)
			{	
				Player3Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Knight;
				Player3Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player3Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = knightController;
				audioSource.clip = knightAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player3 turn into Knight");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("MagicPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Magic)
			{	
				Player3Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Magic;
				Player3Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player3Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[2];
				animator.runtimeAnimatorController = magicController;
				audioSource.clip = magicAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player3 turn into Magic");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("AssassinPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Assassin)
			{	
				Player3Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Assassin;
				Player3Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player3Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[3];
				animator.runtimeAnimatorController = assassinController;
				audioSource.clip = assassinAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player3 turn into Assassin");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("BossPic"))
		{
			if(Player2Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss 
				&& Player1Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss
				&& Player3Status_Level3._instance.playerCharacter != PlayerCharacter_Level3.Boss)
			{	
				Player3Status_Level3._instance.playerCharacter = PlayerCharacter_Level3.Boss;
				Player3Status_Level3._instance.playerPower = PlayerPower_Level3.Default;
				Player3Status_Level3._instance.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[4];
				animator.runtimeAnimatorController = bossController;
				audioSource.clip = bossAttackClip;
				audioSource.Play();
				UpdateFace(playerMove.dir);
				Debug.Log("Player3 turn into Boss");
			}
			powerIndex = 0;
		}
		else if(collision.gameObject.CompareTag("DragonPowerPic"))
		{
			if(Player3Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Dragon)
			{
				if(collision.gameObject.name == "DragonPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower2Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "DragonPower3Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == dragonPower1Count && collision.gameObject.name == "DragonPower1Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower1;
					Player3Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp1);
					Player3Status_Level3._instance.maxHp = dragonHp1;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Dragon Power1");
					TurnEffect();
				}
				else if(powerIndex == dragonPower2Count && collision.gameObject.name == "DragonPower2Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower2;
					Player3Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp1);
					Player3Status_Level3._instance.maxHp = dragonHp1;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Dragon Power2");
					TurnEffect();
				}
				else if(powerIndex == dragonPower3Count && collision.gameObject.name == "DragonPower3Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.DragonPower3;
					Player3Status_Level3._instance.originAttack = dragonAttackAbility;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * dragonHp2);
					Player3Status_Level3._instance.maxHp = dragonHp2;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Dragon Power3");
					TurnEffect();
				}
				
			}
		}
		else if(collision.gameObject.CompareTag("KnightPowerPic"))
		{
			if(Player3Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Knight)
			{
				if(collision.gameObject.name == "KnightPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "KnightPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "KnightPower2Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == knightPower1Count && collision.gameObject.name == "KnightPower1Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.KnightPower1;
					Player3Status_Level3._instance.originAttack = knightAttackAbility;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * knightHp);
					Player3Status_Level3._instance.maxHp = knightHp;

					audioSource.clip = knightAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Knight Power1");
					TurnEffect();
				}
				else if(powerIndex == knightPower2Count && collision.gameObject.name == "KnightPower2Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.KnightPower2;
					Player3Status_Level3._instance.originAttack = knightAttackAbility;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * knightHp);
					Player3Status_Level3._instance.maxHp = knightHp;

					audioSource.clip = knightAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Knight Power2");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("MagicPowerPic"))
		{
			if(Player3Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Magic)
			{
				if(collision.gameObject.name == "MagicPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower2Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "MagicPower3Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == magicPower1Count && collision.gameObject.name == "MagicPower1Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower1;
					Player3Status_Level3._instance.originAttack = magicAttackAbility1;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player3Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Magic Power1");
					TurnEffect();
				}
				else if(powerIndex == magicPower2Count && collision.gameObject.name == "MagicPower2Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower2;
					Player3Status_Level3._instance.originAttack = magicAttackAbility1;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player3Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Magic Power2");
					TurnEffect();
				}
				else if(powerIndex == magicPower3Count && collision.gameObject.name == "MagicPower3Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.MagicPower3;
					Player3Status_Level3._instance.originAttack = magicAttackAbility2;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * magicHp);
					Player3Status_Level3._instance.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Magic Power3");
					TurnEffect();
				}
				
			}
		}
		else if(collision.gameObject.CompareTag("AssassinPowerPic"))
		{
			if(Player3Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Assassin)
			{
				if(collision.gameObject.name == "AssassinPowerPic" + powerIndex.ToString()
					|| collision.gameObject.name == "AssassinPower1Pic" + powerIndex.ToString()
					|| collision.gameObject.name == "AssassinPower2Pic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == assassinPower1Count && collision.gameObject.name == "AssassinPower1Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.AssassinPower1;
					Player3Status_Level3._instance.originAttack = assassinAttackAbility;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * assassinHp1);
					Player3Status_Level3._instance.maxHp = assassinHp1;

					audioSource.clip = assassinAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Assassin Power1");
					TurnEffect();
				}
				else if(powerIndex == assassinPower2Count && collision.gameObject.name == "AssassinPower2Pic" + (powerIndex-1).ToString())
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.AssassinPower2;
					Player3Status_Level3._instance.originAttack = assassinAttackAbility;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * assassinHp2);
					Player3Status_Level3._instance.maxHp = assassinHp2;

					audioSource.clip = assassinAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Knight Power2");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("BossPowerPic"))
		{
			if(Player3Status_Level3._instance.playerCharacter == PlayerCharacter_Level3.Boss)
			{
				if(collision.gameObject.name == "BossPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == bossPowerCount)
				{
					Player3Status_Level3._instance.playerPower = PlayerPower_Level3.BossPower;
					Player3Status_Level3._instance.originAttack = bossAttackAbility;

					float percent = Player3Status_Level3._instance.hp / Player3Status_Level3._instance.maxHp;
					Player3Status_Level3._instance.hp = Mathf.RoundToInt(percent * bossHp);
					Player3Status_Level3._instance.maxHp = bossHp;

					audioSource.clip = bossAttackClip;
					audioSource.Play();

					Debug.Log("Player3 has Boss Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("PoisonPic"))
		{
			powerIndex = 0;
			switch(Player3Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					break;
				case PlayerPower_Level3.DragonPower2:
					break;
				case PlayerPower_Level3.DragonPower3:
					break;
				case PlayerPower_Level3.KnightPower1:
					if(!Player3Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
						{
							Player3Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(3,-1);
						}
					}
					break;
				case PlayerPower_Level3.KnightPower2:
					if(!Player3Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
						{
							Player3Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(3,-1);
						}
					}
					break;
				case PlayerPower_Level3.MagicPower1:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level3.MagicPower2:
					if(!Player3Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
						{
							Player3Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(3,-1);
						}
					}
					break;
				case PlayerPower_Level3.MagicPower3:
					if(!Player3Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
						{
							Player3Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(3,-1);
						}
					}
					break;
				case PlayerPower_Level3.AssassinPower1:
					if(PlayerStatusControl_Level3._instance.playerIdentity == 1 || PlayerStatusControl_Level3._instance.playerIdentity == 2)
					{
						BecomeInvisible();
						visible = false;
					}
					break;
				case PlayerPower_Level3.AssassinPower2:
					if(PlayerStatusControl_Level3._instance.playerIdentity == 1 || PlayerStatusControl_Level3._instance.playerIdentity == 2)
					{
						BecomeInvisible();
						visible = false;
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					if(!Player3Status_Level3._instance.overPoison)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
						{
							Player3Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(3,-1);
						}
					}
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AreaPic"))
		{
			powerIndex = 0;
			switch(Player3Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					ChangeToWind(collision);
					break;
				case PlayerPower_Level3.DragonPower2:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.DragonPower3:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.KnightPower1:
					Player3Status_Level3._instance.attackAbility = Player3Status_Level3._instance.originAttack + 2;
					attackBuffOnArea = true;
					break;
				case PlayerPower_Level3.KnightPower2:
					break;
				case PlayerPower_Level3.MagicPower1:
					break;
				case PlayerPower_Level3.MagicPower2:
					break;
				case PlayerPower_Level3.MagicPower3:
					break;
				case PlayerPower_Level3.AssassinPower1:
					if(!Player3Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
						{
							Player3Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(3,-1);
						}
					}
					break;
				case PlayerPower_Level3.AssassinPower2:
					if(!Player3Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
						{
							Player3Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(3,-1);
						}
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					if(!Player3Status_Level3._instance.overArea)
					{
						if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
						{
							Player3Status_Level3._instance.Damage(1);
							tcpClient.SendHpChange(3,-1);
						}
					}
					break;
			}
		}
		else if(collision.gameObject.CompareTag("WindPic"))
		{
			powerIndex = 0;
			switch(Player3Status_Level3._instance.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.DragonPower2:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.DragonPower3:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level3.KnightPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.KnightPower2:
					break;
				case PlayerPower_Level3.MagicPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.MagicPower2:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level3.MagicPower3:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.AssassinPower1:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
				case PlayerPower_Level3.AssassinPower2:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
					{
						Player3Status_Level3._instance.Damage(1);
						tcpClient.SendHpChange(3,-1);
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					speedUpOnWind = true;
					playerMove.speedUp = 2f;
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AddOnePic"))
		{
			if(collision.gameObject.name == "AddOnePic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == addOneCount)
			{
				if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
				{
					Player3Status_Level3._instance.Recover(2);
					tcpClient.SendHpChange(3,2);
				}
				audioSource.clip = recoverBuffClip;
				audioSource.Play();
				RecoverEffect();
				Debug.Log("Player3 recorve 2 hp");
			}
		}
		else if(collision.gameObject.CompareTag("AddTwoPic"))
		{
			if(collision.gameObject.name == "AddTwoPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == addTwoCount)
			{
				if(PlayerStatusControl_Level3._instance.playerIdentity == 3)
				{
					Player3Status_Level3._instance.Recover(4);
					tcpClient.SendHpChange(3,4);
				}
				audioSource.clip = recoverBuffClip;
				audioSource.Play();
				RecoverEffect();
				Debug.Log("Player3 recorve 4 hp");
			}
		}
		else if(collision.gameObject.CompareTag("OverPoisonPic"))
		{
			if(collision.gameObject.name == "OverPoisonPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == overPoisonCount)
			{
				Player3Status_Level3._instance.overPoison = true;
				audioSource.clip = overBuffClip;
				audioSource.Play();
				Debug.Log("Player3 is poison no effect");
			}
		}
		else if(collision.gameObject.CompareTag("OverAreaPic"))
		{
			if(collision.gameObject.name == "OverAreaPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == overAreaCount)
			{
				Player3Status_Level3._instance.overArea = true;
				audioSource.clip = overBuffClip;
				audioSource.Play();
				Debug.Log("Player3 is area no effect");
			}
		}
		else if(collision.gameObject.CompareTag("DamageReflectPic"))
		{
			if(collision.gameObject.name == "DamageReflectPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == damageReflectCount)
			{
				Player3Status_Level3._instance.damageReflect = true;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player3 is damage Reflect");
			}
		}
		else if(collision.gameObject.CompareTag("AttackBuffPic"))
		{
			if(collision.gameObject.name == "AttackBuffPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player3Status_Level3._instance.attackBuff = true;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player3 is attack buff");
			}
		}
		else if(collision.gameObject.CompareTag("ShieldSkillPic"))
		{
			if(collision.gameObject.name == "ShieldSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == shieldSkillCount)
			{
				Player3Status_Level3._instance.playerSkill = PlayerSkill_Level3.ShieldSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player3 has shield skill");
			}
		}
		else if(collision.gameObject.CompareTag("GunSkillPic"))
		{
			if(collision.gameObject.name == "GunSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player3Status_Level3._instance.playerSkill = PlayerSkill_Level3.GunSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player3 has gun skill");
			}
		}
		else if(collision.gameObject.CompareTag("IceSkillPic"))
		{
			if(collision.gameObject.name == "IceSkillPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == attackBuffCount)
			{
				Player3Status_Level3._instance.playerSkill = PlayerSkill_Level3.IceSkill;
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player3 has ice skill");
			}
		}
		else if(collision.gameObject.CompareTag("Player1Bullet") || collision.gameObject.CompareTag("Player2Bullet") || collision.gameObject.CompareTag("Player3Bullet")
				|| collision.gameObject.CompareTag("PlayerCube"))
		{

		}
		else
		{
			RemovePowerRedCrossOnMap();
			powerIndex = 0;
		}
		if(visible || ( PlayerStatusControl_Level3._instance.playerIdentity == 1 && 
													(Player1Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower1 ||
													Player1Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower2 ||
													Player1Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower3))
				   || ( PlayerStatusControl_Level3._instance.playerIdentity == 2 && 
													(Player2Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower1 ||
													Player2Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower2 ||
													Player2Status_Level3._instance.playerPower == PlayerPower_Level3.MagicPower3)) )
		{
			BecomeVisible();
		}
		if(!attackBuffOnArea)
		{
			Player3Status_Level3._instance.attackAbility = Player3Status_Level3._instance.originAttack;
		}
		if(!speedUpOnWind)
		{
			playerMove.speedUp = 1f;
		}
	}


	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision == null)
		{
			return;
		}
		if(gameObject.CompareTag("Player1"))
		{
			CheckPlayer1Trigger(collision);
		}
		else if(gameObject.CompareTag("Player2"))
		{
			CheckPlayer2Trigger(collision);
		}
		else if(gameObject.CompareTag("Player3"))
		{
			CheckPlayer3Trigger(collision);
		}
	}

	

	void TurnEffect()
	{
		GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
		Destroy(smoke, 1);
	}

	void CharacterRedCrossEffect(Collider2D collision)
	{
		characterRedCross = (GameObject)Instantiate(redCross, collision.transform.position, Quaternion.identity);
	}

	void RedCrossEffect(Collider2D collision)
	{
		GameObject rc = (GameObject)Instantiate(redCross, collision.transform.position, Quaternion.identity);
		AddPowerRedCrossToList(rc);
	}
}
