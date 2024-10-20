 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class GetUserInfo : MonoBehaviour
{
    public string Url = "http://195.2.79.241:5000/api/data ";


    void GetInfo()
    {
        StartCoroutine(GetInfo());
        IEnumerator GetInfo()
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(Url))
            {
                // отправка запроса
                yield return webRequest.SendWebRequest();
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.LogError("Ошибка: " + webRequest.error);
                }
                else
                {
                    Debug.Log(webRequest.downloadHandler.text);

                }
            }
        }
    }
}
