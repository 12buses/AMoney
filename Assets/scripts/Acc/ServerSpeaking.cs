using System.Collections;
using System.Collections.Generic;
using System.Text;

using UnityEngine;
using UnityEngine.Networking;

using DataNamespace;


public class ServerSpeaking : MonoBehaviour
{

    private string url = "http://195.2.79.241:5000/api/check_user";

    public delegate void UniqieUserCheckEndDelegate(UniqueCheck Unique);
    public event UniqieUserCheckEndDelegate UniqieUserCheckEnd;



    public void UniqieUserCheck(string login, string email)
    {
        //������ �������
        UniqieUserCheckEnd += GetComponent<LoginRegister>().WhenWeGotUniqueCheckResult;
        User userDataClass = new User(login, email);
        Debug.Log(userDataClass.Name + "userDataClass.Name");
        Debug.Log(userDataClass.Mail + "userDataClass.Mail");
        string userDataString = JsonUtility.ToJson(userDataClass);
        byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString); ;
        Debug.Log(userDataString + " userData");
        StartCoroutine(ReturnUsersData(userDataRaw));
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
                // �������� ������ � ������������ ������
                ResultToJson(webRequest.downloadHandler.text);
            }
        }
    }
    private void ResultToJson(string dowloadedText)
    {
        Debug.Log(dowloadedText + "dowloadedText");
        UniqueCheck UniqueCheck = JsonUtility.FromJson<UniqueCheck>(dowloadedText);
        UniqieUserCheckEnd(UniqueCheck);
    }
}

