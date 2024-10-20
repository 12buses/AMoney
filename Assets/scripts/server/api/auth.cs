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

	public TMP_Text ROUE_InUnityObj;
    public TMP_Text ROUL_InUnityObj;

    public string URL = "http://195.2.79.241:5000/api/user_authorize";

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
		byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString);
		StartCoroutine(Register(userDataRaw));
		IEnumerator Register(byte[] userDataRaw)
		{
			using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(URL, "POST"))//��������� ������ � �������� ������������ ������ ������������ 
			{
				// ���������� ������
				webRequest.SetRequestHeader("Content-Type", "application/json");

				webRequest.uploadHandler = new UploadHandlerRaw(userDataRaw);
				webRequest.downloadHandler = new DownloadHandlerBuffer();
				
				yield return webRequest.SendWebRequest();
				if (webRequest.result != UnityWebRequest.Result.Success)
				{
					Debug.LogError("Ошибка: " + webRequest.error);
				}
				else
				{
					Debug.Log("Запрос аунтефикации: успешно.");
					User UserAuthResult = JsonUtility.FromJson<User>(webRequest.downloadHandler.text);
					if (UserAuthResult.login == "False" || UserAuthResult.password == "False")
					{
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
						Debug.Log("Аунтификация: успешно?");
					}
				}
			}
		}
	}
}
