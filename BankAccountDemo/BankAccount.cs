using System;
using System.Collections.Generic;

namespace BankAccountDemo
{
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }

    public class Transaction
    {
        public TransactionType Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime Time { get; set; }

        public Transaction(TransactionType type, decimal amount)
        {
            Type = type;
            Amount = amount;
            Time = DateTime.Now;
        }
    }

    public class BankAccount
    {
        public string AccountHolder { get; private set; }
        public decimal Balance { get; private set; }
        public List<Transaction> Transactions { get; private set; }

        public BankAccount(string accountHolder, decimal initialBalance)
        {
            AccountHolder = accountHolder;
            Balance = initialBalance;
            Transactions = new List<Transaction>();
        }

        public void Deposit(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Deposit must be positive");
            Balance += amount;
            Transactions.Add(new Transaction(TransactionType.Deposit, amount));
        }

        public void Withdraw(decimal amount)
        {
            if (amount <= 0) throw new ArgumentException("Withdrawal must be positive");
            if (amount > Balance) throw new InvalidOperationException("Insufficient funds");
            Balance -= amount;
            Transactions.Add(new Transaction(TransactionType.Withdrawal, amount));
        }
    }
}
