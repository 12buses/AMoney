using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using DataNamespace;


public class ServerSpeaking : MonoBehaviour
{
    public User userDataClass;

    public InputField _login; //ник
    public InputField _email; // поле для ввода почты

    private string url = "http://195.2.79.241:5000/api/check_user";


    public Text ROU_InUnityObj; //объект с текстом объясняющий почему данные не подходят



    public void Register()//Нажатие на кнопку регестрации
    {
        userDataClass = new User(_login.text, _email.text);
        Debug.Log(userDataClass.Name + "userDataClass.Name");
        Debug.Log(userDataClass.Mail + "userDataClass.Mail");
        string userDataString = JsonUtility.ToJson(userDataClass);
        byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString); ;
        Debug.Log(userDataString + " userData");
        StartCoroutine(ReturnUsersData(userDataRaw));
    }


    public void WhenWeGotUniqueCheckResult(string dowloadedText)    
    {
        Debug.Log(dowloadedText + "dowloadedText");
        UniqueCheck UniqueCheck = JsonUtility.FromJson<UniqueCheck>(dowloadedText);
        if (UniqueCheck.EmailIsUnique == true)
        {
            if (UniqueCheck.LoginIsUnique == true)
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


    IEnumerator ReturnUsersData(byte[] userDataRaw)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(url, "POST"))//��������� ������ � �������� ������������ ������ ������������ 
        {
            // ���������� ������
            webRequest.uploadHandler = new UploadHandlerRaw(userDataRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            // ��������� ������� ������
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка:" + webRequest.error);
            }
            else
            {
                // �������� ������ � ������������ ������
                WhenWeGotUniqueCheckResult(webRequest.downloadHandler.text);
            }
        }
    }
}

