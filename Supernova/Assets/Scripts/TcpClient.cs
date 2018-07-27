using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine.UI;

public class TcpClient : MonoBehaviour 
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
	private PlayerMove em1;
	private PlayerMove em2;


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
			PlayerStatusControl._instance.isPlayer1 = true;
			recvData = new byte[1024];
			recvLen = serverSocket.Receive(recvData);
			recvStr = Encoding.ASCII.GetString(recvData,0,1);

			if(recvStr == "G")
			{
				PlayerStatusControl._instance.gameStart = true;
				Debug.Log("Game Will Start in 3 seconds");
			}
		}else if(recvStr == "2")
		{
			PlayerStatusControl._instance.isPlayer1 = false;
			PlayerStatusControl._instance.gameStart = true;
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
			recvStr = Encoding.ASCII.GetString(recvData,0,1);
			Debug.Log("Rcvd From Server: " + recvStr + "END");
			Debug.Log("END2");
			if(PlayerStatusControl._instance.isPlayer1)
			{
				em2.SetCurrentCommand(recvStr);
			}else
			{
				em1.SetCurrentCommand(recvStr);
			}
			
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
		em1 = player1.GetComponent<PlayerMove>();
		em2 = player2.GetComponent<PlayerMove>();
		InitSocket();
		
	}

	public void SendSelfCommand(string str)
	{	
		byte[] commandSelf = new byte[msgLen];
		commandSelf = Encoding.ASCII.GetBytes(str);
		serverSocket.Send(commandSelf, 1, SocketFlags.None);
	}


	void OnDisable()
	{
		SocketQuit();	
	}


}
