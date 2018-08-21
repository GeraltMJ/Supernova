#include <sys/types.h>
#include <sys/socket.h>
#include <netinet/in.h>
#include <stdio.h>
#include <unistd.h>
#include <string.h>
#include <stdlib.h>
#include <arpa/inet.h>
#include <sys/select.h>
#include <pthread.h>
#include <unordered_map>
#include <unordered_set>
#include <string>
#include <vector>
#include <iostream>

using namespace std;

#define BUFFER_SIZE 1024
#define MAX_CLIENTS 100

vector<int> sock_vec;

void HandleClientMessage(int sock, char* buffer, int length, int index){
	//printf("Rcvd Broadcast msg from index %d, length: %d\n",index, length);
	fflush(stdout);
	
	if(index%3 == 0)
	{
		send(sock_vec[index+1], buffer, length, 0);
		send(sock_vec[index+2], buffer, length, 0);
	}
	else if(index%3 == 1){
		send(sock_vec[index-1], buffer, length, 0);
		send(sock_vec[index+1], buffer, length, 0);
	}else if(index%3 == 2){
		send(sock_vec[index-2], buffer, length, 0);
		send(sock_vec[index-1], buffer, length, 0);
	}
	//printf("Finishing Broadcasting\n");
	fflush(stdout);
}

void* tcpConnect (void* arg){
	int newsock = *(int*)arg;
	free(arg);

	int index = -1;
	for(int i = 0; i < 8; i++)
	{
		if(sock_vec[i] == -1)
		{
			sock_vec[i] = newsock;
			index = i;
			break;
		}
	}

	char buffer[BUFFER_SIZE];
	memset(buffer,0,BUFFER_SIZE);
	int n;

	//first message
	if(index%3 == 0)
	{
		send(newsock,"0",1,0);
		printf("player0 connect\n");
		fflush(stdout);
	}
	else if(index%3 == 1){
		send(newsock,"1",1,0);
		printf("player1 connect\n");
		fflush(stdout);
	}else if(index%3 == 2){
		send(newsock,"2",1,0);
		printf("player2 connect\n");
		fflush(stdout);
	}else{
		perror("error");
		pthread_exit(NULL);
		return NULL;
	}

	do{
		n = recv(newsock, buffer, BUFFER_SIZE, 0);
		if(n == 0){
		}else{
			//buffer[n] = '\0';
			HandleClientMessage(newsock, buffer, n, index);
		}
	}while(n > 0);


	/*
	if(index%2 == 0)
	{
		close(sock_vec[index]);
		close(sock_vec[index+1]);
		printf("Client disconnected(sock:%d)\n",sock_vec[index]);
		printf("Client disconnected(sock:%d)\n",sock_vec[index+1]);
		sock_vec[index] = -1;
		sock_vec[index+1] = -1;
		fflush(stdout);
	}else{
		close(sock_vec[index]);
		close(sock_vec[index-1]);
		printf("Client disconnected(sock:%d)\n",sock_vec[index]);
		printf("Client disconnected(sock:%d)\n",sock_vec[index-1]);
		sock_vec[index] = -1;
		sock_vec[index-1] = -1;
		fflush(stdout);
	}*/
	
	close(sock_vec[index]);
	printf("Client disconnected(sock:%d)\n",sock_vec[index]);
	sock_vec[index] = -1;
	fflush(stdout);
	pthread_exit(NULL);
	return NULL;
}
int main(int argc, char* argv[]){

	sock_vec = vector<int>(9,-1);

	int tcp_sock = socket(PF_INET, SOCK_STREAM, 0);

	if(tcp_sock < 0){
		perror("socket() failed");
		return EXIT_FAILURE;
	}
	struct sockaddr_in tcp_server;

	tcp_server.sin_family = PF_INET;
	tcp_server.sin_addr.s_addr = INADDR_ANY;
	unsigned int port = atoi(argv[1]);
	tcp_server.sin_port = htons(port);

	int on = 1;
	if((setsockopt(tcp_sock, SOL_SOCKET, SO_REUSEADDR, &on, sizeof(on))) < 0)
	{
		perror("setsockopt() failed");
		exit(EXIT_FAILURE);
	}

	int server_len = sizeof(tcp_server);
	if(bind(tcp_sock, (struct sockaddr*)&tcp_server, server_len) < 0){
		perror("bind() failed");
		return EXIT_FAILURE;
	}
	if(listen(tcp_sock, 10) < 0){
		perror("listen() failed");
		return EXIT_FAILURE;
	}
	printf("Server start listening to port %u\n",port);
	fflush(stdout);

	struct sockaddr_in tcp_client;
	int client_len = sizeof(tcp_client);
	fd_set readfds;
	while(1){
		struct timeval timeout;
		timeout.tv_sec = 3;
		timeout.tv_usec = 500;
		FD_ZERO(&readfds);
		FD_SET(tcp_sock, &readfds);
		int ready = select(FD_SETSIZE, &readfds, NULL, NULL, &timeout);
		if(ready == 0){
			continue;
		}
		if(FD_ISSET(tcp_sock, &readfds)){
			int* newsock = (int*)malloc(sizeof(int));
			*newsock = accept(tcp_sock, (struct sockaddr*)&tcp_client, (socklen_t*)&client_len);
			printf("MAIN: Rcvd incoming TCP connection from: %s, sock:%d\n", inet_ntoa(tcp_client.sin_addr),*newsock);
			fflush(stdout);
			pthread_t tcp_tid;
			int rc = pthread_create(&tcp_tid, NULL, tcpConnect, newsock);
			if(rc < 0){
				perror("pthread_create() failed");
				return EXIT_FAILURE;
			}
			pthread_detach(tcp_tid);
		}
	}
	return EXIT_SUCCESS;
}






































