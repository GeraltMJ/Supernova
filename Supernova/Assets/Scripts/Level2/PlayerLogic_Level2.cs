using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic_Level2 : MonoBehaviour {

	private int dragonPowerCount = 10;
	private int knightPowerCount = 10;
	private int magicPowerCount = 8;

	private int assassinPowerCount = 8;

	private int bossPowerCount = 31;


	private int powerIndex = 0;
	private int addOneCount = 3;
	private int addTwoCount = 6;
	private int overPoisonCount = 5;
	private int overAreaCount = 5;
	private int attackBuffCount = 6;
	private int damageReflectCount = 6;

	public Sprite poisonReplace;
	public Sprite areaReplace;
	

	public GameObject smokeEffect;
	public GameObject redCross;
	private SpriteRenderer spriteRender;
	public Sprite[] sprite;
	public RuntimeAnimatorController dragonController, knightController, magicController, assassinController, bossController;
	private Animator animator;
	private int deleteIndex = 0;

	private GameObject[] powerRedCrossToDelete;
	private GameObject characterRedCross;

	void Awake() {
		spriteRender = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();
	}

	void Start()
	{
		powerRedCrossToDelete = new GameObject[50];
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

	void BecomeInvisible()
	{

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
		if (collision.gameObject.CompareTag("DragonPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Dragon 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Dragon)
			{	
				Player1Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Dragon;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = dragonController;
				Debug.Log("Player1 turn into Dragon");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("KnightPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Knight 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Knight)
			{	
				Player1Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Knight;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = knightController;
				Debug.Log("Player1 turn into Knight");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("MagicPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Magic 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Magic)
			{	
				Player1Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Magic;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[2];
				animator.runtimeAnimatorController = magicController;
				Debug.Log("Player1 turn into Magic");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("AssassinPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Assassin 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Assassin)
			{	
				Player1Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Assassin;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[3];
				animator.runtimeAnimatorController = assassinController;
				Debug.Log("Player1 turn into Assassin");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("BossPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Boss 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Boss)
			{	
				Player1Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Boss;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[4];
				animator.runtimeAnimatorController = bossController;
				Debug.Log("Player1 turn into Boss");
			}
			powerIndex = 0;
		}
		else if(collision.gameObject.CompareTag("DragonPowerPic"))
		{
			if(Player1Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Dragon)
			{
				if(collision.gameObject.name == "DragonPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == dragonPowerCount)
				{
					Player1Status_Level2._instance.playerPower = PlayerPower_Level2.DragonPower;
					Debug.Log("Player1 has Dragon Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("KnightPowerPic"))
		{
			if(Player1Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Knight)
			{
				if(collision.gameObject.name == "KnightPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == knightPowerCount)
				{
					Player1Status_Level2._instance.playerPower = PlayerPower_Level2.KnightPower;
					Debug.Log("Player1 has Knight Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("MagicPowerPic"))
		{
			if(Player1Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Magic)
			{
				if(collision.gameObject.name == "MagicPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == magicPowerCount)
				{
					Player1Status_Level2._instance.playerPower = PlayerPower_Level2.MagicPower;
					Debug.Log("Player1 has Magic Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("AssassinPowerPic"))
		{
			if(Player1Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Assassin)
			{
				if(collision.gameObject.name == "AssassinPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == assassinPowerCount)
				{
					Player1Status_Level2._instance.playerPower = PlayerPower_Level2.AssassinPower;
					Debug.Log("Player1 has Assassin Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("BossPowerPic"))
		{
			if(Player1Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Boss)
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
					Player1Status_Level2._instance.playerPower = PlayerPower_Level2.BossPower;
					Debug.Log("Player1 has Boss Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("PoisonPic"))
		{
			switch(Player1Status_Level2._instance.playerPower)
			{
				case PlayerPower_Level2.DragonPower:
					break;
				case PlayerPower_Level2.KnightPower:
					Player1Status_Level2._instance.Damage(1);
					break;
				case PlayerPower_Level2.MagicPower:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level2.AssassinPower:
					BecomeInvisible();
					break;
				case PlayerPower_Level2.BossPower:
					break;
				case PlayerPower_Level2.Default:
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AreaPic"))
		{
			switch(Player1Status_Level2._instance.playerPower)
			{
				case PlayerPower_Level2.DragonPower:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level2.KnightPower:
					Player1Status_Level2._instance.attackAbility += 1;
					break;
				case PlayerPower_Level2.MagicPower:
					break;
				case PlayerPower_Level2.AssassinPower:
					Player1Status_Level2._instance.Damage(1);
					break;
				case PlayerPower_Level2.BossPower:
					break;
				case PlayerPower_Level2.Default:
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
				Player1Status_Level2._instance.Recover(1f);
				RecoverEffect();
				Debug.Log("Player1 recorve 1 hp");
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
				Player1Status_Level2._instance.Recover(2f);
				RecoverEffect();
				Debug.Log("Player1 recorve 2 hp");
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
				Player1Status_Level2._instance.overPoison = true;
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
				Player1Status_Level2._instance.overArea = true;
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
				Player1Status_Level2._instance.damageReflect = true;
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
				Player1Status_Level2._instance.attackBuff = true;
				Debug.Log("Player1 is attack buff");
			}
		}
		else
		{
			RemovePowerRedCrossOnMap();
			powerIndex = 0;
		}
	}

	void CheckPlayer2Trigger(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("DragonPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Dragon 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Dragon)
			{	
				Player2Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Dragon;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = dragonController;
				Debug.Log("Player2 turn into Dragon");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("KnightPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Knight 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Knight)
			{	
				Player2Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Knight;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = knightController;
				Debug.Log("Player2 turn into Knight");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("MagicPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Magic 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Magic)
			{	
				Player2Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Magic;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[2];
				animator.runtimeAnimatorController = magicController;
				Debug.Log("Player2 turn into Magic");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("AssassinPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Assassin 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Assassin)
			{	
				Player2Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Assassin;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[3];
				animator.runtimeAnimatorController = assassinController;
				Debug.Log("Player2 turn into Assassin");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("BossPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Boss 
				&& Player1Status_Level2._instance.playerCharacter != PlayerCharacter_Level2.Boss)
			{	
				Player2Status_Level2._instance.playerCharacter = PlayerCharacter_Level2.Boss;
				RemoveCharacterRedCross();
				TurnEffect();
				CharacterRedCrossEffect(collision);
				spriteRender.sprite = sprite[4];
				animator.runtimeAnimatorController = bossController;
				Debug.Log("Player2 turn into Boss");
			}
			powerIndex = 0;
		}
		else if(collision.gameObject.CompareTag("DragonPowerPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Dragon)
			{
				if(collision.gameObject.name == "DragonPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == dragonPowerCount)
				{
					Player2Status_Level2._instance.playerPower = PlayerPower_Level2.DragonPower;
					Debug.Log("Player2 has Dragon Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("KnightPowerPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Knight)
			{
				if(collision.gameObject.name == "KnightPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == knightPowerCount)
				{
					Player2Status_Level2._instance.playerPower = PlayerPower_Level2.KnightPower;
					Debug.Log("Player2 has Knight Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("MagicPowerPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Magic)
			{
				if(collision.gameObject.name == "MagicPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == magicPowerCount)
				{
					Player2Status_Level2._instance.playerPower = PlayerPower_Level2.MagicPower;
					Debug.Log("Player2 has Magic Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("AssassinPowerPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Assassin)
			{
				if(collision.gameObject.name == "AssassinPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					RemovePowerRedCrossOnMap();
					powerIndex = 0;
				}
				if(powerIndex == assassinPowerCount)
				{
					Player2Status_Level2._instance.playerPower = PlayerPower_Level2.AssassinPower;
					Debug.Log("Player2 has Assassin Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("BossPowerPic"))
		{
			if(Player2Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Boss)
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
					Player2Status_Level2._instance.playerPower = PlayerPower_Level2.BossPower;
					Debug.Log("Player2 has Boss Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("PoisonPic"))
		{
			switch(Player2Status_Level2._instance.playerPower)
			{
				case PlayerPower_Level2.DragonPower:
					break;
				case PlayerPower_Level2.KnightPower:
					Player2Status_Level2._instance.Damage(1);
					break;
				case PlayerPower_Level2.MagicPower:
					ChangeToArea(collision);
					break;
				case PlayerPower_Level2.AssassinPower:
					BecomeInvisible();
					break;
				case PlayerPower_Level2.BossPower:
					break;
				case PlayerPower_Level2.Default:
					break;
			}
		}
		else if(collision.gameObject.CompareTag("AreaPic"))
		{
			switch(Player2Status_Level2._instance.playerPower)
			{
				case PlayerPower_Level2.DragonPower:
					ChangeToPoison(collision);
					break;
				case PlayerPower_Level2.KnightPower:
					Player2Status_Level2._instance.attackAbility += 1;
					break;
				case PlayerPower_Level2.MagicPower:
					break;
				case PlayerPower_Level2.AssassinPower:
					Player2Status_Level2._instance.Damage(1);
					break;
				case PlayerPower_Level2.BossPower:
					break;
				case PlayerPower_Level2.Default:
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
				Player2Status_Level2._instance.Recover(1f);
				RecoverEffect();
				Debug.Log("Player2 recorve 1 hp");
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
				Player2Status_Level2._instance.Recover(2f);
				RecoverEffect();
				Debug.Log("Player2 recorve 2 hp");
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
				Player2Status_Level2._instance.overPoison = true;
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
				Player2Status_Level2._instance.overArea = true;
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
				Player2Status_Level2._instance.damageReflect = true;
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
				Player2Status_Level2._instance.attackBuff = true;
				Debug.Log("Player2 is attack buff");
			}
		}
		
		else
		{
			RemovePowerRedCrossOnMap();
			powerIndex = 0;
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
		}else if(gameObject.CompareTag("Player2"))
		{
			CheckPlayer2Trigger(collision);
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
