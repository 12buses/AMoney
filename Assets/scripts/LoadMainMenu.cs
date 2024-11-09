using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using DataNamespace;
using UnityEngine.Networking;
using TMPro;

public class LoadMainMenu : MonoBehaviour
{
    public TMP_Text HelloText;
    public GameObject NoWallets;
    public GameObject WalletsTrue;

    public string WalletsDataUrl = "http://195.2.79.241:5000/api/userWallets";
    
    public class UserId
    {
        public int id;
    }

    void Start()
    {
        GetComponent<GetUserInfo>().InfoReq();
        Invoke("SetHelloText", 0.5f);
    }

    void SetHelloText()
    {
        Info UserInfo = GetComponent<GetUserInfo>().GetInfo();
        HelloText.text = "Привет,  " + UserInfo.login + "!";
        UserId x = new UserId();
        x.id = UserInfo.id;
        string userDataString = JsonUtility.ToJson(x);
        byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString);
        StartCoroutine(GetWalletsData(userDataRaw));
    }

    
    IEnumerator GetWalletsData(byte[] x)
    {
        UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(WalletsDataUrl, "POST");
        webRequest.SetRequestHeader("Content-Type", "application/json");

        webRequest.uploadHandler = new UploadHandlerRaw(x);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        // отправка запроса
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка: " + webRequest.error);
        }
        else
        {
            Debug.Log(webRequest.downloadHandler.text);
            WalletsData WalletsDataOBJ = JsonUtility.FromJson<WalletsData>(webRequest.downloadHandler.text);
            if(WalletsDataOBJ.wallets.Count == 0)
            {
                NoWallets.SetActive(true);
                WalletsTrue.SetActive(false);
            }
            else
            {
                NoWallets.SetActive(false);
                WalletsTrue.SetActive(true);
                GetComponent<WalletList>().FillList(WalletsDataOBJ);
            }
        }
    }
}
