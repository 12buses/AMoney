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

		StartCoroutine(TransactionData(userDataString));
	}
	IEnumerator TransactionData(string postJSON)
	{
		UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(Url, "POST");
		webRequest.SetRequestHeader("Content-Type", "application/json");

        byte[] userDataRaw = Encoding.UTF8.GetBytes(postJSON);
        webRequest.uploadHandler = new UploadHandlerRaw(userDataRaw);
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
			if (transactionsDataOBJ.page0.Count > 0)
			{
				FillTransactions(transactionsDataOBJ);
			}
		}
	}

	public void FillTransactions(transactions transactions)
	{
		foreach (var current_transaction in transactions.page0)
		{
			GameObject item = Instantiate(itemPrefab, content.transform);
			item.GetComponent<TransactionListItem>().transaction = current_transaction;
			string AmountText = null;
			switch (current_transaction.type)
			{
				case "income":
                    AmountText = "+" + current_transaction.amount;
					item.GetComponent<TransactionListItem>().ChangeAmountColor("income");
                    item.GetComponent<TransactionListItem>().Name.text = "Доход";
                    break;

				case "expense":
					AmountText = "-" + current_transaction.amount;
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
