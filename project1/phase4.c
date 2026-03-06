#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <time.h>
#include <errno.h>
#include <string.h>

#define NUM_ACCOUNTS 5
#define NUM_THREADS 5
#define TRANSACTIONS_PER_THREAD 1000
#define INITIAL_BALANCE 1000.0

typedef struct {
    int account_id;
    double balance;
    int transaction_count;
    pthread_mutex_t lock;
} Account;

Account accounts[NUM_ACCOUNTS];

void initialize_accounts() {
    for (int i = 0; i < NUM_ACCOUNTS; i++) {
        accounts[i].account_id = i;
        accounts[i].balance = INITIAL_BALANCE;
        accounts[i].transaction_count = 0;
        // FIX: Ensure mutex is initialized before use
        if (pthread_mutex_init(&accounts[i].lock, NULL) != 0) {
            perror("Mutex init failed");
            exit(1);
        }
    }
}

void safe_transfer_ordered(int from_id, int to_id, double amount) {
    if (from_id == to_id) return; 

    // STRATEGY: Lock Ordering to break Circular Wait
    int first = (from_id < to_id) ? from_id : to_id;
    int second = (from_id < to_id) ? to_id : from_id;

    pthread_mutex_lock(&accounts[first].lock);
    pthread_mutex_lock(&accounts[second].lock);

    if (accounts[from_id].balance >= amount) {
        accounts[from_id].balance -= amount;
        accounts[to_id].balance += amount;
        accounts[from_id].transaction_count++;
        accounts[to_id].transaction_count++;
    }

    pthread_mutex_unlock(&accounts[second].lock);
    pthread_mutex_unlock(&accounts[first].lock);
}

void* teller_routine(void* arg) {
    // FIX: Using rand_r with a thread-local seed for thread-safety
    unsigned int seed = (unsigned int)time(NULL) ^ (unsigned int)(size_t)pthread_self();

    for (int i = 0; i < TRANSACTIONS_PER_THREAD; i++) {
        int from = rand_r(&seed) % NUM_ACCOUNTS;
        int to = rand_r(&seed) % NUM_ACCOUNTS;
        while (to == from) to = rand_r(&seed) % NUM_ACCOUNTS;
        
        safe_transfer_ordered(from, to, 10.0);
    }
    return NULL;
}

int main() {
    pthread_t threads[NUM_THREADS];
    initialize_accounts();

    printf("=== Phase 4: Production-Ready Lock Ordering ===\n");

    for (int i = 0; i < NUM_THREADS; i++) {
        // FIX: Check return value of pthread_create
        if (pthread_create(&threads[i], NULL, teller_routine, NULL) != 0) {
            fprintf(stderr, "Error creating thread %d\n", i);
            return 1;
        }
    }

    for (int i = 0; i < NUM_THREADS; i++) {
        // FIX: Ensure main waits for all threads to finish
        pthread_join(threads[i], NULL);
    }

    double final_total = 0;
    for (int i = 0; i < NUM_ACCOUNTS; i++) {
        final_total += accounts[i].balance;
        // FIX: Destroy mutexes to avoid resource leaks
        pthread_mutex_destroy(&accounts[i].lock);
    }

    printf("\nVerification:\n");
    printf("Final Total Balance: $%.2f\n", final_total);
    printf("Expected Total:      $%.2f\n", (double)(NUM_ACCOUNTS * INITIAL_BALANCE));
    
    if (final_total == (NUM_ACCOUNTS * INITIAL_BALANCE)) {
        printf("Result: SUCCESS (No race conditions or deadlocks)\n");
    } else {
        printf("Result: FAILURE\n");
    }

    return 0;
}
