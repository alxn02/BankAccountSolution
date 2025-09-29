using Microsoft.VisualStudio.TestTools.UnitTesting;
using BankAccountDemo;
using System;

namespace BankAccountTest
{
    [TestClass]
    public class BankAccountTests
    {
        [TestMethod]
        public void Constructor_InitializesCorrectly()
        {
            var acc = new BankAccount("Alice", 100m);
            Assert.AreEqual("Alice", acc.AccountHolder);
            Assert.AreEqual(100m, acc.Balance);
            Assert.AreEqual(0, acc.Transactions.Count);
        }

        [TestMethod]
        public void Deposit_Positive_IncreasesBalanceAndRecordsTransaction()
        {
            var acc = new BankAccount("Bob", 50m);
            acc.Deposit(25m);
            Assert.AreEqual(75m, acc.Balance);
            Assert.AreEqual(1, acc.Transactions.Count);
            Assert.AreEqual(TransactionType.Deposit, acc.Transactions[0].Type);
            Assert.AreEqual(25m, acc.Transactions[0].Amount);
        }

        [TestMethod]
        public void Deposit_InvalidAmount_ThrowsArgumentException()
        {
            var acc = new BankAccount("Charlie", 0m);
            Assert.ThrowsException<ArgumentException>(() => acc.Deposit(0m));
            Assert.ThrowsException<ArgumentException>(() => acc.Deposit(-10m));
        }

        [TestMethod]
        public void Withdraw_Success_DecreasesBalanceAndRecordsTransaction()
        {
            var acc = new BankAccount("Dana", 100m);
            acc.Withdraw(40m);
            Assert.AreEqual(60m, acc.Balance);
            Assert.AreEqual(1, acc.Transactions.Count);
            Assert.AreEqual(TransactionType.Withdrawal, acc.Transactions[0].Type);
            Assert.AreEqual(40m, acc.Transactions[0].Amount);
        }

        [TestMethod]
        public void Withdraw_ExceedsBalance_ThrowsInvalidOperationException()
        {
            var acc = new BankAccount("Eve", 30m);
            Assert.ThrowsException<InvalidOperationException>(() => acc.Withdraw(50m));
        }

        [TestMethod]
        public void Withdraw_InvalidAmount_ThrowsArgumentException()
        {
            var acc = new BankAccount("Frank", 100m);
            Assert.ThrowsException<ArgumentException>(() => acc.Withdraw(0m));
            Assert.ThrowsException<ArgumentException>(() => acc.Withdraw(-1m));
        }

        [TestMethod]
        public void MultipleOperations_CorrectBalanceAndTransactions()
        {
            var acc = new BankAccount("Grace", 100m);
            acc.Deposit(50m);   // 150
            acc.Withdraw(20m);  // 130
            acc.Deposit(10m);   // 140

            Assert.AreEqual(140m, acc.Balance);
            Assert.AreEqual(3, acc.Transactions.Count);

            Assert.AreEqual(TransactionType.Deposit, acc.Transactions[0].Type);
            Assert.AreEqual(TransactionType.Withdrawal, acc.Transactions[1].Type);
            Assert.AreEqual(TransactionType.Deposit, acc.Transactions[2].Type);
        }
    }
}
