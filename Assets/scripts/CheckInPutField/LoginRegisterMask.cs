using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;
using TMPro;

using DataNamespace;



public class LoginRegisterMask : MonoBehaviour
{
    public TMP_InputField _login; //ник
    public TMP_InputField _email; // поле для ввода почты
    public TMP_InputField _pass; //поле для ввода пароля 
    public Button Button;

    public GameObject RegisterButton; //кнопка регестрации
    public GameObject _loginGameObject; //ник
    public GameObject _emailGameObject; // поле для ввода почты
    public GameObject _passGameObject; //поле для ввода пароля 

    private string OutUnsuitability;

    public TMP_Text ROUL_InUnityObj; //объект с текстом объясняющий почему данные не подходят
    public TMP_Text ROUP_InUnityObj; //объект с текстом объясняющий почему данные не подходят
    public TMP_Text ROUEM_InUnityObj; //объект с текстом объясняющий почему данные не подходят

    private List<string> ReasonOfUnsuitablity;    //текст объясняющий почему проверка не пройдена

    private bool LoginCheckPassed = true;
    private bool EmailCheckPassed = true;
    private bool PassFieldCheckPassed = true;

    public Sprite InActiveButton;
    public Sprite IsActiveButton;
    public Sprite InputField;
    public Sprite InputFieldWrong;


    public void LoginChecking()
    {
        LoginCheckPassed = true;
        //проверка логина 
        ReasonOfUnsuitablity = new List<string>() { };
        OutUnsuitability = null;

        if (_login.text.Length < 3 || _login.text.Length > 15)
        {
            LoginCheckPassed = false;
            ReasonOfUnsuitablity.Add("Был введен неверный логин. Логин должен иметь длину от 3 до 15 символа.");
        }

        if (!Regex.IsMatch(_login.text, @"^[a-zA-Z0-9]"))
        {
            ReasonOfUnsuitablity.Add("Был введен неверный логин. Вы можете использовать только латинские буквы, цифры.");
            LoginCheckPassed = false;
        }


        if (LoginCheckPassed == true)
        {
            _loginGameObject.GetComponent<Image>().sprite = InputField;
            SetActiveButton();
        }
        else
        {
            foreach (var item in ReasonOfUnsuitablity)
            {
                OutUnsuitability = OutUnsuitability + item + System.Environment.NewLine;
            }
            _loginGameObject.GetComponent<Image>().sprite = InputFieldWrong;
        }
        ROUL_InUnityObj.text = OutUnsuitability; //вывод почему ник не подходит
        SetActiveButton();
    }

    public void EmailCheck()
    {
        //проверка почты 
        EmailCheckPassed = true;
        OutUnsuitability = null;
        ReasonOfUnsuitablity = new List<string>() { };

        if (_email.text.Length > 50)// Проверяем длину адреса
        {
            ReasonOfUnsuitablity.Add("Был введен неверный адрес почтового ящика. Адрес не должен превышать 50 символов");
            EmailCheckPassed = false;
        }


        if (Regex.IsMatch(_email.text, @"\.\.") || Regex.IsMatch(_email.text, @"^[&][=][+][<][>][,][`]$"))
        {
            EmailCheckPassed = false;
            ReasonOfUnsuitablity.Add("Было введено неверное имя почтового ящика. Вы можете использовать только латинские буквы, цифры, символы подчеркивания, точки и знаки минус.");
        }
        else
        {
            if (_email.text.Contains(" "))
            {
                EmailCheckPassed = false;
                ReasonOfUnsuitablity.Add("Было введено неверное имя почтового ящика. Вы можете использовать только латинские буквы, цифры, символы подчеркивания, точки и знаки минус.");
            }
            else
            {
                if (Regex.IsMatch(_email.text, @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+$") != true)
                {
                    EmailCheckPassed = false;
                    ReasonOfUnsuitablity.Add("Было введено неверное имя почтового ящика. Вы можете использовать только латинские буквы, цифры, символы подчеркивания, точки и знаки минус.");
                }
            }
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
                ReasonOfUnsuitablity.Add("Было введено неверное имя почтового ящика. Имя почтового ящика должно иметь один символ @.");
            }
        }

        /*
        if (_email.text.Contains('.') == false) // Проверяем наличие одного символа .
        {
            EmailCheckPassed = false;
        }
        else
        {
            if (_email.text.IndexOf('.') <= _email.text.IndexOf('@') ||
                _email.text.IndexOf('.') >= _email.text.Length - 1) // Проверяем, что до и после . есть хотя бы один допустимый символ
            {
                EmailCheckPassed = false;
            }
        }
        */

        if (EmailCheckPassed == true)
        {
            ROUEM_InUnityObj.text = null;
            _emailGameObject.GetComponent<Image>().sprite = InputField;
        }
        else
        {
            foreach (var item in ReasonOfUnsuitablity)
            {
                OutUnsuitability = OutUnsuitability + item + System.Environment.NewLine;
            }
            _emailGameObject.GetComponent<Image>().sprite = InputFieldWrong;
        }
        ROUEM_InUnityObj.text = OutUnsuitability; //вывод, что почта не подходит
        SetActiveButton();
    }

    public void PassCheckingField()
    {
        //проверка пароля на соответсвие требованиям 
        bool IsLetter = true;
        OutUnsuitability = null;
        ReasonOfUnsuitablity = new List<string>();
        PassFieldCheckPassed = true;

        if (_pass.text.Length < 6 || _pass.text.Length > 15)
        {
            ReasonOfUnsuitablity.Add("Был введен неверный пароль. Пароль должен иметь длину от 6 до 15 символа.");
            PassFieldCheckPassed = false;
        }

        string _allowedCharsInPass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!?@#$%^&*_-+()[]{}></\\|\"'.,:;";

        if (_pass.text.All(c => _allowedCharsInPass.Contains(c)) == false)
        {
            ReasonOfUnsuitablity.Add("Был введен неверный пароль. Вы можете ввести в поле латинские буквы, как минимум одна цифры, как минимум одна заглавная и одна строчная буква, спецсимволы.");
            IsLetter = false;
            PassFieldCheckPassed = false;
        }

        if (_pass.text.Any(char.IsLetter) == false)
        {
            ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 строчную и 1 заглавную буквы");
            IsLetter = false;
            PassFieldCheckPassed = false;
        }
        else
        {
            if (_pass.text.Any(char.IsLower) == false && IsLetter == true)
            {
                ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 строчную букву");
                PassFieldCheckPassed = false;
            }


            if (_pass.text.Any(char.IsUpper) == false && IsLetter == true)
            {
                ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 заглавную букву");
                PassFieldCheckPassed = false;
            }
        }

        if (_pass.text.Any(char.IsDigit) == false)
        {
            ReasonOfUnsuitablity.Add("*Пароль должен содержать хотя-бы 1 цифру");
            PassFieldCheckPassed = false;
        }

        

        if (PassFieldCheckPassed == true)
        {
            ROUP_InUnityObj.text = null;
            _passGameObject.GetComponent<Image>().sprite = InputField;
            
        }
        else
        {
            foreach (var item in ReasonOfUnsuitablity)
            {
                OutUnsuitability = OutUnsuitability + item + System.Environment.NewLine;
            }
            ROUP_InUnityObj.text = OutUnsuitability; //вывод почему пароль не подходит
            _passGameObject.GetComponent<Image>().sprite = InputFieldWrong;
        }
        SetActiveButton();
    }

    public void SetActiveButton()
    {
        if(LoginCheckPassed == true && EmailCheckPassed == true && PassFieldCheckPassed == true)
        {
            Button.enabled = true;
            RegisterButton.GetComponent<Image>().sprite = IsActiveButton;
        }
        else
        {
            Button.enabled = false;
            RegisterButton.GetComponent<Image>().sprite = InActiveButton;
        }
    }
}
