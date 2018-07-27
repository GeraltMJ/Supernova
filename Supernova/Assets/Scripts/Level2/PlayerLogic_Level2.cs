using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic_Level2 : MonoBehaviour {

	private bool[] isDragonPowerPic;
	private bool[] isMagicPowerPic;
	private bool[] isKnightPowerPic;
	private bool[] isAssassinPowerPic;
	private bool[] isBossPowerPic;

	private bool[] dragonPower;
	private bool[] magicPower;
	private bool[] knightPower;
	private bool[] assassinPower;
	private bool[] bossPower;

	public int dragonPowerCount;
	public int magicPowerCount;
	public int knightPowerCount;
	public int assassinPowerCount;
	public int bossPowerCount;

	private int powerIndex = 0;
	private int checkindex = 0;

	private enum isInPicType{
		isInDragonPic,
		isInMagicPic,
		isInKnightPic,
		isInAssassinPic,
		isInBossPic,
		isInDragonPowerPic,
		isInMagicPowerPic,
		isInKnightPowerPic,
		isInAssassinPowerPic,
		isInBossPowerPic,
		defaultType
	}

	private isInPicType picType;

	public GameObject smokeEffect;
	public GameObject redCross;
	public SpriteRenderer spriteRender;
	public Sprite[] sprite;
	public RuntimeAnimatorController catController, mouseController;
	private Animator animator;


	void Awake() {
		spriteRender = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();

		dragonPower = new bool[dragonPowerCount];
		magicPower = new bool[magicPowerCount];
		knightPower = new bool[knightPowerCount];
		assassinPower = new bool[assassinPowerCount];
		bossPower = new bool[bossPowerCount];

		isDragonPowerPic = new bool[dragonPowerCount];
		isMagicPowerPic = new bool[magicPowerCount];
		isKnightPowerPic = new bool[knightPowerCount];
		isAssassinPowerPic = new bool[assassinPowerCount];
		isBossPowerPic = new bool[bossPowerCount];
		picType = isInPicType.defaultType;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision == null)
		{
			return;
		}
		if (collision.gameObject.CompareTag("DragonPic"))
		{
			picType = isInPicType.isInDragonPic;
		}
		else if (collision.gameObject.CompareTag("MagicPic"))
		{
			picType = isInPicType.isInMagicPic;
		}
		else if (collision.gameObject.CompareTag("AssassinPic"))
		{
			picType = isInPicType.isInAssassinPic;
		}
		else if (collision.gameObject.CompareTag("KnightPic"))
		{
			picType = isInPicType.isInKnightPic;
		}
		else if(collision.gameObject.CompareTag("DragonPowerPic"))
		{
			picType = isInPicType.isInDragonPowerPic;

			if(collision.gameObject.name == "DragonPowerPic" + checkindex.ToString())
			{
				checkindex++;
			}
			else
			{
				checkindex = 0;
			}
			if(checkindex == dragonPowerCount)
			{
				Debug.Log("Player1 has Dragon Power");
			}
			/* 

			for(int i = 0; i < dragonPowerCount; i++)
			{
				if(collision.gameObject.name == "DragonPowerPic" + i.ToString())
				{
					isDragonPowerPic[i] = true;
					Debug.Log("DragonPowerPic" + i.ToString());
					Debug.Log(i + "true");
				}
				else
				{
					isDragonPowerPic[i] = false;
				}
			}
			*/
		}
		else if(collision.gameObject.CompareTag("MagicPowerPic"))
		{
			picType = isInPicType.isInMagicPowerPic;
			for(int i = 0; i < magicPowerCount; i++)
			{
				if(collision.gameObject.name == "MagicPowerPic" + i.ToString())
				{
					isMagicPowerPic[i] = true;
				}
				else
				{
					isMagicPowerPic[i] = false;
				}
			}
		}
		else if(collision.gameObject.CompareTag("AssassinPowerPic"))
		{
			picType = isInPicType.isInAssassinPowerPic;
			for(int i = 0; i < assassinPowerCount; i++)
			{
				if(collision.gameObject.name == "AssassinPowerPic" + i.ToString())
				{
					isAssassinPowerPic[i] = true;
				}
				else
				{
					isAssassinPowerPic[i] = false;
				}
			}
		}
		else if(collision.gameObject.CompareTag("KnightPowerPic"))
		{
			picType = isInPicType.isInKnightPowerPic;
			for(int i = 0; i < knightPowerCount; i++)
			{
				if(collision.gameObject.name == "KnightPowerPic" + i.ToString())
				{
					isKnightPowerPic[i] = true;
				}
				else
				{
					isKnightPowerPic[i] = false;
				}
			}
		}
		else if(collision.gameObject.CompareTag("BossPowerPic"))
		{
			picType = isInPicType.isInBossPowerPic;
			for(int i = 0; i < bossPowerCount; i++)
			{
				if(collision.gameObject.name == "BossPowerPic" + i.ToString())
				{
					isBossPowerPic[i] = true;
				}
				else
				{
					isBossPowerPic[i] = false;
				}
			}
		}
		else
		{
			picType = isInPicType.defaultType;
		}
		
	}

	void FixedUpdate()
	{
		if(gameObject.CompareTag("Player1"))
		{
			Player1TurnCheck();
		}
		else if(gameObject.CompareTag("Player2"))
		{
			Player2TurnCheck();
		}
	}

	void TurnEffect()
	{
		GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
		Destroy(smoke, 1);
		//Instantiate(redCross, transform.position, Quaternion.identity);
		Instantiate(redCross, new Vector3(-11,6,0), Quaternion.identity);
	}

	void Player1TurnCheck()
	{	
		switch(picType)
		{
			case isInPicType.isInDragonPic:
				if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter.Dragon && 
					Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Dragon)
				{
					Player1Status_Level2._instance.playerCharacter = PlayerCharacter.Dragon;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player1 turn into Dragon");
				}
				break;
			case isInPicType.isInMagicPic:
				if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter.Magic &&
					Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Magic)
				{
					Player1Status_Level2._instance.playerCharacter = PlayerCharacter.Magic;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player1 turn into Magic");
				}
				break;
			case isInPicType.isInAssassinPic:
				if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter.Assassin &&
					Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Assassin)
				{
					Player1Status_Level2._instance.playerCharacter = PlayerCharacter.Assassin;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player1 turn into Assassin");
				}
				break;
			case isInPicType.isInKnightPic:
				if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter.Knight &&
					Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Knight)
				{
					Player1Status_Level2._instance.playerCharacter = PlayerCharacter.Knight;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player1 turn into Knight");
				}
				break;
			case isInPicType.isInBossPic:
				if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter.Boss &&
					Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Boss)
				{
					Player1Status_Level2._instance.playerCharacter = PlayerCharacter.Boss;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player1 turn into Boss");
				}
				break;
			case isInPicType.isInDragonPowerPic:
				Debug.Log(powerIndex + "h");
				if(isDragonPowerPic[powerIndex])
				{	
					Debug.Log(powerIndex);
					powerIndex++;
					if(powerIndex == dragonPowerCount)
					{
						Player1Status_Level2._instance.playerPower = PlayerPower.DragonPower;
						Debug.Log("Player1 has Dragon Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.isInMagicPowerPic:
				if(isMagicPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == magicPowerCount)
					{
						Player1Status_Level2._instance.playerPower = PlayerPower.MagicPower;
						Debug.Log("Player1 has Magic Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.isInAssassinPowerPic:
				if(isAssassinPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == assassinPowerCount)
					{
						Player1Status_Level2._instance.playerPower = PlayerPower.AssassinPower;
						Debug.Log("Player1 has Assassin Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.isInKnightPowerPic:
				if(isKnightPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == knightPowerCount)
					{
						Player1Status_Level2._instance.playerPower = PlayerPower.KnightPower;
						Debug.Log("Player1 has Knight Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.isInBossPowerPic:
				if(isBossPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == bossPowerCount)
					{
						Player1Status_Level2._instance.playerPower = PlayerPower.BossPower;
						Debug.Log("Player1 has Boss Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.defaultType:
			break;
		}
			
	}

	void Player2TurnCheck()
	{	
		switch(picType)
		{
			case isInPicType.isInDragonPic:
				if(Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Dragon)
				{
					Player2Status_Level2._instance.playerCharacter = PlayerCharacter.Dragon;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player2 turn into Dragon");
				}
				break;
			case isInPicType.isInMagicPic:
				if(Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Magic)
				{
					Player2Status_Level2._instance.playerCharacter = PlayerCharacter.Magic;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player2 turn into Magic");
				}
				break;
			case isInPicType.isInAssassinPic:
				if(Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Assassin)
				{
					Player2Status_Level2._instance.playerCharacter = PlayerCharacter.Assassin;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player2 turn into Assassin");
				}
				break;
			case isInPicType.isInKnightPic:
				if(Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Knight)
				{
					Player2Status_Level2._instance.playerCharacter = PlayerCharacter.Knight;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player2 turn into Knight");
				}
				break;
			case isInPicType.isInBossPic:
				if(Player1Status_Level2._instance.playerCharacter != PlayerCharacter.Boss)
				{
					Player2Status_Level2._instance.playerCharacter = PlayerCharacter.Boss;
					TurnEffect();
					//spriteRender.sprite = sprite[0];
					//animator.runtimeAnimatorController = catController;
					Debug.Log("Player2 turn into Boss");
				}
				break;
			case isInPicType.isInDragonPowerPic:
				if(isDragonPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == dragonPowerCount)
					{
						Player2Status_Level2._instance.playerPower = PlayerPower.DragonPower;
						Debug.Log("Player2 has Dragon Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.isInMagicPowerPic:
				if(isMagicPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == magicPowerCount)
					{
						Player2Status_Level2._instance.playerPower = PlayerPower.MagicPower;
						Debug.Log("Player2 has Magic Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.isInAssassinPowerPic:
				if(isAssassinPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == assassinPowerCount)
					{
						Player2Status_Level2._instance.playerPower = PlayerPower.AssassinPower;
						Debug.Log("Player2 has Assassin Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.isInKnightPowerPic:
				if(isKnightPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == knightPowerCount)
					{
						Player2Status_Level2._instance.playerPower = PlayerPower.KnightPower;
						Debug.Log("Player2 has Knight Power");
						
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.isInBossPowerPic:
				if(isBossPowerPic[powerIndex])
				{
					powerIndex++;
					if(powerIndex == bossPowerCount)
					{
						Player2Status_Level2._instance.playerPower = PlayerPower.BossPower;
						Debug.Log("Player2 has Boss Power");
					}
				}else
				{
					powerIndex = 0;
				}
				break;
			case isInPicType.defaultType:
				powerIndex = 0;
				break;
		}
			
	}
}
