//I watched those YouTube videos and tried to work on server.c.

#include <stdio.h>
#include <stdlib.h>
#include <string.h>
#include <arpa/inet.h>
#include <unistd.h>
#include <mysql/mysql.h>

#define PORT 50015

// Function prototypes for registration and login handling
void handle_registration(MYSQL *conn, int client_socket, char *username, char *password);
void handle_login(MYSQL *conn, int client_socket, char *username, char *password);

int main() {
    int server_fd, new_socket;
    struct sockaddr_in address;
    int addrlen = sizeof(address);
    char buffer[1024] = {0};
    int valread;
    MYSQL *conn;
    char *server = "localhost";
    char *user = "root";
    char *password = "mysql";
    char *database = "Game";

    // Connect to the database
    conn = mysql_init(NULL);
    if (conn == NULL) {
        fprintf(stderr, "mysql_init() failed\n");
        return EXIT_FAILURE;
    }

    if (mysql_real_connect(conn, server, user, password, database, 0, NULL, 0) == NULL) {
        fprintf(stderr, "mysql_real_connect() failed: %s\n", mysql_error(conn));
        mysql_close(conn);
        return EXIT_FAILURE;
    }

    // Create socket
    if ((server_fd = socket(AF_INET, SOCK_STREAM, 0)) == 0) {
        perror("socket failed");
        exit(EXIT_FAILURE);
    }

    // Set address and port
    address.sin_family = AF_INET;
    address.sin_addr.s_addr = INADDR_ANY;
    address.sin_port = htons(PORT);

    // Bind socket and start listening
    if (bind(server_fd, (struct sockaddr *)&address, sizeof(address)) < 0) {
        perror("bind failed");
        exit(EXIT_FAILURE);
    }

    if (listen(server_fd, 3) < 0) {
        perror("listen");
        exit(EXIT_FAILURE);
    }

    printf("Waiting for a connection...\n");

    // Accept incoming client connection
    if ((new_socket = accept(server_fd, (struct sockaddr *)&address, (socklen_t*)&addrlen)) < 0) {
        perror("accept");
        exit(EXIT_FAILURE);
    }

    printf("Connection established\n");

    // Handle client requests
    while ((valread = recv(new_socket, buffer, 1024, 0)) > 0) {
        printf("Message received: %s\n", buffer);

        if (strncmp(buffer, "REGISTER", 8) == 0) {
            // Parse username and password
            char username[50], password[50];
            sscanf(buffer, "REGISTER;%[^;];%s", username, password);

            // Handle registration
            handle_registration(conn, new_socket, username, password);
        } else if (strncmp(buffer, "LOGIN", 5) == 0) {
            // Parse username and password
            char username[50], password[50];
            sscanf(buffer, "LOGIN;%[^;];%s", username, password);

            // Handle login
            handle_login(conn, new_socket, username, password);
        }

        memset(buffer, 0, sizeof(buffer));
    }

    mysql_close(conn);
    close(new_socket);
    close(server_fd);

    return 0;
}

void handle_registration(MYSQL *conn, int client_socket, char *username, char *password) {
    char query[256];
    sprintf(query, "INSERT INTO Users (username, password) VALUES('%s', '%s')", username, password);
    
    if (mysql_query(conn, query)) {
        fprintf(stderr, "Registration failed: %s\n", mysql_error(conn));
        char *response = "REGISTRATION FAILED";
        send(client_socket, response, strlen(response), 0);
    } else {
        printf("Registration successful for user: %s\n", username);
        char *response = "REGISTRATION SUCCESSFUL";
        send(client_socket, response, strlen(response), 0);
    }
}

void handle_login(MYSQL *conn, int client_socket, char *username, char *password) {
    char query[256];
    sprintf(query, "SELECT * FROM Users WHERE username='%s' AND password='%s'", username, password);

    if (mysql_query(conn, query)) {
        fprintf(stderr, "Login query failed: %s\n", mysql_error(conn));
        char *response = "LOGIN FAILED";
        send(client_socket, response, strlen(response), 0);
    } else {
        MYSQL_RES *res = mysql_store_result(conn);
        if (res == NULL) {
            fprintf(stderr, "Error storing result: %s\n", mysql_error(conn));
            char *response = "LOGIN FAILED";
            send(client_socket, response, strlen(response), 0);
        } else {
            if (mysql_num_rows(res) > 0) {
                char *response = "LOGIN SUCCESSFUL";
                send(client_socket, response, strlen(response), 0);
            } else {
                char *response = "LOGIN FAILED";
                send(client_socket, response, strlen(response), 0);
            }
            mysql_free_result(res);
        }
    }
}
