using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlay1Logic : MonoBehaviour {

	
	public bool isCat = false;
	public bool isMouse = false;
	public bool isInCatPic = false;
	public bool isInMousePic = false;

	private GameObject player1;
	private GameObject player2;
	

	public SpriteRenderer spriteRender;
	public Sprite[] sprite;
	public RuntimeAnimatorController catController, mouseController;
	private Animator animator;

	public KeyCode player1Turn;
	public KeyCode player2Turn;

	private void Awake()
	{
		spriteRender = this.GetComponent<SpriteRenderer>();
		animator = this.GetComponent<Animator>();
		player1 = GameObject.FindGameObjectWithTag("Player1");
		player2 = GameObject.FindGameObjectWithTag("Player2");
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.CompareTag("HolePic")&& isMouse == true)
		{
			collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("CatPic"))
		{
			isInCatPic = true;
		}
		if (collision.gameObject.CompareTag("MousePic"))
		{
			isInMousePic = true;
		}
		if(isCat && this.gameObject.tag == "Player1")
		{
			if (collision.gameObject.GetComponent<GamePlay1Logic>().isMouse)
			{
				Player2Status._instance.Dead();
			}
		}
		if(isCat && this.gameObject.tag == "Player2")
		{
			if (collision.gameObject.GetComponent<GamePlay1Logic>().isMouse)
			{
				Player1Status._instance.Dead();
			}
		}

	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.CompareTag("CatPic"))
		{
			isInCatPic = false;
		}
		if (collision.gameObject.CompareTag("MousePic"))
		{
			isInMousePic = false;
		}
		if (collision.gameObject.CompareTag("HolePic"))
		{
			collision.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
		}
	}

	public void Update()
	{
		Player1TurnCheck();
		Player2TurnCheck();
	}

	private void Player1TurnCheck()
	{
		if (Input.GetKeyDown(player1Turn))
		{
			if(isInCatPic == true && !player2.GetComponent<GamePlay1Logic>().isCat)
			{
				isCat = true;
				isMouse = false;
				
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = catController;
				Debug.Log("Player1 turn into cat");
			} else if(isInMousePic == true && !player2.GetComponent<GamePlay1Logic>().isMouse)
			{
				isMouse = true;
				isCat = false;
				
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player1 turn into mouse");
			}
		}
	}

	private void Player2TurnCheck()
	{
		if (Input.GetKeyDown(player2Turn))
		{
			if (isInCatPic == true && !player1.GetComponent<GamePlay1Logic>().isCat)
			{
				isCat = true;
				isMouse = false;
				
				spriteRender.sprite = sprite[0];
				animator.runtimeAnimatorController = catController;
				Debug.Log("Player2 turn into cat");
			}
			else if (isInMousePic == true && !player1.GetComponent<GamePlay1Logic>().isMouse)
			{
				isMouse = true;
				isCat = false;
				
				spriteRender.sprite = sprite[1];
				animator.runtimeAnimatorController = mouseController;
				Debug.Log("Player2 turn into mouse");
			}
		}
	}

}
