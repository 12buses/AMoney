namespace DataNamespace
{
    public class User //класс пользователей
    {
        public string Name { get; set; }
        public string Mail { get; set; }
        public User(string name, string mail)
        {
            Name = name;
            Mail = mail;

        }
    }

    public class OurUser : User
    {
        public string _password { get; set; }
        public OurUser(string name, string mail, string password)
            : base(name, mail)
        {
            _password = password;
        }
    }

    public class UniqueCheck
    {
        public bool EmailIsUnique { get; set; }
        public bool LoginIsUnique { get; set; }
        public UniqueCheck(bool EmailUnique, bool LoginUnique)
        {
            EmailIsUnique = EmailUnique;
            LoginIsUnique = LoginUnique;

        }
    }
}
