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

	public string url = "http://195.2.79.241:5000/api/check_user"; //api url

	public TMP_Text ROUL_InUnityObj; //объект с текстом объясняющий почему данные не подходят
	public TMP_Text ROUE_InUnityObj; //объект с текстом объясняющий почему данные не подходят

	public UniqueCheck UniqueCheckResultObj;

	public void CliclOnButton()
	{
		string username = _login.text;
		string email = _email.text;
		StartCoroutine(CheckUser(username, email));
	}

	IEnumerator CheckUser(string username, string email)
	{

		User userDataObj = new User(username, email);
		string userDataString = JsonUtility.ToJson(userDataObj);

		Debug.Log(userDataString + " userData");

		UnityWebRequest request = new UnityWebRequest(url, "POST");
		request.SetRequestHeader("Content-Type", "application/json");

		byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString);
		request.uploadHandler = new UploadHandlerRaw(userDataRaw);
		request.downloadHandler = new DownloadHandlerBuffer();

		yield return request.SendWebRequest();
		//отправляем запрос
		if (request.result != UnityWebRequest.Result.Success)
		{
			Debug.LogError("Ошибка:" + request.error);
		}
		else
		{
			UniqueCheck response = JsonUtility.FromJson<UniqueCheck>(request.downloadHandler.text);
			Debug.Log(request.downloadHandler.text);
			if (response.login == "False" || response.email == "False")
			{
				Debug.Log("User zanyat");
				if (response.login == "False")
				{
					ROUL_InUnityObj.text = "Был введен неверный логин. Логин должен быть уникальным";
				}
				if (response.email == "False")
				{
					ROUE_InUnityObj.text = "*Аккаунт с таким email уже зарегестрирован.";
				}
			}
			else
			{
				Debug.Log("Проверка на уникальность прошла успешно");
			}
		}
	}
}

