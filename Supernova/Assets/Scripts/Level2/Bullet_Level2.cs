using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Level2 : MonoBehaviour {

	public float speed = 6.0f;
	public GameObject explosion;
	private GameObject explo;
	private TcpClient_Level2 tcpClient;

	void Start() {
		tcpClient = TcpClient_Level2._instance;
	}

	void Update () {

		transform.Translate(Vector3.right * speed * Time.deltaTime);
	}
	void OnTriggerEnter2D(Collider2D collision) {
		
		if(collision.gameObject.tag == "Wall")
		{
			Destroy(gameObject);
		}
		
		if(gameObject.CompareTag("Player2Bullet"))
		{
			if(collision.gameObject.tag == "Player1")
			{	
				if(PlayerStatusControl_Level2._instance.isPlayer1)
				{
					if(Player1Status_Level2._instance.damageReflect)
					{
						Player2Status_Level2._instance.Damage(Player2Status_Level2._instance.attackAbility);
						tcpClient.SendHpChange(2,-Mathf.RoundToInt(Player2Status_Level2._instance.attackAbility));
					}
					else
					{	
						if(Player2Status_Level2._instance.playerPower == PlayerPower_Level2.KnightPower && Player1Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Dragon)
						{
							Player1Status_Level2._instance.Damage(Player2Status_Level2._instance.attackAbility+2);
							tcpClient.SendHpChange(1,-Mathf.RoundToInt(Player2Status_Level2._instance.attackAbility)-2);
						}
						else
						{
							Player1Status_Level2._instance.Damage(Player2Status_Level2._instance.attackAbility);
							tcpClient.SendHpChange(1,-Mathf.RoundToInt(Player2Status_Level2._instance.attackAbility));
						}
					}
				}
				explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
				Destroy(explo,1);
				Destroy(gameObject);
			}
		}
		if (gameObject.CompareTag("Player1Bullet"))
		{	
			if(collision.gameObject.tag == "Player2")
			{
				if(!PlayerStatusControl_Level2._instance.isPlayer1)
				{
					if(Player2Status_Level2._instance.damageReflect)
					{
						Player1Status_Level2._instance.Damage(Player1Status_Level2._instance.attackAbility);
						tcpClient.SendHpChange(1,-Mathf.RoundToInt(Player1Status_Level2._instance.attackAbility));
					}
					else
					{
						if(Player1Status_Level2._instance.playerPower == PlayerPower_Level2.KnightPower && Player2Status_Level2._instance.playerCharacter == PlayerCharacter_Level2.Dragon)
						{
							Player2Status_Level2._instance.Damage(Player1Status_Level2._instance.attackAbility+2);
							tcpClient.SendHpChange(2,-Mathf.RoundToInt(Player1Status_Level2._instance.attackAbility)-2);
						}
						else
						{
							Player2Status_Level2._instance.Damage(Player1Status_Level2._instance.attackAbility);
							tcpClient.SendHpChange(2,-Mathf.RoundToInt(Player1Status_Level2._instance.attackAbility));
						}
					}
				}
				
				explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
				Destroy(explo,1);
				Destroy(gameObject);
			}
		}
	}

}
