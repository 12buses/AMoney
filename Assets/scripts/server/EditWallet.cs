using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class EditWallet : MonoBehaviour
{
    public GameObject NameOfWallet;
    public TMP_InputField Name;
    public TMP_InputField Balance;
    public TMP_Dropdown Currency;
    public int wallet_id;
    public string URL = "http://195.2.79.241:5000/api_app/wallet_edit";
    public GameObject OBJWithReloadSceneScript;
    public GameObject CreateWalletCanvas;
    public GameObject MainCanvas;
    public TMP_Text ErrorText;
    public Sprite InputFieldWrong;
    public Sprite InputFieldOk;

    [System.Serializable]
    public class WalletEditClass
    {
        public int id_wallet;
        public string name;
        public string balance;
        public string currency;
    }
    [System.Serializable]
    public class ServerResponseAddWallet
    {
        public string edition;
        public string existance;
    }

    public void SaveWalletEdit()
    {
        WalletEditClass WalletEditClassOBJ = new WalletEditClass();
        WalletEditClassOBJ.name = Name.text;
        WalletEditClassOBJ.id_wallet = wallet_id;
        WalletEditClassOBJ.balance = Balance.text;
        WalletEditClassOBJ.currency = Currency.captionText.text;
        string WalletEditClassString = JsonUtility.ToJson(WalletEditClassOBJ);
        Debug.Log("Json: " + WalletEditClassString);
        byte[] WalletEditDataRaw = Encoding.UTF8.GetBytes(WalletEditClassString);
        StartCoroutine(EditWalletCOR(WalletEditDataRaw));
    }

    IEnumerator EditWalletCOR(byte[] WalletEditDataRaw)
    {
        UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(URL, "POST");
        webRequest.SetRequestHeader("Content-Type", "application/json");

        webRequest.uploadHandler = new UploadHandlerRaw(WalletEditDataRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();

        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка: " + webRequest.error);
        }
        else
        {
            ServerResponseAddWallet response = JsonUtility.FromJson<ServerResponseAddWallet>(webRequest.downloadHandler.text);
            Debug.Log(webRequest.downloadHandler.text);
            Debug.Log($"existance: {response.existance} edition:{response.edition}");
            if (response.existance == "True" && response.edition == "True")
            {
                MainCanvas.SetActive(true);
                ErrorText.text = "";
                CreateWalletCanvas.SetActive(false);
                NameOfWallet.GetComponent<Image>().sprite = InputFieldOk;
            }
            else
            {
                ErrorText.text = "Было введено неправильное название кошелька. Название кошелька должно быть уникальным";

                NameOfWallet.GetComponent<Image>().sprite = InputFieldWrong;
            }
        }
    }
}