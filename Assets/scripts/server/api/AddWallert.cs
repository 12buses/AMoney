using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using static UnityEngine.Networking.UnityWebRequest;

public class AddWallert : MonoBehaviour
{
    public TMP_InputField Name;
    public TMP_InputField Balance;
    public TMP_Dropdown Currency;
    public GameObject OBJWithReloadSceneScript;
    public GameObject CreateWalletCanvas;
    public GameObject MainCanvas;
    public TMP_Text ErrorText;
    public TMP_Text WalletCreatedText;


    public string Url = "http://195.2.79.241:5000/api_app/add_wallet";

    [System.Serializable]
    public class Wallet
    {
        public string name;
        public string balance;
        public string currency;
    }


    [System.Serializable]
    public class ServerResponseAddWallet
    {
        public string existance;
        public string creation;
    }

    public void AddWallet()
    {
        StartCoroutine(AddWalletCor());

        IEnumerator AddWalletCor()
        {
            Wallet wallet = new Wallet();
            wallet = new Wallet();
            wallet.name = Name.text;
            wallet.balance = Balance.text;
            wallet.currency = Currency.captionText.text;
            string WalletDataString = JsonUtility.ToJson(wallet);
            byte[] WalletDataRaw = Encoding.UTF8.GetBytes(WalletDataString);

            using UnityWebRequest request = new UnityWebRequest(Url, "POST");
            {
                request.SetRequestHeader("Content-Type", "application/json");

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
                    ServerResponseAddWallet response = JsonUtility.FromJson<ServerResponseAddWallet>(request.downloadHandler.text);
                    Debug.Log(response.existance + " " + response.creation);
                    if(response.existance == "True" && response.creation == "True")
                    {
                        MainCanvas.SetActive(true);
                        ErrorText.text = "";
                        CreateWalletCanvas.SetActive(false);
                        WalletCreatedText.text = "Кошелек " + Name.text + " - создан!";
                    }
                    else
                    {
                        ErrorText.text = "Было введено неправильное название кошелька. Название кошелька должно быть уникальным.";
                    }
                }
            }
        }
    }

    
}
