#include <pthread.h>
#include <stdio.h>
#include <stdlib.h>
#include <unistd.h>
#include <time.h>
#include <errno.h>
#include <string.h>

#define NUM_ACCOUNTS 2
#define NUM_THREADS 4
#define TRANSACTIONS_PER_THREAD 1000
#define INITIAL_BALANCE 1000.0

// Updated Account structure with mutex ( GIVEN )
typedef struct {
    int account_id;
    double balance;
    int transaction_count;
    pthread_mutex_t lock; // NEW : Mutex for this account
} Account;

Account accounts[NUM_ACCOUNTS];

// GIVEN : Example of mutex initialization
void initialize_accounts() {
    for (int i = 0; i < NUM_ACCOUNTS; i++) {
        accounts[i].account_id = i;
        accounts[i].balance = INITIAL_BALANCE;
        accounts[i].transaction_count = 0;
        // Initialize the mutex
        pthread_mutex_init(&accounts[i].lock, NULL);
    }
}

// GIVEN : Example deposit function WITH proper protection
void deposit_safe(int account_id, double amount) {
    // Acquire lock BEFORE accessing shared data
    pthread_mutex_lock(&accounts[account_id].lock);
    // ===== CRITICAL SECTION =====
    accounts[account_id].balance += amount;
    accounts[account_id].transaction_count++;
    // ============================
    // Release lock AFTER modifying shared data
    pthread_mutex_unlock(&accounts[account_id].lock);
}

// TODO 1: Implement withdrawal_safe () with mutex protection
void withdrawal_safe(int account_id, double amount) {
    // Acquire lock BEFORE accessing data
    pthread_mutex_lock(&accounts[account_id].lock);
    
    // ===== CRITICAL SECTION =====
    accounts[account_id].balance -= amount;
    accounts[account_id].transaction_count++;
    // ============================
    
    // Release lock AFTER modifying shared data
    pthread_mutex_unlock(&accounts[account_id].lock);
}

// TODO 2: Update teller_thread to use safe functions
void* teller_thread(void* arg) {
    int teller_id = *(int*)arg;
    unsigned int seed = (unsigned int)(time(NULL) ^ (unsigned long)pthread_self());

    for (int i = 0; i < TRANSACTIONS_PER_THREAD; i++) {
        // Option 1: Use Transfers (Zero-Sum)
        // To ensure Account 0 + Account 1 = 2000, we must only move money between them.
        int from_acc = rand_r(&seed) % NUM_ACCOUNTS;
        int to_acc = 1 - from_acc; // If from is 0, to is 1. If from is 1, to is 0.
        
        // Use whole numbers for the amount to avoid floating point precision drift
        double amount = (double)((rand_r(&seed) % 50) + 1);

        // Perform safe transfer
        withdrawal_safe(from_acc, amount);
        deposit_safe(to_acc, amount);
    }
    return NULL;
}

int main() {
    // TODO 3: Add performance timing
    struct timespec start, end;
    clock_gettime(CLOCK_MONOTONIC, &start);

    printf("=== Phase 2: Mutex Protection (Strict 2-Account Transfer) ===\n\n");

    initialize_accounts();

    double expected_total = NUM_ACCOUNTS * INITIAL_BALANCE; // 2000.00
    pthread_t threads[NUM_THREADS];
    int thread_ids[NUM_THREADS];

    // Create threads
    for (int i = 0; i < NUM_THREADS; i++) {
        thread_ids[i] = i;
        pthread_create(&threads[i], NULL, teller_thread, &thread_ids[i]);
    }

    // CRITICAL: We MUST join all threads before checking the total.
    // This ensures every pending deposit and withdrawal is finished.
    for (int i = 0; i < NUM_THREADS; i++) {
        pthread_join(threads[i], NULL);
    }

    clock_gettime(CLOCK_MONOTONIC, &end);
    double elapsed = (end.tv_sec - start.tv_sec) + (end.tv_nsec - start.tv_nsec) / 1e9;

    printf("=== Final Results ===\n");
    double actual_total = 0.0;
    for (int i = 0; i < NUM_ACCOUNTS; i++) {
        printf("  Account %d: $%.2f (%d transactions)\n",
               i, accounts[i].balance, accounts[i].transaction_count);
        actual_total += accounts[i].balance;
    }

    printf("\nExpected total: $%.2f\n", expected_total);
    printf("Actual total:   $%.2f\n", actual_total);
    printf("Execution Time: %.6f seconds\n", elapsed);

    // TODO 4: Add mutex cleanup in main ()
    for (int i = 0; i < NUM_ACCOUNTS; i++) {
        pthread_mutex_destroy(&accounts[i].lock);
    }

    return 0;
}
