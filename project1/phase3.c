#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <time.h>
#include <errno.h>
#include <string.h>

#define NUM_ACCOUNTS 2
#define NUM_THREADS 2
#define INITIAL_BALANCE 1000.0

typedef struct {
    int account_id;
    double balance;
    int transaction_count;
    pthread_mutex_t lock;
} Account;

Account accounts[NUM_ACCOUNTS];
int transactions_completed = 0;
pthread_mutex_t progress_lock = PTHREAD_MUTEX_INITIALIZER;

void initialize_accounts() {
    for (int i = 0; i < NUM_ACCOUNTS; i++) {
        accounts[i].account_id = i;
        accounts[i].balance = INITIAL_BALANCE;
        accounts[i].transaction_count = 0;
        pthread_mutex_init(&accounts[i].lock, NULL);
    }
}

// TODO 1: Implement complete transfer function
void transfer_deadlock(int from_id, int to_id, double amount) {
    // Lock source account
    pthread_mutex_lock(&accounts[from_id].lock);
    printf("Thread %ld: Locked account %d\n", (long)pthread_self(), from_id);

    // Simulate processing delay to ensure the other thread grabs its first lock
    usleep(100000); 

    // Add balance checking (sufficient funds?)
    if (accounts[from_id].balance < amount) {
        printf("Thread %ld: Error - Insufficient funds in %d\n", (long)pthread_self(), from_id);
        pthread_mutex_unlock(&accounts[from_id].lock);
        return;
    }

    // Try to lock destination account
    printf("Thread %ld: Waiting for account %d\n", (long)pthread_self(), to_id);
    pthread_mutex_lock(&accounts[to_id].lock); // FIXED: Changed to_acc_id to to_id

    // Transfer (never reached if deadlocked)
    accounts[from_id].balance -= amount;
    accounts[to_id].balance += amount;
    
    // Track progress for the watchdog timer in main
    pthread_mutex_lock(&progress_lock);
    transactions_completed++;
    pthread_mutex_unlock(&progress_lock);

    pthread_mutex_unlock(&accounts[to_id].lock);
    pthread_mutex_unlock(&accounts[from_id].lock);
}

// TODO 2: Create threads that will deadlock
void* teller_deadlock(void* arg) {
    int id = *(int*)arg;
    if (id == 0) {
        // Thread 1: Locks 0, wants 1
        transfer_deadlock(0, 1, 100.0);
    } else {
        // Thread 2: Locks 1, wants 0
        transfer_deadlock(1, 0, 100.0);
    }
    return NULL;
}

int main() {
    initialize_accounts();
    pthread_t threads[NUM_THREADS];
    int thread_ids[NUM_THREADS] = {0, 1};

    printf("=== Phase 3: Deadlock with Timeout Detection ===\n");

    for (int i = 0; i < NUM_THREADS; i++) {
        pthread_create(&threads[i], NULL, teller_deadlock, &thread_ids[i]);
    }

    // TODO 3: Implement deadlock detection
    time_t start_time = time(NULL);
    int deadlocked = 0;

    while (1) {
        sleep(1);
        pthread_mutex_lock(&progress_lock);
        // If both transactions finish, exit loop normally
        if (transactions_completed >= 2) { 
            pthread_mutex_unlock(&progress_lock);
            break; 
        }
        pthread_mutex_unlock(&progress_lock);

        // If no progress for 5 seconds, report suspected deadlock
        if (difftime(time(NULL), start_time) > 5) {
            printf("\n[!] ALERT: No progress for 5 seconds. Suspected DEADLOCK detected!\n");
            printf("[!] Resource Allocation Graph: T0 holds Acc0, T1 holds Acc1. T0 wants Acc1, T1 wants Acc0.\n");
            deadlocked = 1;
            break;
        }
    }

    if (deadlocked) {
        printf("Terminating program due to deadlock...\n");
        exit(1); 
    }

    for (int i = 0; i < NUM_THREADS; i++) {
        pthread_join(threads[i], NULL);
    }

    return 0;
}
