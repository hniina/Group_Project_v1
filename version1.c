#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <sys/types.h>
#include <sys/socket.h>
#include <fcntl.h>
#include <netinet/in.h>
#include <string.h>
#include <mysql.h>



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
	
	err=mysql_query (conn, query);
	if (err!=0)
	{
		sprintf ("Error while quering data from database %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return;
	}
	
	result = mysql_store_result (conn);
	
	row = mysql_fetch_row (result);
	
	int last_game_id=-1; //We haven't processed any game yet
	char player1[80]=""; //We make sure they are empty
	char player2[80]="";
	
	if (row == NULL)
	{
		sprintf (output,"No data was obtained in the query\n");
		return;
	}
	else
	{
		while (row !=NULL)
		{
			int game_id= atoi(row[0]); //convert to integer
			
			if (game_id != last_game_id && last_game_id != -1) {
				printf ("Game ID: %d\n Players' names: %s,%s \n", last_game_id, player1, player2);
				strcpy(player1, "");  // We clean the names for the following game
				strcpy(player2, "");
			}
			
			if (strcmp(player1, "") == 0) {
				strcpy(player1, row[1]);
			} 
			else {
				strcpy(player2, row[1]);
			}
			
			last_game_id= game_id;
			
			row = mysql_fetch_row (result);
			
		}
		
		if (last_game_id != -1) {
			sprintf(output,"Game ID: %d\nPlayers' names: %s, %s\n\n", last_game_id, player1, player2);
		}
		
		
	}
}
	
void winner(MYSQL *conn, int gameID, char output[1024])
{
	char query[300];
	sprintf(query, "SELECT Player.Name, PlayerGame.PointsGame "
			"FROM PlayerGame JOIN Player ON PlayerGame.Player = Player.Id "
			"WHERE PlayerGame.Games = %d "
			"ORDER BY PlayerGame.PointsGame DESC LIMIT 1", gameID);
	if (mysql_query(conn, query)==0) {
		fprintf(stderr, "Error while querying data from database: %s\n", mysql_error(conn));
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
		sprintf(output,"Winner: %s, Points: %s\n", row[0], row[1]);
	}
	
	mysql_free_result(result);
	}
	

void SignIn(char input_user[80], char output[80], MYSQL *conn){
	char str_query[1024];
	char username[80];
	char password[80];
	char email[80];
	int code;
	char *p=strtok(input_user, "/");

	code=atoi(p);
	
	p=strtok(NULL,"/");
	strcpy(email,p);
	
	p=strtok(NULL,"/");
	strcpy(username, p);
	
	p=strtok(NULL, "/");
	strcpy(password,p);

	
	if (email_exists(email, conn)) {
		
		printf(output,"%d",0);
		return;
		
	}
	
	if (username_exists(username, conn)) {
		
		sprintf(output,"%d",1); //testiiiing
		return;
		
	}
	if ((strlen(email) < 15) || (strlen(email) > 80)) {
		
		sprintf(output, "%d",2);
		return;
		
	}
	
	if ((strlen(username) < 3) || (strlen(username) > 80)) //The name must have a minimum and maximum length, check if it's ok.
	{
		
		sprintf(output, "%d",3);
		return;
	}
	
	if ((strlen(password) < 8) || (strlen(password) > 20)) {
		
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

void LogIn( char input_user[80], char output[80], MYSQL *conn)
{
	char str_query[1024];
	char username[80];
	char password[80];
	char password_database[80];
	char *p=strtok(input_user, "/");
	
	strcpy(username, p);
	
	p=strtok(NULL, "/");
	strcpy(password,p);

	MYSQL_RES *result;
	MYSQL_ROW row;
	
	if (username_exists(username, conn)==0){
	
		printf(output, "%d", 4);
		return;
	}
	
	sprintf(str_query, "SELECT Password FROM Player WHERE Name='%s'", username);
	
	int err=mysql_query (conn, str_query);
	if (err!=0)
	{
		printf ("Error while quering data from database %u %s\n",
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
		mysql_free_result(result);
		
		if (strcmp(password, password_database) == 0) {
			sprintf(output, "%d", 3);  // Sucessful log in
		} else {
			sprintf(output, "%d", 4);  //Wrong password
		}
	}
}


int main(int argc, char *argv[]) 
{
	
	int sock_conn, sock_listen, ret;
	struct sockaddr_in serv_adr;
	char input[512];
	char output[512];
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
	serv_adr.sin_port = htons(50019);
	if (bind(sock_listen, (struct sockaddr *) &serv_adr, sizeof(serv_adr)) < 0)
		printf ("Error al bind");
	//La cola de peticiones pendientes no podr? ser superior a 4
	if (listen(sock_listen, 3) < 0)
		printf("Error en el Listen");
	
	
	MYSQL *conn;

	
	//Inizialize connection
	conn = mysql_init(NULL);
	
	if (conn==NULL)
	{
		printf ("Error creating connection: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return;
	}
	
	conn = mysql_real_connect (conn, "localhost","root", "mysql",
							   "Game",0, NULL, 0);
	if (conn==NULL)
	{
		printf ("Error inizialiting connection: %u %s\n",
				mysql_errno(conn), mysql_error(conn));
		return;
	}
	
	for(;;){
		printf ("Escuchando\n");
		
		sock_conn = accept(sock_listen, NULL, NULL);
		printf ("He recibido conexion\n");
		//sock_conn es el socket que usaremos para este cliente
		int terminar=0;
		
		while (terminar==0){
			// Ahora recibimos su nombre, que dejamos en buff
			ret=read(sock_conn,input, sizeof(input));
			printf ("Recibido\n");
			
			// Tenemos que a?adirle la marca de fin de string 
			// para que no escriba lo que hay despues en el buffer
			input[ret]='\0';
			
			//Escribimos el nombre en la consola
			
			printf ("Se ha conectado: %s\n",input);
			
			
			char *p = strtok(input, "/");
			int codigo =  atoi (p);
			int gameID;
			char input_user[80];
			char output_user[80];
			
			if (codigo == 0) {
				terminar = 1;
			} 
			else {
				p = strtok(NULL, "/");
				if (p != NULL) strcpy(input_user, p);  // Aquí capturamos el input_user
				
				switch (codigo) {
				case 1:
					SignIn(input_user, output_user, conn);
					break;
				case 2:
					LogIn(input_user, output_user, conn);
					break;
				case 3:
					PlayerGames(output_user, conn);
					break;
				case 4:
					winner(conn, atoi(input_user), output_user);  // gameID está en input_user
					break;
			}
			write(sock_conn, output_user, strlen(output_user));  // Enviar respuesta al cliente
			}
		}
		close(sock_conn); 
		mysql_close(conn);
		return 0;
	}
}


