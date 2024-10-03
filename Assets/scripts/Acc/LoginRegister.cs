using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.Profiling.Memory.Experimental;
using UnityEngine.UI;


public class LoginRegister : MonoBehaviour
{
    public InputField _nick; //���
    public InputField _email; // ���� ��� ����� �����
    public InputField _pass; //���� ��� ����� ������ 
    public InputField _RewritePass; //���� ��� ���������� ����� ������ 

    private string OutPass;
    private string OutLogin;

    public Text ROUP_InUnityObj; //������ � ������� ����������� ������ ������ �� ������� 
    public Text ROUL_InUnityObj; //������ � ������� ����������� ������ ����� �� ������� 
    public Text IsEmailSutible;

    private bool CheckPassed;//������ �� ��������

    private List<string> ReasonOfUnsuitablePassword;    //����� ����������� ������ ������ �� ������� 
    private List<string> ReasonOfUnsuitableNick;    //����� ����������� ������ ����� �� ������� 


    public class user //����� �������������
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

    



    public void Register()//�����������
    {
        ReasonOfUnsuitablePassword = new List<string>();
        ReasonOfUnsuitableNick = new List<string>();
        OutPass = null;
        OutLogin = null;
        ROUP_InUnityObj.text = "";
        ROUL_InUnityObj.text = "";
        CheckPassed = true; //���������� �������� �������� ������ �� ���������

        if (_email.text != null && _pass.text != null && _pass.text == _RewritePass.text && _nick.text != null) //�������� ��� ���� ��������� � ���� ��� ����� ������ ���������
        {
            StartCoroutine(NameEmailPassCheking()); //����� �������� ������ �� ������������, ����� � ����� �� ������������,  ��������� ��� ����������� ����� ���������� �����

            if (CheckPassed == true) //���� ������ ��������
            {
                Debug.Log("� ���� �� ������� �����");
                ROUL_InUnityObj.text = "� ���� �� ������� �����";
                ROUP_InUnityObj.text = "� ���� �� ������� �����";
                IsEmailSutible.text = "����� �������" + CheckPassed;
            }
            else //���� ������ �� ��������
            {
                foreach (var item in ReasonOfUnsuitablePassword)
                {
                    OutPass = OutPass + item + System.Environment.NewLine;
                }
                ROUP_InUnityObj.text = OutPass; //����� ������ ������ �� ��������

                foreach (var item in ReasonOfUnsuitableNick)
                {
                    OutLogin = OutLogin + item + System.Environment.NewLine;
                }
                ROUL_InUnityObj.text = OutLogin; //����� ������ ��� �� ��������

                IsEmailSutible.text = "����� �������" + CheckPassed;
            }
        }
        else
        {
            ROUP_InUnityObj.text = "*���� ����� �����, ����� ���� ������ �� ���������. ������ ������ �������� �����, ���� �� ����� 6 �������� � �������� � ���� ��������� � �������� �����."; //����� ������
        }

    }

    

    IEnumerator NameEmailPassCheking() 
    {    
        //�������� ������ �� ����������� ����������� 
        bool IsLetter = true;

        if (_pass.text.Length < 6 || _pass.text.Length > 15)
        {
            ReasonOfUnsuitablePassword.Add("*���������� �������� � ������ ������ ���� �� ������ 6");
            CheckPassed = false;
        }

        string _allowedCharsInPass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!?@#$%^&*_-+()[]{}></\\|\"'.,:;";
        if (_pass.text.All(c => _allowedCharsInPass.Contains(c)) == false)
        {
            ReasonOfUnsuitablePassword.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����, ����� � ������ ����� �������������� ������ ��������� �����, ����� � ��� �������: ~ ! ? @ # $ % ^ & * _ - + ( ) [ ] { } > < / \\ | \" ' . , : ;.");
            IsLetter = false;
            CheckPassed = false;
        }


        if (_pass.text.Any(char.IsLetter) == false)
        {
            ReasonOfUnsuitablePassword.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����");
            IsLetter = false;
            CheckPassed = false;
        }


        if (_pass.text.Any(char.IsDigit) == false)
        {
            ReasonOfUnsuitablePassword.Add("*������ ������ ��������� ����-�� 1 �����");
            CheckPassed = false;
        }


        if (_pass.text.Any(char.IsLower) == false && IsLetter == true)
        {
            ReasonOfUnsuitablePassword.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����");
            CheckPassed = false;
        }


        if (_pass.text.Any(char.IsUpper) == false && IsLetter == true)
        {
            ReasonOfUnsuitablePassword.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����");
            CheckPassed = false;
        }


        //�������� ����� 
        if (_email.text.Length > 50)// ��������� ����� ������
        {
            CheckPassed = false;
        }

        
        if (_email.text.IndexOf('@') != _email.text.LastIndexOf('@')) // ��������� ������� ������ ������� @
        {
            CheckPassed = false;
        }
        else
        {
            int atIndex = _email.text.IndexOf('@');
            if (atIndex <= 0 || atIndex >= _email.text.Length - 1) // ���������, ��� �� � ����� @ ���� ���� �� ���� ���������� ������
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



        //�������� ������ 
        if (_nick.text.Length < 3 || _nick.text.Length > 15) 
        {
            CheckPassed = false;
            ReasonOfUnsuitableNick.Add("*����� ������ ���� ������ �� 3 �� 15 ��������.");
        }

        if (!Regex.IsMatch(_nick.text, @"^[a-zA-Z0-9]{3,15}$"))
        {
            ReasonOfUnsuitableNick.Add("*����� ����� ��������� ������ ��������� ����� � �����.");
            CheckPassed = false;
        }

        if(CheckPassed == true)
        {

            GetComponent<ServerSpeaking>().UniqieUserCheck();
            CheckPassed = GetComponent<ServerSpeaking>().Unique;
            Debug.Log("CheckPassed  " + CheckPassed);
        }
        
        yield return CheckPassed;
        StopCoroutine(NameEmailPassCheking()); //����������� �������� => ������� ����� � �������
    
    }
}
