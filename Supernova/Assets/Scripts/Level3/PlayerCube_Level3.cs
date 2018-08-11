using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube_Level3 : MonoBehaviour {

	public GameObject player;
	private PlayerMove_Level3 pm;
	private	Animator anim;
	// Use this for initialization
	void Start () {
		pm = player.GetComponent<PlayerMove_Level3>();
		anim = player.GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void Opposite()
	{
		if(gameObject.name == "PlayerCubeUp" && pm.dir == FaceDirection.Up)
		{
			pm.dir = FaceDirection.Down;
			anim.SetTrigger("DownWalk");
		}
		else if(gameObject.name == "PlayerCubeDown" && pm.dir == FaceDirection.Down)
		{
			pm.dir = FaceDirection.Up;
			anim.SetTrigger("UpWalk");
		}
		else if(gameObject.name == "PlayerCubeLeft" && pm.dir == FaceDirection.Left)
		{
			pm.dir = FaceDirection.Right;
			anim.SetTrigger("RightWalk");
		}
		else if(gameObject.name == "PlayerCubeRight" && pm.dir == FaceDirection.Right)
		{
			pm.dir = FaceDirection.Left;
			anim.SetTrigger("LeftWalk");
		}
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3"))
		{
			Opposite();
		}
		if(gameObject.name == "PlayerCubeUp" && (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3")))
		{
			pm.upCubeActive = true;
		}
		if(gameObject.name == "PlayerCubeDown" && (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3")))
		{
			pm.downCubeActive = true;
		}
		if(gameObject.name == "PlayerCubeLeft" && (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3")))
		{
			pm.leftCubeActive = true;
		}
		if(gameObject.name == "PlayerCubeRight" && (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3")))
		{
			pm.rightCubeActive = true;
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if(gameObject.name == "PlayerCubeUp" && (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3")))
		{
			pm.upCubeActive = false;
		}
		if(gameObject.name == "PlayerCubeDown" && (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3")))
		{
			pm.downCubeActive = false;
		}
		if(gameObject.name == "PlayerCubeLeft" && (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3")))
		{
			pm.leftCubeActive = false;
		}
		if(gameObject.name == "PlayerCubeRight" && (other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("Player1") || other.gameObject.CompareTag("Player2") || other.gameObject.CompareTag("Player3")))
		{
			pm.rightCubeActive = false;
		}
	}
}
