using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using DataNamespace;



public class LoginRegister : MonoBehaviour
{
    public InputField _login; //ник
    public InputField _email; // поле для ввода почты
    public InputField _pass; //поле для ввода пароля 
    public InputField _RewritePass; //поле для повторного ввода пароля 


    public GameObject _emailGameObject; // поле для ввода почты gameobject
    public GameObject _passGameObject; //поле для ввода пароля gameobject
    public GameObject _RewritePassGameObject; //поле для повторного ввода пароля gameobject
    public GameObject RegisterButton; //кнопка регестрации

    private string OutUnsuitability;


    public Text ROU_InUnityObj; //объект с текстом объясняющий почему данные не подходят


    private List<string> ReasonOfUnsuitablity;    //текст объясняющий почему проверка не пройдена



    private void Start()
    {
        _login.onValueChanged.AddListener(delegate { LoginChecking(); });
        _email.onValueChanged.AddListener(delegate { EmailCheck(); });
        _pass.onValueChanged.AddListener(delegate { PassCheckingField1(); });
        _RewritePass.onValueChanged.AddListener(delegate { PassCheckingField2(); });
    }

    void LoginChecking()
    {
        bool LoginCheckPassed = true;
        //проверка логина 
        ReasonOfUnsuitablity = new List<string>() { };
        OutUnsuitability = null;

        if (_login.text.Length < 3 || _login.text.Length > 15)
        {
            LoginCheckPassed = false;
            ReasonOfUnsuitablity.Add("*Логин должен быть длиной от 3 до 15 символов.");
        }

        if (!Regex.IsMatch(_login.text, @"^[a-zA-Z0-9]"))
        {
            ReasonOfUnsuitablity.Add("*Логин может содержать только латинские буквы и цифры.");
            LoginCheckPassed = false;
        }


        if (LoginCheckPassed == true)
        {
            _emailGameObject.SetActive(true);
        }
        else
        {
            _emailGameObject.SetActive(false);
            _passGameObject.SetActive(false);
            _RewritePassGameObject.SetActive(false);

            foreach (var item in ReasonOfUnsuitablity)
            {
                OutUnsuitability = OutUnsuitability + item + System.Environment.NewLine;
            }
        }
        ROU_InUnityObj.text = OutUnsuitability; //вывод почему ник не подходит

    }

    void EmailCheck()
    {
        //проверка почты 
        bool EmailCheckPassed = true;

        if (_email.text.Length > 50)// Проверяем длину адреса
        {
            EmailCheckPassed = false;
        }


        if (Regex.IsMatch(_email.text, @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+$") != true)
        {
            EmailCheckPassed = false;
        }

        if (Regex.IsMatch(_email.text, @"\.\.") || Regex.IsMatch(_email.text, @"^[&][=][+][<][>][,][`]$"))
        {
            EmailCheckPassed = false;
        }

        if (_email.text.Contains(" "))
        {
            EmailCheckPassed = false;
        }

        if (_email.text.Contains('@') == false) // Проверяем наличие одного символа @
        {
            EmailCheckPassed = false;
        }
        else
        {
            if (_email.text.IndexOf('@') <= 0 ||
                _email.text.IndexOf('@') >= _email.text.Length - 1) // Проверяем, что до и после @ есть хотя бы один допустимый символ
            {
                EmailCheckPassed = false;
            }
        }

        if (EmailCheckPassed == true)
        {
            _passGameObject.SetActive(true);
            ROU_InUnityObj.text = null; //вывод почему ник не подходит
        }
        else
        {
            _passGameObject.SetActive(false);
            _RewritePassGameObject.SetActive(false);
            ROU_InUnityObj.text = "*Почта введена некоректно."; //вывод почему ник не подходит
        }

    }

    void PassCheckingField1()
    {
        //проверка пароля на соответсвие требованиям 
        bool IsLetter = true;
        OutUnsuitability = null;
        ReasonOfUnsuitablity = new List<string>();
        bool PassField1CheckPassed = true;

        if (_pass.text.Length < 6 || _pass.text.Length > 15)
        {
            ReasonOfUnsuitablity.Add("*Количество символов в пароле должно быть не меньше 6");
            PassField1CheckPassed = false;
        }

        string _allowedCharsInPass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!?@#$%^&*_-+()[]{}></\\|\"'.,:;";

        if (_pass.text.All(c => _allowedCharsInPass.Contains(c)) == false)
        {
            ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы, также в пароле могут использоваться только латинские буквы, цифры и эти символы: ~ ! ? @ # $ % ^ & * _ - + ( ) [ ] { } > < / \\ | \" ' . , : ;.");
            IsLetter = false;
            PassField1CheckPassed = false;
        }

        if (_pass.text.Any(char.IsLetter) == false)
        {
            ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы");
            IsLetter = false;
            PassField1CheckPassed = false;
        }

        if (_pass.text.Any(char.IsDigit) == false)
        {
            ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 цифру");
            PassField1CheckPassed = false;
        }

        if (_pass.text.Any(char.IsLower) == false && IsLetter == true)
        {
            ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы");
            PassField1CheckPassed = false;
        }


        if (_pass.text.Any(char.IsUpper) == false && IsLetter == true)
        {
            ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы");
            PassField1CheckPassed = false;
        }

        if (PassField1CheckPassed == true)
        {
            _RewritePassGameObject.SetActive(true);
        }
        else
        {
            _RewritePassGameObject.SetActive(false);
        }

        foreach (var item in ReasonOfUnsuitablity)
        {
            OutUnsuitability = OutUnsuitability + item + System.Environment.NewLine;
        }
        ROU_InUnityObj.text = OutUnsuitability; //вывод почему пароль не подходит
    }

    void PassCheckingField2()
    {
        if (_RewritePass.text == _pass.text)
        {
            ROU_InUnityObj.text = null; //вывод почему пароль не подходит
            RegisterButton.SetActive(true);
        }
        else
        {
            RegisterButton.SetActive(false);
            ROU_InUnityObj.text = "*Пароли не совподают."; //вывод почему пароль не подходит
        }
    }

    public void Register()//Нажатие на кнопку регестрации
    {
        GetComponent<ServerSpeaking>().UniqieUserCheck(_login.text, _email.text);
    }


    public void WhenWeGotUniqueCheckResult(UniqueCheck uniqueCheck)
    {
        if (uniqueCheck.EmailIsUnique == true)
        {
            if (uniqueCheck.LoginIsUnique == true)
            {
                ROU_InUnityObj.text = "Проверка прошла успешно, в том числе на уникальность.";
            }
            else
            {
                ROU_InUnityObj.text = "*Ваш логин уже занят, попробуйте придумать другой.";
            }
        }
        else
        {
            ROU_InUnityObj.text = "*Аккаунт на эту почту уже зарегстрирован, попробуйте войти.";
        }
    }
}
