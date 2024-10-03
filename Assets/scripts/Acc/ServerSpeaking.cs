using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.Networking;

using Newtonsoft.Json;

public class ServerSpeaking : MonoBehaviour
{

    private string url = "http://195.2.79.241:5000/api/all_data";

    public class Root
    {
        public List<string> all_emails { get; set; }
        public List<string> all_logins { get; set; }
    }

    public  bool Unique = false;

    public void UniqieUserCheck()
    {
        //������ �������
        StartCoroutine(ReturnUsersData());
    }

    IEnumerator ReturnUsersData()
    {

        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))//��������� ������ � �������� ������������ ������ ������������ 
        {
            // ���������� ������
            yield return webRequest.SendWebRequest();
            // ��������� ������� ������
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("������: " + webRequest.error);
            }
            else
            {
                // �������� ������ � ������������ ������
                string jsonResult = webRequest.downloadHandler.text;
                Debug.Log(jsonResult);
                Root JSONRES = JsonConvert.DeserializeObject<Root>(jsonResult);
                StartUniqieUserCheck(JSONRES);
            }
        }
    }

    //�������� �� ������������
    void StartUniqieUserCheck(Root JSONRES)
    {
        Unique = false;
        foreach (var item in JSONRES.all_emails)
        {
            if (item == GetComponent<LoginRegister>()._email.text)
            {
                Unique = true;
            }
        }

        foreach (var item in JSONRES.all_logins)
        {
            if (item == GetComponent<LoginRegister>()._nick.text)
            {
                Unique = true;
            }
        }
        Debug.Log(Unique + "Unique");
    }
}
