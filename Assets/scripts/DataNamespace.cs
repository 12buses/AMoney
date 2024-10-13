namespace DataNamespace
{
    public class User //класс пользователей
    {
        public string Name;
        public string Mail;
        public User(string name, string mail)
        {
            Name = name;
            Mail = mail;

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
        public bool EmailIsUnique;  
        public bool LoginIsUnique;
        public UniqueCheck(bool EmailUnique, bool LoginUnique)
        {
            EmailIsUnique = EmailUnique;
            LoginIsUnique = LoginUnique;

        }
    }
}
