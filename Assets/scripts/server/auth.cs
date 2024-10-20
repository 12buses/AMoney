using DataNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEditor.ShaderData;

public class auth : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField password;
    public TMP_Text Text;

    private class User //класс пользователея
    {
        public string login;
        public string password;
        public User(string name, string password)
        {
            login = name;
            this.password = password;

        }
    }

    public void authorization()
    {
        User userDataObj = new User(login.text, password.text);
        string userDataString = JsonUtility.ToJson(userDataObj);
        byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString);
        StartCoroutine(Register(userDataRaw));
        IEnumerator Register(byte[] userDataRaw)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm("http://195.2.79.241:5000/api/user_register", "POST"))//��������� ������ � �������� ������������ ������ ������������ 
            {
                // ���������� ������
                webRequest.uploadHandler = new UploadHandlerRaw(userDataRaw);
                webRequest.downloadHandler = new DownloadHandlerBuffer();
                webRequest.SetRequestHeader("Content-Type", "application/json");
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Text.text = "Неверный логин или пароль.";
                    Debug.LogError("Ошибка: неверный логин или пароль.");
                }
                else
                {
                    Debug.Log(webRequest.downloadHandler.text);

                }
            }
        }
    }
}
