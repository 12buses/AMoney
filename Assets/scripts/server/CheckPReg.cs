using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

using DataNamespace;


public class CheckPReg : MonoBehaviour
{
	public TMP_InputField _login; //ник
	public TMP_InputField _email; // поле для ввода почты
	public TMP_InputField _pass; //поле для ввода пароля 

	public string RegisterUrl = "http://195.2.79.241:5000/api_app/user_register";
	public string url = "http://195.2.79.241:5000/api_app/check_user";

	public UniqueCheck UniqueCheckResultObj;

	public TMP_Text ROUL_InUnityObj; //объект с текстом объясняющий почему данные не подходят
	public TMP_Text ROUE_InUnityObj; //объект с текстом объясняющий почему данные не подходят


	public void Register()//Нажатие на кнопку регестрации
	{
		string username = _login.text;
		string email = _email.text;

		StartCoroutine(CheckUser(username, email));
	}

	void RegisterUser(string email, string login, string password)
	{
		Debug.Log(email + " " + login + " " + password);
		StartCoroutine(WhenWeGotUniqueCheckResult(email, login, password));
	}

	IEnumerator WhenWeGotUniqueCheckResult(string email, string login, string password)
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
            GetComponent<changescene>().ChangeScene("Main");
        }
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
			// ????????? ??????? ??????
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
				if(response.login == "False")
				{
                    ROUL_InUnityObj.text = "*Ваш логин уже занят, попробуйте придумать другой.";
                }
				if(response.email == "False")
				{
                    ROUE_InUnityObj.text = "*Аккаунт с таким email уже зарегестрирован. Попробуйте войти.";
                }
            }
			else
			{
				Debug.Log("REgACtion");
				RegisterUser(_email.text, _login.text, _pass.text);
			}
		}
	}
	[System.Serializable]
	public class RegisterData
	{
		public string email;
		public string login;
		public string password;
	}
}

