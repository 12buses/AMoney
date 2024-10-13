using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

using DataNamespace;


public class ServerSpeaking : MonoBehaviour
{
	public User userDataObj;
	public OurUser OurUserObj;

	public InputField _login; //ник
	public InputField _email; // поле для ввода почты
	public InputField _pass; //поле для ввода пароля 

	private string url = "http://195.2.79.241:5000/api/check_user";


	public Text ROU_InUnityObj; //объект с текстом объясняющий почему данные не подходят



	public void Register()//Нажатие на кнопку регестрации
	{
		userDataObj = new User(_login.text, _email.text);
		string userDataString = JsonUtility.ToJson(userDataObj);
		byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString); ;
		Debug.Log(userDataString + " userData");
		StartCoroutine(ReturnUsersData(userDataRaw, "UniqueCheck"));
	}


	public void WhenWeGotUniqueCheckResult(string dowloadedText)    
	{
		Debug.Log(dowloadedText + "dowloadedText");
		UniqueCheck UniqueCheck = JsonUtility.FromJson<UniqueCheck>(dowloadedText);
		if (UniqueCheck.EmailIsUnique == false)
		{
			if (UniqueCheck.LoginIsUnique == false)
			{
				ROU_InUnityObj.text = "Проверка прошла успешно, в том числе на уникальность.";
			}
			else
			{
				ROU_InUnityObj.text = "*Ваш логин уже занят, попробуйте придумать другой.";
				OurUserObj = new OurUser(_login.text, _email.text, _pass.text);
				string OurUserDataString = JsonUtility.ToJson(OurUserObj);
				byte[] OurUserDataRaw = Encoding.UTF8.GetBytes(OurUserDataString); ;
				StartCoroutine(ReturnUsersData(OurUserDataRaw, "Register"));
			}   
		}
		else
		{
			ROU_InUnityObj.text = "*Аккаунт на эту почту уже зарегстрирован, попробуйте войти.";
		}
	}


	IEnumerator ReturnUsersData(byte[] userDataRaw, string ReqType)
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
				switch (ReqType)
				{
					case "UniqueCheck":
						WhenWeGotUniqueCheckResult(webRequest.downloadHandler.text);
						break;
					case "Register":
                        Debug.Log(webRequest.downloadHandler.text);
                        break;
                    default :
						break;
				}
			}
		}
	}
}

