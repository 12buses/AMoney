using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

using UnityEngine;
using UnityEngine.UI;

using DataNamespace;



public class LoginRegister : MonoBehaviour
{
    public InputField _login; //���
    public InputField _email; // ���� ��� ����� �����
    public InputField _pass; //���� ��� ����� ������ 
    public InputField _RewritePass; //���� ��� ���������� ����� ������ 


    public GameObject _emailGameObject; // ���� ��� ����� ����� gameobject
    public GameObject _passGameObject; //���� ��� ����� ������ gameobject
    public GameObject _RewritePassGameObject; //���� ��� ���������� ����� ������ gameobject
    public GameObject RegisterButton; //������ �����������

    private string OutUnsuitability;


    public Text ROU_InUnityObj; //������ � ������� ����������� ������ ������ �� ��������


    private List<string> ReasonOfUnsuitablity;    //����� ����������� ������ �������� �� ��������



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
        //�������� ������ 
        ReasonOfUnsuitablity = new List<string>() { };
        OutUnsuitability = null;

        if (_login.text.Length < 3 || _login.text.Length > 15)
        {
            LoginCheckPassed = false;
            ReasonOfUnsuitablity.Add("*����� ������ ���� ������ �� 3 �� 15 ��������.");
        }

        if (!Regex.IsMatch(_login.text, @"^[a-zA-Z0-9]"))
        {
            ReasonOfUnsuitablity.Add("*����� ����� ��������� ������ ��������� ����� � �����.");
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
        ROU_InUnityObj.text = OutUnsuitability; //����� ������ ��� �� ��������

    }

    void EmailCheck()
    {
        //�������� ����� 
        bool EmailCheckPassed = true;

        if (_email.text.Length > 50)// ��������� ����� ������
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

        if (_email.text.Contains('@') == false) // ��������� ������� ������ ������� @
        {
            EmailCheckPassed = false;
        }
        else
        {
            if (_email.text.IndexOf('@') <= 0 ||
                _email.text.IndexOf('@') >= _email.text.Length - 1) // ���������, ��� �� � ����� @ ���� ���� �� ���� ���������� ������
            {
                EmailCheckPassed = false;
            }
        }

        if (EmailCheckPassed == true)
        {
            _passGameObject.SetActive(true);
            ROU_InUnityObj.text = null; //����� ������ ��� �� ��������
        }
        else
        {
            _passGameObject.SetActive(false);
            _RewritePassGameObject.SetActive(false);
            ROU_InUnityObj.text = "*����� ������� ����������."; //����� ������ ��� �� ��������
        }

    }

    void PassCheckingField1()
    {
        //�������� ������ �� ����������� ����������� 
        bool IsLetter = true;
        OutUnsuitability = null;
        ReasonOfUnsuitablity = new List<string>();
        bool PassField1CheckPassed = true;

        if (_pass.text.Length < 6 || _pass.text.Length > 15)
        {
            ReasonOfUnsuitablity.Add("*���������� �������� � ������ ������ ���� �� ������ 6");
            PassField1CheckPassed = false;
        }

        string _allowedCharsInPass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!?@#$%^&*_-+()[]{}></\\|\"'.,:;";

        if (_pass.text.All(c => _allowedCharsInPass.Contains(c)) == false)
        {
            ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����, ����� � ������ ����� �������������� ������ ��������� �����, ����� � ��� �������: ~ ! ? @ # $ % ^ & * _ - + ( ) [ ] { } > < / \\ | \" ' . , : ;.");
            IsLetter = false;
            PassField1CheckPassed = false;
        }

        if (_pass.text.Any(char.IsLetter) == false)
        {
            ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����");
            IsLetter = false;
            PassField1CheckPassed = false;
        }

        if (_pass.text.Any(char.IsDigit) == false)
        {
            ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 �����");
            PassField1CheckPassed = false;
        }

        if (_pass.text.Any(char.IsLower) == false && IsLetter == true)
        {
            ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����");
            PassField1CheckPassed = false;
        }


        if (_pass.text.Any(char.IsUpper) == false && IsLetter == true)
        {
            ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����");
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
        ROU_InUnityObj.text = OutUnsuitability; //����� ������ ������ �� ��������
    }

    void PassCheckingField2()
    {
        if (_RewritePass.text == _pass.text)
        {
            ROU_InUnityObj.text = null; //����� ������ ������ �� ��������
            RegisterButton.SetActive(true);
        }
        else
        {
            RegisterButton.SetActive(false);
            ROU_InUnityObj.text = "*������ �� ���������."; //����� ������ ������ �� ��������
        }
    }

    public void Register()//������� �� ������ �����������
    {
        GetComponent<ServerSpeaking>().UniqieUserCheck(_login.text, _email.text);
    }


    public void WhenWeGotUniqueCheckResult(UniqueCheck uniqueCheck)
    {
        if (uniqueCheck.EmailIsUnique == true)
        {
            if (uniqueCheck.LoginIsUnique == true)
            {
                ROU_InUnityObj.text = "�������� ������ �������, � ��� ����� �� ������������.";
            }
            else
            {
                ROU_InUnityObj.text = "*��� ����� ��� �����, ���������� ��������� ������.";
            }
        }
        else
        {
            ROU_InUnityObj.text = "*������� �� ��� ����� ��� ��������������, ���������� �����.";
        }
    }
}
