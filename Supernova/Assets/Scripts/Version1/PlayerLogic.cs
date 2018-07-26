using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLogic : MonoBehaviour {

	public bool isInCatPic = false;
	public bool isInMousePic = false;
	public bool isInWinPic = false;

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
		if (Input.GetKeyDown(turnKey))
		{
			if(isInCatPic == true && Player2Status._instance.isCat == false)
			{
				Player1Status._instance.isCat = true;
				Player1Status._instance.isMouse = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = catController;
				Debug.Log("Player1 turn into cat");
			} 
			else if(isInMousePic == true && Player2Status._instance.isMouse == false)
			{
				Player1Status._instance.isMouse = true;
				Player1Status._instance.isCat = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into mouse");
			}
		}
	}

	void Player2TurnCheck()
	{
		if (Input.GetKeyDown(turnKey))
		{
			if(isInCatPic == true && Player1Status._instance.isCat == false)
			{
				Player2Status._instance.isCat = true;
				Player2Status._instance.isMouse = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = catController;
				Debug.Log("Player2 turn into cat");
			} 
			else if(isInMousePic == true && Player1Status._instance.isMouse == false)
			{
				Player2Status._instance.isMouse = true;
				Player2Status._instance.isCat = false;
				
				GameObject smoke = Instantiate(smokeEffect, transform.position, Quaternion.identity) as GameObject;
				Destroy(smoke, 1);
				Instantiate(redCross, transform.position, Quaternion.identity);
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player2 turn into mouse");
			}
		}
	}
}
