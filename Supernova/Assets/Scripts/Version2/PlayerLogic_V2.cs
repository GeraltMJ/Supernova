using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic_V2 : MonoBehaviour {

	public int catPowerCount;
	public int mousePowerCount;

	private int powerIndex = 0;
	

	public GameObject smokeEffect;
	public GameObject redCross;
	public SpriteRenderer spriteRender;
	public Sprite[] sprite;
	public RuntimeAnimatorController catController, mouseController;
	private Animator animator;

	void Awake() {
		spriteRender = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();
	}

	void CheckPlayer1Trigger(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("CatPic"))
		{
			if(Player2Status_V2._instance.playerCharacter != PlayerCharacter_V2.Cat 
				&& Player1Status_V2._instance.playerCharacter != PlayerCharacter_V2.Cat)
			{	
				Player1Status_V2._instance.playerCharacter = PlayerCharacter_V2.Cat;
				TurnEffect();
				RedCrossEffect(collision);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = catController;
				Debug.Log("Player1 turn into Cat");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("MousePic"))
		{
			if(Player2Status_V2._instance.playerCharacter != PlayerCharacter_V2.Mouse 
				&& Player1Status_V2._instance.playerCharacter != PlayerCharacter_V2.Mouse)
			{	
				Player1Status_V2._instance.playerCharacter = PlayerCharacter_V2.Mouse;
				TurnEffect();
				RedCrossEffect(collision);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into Mouse");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("WinPic"))
		{
			PlayerStatusControl._instance.player1Win = true;
			powerIndex = 0;
			Debug.Log("Player1 Win");
		}
		else if(collision.gameObject.CompareTag("CatPowerPic"))
		{
			if(Player1Status_V2._instance.playerCharacter == PlayerCharacter_V2.Cat)
			{
				if(collision.gameObject.name == "CatPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					powerIndex = 0;
				}
				if(powerIndex == catPowerCount)
				{
					Player1Status_V2._instance.playerPower = PlayerPower_V2.CatPower;
					Debug.Log("Player1 has Cat Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("MousePowerPic"))
		{
			if(Player1Status_V2._instance.playerCharacter == PlayerCharacter_V2.Mouse)
			{
				if(collision.gameObject.name == "MousePowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					powerIndex = 0;
				}
				if(powerIndex == mousePowerCount)
				{
					Player1Status_V2._instance.playerPower = PlayerPower_V2.MousePower;
					Debug.Log("Player1 has Mouse Power");
					TurnEffect();
				}
			}
		}
	}

	void CheckPlayer2Trigger(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("CatPic"))
		{
			Debug.Log("Catpic");
			if(Player2Status_V2._instance.playerCharacter != PlayerCharacter_V2.Cat 
				&& Player1Status_V2._instance.playerCharacter != PlayerCharacter_V2.Cat)
			{	
				Debug.Log("Turn");
				Player2Status_V2._instance.playerCharacter = PlayerCharacter_V2.Cat;
				TurnEffect();
				RedCrossEffect(collision);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = catController;
				Debug.Log("Player2 turn into Cat");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("MousePic"))
		{
			if(Player2Status_V2._instance.playerCharacter != PlayerCharacter_V2.Mouse 
				&& Player1Status_V2._instance.playerCharacter != PlayerCharacter_V2.Mouse)
			{	
				Player2Status_V2._instance.playerCharacter = PlayerCharacter_V2.Mouse;
				TurnEffect();
				RedCrossEffect(collision);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player2 turn into Mouse");
			}
			powerIndex = 0;
		}
		else if (collision.gameObject.CompareTag("WinPic"))
		{
			PlayerStatusControl._instance.player2Win = true;
			powerIndex = 0;
			Debug.Log("Player2 Win");
		}
		else if(collision.gameObject.CompareTag("CatPowerPic"))
		{
			if(Player2Status_V2._instance.playerCharacter == PlayerCharacter_V2.Cat)
			{
				if(collision.gameObject.name == "CatPowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					powerIndex = 0;
				}
				if(powerIndex == catPowerCount)
				{
					Player2Status_V2._instance.playerPower = PlayerPower_V2.CatPower;
					Debug.Log("Player2 has Cat Power");
					TurnEffect();
				}
			}
		}
		else if(collision.gameObject.CompareTag("MousePowerPic"))
		{
			if(Player2Status_V2._instance.playerCharacter == PlayerCharacter_V2.Mouse)
			{
				if(collision.gameObject.name == "MousePowerPic" + powerIndex.ToString())
				{
					RedCrossEffect(collision);
					powerIndex++;
				}else
				{
					powerIndex = 0;
				}
				if(powerIndex == mousePowerCount)
				{
					Player2Status_V2._instance.playerPower = PlayerPower_V2.MousePower;
					Debug.Log("Player2 has Mouse Power");
					TurnEffect();
				}
			}
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

	
	void OnTriggerExit2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("HolePic"))
		{
			if(gameObject.CompareTag("Player1"))
			{
				if(Player1Status_V2._instance.playerPower == PlayerPower_V2.MousePower)
				{
					other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
				}

			}
			else if(gameObject.CompareTag("Player2"))
			{
				if(Player1Status_V2._instance.playerPower == PlayerPower_V2.MousePower)
				{
					other.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
				}
			}
		}	
	}

	void TurnEffect()
	{
		GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
		Destroy(smoke, 1);
	}

	void RedCrossEffect(Collider2D collision)
	{
		Instantiate(redCross, collision.transform.position, Quaternion.identity);
	}
}
