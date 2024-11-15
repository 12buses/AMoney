using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class EditWallet : MonoBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Balance;
    public TMP_Dropdown Currency;
    public int wallet_id;
    public string URL = "http://195.2.79.241:5000/api_app/wallet_edit";
    [System.Serializable]
    public class WalletEditClass
    {
        public int id_wallet;
        public string name;
        public string balance;
        public string currency;
    }

    public void SaveWalletEdit()
    {
        WalletEditClass WalletEditClassOBJ = new WalletEditClass();
        WalletEditClassOBJ.name = Name.text;
        WalletEditClassOBJ.id_wallet = wallet_id;
        WalletEditClassOBJ.balance = Balance.text;
        WalletEditClassOBJ.currency = Currency.captionText.text;
        string WalletEditClassString = JsonUtility.ToJson(WalletEditClassOBJ);
        byte[] WalletEditDataRaw = Encoding.UTF8.GetBytes(WalletEditClassString);
        StartCoroutine(EditWalletCOR(WalletEditDataRaw));
    }

    IEnumerator EditWalletCOR(byte[] WalletEditDataRaw)
    {
        UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(URL, "POST");
        webRequest.SetRequestHeader("Content-Type", "application/json");

        webRequest.uploadHandler = new UploadHandlerRaw(WalletEditDataRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        // отправка запроса
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка: " + webRequest.error);
        }
        else
        {

        }
    }
}
