#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <fcntl.h>
#include <netinet/in.h>
#include <string.h>
#include <mysql.h>
#include <pthread.h>

	

typedef struct{
	char name[20];
	int socket;
}Connected;

typedef struct{
	Connected conectados[100];
	int num;
}ConnectedList;

ConnectedList Connlist; // global connected list

pthread_mutex_t mutex = PTHREAD_MUTEX_INITIALIZER; //Variable for mutex lock and unlock

int sockets[2000];
int n = 0;

int Add (ConnectedList *lista, char name[20], int socket){
	//Add new connected. Return 0 if ok and -1 if list is full
	if (lista -> num == 100)
		return -1;
	else{
		strcpy(lista->conectados[lista->num].name,name);
		lista->conectados[lista->num].socket = socket;
		lista->num++;
		return 0;
	}
}

int GiveMePosition(ConnectedList *lista, char name[20]){
	//Gives back socket or -1 if it is not in the list
	pthread_mutex_lock(&mutex);
	int i=0;
	int found=0;
	while ((i< lista->num) && !found){
		if (strcmp(lista->conectados[i].name, name)==0)
			found=1;
		if (!found)
			i++;
	}
	
	if (found)
		return i;
	else 
		return -1;
	pthread_mutex_unlock(&mutex);
}

int Delete (ConnectedList *lista, char name [20]){
	
	//Returns 0 if delete and -1 if user does not exist
	pthread_mutex_lock(&mutex);
	int pos= GiveMePosition (lista, name);
	if (pos==-1)
		return -1;
	
	else {
		int i;
		for (i=pos; i< lista->num-1;i++){
			lista->conectados[i]= lista->conectados[i+1];
			//strcpy(lista->conectados[i].nombre,lista->conectados[i+1].nombre);
			//lista->conectados[i].socket= lista->conectados[i+1].socket
		}
		lista->num--;
		return 0;
	}
	pthread_mutex_unlock(&mutex);
	
}



int username_exists(char username[80], MYSQL *conn) {
	
	MYSQL_RES *result;
	MYSQL_ROW row;
	
	char str_query[1024];
	
	sprintf(str_query, "SELECT Name FROM Player WHERE Name = '%s'", username);
	int err=mysql_query (conn, str_query);
	if (err!=0)
	{
		printf ("Error while quering data from database %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	
	result = mysql_store_result(conn);
	row = mysql_fetch_row(result);
	
	if (row == NULL)
		return 0; // Username is not in database
	else
		return 1; // Username exists
	
}

int email_exists(char email[80], MYSQL *conn) {
	
	MYSQL_RES *result;
	MYSQL_ROW row;
	
	char str_query[1024];
	
	sprintf(str_query, "SELECT Email FROM Player WHERE Email = '%s'", email);
	int err=mysql_query (conn, str_query);
	if (err!=0)
	{
		printf ("Error while quering data from database %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return -1;
	}
	
	result = mysql_store_result(conn);
	row = mysql_fetch_row(result);
	
	if (row == NULL)
		return 0; // Email is not in database
	else
		return 1; // Email exists
	
}

void PlayerGames(char output[80], MYSQL *conn)
{
	int err;
	MYSQL_RES *result;
	MYSQL_ROW row;
	
	char query[300];
	strcpy(query, "SELECT Games.Id AS GameID, Player.Name AS PlayerName FROM Games JOIN PlayerGame ON Games.Id = PlayerGame.Games JOIN Player ON Player.Id = PlayerGame.Player ORDER BY Games.Id;");
	
	err = mysql_query(conn, query);
	if (err != 0)
	{
		sprintf(output, "Error querying database %u %s\n", mysql_errno(conn), mysql_error(conn));
		return;
	}
	
	result = mysql_store_result(conn);
	row = mysql_fetch_row(result);
	
	//player_list is not strictly necessary, but it makes the code clearer 
	int last_game_id = -1; // Track last game processed
	char player_list[256] = ""; // Buffer to hold player names
	strcpy(output, ""); // Reset output buffer
	
	if (row == NULL)
	{
		sprintf(output, "No data was obtained in the query\n");
		return;
	}
	else
	{
		while (row != NULL)
		{
			int game_id = atoi(row[0]); // Convert GameID to an integer
			
			// Check if we've switched to a new game
			if (game_id != last_game_id && last_game_id != -1)
			{
				// Print the results for the previous game to output buffer
				sprintf(output + strlen(output), "Game ID: %d\nPlayers' names: %s\n\n", last_game_id, player_list);
				strcpy(player_list, "");  // Reset player list for the new game
			}
			// Append player names for the current game
			if (strlen(player_list) == 0)
			{
				strcpy(player_list, row[1]); // First player in list
			}		
			else
			{
				strcat(player_list, ", ");
				strcat(player_list, row[1]); // Append subsequent players
			}
			
			last_game_id = game_id; // Update to the current game ID
			row = mysql_fetch_row(result); // Move to the next row!!
		}
		// After the loop ends, print the last game's players
		if (last_game_id != -1)
		{
			sprintf(output + strlen(output), "Game ID: %d\nPlayers' names: %s\n\n", last_game_id, player_list);
		}
		
		mysql_free_result(result); 
	}
}
	
char winner(MYSQL *conn, int gameID, char output[1024])
{
	char query[300];
	sprintf(query, "SELECT Player.Name, PlayerGame.PointsGame "
			"FROM PlayerGame JOIN Player ON PlayerGame.Player = Player.Id "
			"WHERE PlayerGame.Games = %d "
			"ORDER BY PlayerGame.PointsGame DESC LIMIT 1", gameID);
	
	int err=mysql_query (conn, query);
	if (err!=0)
	{
		sprintf ("Error while quering data from database %u %s\n",
				 mysql_errno(conn), mysql_error(conn));
		return;
	}
	
	MYSQL_RES *result = mysql_store_result(conn);
	if (result == NULL) {
		fprintf(stderr, "Error storing result: %s\n", mysql_error(conn));
		return;
	}
	
	MYSQL_ROW row = mysql_fetch_row(result);
	if (row == NULL) {
		sprintf(output,"No winner found for game ID %d\n", gameID);
	} else {
		sprintf(output,"Winner: %s, Points: %s\n",row[0], row[1]);
	}
	
	mysql_free_result(result);
}
	

void SignIn( char username[80], char password[80],  char email[80],char output[80], MYSQL *conn){
	char str_query[1024];
		
	if (email_exists(email, conn)) {
		
		sprintf(output,"%d",0);
		return;
		
	}
	
	if (username_exists(username, conn)) {
		
		sprintf(output,"%d",1);
		return;
		
	}

	
	if ((strlen(username) < 3) || (strlen(username) > 80)) //The name must have a minimum and maximum length, check if it's ok.
	{
		
		sprintf(output, "%d",3);
		return;
	}
	
	if ((strlen(password) < 5) || (strlen(password) > 20)) {
		
		sprintf(output,"%d",4);
		return;
		
	}
	sprintf(str_query, "INSERT INTO Player(Email, Name, Password) VALUES ('%s','%s','%s')",email, username, password);
	
	int err=mysql_query (conn, str_query);
	if (err!=0)
	{
		printf ("Error while quering data from database %u %s\n",mysql_errno(conn), mysql_error(conn));
		sprintf(output,"%d",5);
		 return;
	}
	
	sprintf(output, "%d",6);
	
}

void LogIn( char username[80],char password[80], char output[80], MYSQL *conn)
{
	char str_query[1024];
	char password_database[80];

	MYSQL_RES *result;
	MYSQL_ROW row;
	
	int exists=(username_exists(username, conn));
	
	if (!username_exists(username, conn)){
		
		sprintf(output, "%d", 0);
		return;
	}
	
	sprintf(str_query, "SELECT Password FROM Player WHERE Name='%s'", username);
	
	int err=mysql_query (conn, str_query);
	if (err!=0)
	{
		sprintf ("Error while quering data from database %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		sprintf(output,"%d",1);
		return;
	}
	
	result = mysql_store_result(conn);
	row = mysql_fetch_row(result);
	
	if (row == NULL)
		sprintf(output,"%d",2); 
	else
	{	
		strcpy(password_database, row[0]);		
		if (strcmp(password, password_database) == 0) {
			sprintf(output, "%d", 3);  //Successful login
			return;
		} 
		
		else {
			sprintf(output, "%d", 4);  //Wrong password
			return;
		}
	}
}

void GiveMeConnected (ConnectedList *lista, char conectados [300]) {
	//Pone en conectados los nombres de todos los conectados separados por /. 
	//Primero pone el nº de conectados.
	pthread_mutex_lock(&mutex);
	
	sprintf(conectados, "%d%", lista->num);
	int i;
	for(i=0; i< lista->num;i++){
		strcat(conectados, "/");
		strcat(conectados, lista->conectados[i].name);
	}
	pthread_mutex_unlock(&mutex);
}

void *Client(int *socket){
	int sock_conn;
	int *s;
	s=(int *) socket;
	sock_conn= *s;
	
	MYSQL *conn;
	
	char input[512];
	char output[512];
	int ret;	
	char username[80] = "";
	int terminar;
	while (terminar==0){
		// Ahora recibimos su nombre, que dejamos en buff
		ret=read(sock_conn,input, sizeof(input));
		printf ("Recibido\n");
		
		// Tenemos que a?adirle la marca de fin de string 
		// para que no escriba lo que hay despues en el buffer
		input[ret]='\0';
		
		//Escribimos el nombre en la consola
		
		printf ("<-Se ha conectado: %s\n",input);
		
		MYSQL *conn;
		
		
		//Inizialize connection
		conn = mysql_init(NULL);
		
		if (conn==NULL)
		{
			printf ("Error creating connection: %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return 0;
		}
		
		conn = mysql_real_connect (conn, "localhost","root", "mysql",
								   "Game",0, NULL, 0);
		if (conn==NULL)
		{
			printf ("Error inizialiting connection: %u %s\n",
					mysql_errno(conn), mysql_error(conn));
			return 0;
		}
		
		char input_user[80];
		char output_user[1024];
		char *p = strtok( input, "/");
		int codigo =  atoi (p);
		char username[80];
		char password[80];
		char email[80];
		ConnectedList ConnList;
		
		if (codigo == 0 || ret==0) {
			terminar = 1;
		} 
		else {
			
			switch (codigo) {
			case 1:
				p=strtok(NULL,"/");
				strcpy(email,p);
				p=strtok(NULL,"/");
				strcpy(username,p);
				p=strtok(NULL,"/");
				strcpy(password,p);
				SignIn(username	, password, email, output, conn);
				break;
			case 2:
				p=strtok(NULL,"/");
				strcpy(username,p);
				p=strtok(NULL,"/");
				strcpy(password,p);
				
				LogIn(username, password, output, conn);
				if (strcmp(output,'4')==0){
					Add(&ConnList, username, sock_conn);
					GiveMeConnected(&Connlist, output);
				}
				break;
			case 3:
				PlayerGames(output, conn);
				break;
			case 4:
				p=strtok(NULL,"/");
				int game_id=atoi(p);
				winner(conn,game_id, output);  // gameID estÃ¡ en input_user
				break;
			case 5:
				GiveMeConnected(&ConnList, output);
				break;
			}
			printf("->%s\n", output);
			write(sock_conn, output, strlen(output));  // Enviar respuesta al cliente
		}
	}
	if (strlen(username) > 0) {
	Delete(&Connlist, username);	
	GiveMeConnected(&Connlist, output);
	}
	close(sock_conn); 
}

int main(int argc, char *argv[]) 
{
	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	int i =i;
	// INICIALITZACIONS
	// Obrim el socket
	if ((sock_listen = socket(AF_INET, SOCK_STREAM, 0)) < 0)
		printf("Error creant socket");
	// Fem el bind al port
	
	
	memset(&serv_adr, 0, sizeof(serv_adr));// inicialitza a zero serv_addr
	serv_adr.sin_family = AF_INET;
	
	// asocia el socket a cualquiera de las IP de la m?quina. 
	//htonl formatea el numero que recibe al formato necesario
	serv_adr.sin_addr.s_addr = htonl(INADDR_ANY);
	// escucharemos en el port 9050
	serv_adr.sin_port = htons(50029);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	int sockets[100];
	pthread_t thread[100];
	// Atenderemos solo 5 peticiones
	for(;;){
		printf ("Escuchando\n");
		pthread_mutex_lock(&mutex);
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		
		sockets[i]= sock_conn;
		
		printf ("Recibido\n");
		
		
		pthread_create(&thread[i], NULL, Client,&sockets[i]);
		i=i+1;
		pthread_mutex_lock(&mutex);
		}
}



