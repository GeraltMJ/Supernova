using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Level3 : MonoBehaviour {

	public float speed = 6.0f;
	public GameObject explosion;
	private GameObject explo;
	private TcpClient_Level2 tcpClient;

	public string parentTag; //表示实例化该子弹的玩家的Tag
	public Sprite[] weapon; //子弹的Sprites数组
	public Sprite this_sprite; //该子弹实例的Sprite
	public Role role; //在子弹被创建的时候由 Player 赋值
	

	public float timer = 0f;

	void Start() {
		tcpClient = TcpClient_Level2._instance;
		this_sprite = this.GetComponent<SpriteRenderer>().sprite;
		switch (role)
		{
			case Role.Assasin:
				this_sprite = weapon[0];
				break;

			case Role.Dragon:
				this_sprite = weapon[1];
				break;

			case Role.Knight:
				this_sprite = weapon[2];
				break;

			case Role.Mage:
				this_sprite = weapon[3];
				break;

			case Role.King:
				this_sprite = weapon[4];
				break;

		}
	}

	void Update () {
		switch (role)
		{
			case Role.Assasin:
				AssasinWeaponControl();
				break;

			case Role.Dragon:
				DragonWeaponControl();
				break;

			case Role.Knight:
				KnightWeaponControl();
				break;

			case Role.Mage:
				MageWeaponControl();
				break;
		}
	}

	private void AssasinWeaponControl()
	{
		this.transform.Rotate(0, 0, Time.deltaTime);
		timer += Time.deltaTime;
		if(timer >= 2.5f)
		{
			transform.Translate(Vector3.left * speed * Time.deltaTime);
		}
		else
		{
			transform.Translate(Vector3.right * speed * Time.deltaTime);
		}
	}

	private void KnightWeaponControl()
	{

	}

	private void MageWeaponControl()
	{

	}

	private void DragonWeaponControl()
	{

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
