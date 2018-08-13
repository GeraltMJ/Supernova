using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic_Level3 : MonoBehaviour {


	private int shieldSkillLimit = 2;
	private int iceSkillLimit = 4;
	private int doubleDmgSkillLimit = 3;
	private int gunSkillLimit = 4;

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
	private int teleportCount = 3;
	//private TcpClient_Level3 tcpClient;

	public Sprite poisonReplace;
	public Sprite areaReplace;
	public Sprite windReplace;

	public GameObject[] teleportPosition;

	public GameObject dragonTurnEffect, knightTurnEffect, magicTurnEffect, assassinTurnEffect, bossTurnEffect;
	public GameObject dragonGainPowerEffect, knightGainPowerEffect, magicGainPowerEffect, assassinGainPowerEffect;
	public GameObject gainShieldSkillEffect, gainIceSkillEffect, gainGunSkillEffect, gainDoubleDmgSkillEffect;
	public GameObject changeHpEffect;
	public GameObject poisonEffect, areaEffect, windEffect;
	public GameObject teleportEffect;
	public GameObject recoverEffect2, recoverEffect4;
	public GameObject carryIceSkillEffect, carryGunSkillEffect, carryDoubleDmgSkillEffect;
	

	public GameObject smokeEffect;
	public GameObject redCross;
	private SpriteRenderer spriteRender;
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
	//public GameObject networkManager;

	public GameObject enemy1, enemy2;
	private PlayerStatus_Level3 playerStatus, enemy1Status, enemy2Status;

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
		playerStatus = GetComponent<PlayerStatus_Level3>();
		enemy1Status = enemy1.GetComponent<PlayerStatus_Level3>();
		enemy2Status = enemy2.GetComponent<PlayerStatus_Level3>();
	}

	void Start()
	{
		powerRedCrossToDelete = new GameObject[50];
		audioSources = GetComponents<AudioSource>();
		audioSource = audioSources[0];
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

	

	void CheckPlayerTrigger(Collider2D collision)
	{
		bool visible = true;
		bool attackBuffOnArea = false;
		bool speedUpOnWind = false;
		if (collision.gameObject.CompareTag("DragonPic"))
		{
			if(playerStatus.playerCharacter != PlayerCharacter_Level3.Dragon 
				&& enemy1Status.playerCharacter != PlayerCharacter_Level3.Dragon
				&& enemy2Status.playerCharacter != PlayerCharacter_Level3.Dragon)
			{	
				playerStatus.playerCharacter = PlayerCharacter_Level3.Dragon;
				playerStatus.playerPower = PlayerPower_Level3.Default;
				playerStatus.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect(playerStatus.playerCharacter, collision);
				CharacterRedCrossEffect(collision);
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
			if(playerStatus.playerCharacter != PlayerCharacter_Level3.Knight 
				&& enemy1Status.playerCharacter != PlayerCharacter_Level3.Knight
				&& enemy2Status.playerCharacter != PlayerCharacter_Level3.Knight)
			{	
				playerStatus.playerCharacter = PlayerCharacter_Level3.Knight;
				playerStatus.playerPower = PlayerPower_Level3.Default;
				playerStatus.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect(playerStatus.playerCharacter, collision);
				CharacterRedCrossEffect(collision);
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
			if(playerStatus.playerCharacter != PlayerCharacter_Level3.Magic 
				&& enemy1Status.playerCharacter != PlayerCharacter_Level3.Magic
				&& enemy2Status.playerCharacter != PlayerCharacter_Level3.Magic)
			{	
				playerStatus.playerCharacter = PlayerCharacter_Level3.Magic;
				playerStatus.playerPower = PlayerPower_Level3.Default;
				playerStatus.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect(playerStatus.playerCharacter, collision);
				CharacterRedCrossEffect(collision);
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
			if(playerStatus.playerCharacter != PlayerCharacter_Level3.Assassin 
				&& enemy1Status.playerCharacter != PlayerCharacter_Level3.Assassin
				&& enemy2Status.playerCharacter != PlayerCharacter_Level3.Assassin)
			{	
				playerStatus.playerCharacter = PlayerCharacter_Level3.Assassin;
				playerStatus.playerPower = PlayerPower_Level3.Default;
				playerStatus.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect(playerStatus.playerCharacter, collision);
				CharacterRedCrossEffect(collision);
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
			if(playerStatus.playerCharacter != PlayerCharacter_Level3.Boss 
				&& enemy1Status.playerCharacter != PlayerCharacter_Level3.Boss
				&& enemy2Status.playerCharacter != PlayerCharacter_Level3.Boss)
			{	
				playerStatus.playerCharacter = PlayerCharacter_Level3.Boss;
				playerStatus.playerPower = PlayerPower_Level3.Default;
				playerStatus.originAttack = 0.0f;
				RemoveCharacterRedCross();
				TurnEffect(playerStatus.playerCharacter, collision);
				CharacterRedCrossEffect(collision);
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
			if(playerStatus.playerCharacter == PlayerCharacter_Level3.Dragon)
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
					playerStatus.playerPower = PlayerPower_Level3.DragonPower1;
					playerStatus.originAttack = dragonAttackAbility;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * dragonHp1);
					playerStatus.maxHp = dragonHp1;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Dragon Power1");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
				else if(powerIndex == dragonPower2Count && collision.gameObject.name == "DragonPower2Pic" + (powerIndex-1).ToString())
				{
					playerStatus.playerPower = PlayerPower_Level3.DragonPower2;
					playerStatus.originAttack = dragonAttackAbility;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * dragonHp1);
					playerStatus.maxHp = dragonHp1;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Dragon Power2");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
				else if(powerIndex == dragonPower3Count && collision.gameObject.name == "DragonPower3Pic" + (powerIndex-1).ToString())
				{
					playerStatus.playerPower = PlayerPower_Level3.DragonPower3;
					playerStatus.originAttack = dragonAttackAbility;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * dragonHp2);
					playerStatus.maxHp = dragonHp2;

					audioSource.clip = dragonAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Dragon Power3");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
				
			}
		}
		else if(collision.gameObject.CompareTag("KnightPowerPic"))
		{
			if(playerStatus.playerCharacter == PlayerCharacter_Level3.Knight)
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
					playerStatus.playerPower = PlayerPower_Level3.KnightPower1;
					playerStatus.originAttack = knightAttackAbility;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * knightHp);
					playerStatus.maxHp = knightHp;

					audioSource.clip = knightAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Knight Power1");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
				else if(powerIndex == knightPower2Count && collision.gameObject.name == "KnightPower2Pic" + (powerIndex-1).ToString())
				{
					playerStatus.playerPower = PlayerPower_Level3.KnightPower2;
					playerStatus.originAttack = knightAttackAbility;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * knightHp);
					playerStatus.maxHp = knightHp;

					audioSource.clip = knightAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Knight Power2");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
			}
		}
		else if(collision.gameObject.CompareTag("MagicPowerPic"))
		{
			if(playerStatus.playerCharacter == PlayerCharacter_Level3.Magic)
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
					playerStatus.playerPower = PlayerPower_Level3.MagicPower1;
					playerStatus.originAttack = magicAttackAbility1;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * magicHp);
					playerStatus.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Magic Power1");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
				else if(powerIndex == magicPower2Count && collision.gameObject.name == "MagicPower2Pic" + (powerIndex-1).ToString())
				{
					playerStatus.playerPower = PlayerPower_Level3.MagicPower2;
					playerStatus.originAttack = magicAttackAbility1;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * magicHp);
					playerStatus.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Magic Power2");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
				else if(powerIndex == magicPower3Count && collision.gameObject.name == "MagicPower3Pic" + (powerIndex-1).ToString())
				{
					playerStatus.playerPower = PlayerPower_Level3.MagicPower3;
					playerStatus.originAttack = magicAttackAbility2;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * magicHp);
					playerStatus.maxHp = magicHp;

					audioSource.clip = magicAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Magic Power3");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
				
			}
		}
		else if(collision.gameObject.CompareTag("AssassinPowerPic"))
		{
			if(playerStatus.playerCharacter == PlayerCharacter_Level3.Assassin)
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
					playerStatus.playerPower = PlayerPower_Level3.AssassinPower1;
					playerStatus.originAttack = assassinAttackAbility;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * assassinHp1);
					playerStatus.maxHp = assassinHp1;

					audioSource.clip = assassinAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Assassin Power1");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
				else if(powerIndex == assassinPower2Count && collision.gameObject.name == "AssassinPower2Pic" + (powerIndex-1).ToString())
				{
					playerStatus.playerPower = PlayerPower_Level3.AssassinPower2;
					playerStatus.originAttack = assassinAttackAbility;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * assassinHp2);
					playerStatus.maxHp = assassinHp2;

					audioSource.clip = assassinAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Knight Power2");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
			}
		}
		else if(collision.gameObject.CompareTag("BossPowerPic"))
		{
			if(playerStatus.playerCharacter == PlayerCharacter_Level3.Boss)
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
					playerStatus.playerPower = PlayerPower_Level3.BossPower;
					playerStatus.originAttack = bossAttackAbility;

					float percent = playerStatus.hp / playerStatus.maxHp;
					playerStatus.hp = Mathf.RoundToInt(percent * bossHp);
					playerStatus.maxHp = bossHp;

					audioSource.clip = bossAttackClip;
					audioSource.Play();

					Debug.Log("Player1 has Boss Power");
					GainPowerEffect(playerStatus.playerCharacter, collision);
				}
			}
		}
		else if(collision.gameObject.CompareTag("PoisonPic"))
		{
			powerIndex = 0;
			switch(playerStatus.playerPower)
			{
				case PlayerPower_Level3.DragonPower1:
					break;
				case PlayerPower_Level3.DragonPower2:
					break;
				case PlayerPower_Level3.DragonPower3:
					break;
				case PlayerPower_Level3.KnightPower1:
					if(!playerStatus.overPoison)
					{
						if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
						}
						GameObject effect = Instantiate(poisonEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
						Destroy(effect, 1);
					}
					break;
				case PlayerPower_Level3.KnightPower2:
					if(!playerStatus.overPoison)
					{
						if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
						}
						GameObject effect = Instantiate(poisonEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
						Destroy(effect, 1);
					}
					break;
				case PlayerPower_Level3.MagicPower1:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level3.MagicPower2:
					if(!playerStatus.overPoison)
					{
						if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
						}
						GameObject effect = Instantiate(poisonEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
						Destroy(effect, 1);
					}
					break;
				case PlayerPower_Level3.MagicPower3:
					if(!playerStatus.overPoison)
					{
						if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
						}
						GameObject effect = Instantiate(poisonEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
						Destroy(effect, 1);
					}
					break;
				case PlayerPower_Level3.AssassinPower1:
					if(playerStatus.playerIdentity != PlayerStatusControl_Level3._instance.playerIdentity)
					{
						BecomeInvisible();
						visible = false;
					}
					break;
				case PlayerPower_Level3.AssassinPower2:
					if(playerStatus.playerIdentity != PlayerStatusControl_Level3._instance.playerIdentity )
					{
						BecomeInvisible();
						visible = false;
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					if(!playerStatus.overPoison)
					{
						if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
						}
						GameObject effect = Instantiate(poisonEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
						Destroy(effect, 1);
					}
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AreaPic"))
		{
			powerIndex = 0;
			switch(playerStatus.playerPower)
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
					playerStatus.attackAbility = playerStatus.originAttack + 2;
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
					if(!playerStatus.overArea)
					{
						if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
						}
						GameObject effect = Instantiate(areaEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
						Destroy(effect, 1);
					}
					break;
				case PlayerPower_Level3.AssassinPower2:
					if(!playerStatus.overArea)
					{
						if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
						}
						GameObject effect = Instantiate(areaEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
						Destroy(effect, 1);
					}
					break;
				case PlayerPower_Level3.BossPower:
					break;
				case PlayerPower_Level3.Default:
					if(!playerStatus.overArea)
					{
						if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
						}
						GameObject effect = Instantiate(areaEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
						Destroy(effect, 1);
					}
					break;
			}
		}
		else if(collision.gameObject.CompareTag("WindPic"))
		{
			GameObject effect = Instantiate(windEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
			Destroy(effect, 1);
			powerIndex = 0;
			switch(playerStatus.playerPower)
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
					if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
						{
							playerStatus.Damage(1);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-1);
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
				if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
				{
					playerStatus.Recover(2);
					TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,2);
				}
				audioSource.clip = recoverBuffClip;
				audioSource.Play();
				RecoverEffect(2);
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
				if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
				{
					playerStatus.Recover(4);
					TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,4);
				}
				audioSource.clip = recoverBuffClip;
				audioSource.Play();
				RecoverEffect(4);
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
				playerStatus.overPoison = true;
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
				playerStatus.overArea = true;
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
				playerStatus.damageReflect = true;
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
				playerStatus.playerSkill = PlayerSkill_Level3.DoubleDmgSkill;
				playerStatus.skillRemain = doubleDmgSkillLimit;
				GainSkillEffect(playerStatus.playerSkill);
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
				playerStatus.playerSkill = PlayerSkill_Level3.ShieldSkill;
				playerStatus.skillRemain = shieldSkillLimit;
				GainSkillEffect(playerStatus.playerSkill);
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
			if(powerIndex == gunSkillCount)
			{
				playerStatus.playerSkill = PlayerSkill_Level3.GunSkill;
				playerStatus.skillRemain = gunSkillLimit;
				GainSkillEffect(playerStatus.playerSkill);
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
			if(powerIndex == iceSkillCount)
			{
				playerStatus.playerSkill = PlayerSkill_Level3.IceSkill;
				playerStatus.skillRemain = iceSkillLimit;
				GainSkillEffect(playerStatus.playerSkill);
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player1 has ice skill");
			}
		}
		else if(collision.gameObject.CompareTag("ChangeHpPic"))
		{
			if(collision.gameObject.name == "ChangeHpPic" + powerIndex.ToString())
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == changeHpSkillCount)
			{
				if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
				{
					if(enemy1Status.hp >= enemy2Status.hp)
					{
						if(enemy1Status.hp > playerStatus.hp)
						{
							int diff = Mathf.RoundToInt(enemy1Status.hp - playerStatus.hp);
							playerStatus.Recover(diff);
							enemy1Status.Damage(diff);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,diff);
							TcpClient_All._instance.SendHpChange(enemy1Status.playerIdentity,-diff);
							Debug.Log("Player1 and Player2 exchange hp");
						}
					}
					else
					{
						if(enemy2Status.hp > playerStatus.hp)
						{
							int diff = Mathf.RoundToInt(enemy2Status.hp - playerStatus.hp);
							playerStatus.Recover(diff);
							enemy2Status.Damage(diff);
							TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,diff);
							TcpClient_All._instance.SendHpChange(enemy2Status.playerIdentity,-diff);
							
							Debug.Log("Player1 and Player3 exchange hp");
						}
					}
				}
				GameObject effect = Instantiate(changeHpEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
			}
		}
		else if(collision.gameObject.CompareTag("TeleportPic"))
		{
			if(collision.gameObject.name == "TeleportPic" + powerIndex.ToString() ||
			   collision.gameObject.name == "Teleport1Pic" + powerIndex.ToString() ||
			   collision.gameObject.name == "Teleport2Pic" + powerIndex.ToString() ||
			   collision.gameObject.name == "Teleport3Pic" + powerIndex.ToString() ||
			   collision.gameObject.name == "Teleport4Pic" + powerIndex.ToString() )
			{
				RedCrossEffect(collision);
				powerIndex++;
			}else
			{
				RemovePowerRedCrossOnMap();
				powerIndex = 0;
			}
			if(powerIndex == teleportCount)
			{
				if(playerStatus.playerIdentity == PlayerStatusControl_Level3._instance.playerIdentity)
				{
					if(collision.gameObject.name == "Teleport1Pic" + (powerIndex-1).ToString())
					{
						playerMove.endPosition = teleportPosition[0].transform.position;
						gameObject.transform.position = teleportPosition[0].transform.position;
					}
					else if(collision.gameObject.name == "Teleport2Pic" + (powerIndex-1).ToString())
					{
						playerMove.endPosition = teleportPosition[1].transform.position;
						gameObject.transform.position = teleportPosition[1].transform.position;
					}
					else if(collision.gameObject.name == "Teleport3Pic" + (powerIndex-1).ToString())
					{
						playerMove.endPosition = teleportPosition[2].transform.position;
						gameObject.transform.position = teleportPosition[2].transform.position;
					}
					else if(collision.gameObject.name == "Teleport4Pic" + (powerIndex-1).ToString())
					{
						playerMove.endPosition = teleportPosition[3].transform.position;
						gameObject.transform.position = teleportPosition[3].transform.position;
					}
				}
				TeleportEffect();
				audioSource.clip = enhanceBuffClip;
				audioSource.Play();
				Debug.Log("Player1 has use teleport");
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
		if(visible || ( PlayerStatusControl_Level3._instance.playerIdentity == enemy1Status.playerIdentity && 
													(enemy1Status.playerPower == PlayerPower_Level3.MagicPower1 ||
													enemy1Status.playerPower == PlayerPower_Level3.MagicPower2 ||
													enemy1Status.playerPower == PlayerPower_Level3.MagicPower3))
				   || ( PlayerStatusControl_Level3._instance.playerIdentity == enemy2Status.playerIdentity && 
													(enemy2Status.playerPower == PlayerPower_Level3.MagicPower1 ||
													enemy2Status.playerPower == PlayerPower_Level3.MagicPower2 ||
													enemy2Status.playerPower == PlayerPower_Level3.MagicPower3)) )
		{
			BecomeVisible();
		}
		if(!attackBuffOnArea)
		{
			playerStatus.attackAbility = playerStatus.originAttack;
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
		CheckPlayerTrigger(collision);
	}


	void TeleportEffect()
	{
		GameObject effect = Instantiate(teleportEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
		effect.transform.parent = gameObject.transform;
		Destroy(effect, 1);
	}

	void RecoverEffect(int value)
	{
		if(value == 2)
		{
			GameObject effect = Instantiate(recoverEffect2, gameObject.transform.position, Quaternion.identity) as GameObject;
			effect.transform.parent = gameObject.transform;
			Destroy(effect, 1);
		}
		else if(value == 4)
		{
			GameObject effect = Instantiate(recoverEffect4, gameObject.transform.position, Quaternion.identity) as GameObject;
			effect.transform.parent = gameObject.transform;
			Destroy(effect, 1);
		}
	}

	

	void TurnEffect(PlayerCharacter_Level3 playerCharacter, Collider2D collision)
	{
		GameObject effect;
		switch(playerCharacter)
		{
			case PlayerCharacter_Level3.Dragon:
				effect = Instantiate(dragonTurnEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
				Destroy(effect, 1);
				break;
			case PlayerCharacter_Level3.Knight:
				effect = Instantiate(knightTurnEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
				Destroy(effect, 1);
				break;
			case PlayerCharacter_Level3.Magic:
				effect = Instantiate(magicTurnEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
				Destroy(effect, 1);
				break;
			case PlayerCharacter_Level3.Assassin:
				effect = Instantiate(assassinTurnEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
				Destroy(effect, 1);
				break;
			case PlayerCharacter_Level3.Boss:
				effect = Instantiate(bossTurnEffect, collision.gameObject.transform.position, Quaternion.identity) as GameObject;
				Destroy(effect, 1);
				break;
		}
		
	}

	void GainPowerEffect(PlayerCharacter_Level3 playerCharacter, Collider2D collision)
	{
		GameObject effect;
		switch(playerCharacter)
		{
			case PlayerCharacter_Level3.Dragon:
				effect = Instantiate(dragonGainPowerEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				break;
			case PlayerCharacter_Level3.Knight:
				effect = Instantiate(knightGainPowerEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				break;
			case PlayerCharacter_Level3.Magic:
				effect = Instantiate(magicGainPowerEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				break;
			case PlayerCharacter_Level3.Assassin:
				effect = Instantiate(assassinGainPowerEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				break;
			case PlayerCharacter_Level3.Boss:
				break;
		}
		
	}

	void GainSkillEffect(PlayerSkill_Level3 playerCharacter)
	{
		GameObject effect;
		switch(playerCharacter)
		{
			case PlayerSkill_Level3.ShieldSkill:
				effect = Instantiate(gainShieldSkillEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				break;
			case PlayerSkill_Level3.GunSkill:
				effect = Instantiate(gainGunSkillEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				playerStatus.carrySkillEffect = Instantiate(carryGunSkillEffect,
															new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-0.5f,gameObject.transform.position.z), 
															Quaternion.identity) as GameObject;
				playerStatus.carrySkillEffect.transform.parent = gameObject.transform;
				break;
			case PlayerSkill_Level3.IceSkill:
				effect = Instantiate(gainIceSkillEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				playerStatus.carrySkillEffect = Instantiate(carryIceSkillEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				playerStatus.carrySkillEffect.transform.parent = gameObject.transform;
				break;
			case PlayerSkill_Level3.DoubleDmgSkill:
				effect = Instantiate(gainDoubleDmgSkillEffect, gameObject.transform.position, Quaternion.identity) as GameObject;
				effect.transform.parent = gameObject.transform;
				Destroy(effect, 1);
				playerStatus.carrySkillEffect = Instantiate(carryDoubleDmgSkillEffect, 
															new Vector3(gameObject.transform.position.x,gameObject.transform.position.y-0.5f,gameObject.transform.position.z), 
															Quaternion.identity) as GameObject;
				playerStatus.carrySkillEffect.transform.parent = gameObject.transform;
				break;
		}
		
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
