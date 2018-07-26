using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic_Level2 : MonoBehaviour {

	private bool isInDragonPic = false;
	private bool isInMagicPic = false;
	private bool isInKnightPic = false;
	private bool isInAssassinPic = false;
	private bool isInBossPic = false;

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

	private int dragonPowerIndex = 0;
	private int magicPowerIndex = 0;
	private int knightPowerIndex = 0;
	private int assassinPowerIndex = 0;
	private int bossPowerIndex = 0;

	private bool dragonPowerReady = false;
	private bool magicPowerReady = false;
	private bool knightPowerReady = false;
	private bool assassinPowerReady = false;
	private bool bossPowerReady = false;

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

	public KeyCode turnKey;

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
			for(int i = 0; i < dragonPowerCount; i++)
			{
				if(collision.gameObject.name == "DragonPowerPic" + i.ToString())
				{
					isDragonPowerPic[i] = true;
				}
				else
				{
					isDragonPowerPic[i] = false;
				}
			}
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

	void Update()
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

	void Player1TurnCheck()
	{	
		if (Input.GetKeyDown(turnKey) || ETCInput.GetButton("TurnButton"))
		{
			switch(picType)
			{
				case isInPicType.isInDragonPic:
				break;
				case isInPicType.isInMagicPic:
				break;
				case isInPicType.isInAssassinPic:
				break;
				case isInPicType.isInKnightPic:
				break;
				case isInPicType.isInBossPic:
				break;
				case isInPicType.isInDragonPowerPic:
				break;
				case isInPicType.isInMagicPowerPic:
				break;
				case isInPicType.isInAssassinPowerPic:
				break;
				case isInPicType.isInKnightPowerPic:
				break;
				case isInPicType.isInBossPowerPic:
				break;
				case isInPicType.defaultType:
				break;
			}
			if(isInDragonPic == true && Player2Status_Level2._instance.isDragon == false)
			{
				Player1Status_Level2._instance.isDragon = true;
				Player1Status_Level2._instance.isMagic = false;
				Player1Status_Level2._instance.isKnight = false;
				Player1Status_Level2._instance.isAssassin = false;
				Player1Status_Level2._instance.isBoss = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[0];
				//animator.runtimeAnimatorController = catController;
				Debug.Log("Player1 turn into Dragon");
			} 
			else if(isInMagicPic == true && Player2Status_Level2._instance.isMagic == false)
			{
				Player1Status_Level2._instance.isDragon = false;
				Player1Status_Level2._instance.isMagic = true;
				Player1Status_Level2._instance.isKnight = false;
				Player1Status_Level2._instance.isAssassin = false;
				Player1Status_Level2._instance.isBoss = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[1];
				//animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into Magic");
			}
			else if(isInKnightPic == true && Player2Status_Level2._instance.isKnight == false)
			{
				Player1Status_Level2._instance.isDragon = false;
				Player1Status_Level2._instance.isMagic = false;
				Player1Status_Level2._instance.isKnight = true;
				Player1Status_Level2._instance.isAssassin = false;
				Player1Status_Level2._instance.isBoss = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[1];
				//animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into Knight");
			}
			else if(isInAssassinPic == true && Player2Status_Level2._instance.isAssassin == false)
			{
				Player1Status_Level2._instance.isDragon = false;
				Player1Status_Level2._instance.isMagic = false;
				Player1Status_Level2._instance.isKnight = false;
				Player1Status_Level2._instance.isAssassin = true;
				Player1Status_Level2._instance.isBoss = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[1];
				//animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into Assassin");
			}
			else if(isInBossPic == true && Player2Status_Level2._instance.isBoss == false)
			{
				Player1Status_Level2._instance.isDragon = false;
				Player1Status_Level2._instance.isMagic = false;
				Player1Status_Level2._instance.isKnight = false;
				Player1Status_Level2._instance.isAssassin = false;
				Player1Status_Level2._instance.isBoss = true;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[1];
				//animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into Boss");
			}
			
		}
	}

	void Player2TurnCheck()
	{	
		if (Input.GetKeyDown(turnKey) || ETCInput.GetButton("TurnButton"))
		{
			if(isInDragonPic == true && Player1Status_Level2._instance.isDragon == false)
			{
				Player2Status_Level2._instance.isDragon = true;
				Player2Status_Level2._instance.isMagic = false;
				Player2Status_Level2._instance.isKnight = false;
				Player2Status_Level2._instance.isAssassin = false;
				Player2Status_Level2._instance.isBoss = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[0];
				//animator.runtimeAnimatorController = catController;
				Debug.Log("Player2 turn into Dragon");
			} 
			else if(isInMagicPic == true && Player1Status_Level2._instance.isMagic == false)
			{
				Player2Status_Level2._instance.isDragon = false;
				Player2Status_Level2._instance.isMagic = true;
				Player2Status_Level2._instance.isKnight = false;
				Player2Status_Level2._instance.isAssassin = false;
				Player2Status_Level2._instance.isBoss = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[1];
				//animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player2 turn into Magic");
			}
			else if(isInKnightPic == true && Player1Status_Level2._instance.isKnight == false)
			{
				Player2Status_Level2._instance.isDragon = false;
				Player2Status_Level2._instance.isMagic = false;
				Player2Status_Level2._instance.isKnight = true;
				Player2Status_Level2._instance.isAssassin = false;
				Player2Status_Level2._instance.isBoss = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[1];
				//animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player2 turn into Knight");
			}
			else if(isInAssassinPic == true && Player1Status_Level2._instance.isAssassin == false)
			{
				Player2Status_Level2._instance.isDragon = false;
				Player2Status_Level2._instance.isMagic = false;
				Player2Status_Level2._instance.isKnight = false;
				Player2Status_Level2._instance.isAssassin = true;
				Player2Status_Level2._instance.isBoss = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[1];
				//animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into Assassin");
			}
			else if(isInBossPic == true && Player1Status_Level2._instance.isBoss == false)
			{
				Player2Status_Level2._instance.isDragon = false;
				Player2Status_Level2._instance.isMagic = false;
				Player2Status_Level2._instance.isKnight = false;
				Player2Status_Level2._instance.isAssassin = false;
				Player2Status_Level2._instance.isBoss = true;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				//spriteRender.sprite = sprite[1];
				//animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into Boss");
			}
		}
	}
}
