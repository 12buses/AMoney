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
        public string _password;
        public OurUser(string name, string mail, string password)
            : base(name, mail)
        {
            _password = password;
        }
    }

    public class UniqueCheck
    {
        public bool email;  
        public bool login;
        public UniqueCheck(bool EmailUnique, bool LoginUnique)
        {
            email = EmailUnique;
            login = LoginUnique;

        }
    }
}
