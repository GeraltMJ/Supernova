using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public FaceDirection dir;
	public float speed = 6.0f;

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

	private void OnCollisionEnter2D(Collision2D collision)
	{
		if(collision.gameObject.tag == "Wall")
		{
			Destroy(this.gameObject);
		}
		
		if(collision.gameObject.tag == "Player1")
		{
			if(this.gameObject.tag == "Player2")
			{
				Player1Status._instance.Damage(1);
				Destroy(this.gameObject);
			}
		}
		if (collision.gameObject.tag == "Player2")
		{
			if(this.gameObject.tag == "Player1")
			{
				Player2Status._instance.Damage(1);
				Destroy(this.gameObject);
			}
		}
	}

}
