using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;
using TMPro;
using UnityEngine.SceneManagement;
using DataNamespace;
using System;
using static UnityEngine.Networking.UnityWebRequest;
using System.Net;


public class AuthPGetInfo : MonoBehaviour
{
    public TMP_InputField login;
    public TMP_InputField password;
    public string SceneName;
    public TMP_Text ROUE_InUnityObj;
    public TMP_Text ROUL_InUnityObj;

    public string URLAuth = "http://195.2.79.241:5000/api_app/user_authorize";

    public class User //класс пользователея
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
        Req req = gameObject.AddComponent<Req>();
        req.PostReq(userDataString, URLAuth, result => reqSuccess(result), error => reqUnsuccess());
    }

    public void reqSuccess(string result)
    {
        Debug.Log("Запрос аунтефикации: успешно.");
        User UserAuthResult = JsonUtility.FromJson<User>(result);
        if (UserAuthResult.login == "False" || UserAuthResult.password == "False")
        {
            Debug.Log(result);
            if (UserAuthResult.login == "False")
            {
                ROUL_InUnityObj.text = "*Такого пользавателя не существует.";
            }
            if (UserAuthResult.password == "False")
            {
                ROUE_InUnityObj.text = "*Неверный пароль.";
            }
        }
        else
        {
            Debug.Log("Аунтификация: успешно");
            SceneManager.LoadScene(SceneName);
        }
    }

    public void reqUnsuccess()
    {

    }
}
