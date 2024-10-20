namespace DataNamespace
{
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
    public class UniqueCheck
    {
        public string email;  
        public string login;
    }
}
