using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic_V2 : MonoBehaviour {

	public bool isInCatPic = false;
	public bool isInMousePic = false;
	public bool isInWinPic = false;

	private bool isInCatAbilityPic1 = false;
	private bool isInCatAbilityPic2 = false;
	private bool isInMouseAbilityPic1 = false;
	private bool isInMouseAbilityPic2 = false;

	private bool hasCatAbility1 = false;
	private bool hasCatAbility2 = false;
	private bool hasMouseAbility1 = false;
	private bool hasMouseAbility2 = false;
	

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
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision == null)
		{
			return;
		}
		if (collision.gameObject.CompareTag("CatPic"))
		{
			isInCatPic = true;
		}
		if (collision.gameObject.CompareTag("MousePic"))
		{
			isInMousePic = true;
		}
		if (collision.gameObject.CompareTag("WinPic"))
		{
			isInWinPic = true;
		}
		if (collision.gameObject.CompareTag("CatAbility1"))
		{
			isInCatAbilityPic1 = true;
		}
		if (collision.gameObject.CompareTag("CatAbility2"))
		{
			isInCatAbilityPic2 = true;
		}
		if (collision.gameObject.CompareTag("MouseAbility1"))
		{
			isInMouseAbilityPic1 = true;
		}
		if (collision.gameObject.CompareTag("MouseAbility2"))
		{
			isInMouseAbilityPic2 = true;
		}
	}


	void OnTriggerExit2D(Collider2D collision)
	{	
		if(collision == null)
		{
			return;
		}
		if (collision.gameObject.CompareTag("CatPic"))
		{
			isInCatPic = false;
		}
		if (collision.gameObject.CompareTag("MousePic"))
		{
			isInMousePic = false;
		}
		if (collision.gameObject.CompareTag("WinPic"))
		{
			isInWinPic = false;
		}
		if (collision.gameObject.CompareTag("CatAbility1"))
		{
			isInCatAbilityPic1 = false;
		}
		if (collision.gameObject.CompareTag("CatAbility2"))
		{
			isInCatAbilityPic2 = false;
		}
		if (collision.gameObject.CompareTag("MouseAbility1"))
		{
			isInMouseAbilityPic1 = false;
		}
		if (collision.gameObject.CompareTag("MouseAbility2"))
		{
			isInMouseAbilityPic2 = false;
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
			if(isInCatPic == true && Player2Status_V2._instance.isCat == false)
			{
				Player1Status_V2._instance.isCat = true;
				Player1Status_V2._instance.hp = 5.0f;
				Player1Status_V2._instance.isMouse = false;
				Player1Status_V2._instance.hasMouseAbility = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = catController;
				Debug.Log("Player1 turn into cat");
			} 
			else if(isInMousePic == true && Player2Status_V2._instance.isMouse == false)
			{
				Player1Status_V2._instance.isMouse = true;
				Player1Status_V2._instance.hp = 1.0f;
				Player1Status_V2._instance.isCat = false;
				Player1Status_V2._instance.hasCatAbility = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into mouse");
			}
			else if(isInCatAbilityPic1 == true && Player1Status_V2._instance.isCat == true)
			{
				hasCatAbility1 = true;
				Instantiate(redCross, transform.position, Quaternion.identity);
			}
			else if(isInCatAbilityPic2 == true && Player1Status_V2._instance.isCat == true && hasCatAbility1 == true)
			{
				hasCatAbility2 = true;
				Player1Status_V2._instance.hasCatAbility = true;
				Player1Status_V2._instance.hasMouseAbility = false;
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				Debug.Log("Player1 has cat ability");
			}
			else if(isInMouseAbilityPic1 == true && Player1Status_V2._instance.isMouse == true)
			{
				hasMouseAbility1 = true;
				Instantiate(redCross, transform.position, Quaternion.identity);
			}
			else if(isInMouseAbilityPic2 == true && Player1Status_V2._instance.isMouse == true && hasMouseAbility1 == true)
			{
				hasMouseAbility2 = true;
				Player1Status_V2._instance.hasMouseAbility = true;
				Player1Status_V2._instance.hasCatAbility = false;
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				Debug.Log("Player1 has mouse ability");
			}
			
		}
	}

	void Player2TurnCheck()
	{
		if (Input.GetKeyDown(turnKey))
		{
			if(isInCatPic == true && Player1Status_V2._instance.isCat == false)
			{
				Player2Status_V2._instance.isCat = true;
				Player1Status_V2._instance.hp = 5.0f;
				Player2Status_V2._instance.isMouse = false;
				Player2Status_V2._instance.hasMouseAbility = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = catController;
				Debug.Log("Player2 turn into cat");
			} 
			else if(isInMousePic == true && Player1Status_V2._instance.isMouse == false)
			{
				Player2Status_V2._instance.isMouse = true;
				Player1Status_V2._instance.hp = 1.0f;
				Player2Status_V2._instance.isCat = false;
				Player2Status_V2._instance.hasCatAbility = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player2 turn into mouse");
			}
			else if(isInCatAbilityPic1 == true && Player2Status_V2._instance.isCat == true)
			{
				hasCatAbility1 = true;
				Instantiate(redCross, transform.position, Quaternion.identity);
			}
			else if(isInCatAbilityPic2 == true && Player2Status_V2._instance.isCat == true && hasCatAbility1 == true)
			{
				hasCatAbility2 = true;
				Player2Status_V2._instance.hasCatAbility = true;
				Player2Status_V2._instance.hasMouseAbility = false;
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				Debug.Log("Player1 has cat ability");
			}
			else if(isInMouseAbilityPic1 == true && Player2Status_V2._instance.isMouse == true)
			{
				hasCatAbility1 = true;
				Instantiate(redCross, transform.position, Quaternion.identity);
			}
			else if(isInMouseAbilityPic2 == true && Player2Status_V2._instance.isMouse == true && hasMouseAbility1 == true)
			{
				hasCatAbility2 = true;
				Player2Status_V2._instance.hasMouseAbility = true;
				Player2Status_V2._instance.hasCatAbility = false;
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				Debug.Log("Player1 has mouse ability");
			}
		}
	}
}
