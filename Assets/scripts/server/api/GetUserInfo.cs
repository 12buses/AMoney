using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataNamespace;


public class GetUserInfo : MonoBehaviour
{

	public string Url = "http://195.2.79.241:5000/api/data ";

	private Info result = new Info();

	public void InfoReq()
	{
		StartCoroutine(GetInfo());
		IEnumerator GetInfo()
		{
			UnityWebRequest webRequest = UnityWebRequest.Get(Url);	
			// отправка запроса
			yield return webRequest.SendWebRequest();
			if (webRequest.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Ошибка: " + webRequest.error);
			}
			else
			{
				Debug.Log(webRequest.downloadHandler.text);
				Info Info = JsonUtility.FromJson<Info>(webRequest.downloadHandler.text);
				SetInfo(Info);
			}
		
		}
		void SetInfo(Info Info)
		{
			result = Info;
        }
	}

	

	public Info GetInfo()
	{
		return result;
	}
}
