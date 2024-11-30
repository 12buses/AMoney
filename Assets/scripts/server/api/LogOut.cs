using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using static System.Net.WebRequestMethods;

public class LogOut : MonoBehaviour
{
    public string Url = "http://195.2.79.241:5000/api_app/user_deauthorize";
    public string SceneName;

    public void LogOutAndGo()
    {
        StartCoroutine(LogOutAndGoCor());
        IEnumerator LogOutAndGoCor()
        {
            UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(Url, "POST");
            // отправка запроса
            yield return webRequest.SendWebRequest();
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка: " + webRequest.error);
            }
            else
            {
                SceneManager.LoadScene(SceneName);
            }
        }
    }
}
