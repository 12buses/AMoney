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
    public TMP_InputField _login; //���
    public TMP_InputField _email; // ���� ��� ����� �����
    public TMP_InputField _pass; //���� ��� ����� ������ 
    public Button Button;

    public GameObject RegisterButton; //������ �����������
    public GameObject _loginGameObject; //���
    public GameObject _emailGameObject; // ���� ��� ����� �����
    public GameObject _passGameObject; //���� ��� ����� ������ 

    private string OutUnsuitability;

    public TMP_Text ROUL_InUnityObj; //������ � ������� ����������� ������ ������ �� ��������
    public TMP_Text ROUP_InUnityObj; //������ � ������� ����������� ������ ������ �� ��������
    public TMP_Text ROUEM_InUnityObj; //������ � ������� ����������� ������ ������ �� ��������

    private List<string> ReasonOfUnsuitablity;    //����� ����������� ������ �������� �� ��������

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
        //�������� ������ 
        ReasonOfUnsuitablity = new List<string>() { };
        OutUnsuitability = null;

        if (_login.text.Length < 3 || _login.text.Length > 15)
        {
            LoginCheckPassed = false;
            ReasonOfUnsuitablity.Add("��� ������ �������� �����. ����� ������ ����� ����� �� 3 �� 15 �������.");
        }

        if (!Regex.IsMatch(_login.text, @"^[a-zA-Z0-9]"))
        {
            ReasonOfUnsuitablity.Add("��� ������ �������� �����. �� ������ ������������ ������ ��������� �����, �����.");
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
        ROUL_InUnityObj.text = OutUnsuitability; //����� ������ ��� �� ��������
        SetActiveButton();
    }

    public void EmailCheck()
    {
        //�������� ����� 
        EmailCheckPassed = true;
        OutUnsuitability = null;
        ReasonOfUnsuitablity = new List<string>() { };

        if (_email.text.Length > 50)// ��������� ����� ������
        {
            ReasonOfUnsuitablity.Add("��� ������ �������� ����� ��������� �����. ����� �� ������ ��������� 50 ��������");
            EmailCheckPassed = false;
        }


        if (Regex.IsMatch(_email.text, @"\.\.") || Regex.IsMatch(_email.text, @"^[&][=][+][<][>][,][`]$"))
        {
            EmailCheckPassed = false;
            ReasonOfUnsuitablity.Add("���� ������� �������� ��� ��������� �����. �� ������ ������������ ������ ��������� �����, �����, ������� �������������, ����� � ����� �����.");
        }
        else
        {
            if (_email.text.Contains(" "))
            {
                EmailCheckPassed = false;
                ReasonOfUnsuitablity.Add("���� ������� �������� ��� ��������� �����. �� ������ ������������ ������ ��������� �����, �����, ������� �������������, ����� � ����� �����.");
            }
            else
            {
                if (Regex.IsMatch(_email.text, @"^[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+$") != true)
                {
                    EmailCheckPassed = false;
                    ReasonOfUnsuitablity.Add("���� ������� �������� ��� ��������� �����. �� ������ ������������ ������ ��������� �����, �����, ������� �������������, ����� � ����� �����.");
                }
            }
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
                ReasonOfUnsuitablity.Add("���� ������� �������� ��� ��������� �����. ��� ��������� ����� ������ ����� ���� ������ @.");
            }
        }

        /*
        if (_email.text.Contains('.') == false) // ��������� ������� ������ ������� .
        {
            EmailCheckPassed = false;
        }
        else
        {
            if (_email.text.IndexOf('.') <= _email.text.IndexOf('@') ||
                _email.text.IndexOf('.') >= _email.text.Length - 1) // ���������, ��� �� � ����� . ���� ���� �� ���� ���������� ������
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
        ROUEM_InUnityObj.text = OutUnsuitability; //�����, ��� ����� �� ��������
        SetActiveButton();
    }

    public void PassCheckingField()
    {
        //�������� ������ �� ����������� ����������� 
        bool IsLetter = true;
        OutUnsuitability = null;
        ReasonOfUnsuitablity = new List<string>();
        PassFieldCheckPassed = true;

        if (_pass.text.Length < 6 || _pass.text.Length > 15)
        {
            ReasonOfUnsuitablity.Add("��� ������ �������� ������. ������ ������ ����� ����� �� 6 �� 15 �������.");
            PassFieldCheckPassed = false;
        }

        string _allowedCharsInPass = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789~!?@#$%^&*_-+()[]{}></\\|\"'.,:;";

        if (_pass.text.All(c => _allowedCharsInPass.Contains(c)) == false)
        {
            ReasonOfUnsuitablity.Add("��� ������ �������� ������. �� ������ ������ � ���� ��������� �����, ��� ������� ���� �����, ��� ������� ���� ��������� � ���� �������� �����, �����������.");
            IsLetter = false;
            PassFieldCheckPassed = false;
        }

        if (_pass.text.Any(char.IsLetter) == false)
        {
            ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 �������� � 1 ��������� �����");
            IsLetter = false;
            PassFieldCheckPassed = false;
        }
        else
        {
            if (_pass.text.Any(char.IsLower) == false && IsLetter == true)
            {
                ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 �������� �����");
                PassFieldCheckPassed = false;
            }


            if (_pass.text.Any(char.IsUpper) == false && IsLetter == true)
            {
                ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 ��������� �����");
                PassFieldCheckPassed = false;
            }
        }

        if (_pass.text.Any(char.IsDigit) == false)
        {
            ReasonOfUnsuitablity.Add("*������ ������ ��������� ����-�� 1 �����");
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
            ROUP_InUnityObj.text = OutUnsuitability; //����� ������ ������ �� ��������
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
