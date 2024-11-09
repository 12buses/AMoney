using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class AddWallert : MonoBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Balance;
    public TMP_Dropdown Currency;

    public string Url = "http://195.2.79.241:5000/api/add_wallet";

    [System.Serializable]
    public class Wallet
    {
        public string name;
        public string balance;
        public string currency;
    }

    public void AddWallet()
    {
        StartCoroutine(AddWalletCor());
    }
    IEnumerator AddWalletCor()
    {
        UnityWebRequest request = new UnityWebRequest(Url, "POST");
        request.SetRequestHeader("Content-Type", "application/json");
        Wallet wallet = new Wallet();
        wallet = new Wallet();
        wallet.name = Name.text;
        wallet.balance = Balance.text;
        wallet.currency = Currency.captionText.text;
        string WalletDataString = JsonUtility.ToJson(wallet);
        byte[] WalletDataRaw = Encoding.UTF8.GetBytes(WalletDataString);
        request.uploadHandler = new UploadHandlerRaw(WalletDataRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();
        // ????????? ??????? ??????
        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка:" + request.error);
        }
        else
        {

        }
    }

}
