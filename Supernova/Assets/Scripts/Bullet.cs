using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public FaceDirection dir;
	public float speed = 6.0f;
	public GameObject explosion;
	private GameObject explo;

	// Use this for initialization
	void Start () {
			
	}
	
	// Update is called once per frame
	void Update () {
		switch (dir)
		{
			case FaceDirection.Down:
				transform.Translate(Vector3.down * speed * Time.deltaTime);
				break;

			case FaceDirection.Up:
				transform.Translate(Vector3.up * speed * Time.deltaTime);
				break;

			case FaceDirection.Left:
				transform.Translate(Vector3.left * speed * Time.deltaTime);
				break;

			case FaceDirection.Right:
				transform.Translate(Vector3.right * speed * Time.deltaTime);
				break;

		}
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
				if(Player2Status_V2._instance.hasCatAbility == true && Player1Status_V2._instance.isMouse == true)
				{
					Player1Status_V2._instance.Damage(10);
				}
				else
				{
					Player1Status_V2._instance.Damage(1);
				}
				explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
				Destroy(explo,0.5f);
				Destroy(gameObject);
			}
			else if(collision.gameObject.CompareTag("Player1Bullet"))
			{
				explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
				Destroy(explo,0.5f);
				Destroy(gameObject);
			}
		}
		if (gameObject.CompareTag("Player1Bullet"))
		{	
			if(collision.gameObject.tag == "Player2")
			{
				if(Player1Status_V2._instance.hasCatAbility == true && Player2Status_V2._instance.isMouse == true)
				{
					Player2Status_V2._instance.Damage(10);
				}
				else
				{
					Player2Status_V2._instance.Damage(1);
				}
				explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
				Destroy(explo,0.5f);
				Destroy(gameObject);
			}
			else if(collision.gameObject.CompareTag("Player2Bullet"))
			{
				explo = (GameObject)Instantiate(explosion, collision.transform.position, Quaternion.identity);
				Destroy(explo,0.5f);
				Destroy(gameObject);
			}
		}
	}

}
