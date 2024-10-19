using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

using DataNamespace;


public class CheckUserDataUnique : MonoBehaviour
{
	public TMP_InputField _login; //ник
	public TMP_InputField _email; // поле для ввода почты
	public TMP_InputField _pass; //поле для ввода пароля 

	private string url = "http://195.2.79.241:5000/api/check_user";

	public UniqueCheck UniqueCheckResultObj;

	public TMP_Text ROUL_InUnityObj; //объект с текстом объясняющий почему данные не подходят
	public TMP_Text ROUE_InUnityObj; //объект с текстом объясняющий почему данные не подходят


	public void Register()//Нажатие на кнопку регестрации
	{
		User userDataObj = new User(_login.text, _email.text);
		string userDataString = JsonUtility.ToJson(userDataObj);
		byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString); ;
		Debug.Log(userDataString + " userData");
		StartCoroutine(ReturnUsersData(userDataRaw));
	}


	public void WhenWeGotUniqueCheckResult(string dowloadedText)
	{
		Debug.Log(dowloadedText + "dowloadedText");
		UniqueCheckResultObj = JsonUtility.FromJson<UniqueCheck>(dowloadedText);
		Debug.Log(UniqueCheckResultObj.login + " UniqueCheckResultObj.LoginIsUnique" + UniqueCheckResultObj.email + " UniqueCheckResultObj.EmailIsUnique");
		if (UniqueCheckResultObj.email == true && UniqueCheckResultObj.login == true)
		{ 
			OurUser userDataObj = new OurUser(_login.text, _email.text, _pass.text);
			string userDataString = JsonUtility.ToJson(userDataObj);
			byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString);
			GetComponent<RegisterUser>().RegCall(userDataRaw);
		}
		else
		{
			if (UniqueCheckResultObj.email == false)
			{
				ROUE_InUnityObj.text = "*Аккаунт на эту почту уже зарегстрирован, попробуйте войти.";
			}
			if (UniqueCheckResultObj.login == false)
			{
				ROUL_InUnityObj.text = "*Ваш логин уже занят, попробуйте придумать другой.";
			}
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
				WhenWeGotUniqueCheckResult(webRequest.downloadHandler.text);
			
			}
		}
	}
}

