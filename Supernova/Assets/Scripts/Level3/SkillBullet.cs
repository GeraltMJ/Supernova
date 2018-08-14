using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillBullet : MonoBehaviour {
private GameObject player1, player2, player3;
private PlayerStatus_Level3 ps1, ps2, ps3;
	
	void Start () {
		player1 = GameObject.FindWithTag("Player1");
		player2 = GameObject.FindWithTag("Player2");
		player3 = GameObject.FindWithTag("Player3");
		ps1 = player1.GetComponent<PlayerStatus_Level3>();
		ps2 = player2.GetComponent<PlayerStatus_Level3>();
		ps3 = player3.GetComponent<PlayerStatus_Level3>();
	}
	
	void OnTriggerEnter2D(Collider2D other)
	{
		if(gameObject.CompareTag("Player1Skill"))
		{
			if(other.gameObject.CompareTag("Player1Bullet") || other.gameObject.CompareTag("Player2Bullet"))
			{
				Destroy(other.gameObject);
			}
			if(other.gameObject.CompareTag("Player2") && PlayerStatusControl_Level3._instance.playerIdentity == 2)
			{
				ps2.Damage(ps1.attackAbility);
				//TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-Mathf.RoundToInt(ps1.attackAbility));
			}
			if(other.gameObject.CompareTag("Player3") && PlayerStatusControl_Level3._instance.playerIdentity == 3)
			{
				ps3.Damage(ps1.attackAbility);
				//TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-Mathf.RoundToInt(ps1.attackAbility));
			}
		}
		else if(gameObject.CompareTag("Player2Skill"))
		{
			if(other.gameObject.CompareTag("Player1Bullet") || other.gameObject.CompareTag("Player3Bullet"))
			{
				Destroy(other.gameObject);
			}
			if(other.gameObject.CompareTag("Player1") && PlayerStatusControl_Level3._instance.playerIdentity == 1)
			{
				ps1.Damage(ps2.attackAbility);
				//TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-Mathf.RoundToInt(ps2.attackAbility));
			}
			if(other.gameObject.CompareTag("Player3") && PlayerStatusControl_Level3._instance.playerIdentity == 3)
			{
				ps3.Damage(ps2.attackAbility);
				//TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-Mathf.RoundToInt(ps2.attackAbility));
			}
		}
		else if(gameObject.CompareTag("Player3Skill"))
		{
			if(other.gameObject.CompareTag("Player1Bullet") || other.gameObject.CompareTag("Player2Bullet"))
			{
				Destroy(other.gameObject);
			}
			if(other.gameObject.CompareTag("Player1") && PlayerStatusControl_Level3._instance.playerIdentity == 1)
			{
				ps1.Damage(ps3.attackAbility);
				//TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-Mathf.RoundToInt(ps3.attackAbility));
			}
			if(other.gameObject.CompareTag("Player2") && PlayerStatusControl_Level3._instance.playerIdentity == 2)
			{
				ps2.Damage(ps3.attackAbility);
				//TcpClient_All._instance.SendHpChange(PlayerStatusControl_Level3._instance.playerIdentity,-Mathf.RoundToInt(ps3.attackAbility));
			}
		}
	}
}
