using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCube_Level2 : MonoBehaviour {

	public PlayerMove player;

	// Use this for initialization
	void OnTriggerEnter2D(Collider2D other) {
		if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("HolePic"))
		{	
			if(other.gameObject.CompareTag("HolePic") && player.CompareTag("Player1"))
			{
				if(Player1Status_V2._instance.hasMouseAbility == true)
				{
					return;
				}
			}
			else if(other.gameObject.CompareTag("HolePic") && player.CompareTag("Player2"))
			{
				if(Player2Status_V2._instance.hasMouseAbility == true)
				{
					return;
				}
			}
			if(gameObject.CompareTag("RightCube"))
			{
				player.rightObstacle += 1;
			}
			if(gameObject.CompareTag("LeftCube"))
			{
				player.leftObstacle += 1;
			}
			if(gameObject.CompareTag("UpCube"))
			{
				player.upObstacle += 1;
			}
			if(gameObject.CompareTag("DownCube"))
			{
				player.downObstacle += 1;
			}
		}
	}
	void OnTriggerExit2D(Collider2D other) {
		
		if(other.gameObject.CompareTag("Wall") || other.gameObject.CompareTag("HolePic"))
		{
			if(other.gameObject.CompareTag("HolePic") && player.CompareTag("Player1"))
			{
				if(Player1Status_V2._instance.hasMouseAbility == true)
				{
					return;
				}
			}
			else if(other.gameObject.CompareTag("HolePic") && player.CompareTag("Player2"))
			{
				if(Player2Status_V2._instance.hasMouseAbility == true)
				{
					return;
				}
			}
			if(gameObject.CompareTag("RightCube"))
			{
				player.rightObstacle -= 1;
			}
			if(gameObject.CompareTag("LeftCube"))
			{
				player.leftObstacle -= 1;
			}
			if(gameObject.CompareTag("UpCube"))
			{
				player.upObstacle -= 1;
			}
			if(gameObject.CompareTag("DownCube"))
			{
				player.downObstacle -= 1;
			}
		}
	}
}
