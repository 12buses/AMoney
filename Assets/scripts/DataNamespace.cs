using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace DataNamespace
{
    [System.Serializable]
    public class User //класс пользователей
    {
        public string login;
        public string email;
        public User(string name, string mail)
        {
            login = name;
            email = mail;

        }
    }

    [System.Serializable]
    public class WalletsData
    {
        public int id_user;
        public List<Wallet> wallets;
    }

    [System.Serializable]
    public class Wallet
    {
        public float balance;
        public string name;
        public string currency;
        public int id_wallet;
    }

    [System.Serializable]
    public class OurUser : User
    {
        public string password;
        public OurUser(string name, string mail, string pass)
            : base(name, mail)
        {
            password = pass;
        }
    }

    [System.Serializable]
    public class Info
    {
        public string email;
        public int id;
        public string login;
    }

    [System.Serializable]
    public class UniqueCheck
    {
        public string email;  
        public string login;
    }

    [System.Serializable]
    public class transaction
    {
        public double amount;
        public string comment;
        public int data_of_creation;
        public int data_of_transaction;
        public int id_transaction;
        public string id_category;
        public int id_wallet;
        public string type;
        public string FormattedData_of_Creation;
        public string FormattedData_of_transaction;
    }

    [System.Serializable]
    public class transactions
    {
        public string id;
        public string expense;
        public string income;
        public List<transaction> page0;

        public void ConvertSecondsToDate()
        {
            foreach(transaction transaction in page0)
            {
                DateTime dateTime = new DateTime(1970, 1, 2).AddSeconds(transaction.data_of_transaction);
                transaction.FormattedData_of_transaction = $"{dateTime.Day}-{dateTime.Month}-{dateTime.Year}";
                dateTime = new DateTime(1970, 1, 2).AddSeconds(transaction.data_of_creation);
                transaction.FormattedData_of_Creation = $"{dateTime.Day}-{dateTime.Month}-{dateTime.Year}";
            }
        }
    }

    [System.Serializable]
    public class Category
    {
        public int id_category;
        public string name;
        public string type;
        public string value;
    }

    [System.Serializable]
    public class Categories
    {
        public List<Category> expense;
        public List<Category> income;
    }
}
