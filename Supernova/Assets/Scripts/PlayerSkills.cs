using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkills : MonoBehaviour {

	public PlayerSkill_Level3 skill = PlayerSkill_Level3.Default;
	//技能使用的次数
	private int shieldCountLmt = 2;
	private int iceCountLmt = 4;
	private int doubleDmgCountLmt = 3;
	private int laserCountLmt = 4;

	//技能使用的CD
	private float shieldCD = 4f;
	private float iceCD = 3f;
	private float doubleDmgCD = 3f;
	private float laserCD = 4f;

	//技能计时器
	private float timer = 0.0f;

	//当前技能使用次数
	private int skillCounter = 0;

	Dictionary<PlayerSkill_Level3, int> skillCount = new Dictionary<PlayerSkill_Level3, int>();

	public GameObject bullet;
	private PlayerStatus_Level3 status;
	private PlayerMove_Level3 move;

	public GameObject gun;
	public GameObject shield;
	

	// Use this for initialization
	void Start () {
		skillCount.Add(PlayerSkill_Level3.ShieldSkill, 2);
		skillCount.Add(PlayerSkill_Level3.IceSkill, 4);
		skillCount.Add(PlayerSkill_Level3.DoubleDmgSkill, 3);
		skillCount.Add(PlayerSkill_Level3.GunSkill, 4);
		status = this.GetComponent<PlayerStatus_Level3>();
		move = this.GetComponent<PlayerMove_Level3>();
	}

	
	
	// Update is called once per frame
	void Update () {
		if (skill != PlayerSkill_Level3.Default && ETCInput.GetButtonDown("SkillBtn") && skillCounter < skillCount[skill])
		{
			switch (skill)
			{
				case PlayerSkill_Level3.DoubleDmgSkill:
					DoubleDmgSkill();
					break;

				case PlayerSkill_Level3.HPSkill:
					HPExchangeSkill();
					break;

				case PlayerSkill_Level3.IceSkill:
					IceSkill();
					break;

				case PlayerSkill_Level3.GunSkill:
					GunSkill();
					break;

				case PlayerSkill_Level3.ShieldSkill:
					ShieldSkill();
					break;
			}
		}
	}

	private void DoubleDmgSkill()
	{

		switch (move.dir)
		{
			case FaceDirection.Down:
				DownDmg();
				break;

			case FaceDirection.Up:
				UpDmg();
				break;

			case FaceDirection.Left:
				LeftDmg();
				break;

			case FaceDirection.Right:
				RightDmg();
				break;
		}
		

	}

	private void DownDmg()
	{
		GameObject b1 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -45), this.transform);
		GameObject b2 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -90), this.transform);
		GameObject b3 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -135), this.transform);
		b1.GetComponent<Bullet_Level3>().parentTag = this.tag;
		b2.GetComponent<Bullet_Level3>().parentTag = this.tag;
		b3.GetComponent<Bullet_Level3>().parentTag = this.tag;
		skillCounter++;
		if(skillCounter == skillCount[skill])
		{
			skill = PlayerSkill_Level3.Default;
			skillCounter = 0;
		}
	}

	private void UpDmg()
	{
		GameObject b1 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 45), this.transform);
		GameObject b2 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 90), this.transform);
		GameObject b3 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 135), this.transform);
		b1.GetComponent<Bullet_Level3>().parentTag = this.tag;
		b2.GetComponent<Bullet_Level3>().parentTag = this.tag;
		b3.GetComponent<Bullet_Level3>().parentTag = this.tag;
		skillCounter++;
		if (skillCounter == skillCount[skill])
		{
			skill = PlayerSkill_Level3.Default;
			skillCounter = 0;
		}
	}

	private void LeftDmg()
	{
		GameObject b1 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -135), this.transform);
		GameObject b2 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -180), this.transform);
		GameObject b3 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 135), this.transform);
		b1.GetComponent<Bullet_Level3>().parentTag = this.tag;
		b2.GetComponent<Bullet_Level3>().parentTag = this.tag;
		b3.GetComponent<Bullet_Level3>().parentTag = this.tag;
		skillCounter++;
		if (skillCounter == skillCount[skill])
		{
			skill = PlayerSkill_Level3.Default;
			skillCounter = 0;
		}
	}

	private void RightDmg()
	{
		GameObject b1 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 45), this.transform);
		GameObject b2 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity, this.transform);
		GameObject b3 = GameObject.Instantiate(bullet, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 45), this.transform);
		b1.GetComponent<Bullet_Level3>().parentTag = this.tag;
		b2.GetComponent<Bullet_Level3>().parentTag = this.tag;
		b3.GetComponent<Bullet_Level3>().parentTag = this.tag;
		skillCounter++;
		if (skillCounter == skillCount[skill])
		{
			skill = PlayerSkill_Level3.Default;
			skillCounter = 0;
		}
	}

	private void HPExchangeSkill()
	{
		float p1 = GameObject.FindGameObjectWithTag("Player1").GetComponent<PlayerStatus_Level3>().hp;
		float p2 = GameObject.FindGameObjectWithTag("Player2").GetComponent<PlayerStatus_Level3>().hp;
		float p3 = GameObject.FindGameObjectWithTag("Player3").GetComponent<PlayerStatus_Level3>().hp;
		string temp;
		float maxhp = p1;
		temp = "Player1";
		if(maxhp < p2)
		{
			maxhp = p2;
			temp = "Player2";
		}
		if(maxhp < p3)
		{
			maxhp = p3;
			temp = "Player3";
		}
		float temphp = this.GetComponent<PlayerStatus_Level3>().hp;
		this.GetComponent<PlayerStatus_Level3>().hp = maxhp;
		GameObject.FindGameObjectWithTag(temp).GetComponent<PlayerStatus_Level3>().hp = temphp;
	}

	private void IceSkill()
	{
		//TODO:让角色移动速度减半
	}

	private void GunSkill()
	{
		switch (move.dir)
		{
			case FaceDirection.Down:
				DownGun();
				break;

			case FaceDirection.Up:
				UpGun();
				break;

			case FaceDirection.Left:
				LeftGun();
				break;

			case FaceDirection.Right:
				RightGun();
				break;
		}
	}

	private void DownGun()
	{
		GameObject go = GameObject.Instantiate(gun, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -90), this.transform);
		skillCounter++;
		if(skillCounter == skillCount[skill])
		{
			skillCounter = 0;
			skill = PlayerSkill_Level3.Default;
		}
		GunDamage(move.dir, this.tag);
	}

	private void UpGun()
	{
		GameObject go = GameObject.Instantiate(gun, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, 90), this.transform);
		skillCounter++;
		if (skillCounter == skillCount[skill])
		{
			skillCounter = 0;
			skill = PlayerSkill_Level3.Default;
		}
		GunDamage(move.dir, this.tag);
	}

	private void LeftGun()
	{
		GameObject go = GameObject.Instantiate(gun, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.Euler(0, 0, -190), this.transform);
		skillCounter++;
		if (skillCounter == skillCount[skill])
		{
			skillCounter = 0;
			skill = PlayerSkill_Level3.Default;
		}
		GunDamage(move.dir, this.tag);
	}

	private void RightGun()
	{
		GameObject go = GameObject.Instantiate(gun, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z), Quaternion.identity, this.transform);
		skillCounter++;
		if (skillCounter == skillCount[skill])
		{
			skillCounter = 0;
			skill = PlayerSkill_Level3.Default;
		}
		GunDamage(move.dir, this.tag);
	}
	
	private void GunDamage(FaceDirection dir, string tag)
	{
		GameObject p1 = GameObject.FindGameObjectWithTag("Player1");
		GameObject p2 = GameObject.FindGameObjectWithTag("Player2");
		GameObject p3 = GameObject.FindGameObjectWithTag("Player3");
		switch (tag)
		{
			case "Player1":
				switch (dir)
				{
					case FaceDirection.Down:
						if(p2.transform.position.x == this.transform.position.x && p2.transform.position.y < this.transform.position.y)
						{
							p2.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if(p3.transform.position.x == this.transform.position.x && p3.transform.position.y < this.transform.position.y)
						{
							p3.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Up:
						if(p2.transform.position.x == this.transform.position.x && p2.transform.position.y > this.transform.position.y)
						{
							p2.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p3.transform.position.x == this.transform.position.x && p3.transform.position.y > this.transform.position.y)
						{
							p3.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Left:
						if(p2.transform.position.y == this.transform.position.y && p2.transform.position.x < this.transform.position.x)
						{
							p2.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p3.transform.position.y == this.transform.position.y && p3.transform.position.x < this.transform.position.x)
						{
							p3.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Right:
						if(p2.transform.position.y == this.transform.position.x && p2.transform.position.x > this.transform.position.x)
						{
							p2.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p3.transform.position.y == this.transform.position.x && p3.transform.position.x > this.transform.position.x)
						{
							p3.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;
				}
				break;

			case "Player2":
				switch (dir)
				{
					case FaceDirection.Down:
						if (p1.transform.position.x == this.transform.position.x && p1.transform.position.y < this.transform.position.y)
						{
							p1.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p3.transform.position.x == this.transform.position.x && p3.transform.position.y < this.transform.position.y)
						{
							p3.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Up:
						if (p1.transform.position.x == this.transform.position.x && p1.transform.position.y > this.transform.position.y)
						{
							p1.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p3.transform.position.x == this.transform.position.x && p3.transform.position.y > this.transform.position.y)
						{
							p3.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Left:
						if (p1.transform.position.y == this.transform.position.y && p1.transform.position.x < this.transform.position.x)
						{
							p1.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p3.transform.position.y == this.transform.position.y && p3.transform.position.x < this.transform.position.x)
						{
							p3.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Right:
						if (p1.transform.position.y == this.transform.position.x && p1.transform.position.x > this.transform.position.x)
						{
							p1.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p3.transform.position.y == this.transform.position.x && p3.transform.position.x > this.transform.position.x)
						{
							p3.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;
				}
				break;

			case "Player3":
				switch (dir)
				{
					case FaceDirection.Down:
						if (p2.transform.position.x == this.transform.position.x && p2.transform.position.y < this.transform.position.y)
						{
							p2.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p1.transform.position.x == this.transform.position.x && p1.transform.position.y < this.transform.position.y)
						{
							p1.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Up:
						if (p2.transform.position.x == this.transform.position.x && p2.transform.position.y > this.transform.position.y)
						{
							p2.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p1.transform.position.x == this.transform.position.x && p1.transform.position.y > this.transform.position.y)
						{
							p1.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Left:
						if (p2.transform.position.y == this.transform.position.y && p2.transform.position.x < this.transform.position.x)
						{
							p2.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p1.transform.position.y == this.transform.position.y && p1.transform.position.x < this.transform.position.x)
						{
							p1.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;

					case FaceDirection.Right:
						if (p2.transform.position.y == this.transform.position.x && p2.transform.position.x > this.transform.position.x)
						{
							p2.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						if (p1.transform.position.y == this.transform.position.x && p1.transform.position.x > this.transform.position.x)
						{
							p1.GetComponent<PlayerStatus_Level3>().Damage(this.GetComponent<PlayerStatus_Level3>().attackAbility);
						}
						break;
				}
				break;
		}
	}

	private void ShieldSkill()
	{
		GameObject.Instantiate(shield, new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y, this.transform.position.z), Quaternion.identity, this.transform);
		skillCounter++;
		if(skillCounter == skillCount[skill])
		{
			skillCounter = 0;
			skill = PlayerSkill_Level3.Default;
		}
	}

	

}
