#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <string.h>
#include <signal.h>

volatile sig_atomic_t shutdown_flag = 0;
volatile sig_atomic_t print_stats_flag = 0;

void handle_sigint(int sig) {
    shutdown_flag = 1;
}

void handle_sigusr1(int sig) {
    print_stats_flag = 1;
}

int main(int argc, char *argv[]) {
    int opt;
    int max_lines = -1;
    int verbose = 0;

    struct sigaction sa_int;
    struct sigaction sa_usr1;

    sa_int.sa_handler = handle_sigint;
    sigemptyset(&sa_int.sa_mask);
    sa_int.sa_flags = 0;
    sigaction(SIGINT, &sa_int, NULL);

    sa_usr1.sa_handler = handle_sigusr1;
    sigemptyset(&sa_usr1.sa_mask);
    sa_usr1.sa_flags = 0;
    sigaction(SIGUSR1, &sa_usr1, NULL);

    while ((opt = getopt(argc, argv, "n:v")) != -1) {
        switch (opt) {
            case 'n':
                max_lines = atoi(optarg);
                break;
            case 'v':
                verbose = 1;
                break;
            default:
                printf("Usage: ./consumer [-n max] [-v]\n");
                return 1;
        }
	usleep(1000000);
    }

    char line[1024];
    int line_count = 0;
    int char_count = 0;

    while (!shutdown_flag && fgets(line, sizeof(line), stdin) != NULL) {
        if (verbose) {
            printf("%s", line);
        }

        line_count++;
        char_count += strlen(line);

        if (print_stats_flag) {
            fprintf(stderr, "Lines: %d\n", line_count);
            fprintf(stderr, "Characters: %d\n", char_count);
            print_stats_flag = 0;
        }

        if (max_lines > 0 && line_count >= max_lines) {
            break;
        }
    }

    fprintf(stderr, "Final Lines: %d\n", line_count);
    fprintf(stderr, "Final Characters: %d\n", char_count);

    return 0;
}
