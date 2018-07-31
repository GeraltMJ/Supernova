using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class TcpClient_Level2 : MonoBehaviour 
{	
	public static TcpClient_Level2 _instance;
	public string targetIP;
	public int port;
	Socket serverSocket;
	IPAddress ip;
	IPEndPoint ipEnd;
	int msgLen = 1024;
	string recvStr;
	byte[] recvData = new byte[1024];
	byte[] sendData = new byte[1024];
	int recvLen;
	Thread connectThread;
	public GameObject player1;
	public GameObject player2;
	private PlayerMove_Level2 em1;
	private PlayerMove_Level2 em2;
	private PlayerAttack_Level2 pa1;
	private PlayerAttack_Level2 pa2;

	void HandleSynMessage(string[] number, int startIndex)
	{
		int msgType = int.Parse(number[startIndex++]);
		if(msgType == 0)
		{

		}
		else if(msgType == 1)
		{
			Vector2 positionToSet = new Vector2(float.Parse(number[startIndex++]),float.Parse(number[startIndex++]));
			if(PlayerStatusControl_Level2._instance.isPlayer1)
			{
				pa2.SetFireCommand(positionToSet);
			}else
			{
				pa1.SetFireCommand(positionToSet);
			}
		}
		else if(msgType == 2)
		{
			int player = int.Parse(number[startIndex++]);
			float hpChange = float.Parse(number[startIndex++]);
			if(player == 1)
			{
				Player1Status_Level2._instance.hpChange = hpChange;
			}
			else if(player == 2)
			{
				Player2Status_Level2._instance.hpChange = hpChange;
			}
		}
	}


	void StringToInfo(string str)
	{
		str = str.Replace("(","").Replace(")","");
		string[] number = str.Split(',');
		int msgType = int.Parse(number[1]);
		if(msgType == 0)
		{
			Vector2 positionToSet = new Vector2(float.Parse(number[2]),float.Parse(number[3]));
			int dirCount = int.Parse(number[4]);
			if(PlayerStatusControl_Level2._instance.isPlayer1)
			{
				em2.SetNextPosition(positionToSet);
				em2.SetDirection(dirCount);
			}
			else
			{
				em1.SetNextPosition(positionToSet);
				em1.SetDirection(dirCount);
			}
			
			if(number.Length > 5)
			{
				HandleSynMessage(number,5);
			}
		}
		else if(msgType == 1)
		{
			Vector2 positionToSet = new Vector2(float.Parse(number[2]),float.Parse(number[3]));
			if(PlayerStatusControl_Level2._instance.isPlayer1)
			{
				pa2.SetFireCommand(positionToSet);
			}else
			{
				pa1.SetFireCommand(positionToSet);
			}
			if(number.Length > 4)
			{
				HandleSynMessage(number,4);
			}
		}
		else if(msgType == 2)
		{	
			int player = int.Parse(number[2]);
			float hpChange = float.Parse(number[3]);
			if(player == 1)
			{
				Player1Status_Level2._instance.hpChange = hpChange;
			}
			else if(player == 2)
			{
				Player2Status_Level2._instance.hpChange = hpChange;
			}
			if(number.Length > 4)
			{
				HandleSynMessage(number,4);
			}
		}
	}

	void PlayerStringToInfo(string str)
	{
		str = str.Replace("(","").Replace(")","");
		string[] number = str.Split(',');
		int msgType = int.Parse(number[0]);
		int player = int.Parse(number[1]);
		if(msgType == 0)
		{
			Vector2 positionToSet = new Vector2(float.Parse(number[2]),float.Parse(number[3]));
			int dirCount = int.Parse(number[4]);
			if(player == 1)
			{
				em1.SetNextPosition(positionToSet);
				em1.SetDir(dirCount);
			}
			else if(player == 2)
			{
				em2.SetNextPosition(positionToSet);
				em2.SetDir(dirCount);
			}
		}
		else if(msgType == 1)
		{
			Vector2 positionToSet = new Vector2(float.Parse(number[2]),float.Parse(number[3]));
			if(player == 1)
			{
				pa1.SetFireCommand(positionToSet);
			}
			else if(player == 2)
			{
				pa2.SetFireCommand(positionToSet);
			}
			
		}
	}

	void SimpleCommandHandle(string str)
	{
		if(str == "R")
		{
			PlayerStatusControl_Level2._instance.twoReady = true;
		}
		else if(str == "G")
		{
			/* 
			PlayerStatusControl_Level2._instance.checkCount += 1;
			if(PlayerStatusControl_Level2._instance.checkCount == 2)
			{
				PlayerStatusControl_Level2._instance.enemyCheck = true;
			}
			*/
			PlayerStatusControl_Level2._instance.enemyCheck = true;
		}
	}

	void InitSocket()
	{
		ip = IPAddress.Parse(targetIP);
		ipEnd = new IPEndPoint(ip, port);
		connectThread = new Thread(StartTCPConnect);
		connectThread.Start();
	}

	void StartTCPConnect()
	{
		serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
		Debug.Log("Before Connect");
		serverSocket.Connect(ipEnd);
		Debug.Log("Connecting success");
		recvData = new byte[1024];
		recvLen = serverSocket.Receive(recvData);
		recvStr = Encoding.ASCII.GetString(recvData,0,1);
		Debug.Log("I will be player " + recvStr);

		if(recvStr == "1"){
			PlayerStatusControl_Level2._instance.isPlayer1 = true;
		}else if(recvStr == "2")
		{
			PlayerStatusControl_Level2._instance.isPlayer1 = false;
			PlayerStatusControl_Level2._instance.twoReady = true;
		}

		while(true)
		{
			recvData = new byte[1024];
			recvLen = serverSocket.Receive(recvData);
			if(recvLen == 0)
			{
				continue;
			}
			if(recvLen <= 2)
			{
				recvStr = Encoding.ASCII.GetString(recvData,0,1);
				SimpleCommandHandle(recvStr);
			}
			else
			{
				recvStr = Encoding.ASCII.GetString(recvData);
				StringToInfo(recvStr);
			}
			Debug.Log("Rcvd From Server: " + recvStr + "END");
		}
	}

	void SocketQuit()
	{
		if(connectThread != null)
		{
			connectThread.Interrupt();
			connectThread.Abort();
		}

		if(serverSocket != null)
		{
			serverSocket.Close();
		}
		Debug.Log("Client Quit");
	}

	void Start()
	{
		_instance = this;
		em1 = player1.GetComponent<PlayerMove_Level2>();
		em2 = player2.GetComponent<PlayerMove_Level2>();
		pa1 = player1.GetComponent<PlayerAttack_Level2>();
		pa2 = player2.GetComponent<PlayerAttack_Level2>();
		InitSocket();
		
	}

	public void SendSelfCommand(string str)
	{	
		byte[] commandSelf = new byte[msgLen];
		commandSelf = Encoding.ASCII.GetBytes(str);
		serverSocket.Send(commandSelf, 1, SocketFlags.None);
	}

	public void SendCurrentInfo(Vector2 pos, FaceDirection dir)
	{
		int count = 0;
		switch(dir)
		{
			case FaceDirection.Up:
				count = 0;
				break;
			case FaceDirection.Down:
				count = 1;
				break;
			case FaceDirection.Left:
				count = 2;
				break;
			case FaceDirection.Right:
				count = 3;
				break;
		}
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",0" + "," + pos.ToString() + "," + count.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendPlayerCurrentInfo(Vector2 pos, FaceDirection dir, int player)
	{
		int count = 0;
		switch(dir)
		{
			case FaceDirection.Up:
				count = 0;
				break;
			case FaceDirection.Down:
				count = 1;
				break;
			case FaceDirection.Left:
				count = 2;
				break;
			case FaceDirection.Right:
				count = 3;
				break;
		}
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",0" + "," + player.ToString() + "," + pos.ToString() + "," + count.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendFireCommand(Vector2 pos)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",1" + "," + pos.ToString() + ",";
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendHpChange(int player, int value)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",2" + "," + player.ToString() + "," + value.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}
	void OnDisable()
	{
		SocketQuit();	
	}


}
