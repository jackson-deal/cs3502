#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <signal.h>

volatile sig_atomic_t shutdown_flag = 0;

void handle_sigint(int sig) {
    shutdown_flag = 1;
}

int main(int argc, char *argv[]) {
    int opt;
    char *filename = NULL;
    int buffer_size = 4096;

    struct sigaction sa;

    sa.sa_handler = handle_sigint;
    sigemptyset(&sa.sa_mask);
    sa.sa_flags = 0;
    sigaction(SIGINT, &sa, NULL);

    while ((opt = getopt(argc, argv, "f:b:")) != -1) {
        switch (opt) {
            case 'f':
                filename = optarg;
                break;
            case 'b':
                buffer_size = atoi(optarg);
                break;
            default:
                printf("Usage: ./producer [-f file] [-b size]\n");
                return 1;
        }
    }

    FILE *input = stdin;
    if (filename != NULL) {
        input = fopen(filename, "r");
        if (input == NULL) {
            perror("fopen");
            return 1;
        }
    }

    char buffer[buffer_size];
    size_t n;

    while (!shutdown_flag && (n = fread(buffer, 1, buffer_size, input)) > 0) {

        fwrite(buffer, 1, n, stdout);
    }

    if (input != stdin) {
        fclose(input);
    }

    return 0;
}
