using System.Collections;
using System.Collections.Generic;
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
        public int amount;
        public string comment;
        public int data_of_creation;
        public int data_of_transaction;
        public int id_transaction;
        public int id_wallet;
        public int status;
        public string type;
    }

    [System.Serializable]
    public class transactions
    {
        public string id;
        public List<transaction> page0;
    }
}
