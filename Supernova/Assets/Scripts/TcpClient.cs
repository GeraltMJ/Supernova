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
	public GameObject player2;
	private EnemyMove em;


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
			em.SetCurrentCommand(recvStr);
			
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
		InitSocket();
		em = player2.GetComponent<EnemyMove>();
	}

	public void SendSelfCommand(string str)
	{	
		byte[] commandSelf = new byte[msgLen];
		commandSelf = Encoding.ASCII.GetBytes(str);
		serverSocket.Send(commandSelf, commandSelf.Length, SocketFlags.None);
	}


	void OnDisable()
	{
		SocketQuit();	
	}


}
