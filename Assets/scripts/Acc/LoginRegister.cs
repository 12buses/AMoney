using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;
using UnityEngine.UI;


public class LoginRegister : MonoBehaviour
{
    public InputField _nick; //ник
    public InputField _email; // поле для ввода почты
    public InputField _pass; //поле для ввода пароля 
    public InputField _RewritePass; //поле для повторного ввода пароля 

    private string OutPass;
    private string OutLogin;

    public Text ROUP_InUnityObj; //объект с текстом объясняющий почему пароль не подошёл 
    public Text ROUL_InUnityObj; //объект с текстом объясняющий почему логин не подошёл 
    public Text IsEmailSutible;

    private bool CheckPassed;//прошла ли проверка

    private List<string> ReasonOfUnsuitablePassword;    //текст объясняющий почему пароль не подошёл 
    private List<string> ReasonOfUnsuitableNick;    //текст объясняющий почему логин не подошёл 


    public class user //класс пользователей
    {
        public string _name { get; set; }
        public string _mail { get; set; }
        public string _password { get; set; }
        public user(string name, string mail, string password)
        {
            _name = name;
            _mail = mail;
            _password = password;
        }
    }

    



    public void Register()//регестрация
    {
        ReasonOfUnsuitablePassword = new List<string>();
        ReasonOfUnsuitableNick = new List<string>();
        OutPass = null;
        OutLogin = null;
        ROUP_InUnityObj.text = "";
        ROUL_InUnityObj.text = "";
        CheckPassed = true; //сбрасывает значение проверки пароля на надёжность

        if (_email.text != null && _pass.text != null && _pass.text == _RewritePass.text && _nick.text != null) //проверка что поля заполнены и поля для ввода пароля совподают
        {
            StartCoroutine(NameEmailPassCheking()); //вызов проверки пароля на безопасность, почты и нейма на уникальность,  следуйший код выполниться после завершения этого

            if (CheckPassed == true) //если пароль подходит
            {
                Debug.Log("в поля всё введено верно");
                ROUL_InUnityObj.text = "в поля всё введено верно";
                ROUP_InUnityObj.text = "в поля всё введено верно";
                IsEmailSutible.text = "Почта введена" + CheckPassed;
            }
            else //если пароль не подходит
            {
                foreach (var item in ReasonOfUnsuitablePassword)
                {
                    OutPass = OutPass + item + System.Environment.NewLine;
                }
                ROUP_InUnityObj.text = OutPass; //вывод почему пароль не подходит

                foreach (var item in ReasonOfUnsuitableNick)
                {
                    OutLogin = OutLogin + item + System.Environment.NewLine;
                }
                ROUL_InUnityObj.text = OutLogin; //вывод почему ник не подходит

                IsEmailSutible.text = "Почта введена" + CheckPassed;
            }
        }
        else
        {
            ROUP_InUnityObj.text = "*Поля ввода почты, имени либо пароля не заполнены. Пароль должен содержть цифру, быть не менне 6 символов и включать в себя заглавную и строчную букву."; //вывод ошибки
        }

    }

    

    IEnumerator NameEmailPassCheking() 
    {    
        //проверка пароля на соответсвие требованиям 
        bool IsLetter = true;

        if (_pass.text.Length < 6 || _pass.text.Length > 15)
        {
            ReasonOfUnsuitablePassword.Add("*Количество символов в пароле должно быть не меньше 6");
            CheckPassed = false;
        }

        string _allowedCharsInPass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!?@#$%^&*_-+()[]{}></\\|\"'.,:;";
        if (_pass.text.All(c => _allowedCharsInPass.Contains(c)) == false)
        {
            ReasonOfUnsuitablePassword.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы, также в пароле могут использоваться только латинские буквы, цифры и эти символы: ~ ! ? @ # $ % ^ & * _ - + ( ) [ ] { } > < / \\ | \" ' . , : ;.");
            IsLetter = false;
            CheckPassed = false;
        }


        if (_pass.text.Any(char.IsLetter) == false)
        {
            ReasonOfUnsuitablePassword.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы");
            IsLetter = false;
            CheckPassed = false;
        }


        if (_pass.text.Any(char.IsDigit) == false)
        {
            ReasonOfUnsuitablePassword.Add("*Пароль должен содержать хотя-бы 1 цифру");
            CheckPassed = false;
        }


        if (_pass.text.Any(char.IsLower) == false && IsLetter == true)
        {
            ReasonOfUnsuitablePassword.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы");
            CheckPassed = false;
        }


        if (_pass.text.Any(char.IsUpper) == false && IsLetter == true)
        {
            ReasonOfUnsuitablePassword.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы");
            CheckPassed = false;
        }


        //проверка почты 
        if (_email.text.Length > 50)// Проверяем длину адреса
        {
            CheckPassed = false;
        }

        
        if (_email.text.IndexOf('@') != _email.text.LastIndexOf('@')) // Проверяем наличие одного символа @
        {
            CheckPassed = false;
        }
        else
        {
            int atIndex = _email.text.IndexOf('@');
            if (atIndex <= 0 || atIndex >= _email.text.Length - 1) // Проверяем, что до и после @ есть хотя бы один допустимый символ
            {
                CheckPassed = false;
            }
        }
        

        if (Regex.IsMatch(_email.text, @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+$") != true)
        {
            CheckPassed = false;
        }

        if (Regex.IsMatch(_email.text, @"\.\.") || Regex.IsMatch(_email.text, @"^[&][=][+][<][>][,][`]$"))
        {
            CheckPassed = false;
        }

        if (_email.text.Contains(" "))
        {
            CheckPassed = false;
        }



        //проверка логина 
        if (_nick.text.Length < 3 || _nick.text.Length > 15) 
        {
            CheckPassed = false;
            ReasonOfUnsuitableNick.Add("*Логин должен быть длиной от 3 до 15 символов.");
        }

        if (!Regex.IsMatch(_nick.text, @"^[a-zA-Z0-9]{3,15}$"))
        {
            ReasonOfUnsuitableNick.Add("*Логин может содержать только латинские буквы и цифры.");
            CheckPassed = false;
        }

        if(CheckPassed == true)
        {

            GetComponent<ServerSpeaking>().UniqieUserCheck();
            CheckPassed = GetComponent<ServerSpeaking>().Unique;
            Debug.Log("CheckPassed  " + CheckPassed);
        }
        
        yield return CheckPassed;
        StopCoroutine(NameEmailPassCheking()); //заверешение проверки => переход назад к функции
    
    }
}
