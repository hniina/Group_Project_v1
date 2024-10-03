#include <mysql/mysql.h>
#include <stdio.h>
#include <stdlib.h>

int main() {
    MYSQL *conn;
    MYSQL_RES *res;
    MYSQL_ROW row;
    char *server = "localhost";
    char *user = "root";  
    char *password = "mysql";  
    char *database = "Game";  

    conn = mysql_init(NULL);

    // Connecting to database
    if (conn == NULL) {
        fprintf(stderr, "mysql_init() failed\n");
        return EXIT_FAILURE;
    }

    if (mysql_real_connect(conn, server, user, password, database, 0, NULL, 0) == NULL) {
        fprintf(stderr, "mysql_real_connect() failed\n");
        mysql_close(conn);
        return EXIT_FAILURE;
    }

    // Executing the query to retrieve the winner or the points from a specific game
    if (mysql_query(conn, "SELECT Player.Name, PlayerGame.PointsGame "
                          "FROM Player "
                          "JOIN PlayerGame ON Player.Id = PlayerGame.Player "
                          "JOIN Games ON PlayerGame.Games = Games.Id "
                          "WHERE Games.Id = 1;")) {
        fprintf(stderr, "%s\n", mysql_error(conn));
        mysql_close(conn);
        return EXIT_FAILURE;
    }

    res = mysql_store_result(conn);

    if (res == NULL) {
        fprintf(stderr, "%s\n", mysql_error(conn));
        mysql_close(conn);
        return EXIT_FAILURE;
    }

    // Printing the players and their points
    printf("Players in game 1 and their points:\n");
    while ((row = mysql_fetch_row(res)) != NULL) {
        printf("Player: %s, Points: %s\n", row[0], row[1]);
    }

    mysql_free_result(res);
    mysql_close(conn);

    return EXIT_SUCCESS;
}
