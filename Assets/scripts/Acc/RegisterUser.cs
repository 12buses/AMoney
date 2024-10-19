using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using DataNamespace;
using TMPro;
using static UnityEditor.ShaderData;
using System.Text;
using UnityEditor.VersionControl;

public class RegisterUser : MonoBehaviour
{
    public TMP_InputField Login;
    public TMP_InputField email;
    public TMP_InputField pass;


    public void RegCall(byte[] userDataRaw)
    {
        StartCoroutine(Register(userDataRaw));
    }

    public void Reg()
    {
        OurUser userDataObj = new OurUser(Login.text, email.text, pass.text);
        string userDataString = JsonUtility.ToJson(userDataObj);
        byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString);
        StartCoroutine(Register(userDataRaw));
    }

    IEnumerator Register(byte[] userDataRaw)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.PostWwwForm("http://195.2.79.241:5000/api/user_register", "POST"))//��������� ������ � �������� ������������ ������ ������������ 
        {
            // ���������� ������
            webRequest.uploadHandler = new UploadHandlerRaw(userDataRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            yield return webRequest.SendWebRequest();
            /*
            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("Ошибка:" + webRequest.error);
            }
            */
        }
    }
}
