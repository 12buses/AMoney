using DataNamespace;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OpenURL : MonoBehaviour
{

   private string GenURL = "http://195.2.79.241:5005/generate_tg_key";


    public void Open()
    {
        Req req = gameObject.AddComponent<Req>();
        TgKeyRequest requestData = new TgKeyRequest { id_user_web = this.GetComponent<GetUserInfo>().GetInfo().id };
        string json = JsonUtility.ToJson(requestData);

        req.PostReq(json, GenURL,
            result => HandleSuccess(result),
            error => HandleError(error));
    }

    private void HandleSuccess(string result)
    {
        try
        {
            TgKeyResponse response = JsonUtility.FromJson<TgKeyResponse>(result);
            Debug.Log("Telegram URL received: " + response.url);
            Application.OpenURL(response.url);
        }
        catch (System.Exception e)
        {
            Debug.LogError("JSON Parse Error: " + e.Message);
        }
    }

    private void HandleError(string error)
    {
        Debug.LogError("Request Failed: " + error);
    }

    [System.Serializable]
    private class TgKeyRequest
    {
        public int id_user_web;
    }

    [System.Serializable]
    private class TgKeyResponse
    {
        public string url;
    }
}

