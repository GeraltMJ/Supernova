using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetWorkMove : MonoBehaviour {
	
	// Update is called once per frame
	private string currentCommand;
	public GameObject NetworkManager;
	private TcpClient tcpClient;

	public void SetCurrentCommand(string command)
	{
		currentCommand = command;
	}
	void EnemyMove()
	{
		Vector3 oldPosition = transform.position;
		if(currentCommand == "W")
		{
			oldPosition.y += 1;
		}
		else if(currentCommand == "S")
		{
			oldPosition.y -= 1;
		}
		else if(currentCommand == "A")
		{
			oldPosition.x -= 1;
		}
		else if(currentCommand == "D")
		{
			oldPosition.x += 1;
		}
		currentCommand = "";
		transform.position = oldPosition;
	}

	void MoveControl()
	{
		Vector3 oldPosition = transform.position;
		if(Input.GetKeyDown(KeyCode.W))
		{
			tcpClient.SendSelfCommand("W");
			oldPosition.y += 1;
		}
		else if(Input.GetKeyDown(KeyCode.S))
		{
			tcpClient.SendSelfCommand("S");
			oldPosition.y -= 1;
		}
		else if(Input.GetKeyDown(KeyCode.A))
		{
			tcpClient.SendSelfCommand("A");
			oldPosition.x -= 1;
		}
		else if(Input.GetKeyDown(KeyCode.D))
		{
			tcpClient.SendSelfCommand("D");
			oldPosition.x += 1;
		}
		transform.position = oldPosition;
	}

	void Start()
	{
		tcpClient = NetworkManager.GetComponent<TcpClient>();
	}
	void FixedUpdate () {
		
		if(gameObject.name == "Player1")
		{
			if(PlayerStatusControl._instance.isPlayer1)
			{
				MoveControl();
			}
			else
			{
				EnemyMove();
			}
		}
		else if(gameObject.name == "Player2")
		{
			if(!PlayerStatusControl._instance.isPlayer1)
			{
				MoveControl();
			}else
			{
				EnemyMove();
			}
		}

	}
}
