#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <sys/wait.h>

int main() {
    int pipe1[2];
    int pipe2[2];
    pid_t pid;
    char buffer[100];
    char *parent_msg = "Hello from parent";
    char *child_msg = "Hello from child";

    pipe(pipe1);
    pipe(pipe2);

    pid = fork();

    if (pid == 0) {
        close(pipe1[1]);
        close(pipe2[0]);

        read(pipe1[0], buffer, sizeof(buffer));
        printf("Child received: %s\n", buffer);

        write(pipe2[1], child_msg, strlen(child_msg) + 1);

        close(pipe1[0]);
        close(pipe2[1]);
    } else {
        close(pipe1[0]);
        close(pipe2[1]);

        write(pipe1[1], parent_msg, strlen(parent_msg) + 1);

        read(pipe2[0], buffer, sizeof(buffer));
        printf("Parent received: %s\n", buffer);

        close(pipe1[1]);
        close(pipe2[0]);

        wait(NULL);
    }

    return 0;
}
