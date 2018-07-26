using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerAttack : MonoBehaviour {

	public KeyCode shootKey;
	private Animator anim;
	public GameObject[] bullet;
	private bool isShoot = false; //表示是否射击
	private float shootTimer = 0.0f;// 射击的计时器
	public float shootTime = 0.47f;// 表示射击的CD时间
	private FaceDirection faceDir;

	public bool autoAttack;

	void Start()
	{
		anim = GetComponent<Animator>();
	}

	void FixedUpdate()
	{
		if (isShoot)
		{
			shootTimer += Time.deltaTime;
			if(shootTimer >= shootTime)
			{
				shootTimer = 0.0f;
				isShoot = false;
			}
		}
		else
		{
			Shoot();
		}
	}

	void Shoot()
	{
		if(gameObject.CompareTag("Player1"))
		{
			if (autoAttack || Input.GetKeyDown(shootKey) || ETCInput.GetButton("Button"))
			{
				isShoot = true;
				faceDir = gameObject.GetComponent<PlayerMove>().dir;
				switch (faceDir)
				{
					case FaceDirection.Up:
						anim.SetTrigger("UpAttack");
						GameObject go1 = GameManager.Instantiate(bullet[0], new Vector3(this.transform.position.x, this.transform.position.y + 0.5f, this.transform.position.z), Quaternion.identity);
						go1.GetComponent<Bullet>().dir = FaceDirection.Up;
						break;

					case FaceDirection.Down:
						anim.SetTrigger("DownAttack");
						GameObject go2 = GameManager.Instantiate(bullet[1], new Vector3(this.transform.position.x, this.transform.position.y - 0.5f, this.transform.position.z), Quaternion.identity);
						go2.GetComponent<Bullet>().dir = FaceDirection.Down;
						break;

					case FaceDirection.Left:
						anim.SetTrigger("LeftAttack");
						GameObject go3 = GameManager.Instantiate(bullet[2], new Vector3(this.transform.position.x - 0.5f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
						go3.GetComponent<Bullet>().dir = FaceDirection.Left;
						break;

					case FaceDirection.Right:
						anim.SetTrigger("RightAttack");
						GameObject go4 = GameManager.Instantiate(bullet[3], new Vector3(this.transform.position.x + 0.5f, this.transform.position.y, this.transform.position.z), Quaternion.identity);
						go4.GetComponent<Bullet>().dir = FaceDirection.Right;
						break;
				}
			}
		}
	}
}
