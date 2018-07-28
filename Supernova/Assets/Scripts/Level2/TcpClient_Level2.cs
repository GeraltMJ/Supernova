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


	void StringToInfo(string str)
	{
		str = str.Replace("(","").Replace(")","");
		string[] number = str.Split(',');
		Vector3 positionToSet = new Vector3(float.Parse(number[0]),float.Parse(number[1]),float.Parse(number[2]));
		int dirCount = int.Parse(number[3]);
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
	}

	void FireInfo(string str)
	{
		if(PlayerStatusControl_Level2._instance.isPlayer1)
		{
			pa2.SetCurrentCommand(str);
		}else
		{
			pa1.SetCurrentCommand(str);
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
			recvData = new byte[1024];
			recvLen = serverSocket.Receive(recvData);
			recvStr = Encoding.ASCII.GetString(recvData,0,1);

			if(recvStr == "G")
			{
				PlayerStatusControl_Level2._instance.gameStart = true;
				Debug.Log("Game Will Start in 3 seconds");
			}
		}else if(recvStr == "2")
		{
			PlayerStatusControl_Level2._instance.isPlayer1 = false;
			PlayerStatusControl_Level2._instance.gameStart = true;
			Debug.Log("Game Will Start in 3 seconds");
		}
		else
		{
			Debug.Log("unknown player");
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
				FireInfo(recvStr);
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

	public void SendCurrentInfo(Vector3 pos, FaceDirection dir)
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
		string posStr = pos.ToString() + "," + count.ToString();
		byteToSend = Encoding.ASCII.GetBytes(posStr);
		serverSocket.Send(byteToSend);
	}


	void OnDisable()
	{
		SocketQuit();	
	}


}
