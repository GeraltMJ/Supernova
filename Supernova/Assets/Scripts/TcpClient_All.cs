using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TcpClient_All : MonoBehaviour 
{	
	public static TcpClient_All _instance;
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
	private GameObject player1, player2, player3;
	private PlayerMove_Level3 pm1, pm2, pm3;

	private PlayerAttack_Level3 pa1, pa2, pa3;
	
	private PlayerStatus_Level3 ps1, ps2, ps3;
	
	void SendEnterRoomInfo(int player)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",0" + "," + player.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}
	public void SendReadyCommand(int player)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",1" + "," + player.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendCharacterSelectCommand(int player, int character)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",2" + "," + player.ToString() + "," + character.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendMapSelectCommand(int mapNumber, int player)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",3" + "," + player.ToString() + "," + mapNumber.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendQuitCommand(int player)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",4" + "," + player.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}
	public void SendNoReadyCommand(int player)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",5" + "," + player.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendRoundStartCommand(int player)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",9" + "," + player.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendStarTeleportCommand(int player)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",10" + "," + player.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendStartCommand(int player){
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",999" + "," + player.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	private void SynHandleRoomCommand(string[] number, int startIndex)
	{
		int msgType = int.Parse(number[startIndex++]);
		int player = int.Parse(number[startIndex++]);
		if(msgType == 0)
		{
			RoomMenuLogic._instance.ReceiveNewIncomer(player);
		}
		else if(msgType == 1)
		{
			RoomMenuLogic._instance.SetPlayerReady(player);
		}
		else if(msgType == 2)
		{
			int character = int.Parse(number[startIndex++]);
			RoomMenuLogic._instance.SetPlayerCharacter(player, character);
		}
		else if(msgType == 3)
		{
			int map = int.Parse(number[startIndex++]);
			RoomMenuLogic._instance.SetMap(map);
		}
		else if(msgType == 4)
		{
			RoomMenuLogic._instance.SetPlayerQuit(player);
		}
		else if(msgType == 5)
		{
			RoomMenuLogic._instance.SetPlayerNoReady(player);
		}
		else if(msgType == 6)
		{
			Vector2 positionToSet = new Vector2(float.Parse(number[startIndex++]),float.Parse(number[startIndex++]));
			int dirCount = int.Parse(number[startIndex++]);
			switch(player)
			{
				case 1:
					pm1.SetNextPosition(positionToSet);
					pm1.SetDirection(dirCount);
					break;
				case 2:
					pm2.SetNextPosition(positionToSet);
					pm2.SetDirection(dirCount);
					break;
				case 3:
					pm3.SetNextPosition(positionToSet);
					pm3.SetDirection(dirCount);
					break;
			}
		}
		else if(msgType == 7)
		{
			Vector2 positionToSet = new Vector2(float.Parse(number[startIndex++]),float.Parse(number[startIndex++]));

			switch(player)
			{
				case 1:
					pa1.SetFireCommand(positionToSet);
					break;
				case 2:
					pa2.SetFireCommand(positionToSet);
					break;
				case 3:
					pa3.SetFireCommand(positionToSet);
					break;
			}
		}
		else if(msgType == 8)
		{
			float hpChange = float.Parse(number[startIndex++]);

			switch(player)
			{
				case 1:
					ps1.hpChange = hpChange;
					break;
				case 2:
					ps2.hpChange = hpChange;
					break;
				case 3:
					ps3.hpChange = hpChange;
					break;
			}
		}
		else if(msgType == 9)
		{
			PlayerStatusControl_Level3._instance.playerReady[player-1] = true;
		}
		else if(msgType == 10)
		{
			switch(PlayerStatusControl_Level3._instance.playerIdentity)
			{
				case 1:
					pm1.starTeleport = true;
					pm1.starRemain = 6f;
					break;
				case 2:
					pm2.starTeleport = true;
					pm2.starRemain = 6f;
					break;
				case 3:
					pm3.starTeleport = true;
					pm3.starRemain = 6f;
					break;
			}
		}
		else if(msgType == 999)
		{
			RoomMenuLogic._instance.gameStart = true;
		}
		if(startIndex < number.Length)
		{
			SynHandleRoomCommand(number, startIndex);
		}
	}

	private void HandleRoomCommand(string command)
	{
		command = command.Replace("(","").Replace(")","");
		string[] number = command.Split(',');
		SynHandleRoomCommand(number, 1);
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
		 
		if(recvStr == "0"){
			PlayerStatusControl_All._instance.playerIndex = 0;
			RoomMenuLogic._instance.playerIndex = 0;
		}else if(recvStr == "1")
		{
			PlayerStatusControl_All._instance.playerIndex = 1;
			RoomMenuLogic._instance.playerIndex = 1;
		}
		else if(recvStr == "2")
		{
			PlayerStatusControl_All._instance.playerIndex = 2;
			RoomMenuLogic._instance.playerIndex = 2;
		}
		RoomMenuLogic._instance.SetPlayerCharacter(PlayerStatusControl_All._instance.playerIndex, 0);
		SendEnterRoomInfo(PlayerStatusControl_All._instance.playerIndex);
		

	/*
		if(recvStr == "0"){
			PlayerStatusControl_Level3._instance.playerIdentity = 1;
		}
		else if(recvStr == "1")
		{
			PlayerStatusControl_Level3._instance.playerIdentity = 2;
		}
		else if(recvStr == "2")
		{
			PlayerStatusControl_Level3._instance.playerIdentity = 3;
		}
	*/

		while(true)
		{
			recvData = new byte[1024];
			recvLen = serverSocket.Receive(recvData);
			if(recvLen == 0)
			{
				continue;
			}
			recvStr = Encoding.ASCII.GetString(recvData);
			HandleRoomCommand(recvStr);
			Debug.Log("Rcvd From Server: " + recvStr + "END");
		}
	}

	void SocketQuit()
	{

		//SendQuitCommand(PlayerStatusControl_All._instance.playerIndex);
		if(serverSocket != null)
		{
			serverSocket.Close();
		}
		if(connectThread != null)
		{
			connectThread.Interrupt();
			connectThread.Abort();
		}
		Debug.Log("Client Quit");
	}

	void Start()
	{
		_instance = this;
		InitSocket();
	}

	void Update()
	{
		player1 = GameObject.FindWithTag("Player1");
		player2 = GameObject.FindWithTag("Player2");
		player3 = GameObject.FindWithTag("Player3");
		if(player1)
		{
			pm1 = player1.GetComponent<PlayerMove_Level3>();
			pa1 = player1.GetComponent<PlayerAttack_Level3>();
			ps1 = player1.GetComponent<PlayerStatus_Level3>();
		}
		if(player2)
		{
			pm2 = player2.GetComponent<PlayerMove_Level3>();
			pa2 = player2.GetComponent<PlayerAttack_Level3>();
			ps2 = player2.GetComponent<PlayerStatus_Level3>();
		}
		if(player3)
		{
			pm3 = player3.GetComponent<PlayerMove_Level3>();
			pa3 = player3.GetComponent<PlayerAttack_Level3>();
			ps3 = player3.GetComponent<PlayerStatus_Level3>();
		}
	}
	
	void OnDisable()
	{
		SocketQuit();	
	}

	public void SendSelfCommand(string str)
	{	
		byte[] commandSelf = new byte[msgLen];
		commandSelf = Encoding.ASCII.GetBytes(str);
		serverSocket.Send(commandSelf, 1, SocketFlags.None);
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
		string posStr = ",6" + "," + player.ToString() + "," + pos.ToString() + "," + count.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendFireCommand(Vector2 pos, int player)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",7" + "," + player.ToString() + "," + pos.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

	public void SendHpChange(int player, int value)
	{
		byte[] byteToSend = new byte[msgLen];
		string posStr = ",8" + "," + player.ToString() + "," + value.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}

}

