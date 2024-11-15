using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class deleteInfo : MonoBehaviour
{
    public int id_wallet;
    public GameObject nextPopUp;
    public string Url = "http://195.2.79.241:5000/api_app/delete_wallet";
    public GameObject ReloadMainSceneOBJ;

    [System.Serializable]
    public class DeleteClass
    {
        public int id_wallet;
    }

    public DeleteClass deleteOBJ;

    public void Delete()
    {
        StartCoroutine(delete());
        IEnumerator delete()
        {
            UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(Url, "POST");
            webRequest.SetRequestHeader("Content-Type", "application/json");
            deleteOBJ.id_wallet = id_wallet;
            string WalletId = JsonUtility.ToJson(deleteOBJ);
            byte[] WalletIdRaw = Encoding.UTF8.GetBytes(WalletId);
            Debug.Log(WalletId);
            webRequest.uploadHandler = new UploadHandlerRaw(WalletIdRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            yield return webRequest.SendWebRequest();
            ReloadMainSceneOBJ.GetComponent<LoadMainMenu>().Reload();
            this.gameObject.SetActive(false);
            nextPopUp.SetActive(true);
        }
    }
}
