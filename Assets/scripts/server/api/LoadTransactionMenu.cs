using DataNamespace;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

public class LoadTransactionMenu : MonoBehaviour
{
	public string Url = "http://195.2.79.241:5000/api_app/transactions";

	public int WalletIdd;

	public GameObject itemPrefab;// Prefab элемента списка

	public GameObject content;// Объект, содержащий элементы списка

	public class WalletId
	{
		public int id_wallet;
	}

	public void LoadTranasactionMenu()
	{
		WalletId x = new WalletId();
		x.id_wallet = WalletIdd;
		string userDataString = JsonUtility.ToJson(x);
		byte[] userDataRaw = Encoding.UTF8.GetBytes(userDataString);
		StartCoroutine(TransactionData(userDataRaw));
	}
	IEnumerator TransactionData(byte[] x)
	{
		UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(Url, "POST");
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
			transactions transactionsDataOBJ = JsonUtility.FromJson<transactions>(webRequest.downloadHandler.text);
			if (transactionsDataOBJ.page0.Count != 0)
			{
				FillTransactions(transactionsDataOBJ);
			}
		}
	}

	public void FillTransactions(transactions transactions)
	{
		for (int i = 0; i < transactions.page0.Count; i++)
		{
			GameObject item = Instantiate(itemPrefab, content.transform);
			item.GetComponent<TransactionListItem>().transaction = transactions.page0[i];
			string AmountText = null;
			switch (transactions.page0[i].type)
			{
				case "income":
                    AmountText = "+" + transactions.page0[i].amount;
					item.GetComponent<TransactionListItem>().ChangeAmountColor("income");
                    item.GetComponent<TransactionListItem>().Name.text = "Доход";
                    break;

				case "expense":
					AmountText = "-" + transactions.page0[i].amount;
                    item.GetComponent<TransactionListItem>().ChangeAmountColor("expense");
                    item.GetComponent<TransactionListItem>().Name.text = "Трата";
                    break;

                default:
					break;
			}
            item.GetComponent<TransactionListItem>().Amount.text = AmountText;
        }
	}

    public void DeleteList()
    {
        foreach (Transform child in content.GetComponent<Transform>()) Destroy(child.gameObject);
    }
}
