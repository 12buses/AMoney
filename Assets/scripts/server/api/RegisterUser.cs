using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataNamespace;
using TMPro;
using static UnityEditor.ShaderData;
using System.Text;
using UnityEditor.VersionControl;

public class RegisterUser : MonoBehaviour
{
    public TMP_InputField Login;
    public TMP_InputField email;
    public TMP_InputField pass;
    public string RegisterUrl = "http://195.2.79.241:5000/api/user_register";

    [System.Serializable]
    public class RegisterData
    {
        public string email;
        public string login;
        public string password;
    }

    public void RegCall(string login, string email, string password)
    {
        StartCoroutine(Register(login, email, password));
    }

    public void Reg()
    {
        StartCoroutine(Register(Login.text, email.text, pass.text));
    }

    IEnumerator Register(string login, string email, string password)
    {
        var userDataObj = new RegisterData()
        {
            email = email,
            login = login,
            password = password
        };
        string userDataString = JsonUtility.ToJson(userDataObj);

        Debug.Log(userDataString + " userData");

        UnityWebRequest request = new UnityWebRequest(RegisterUrl, "POST");
        request.SetRequestHeader("Content-Type", "application/json");

        byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString);
        request.uploadHandler = new UploadHandlerRaw(userDataRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        // ????????? ??????? ??????
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка:" + request.error);
        }
        else
        {
            Debug.Log("Успешная регистрация");
        }
    }
}
