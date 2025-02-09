using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using UnityEngine.UI;
using System.Xml.Linq;
using System.Globalization;
using System;
using System.Net;
using DataNamespace;

public class AddTransactions : MonoBehaviour
{
	public GameObject AddTransactionMenu;

	public string WalletId;

	public TMP_InputField AmountInPutField;
	public TMP_InputField Data;
	public TMP_InputField Comment;

	public TMP_Dropdown Cattegory;
	public TMP_Dropdown Type;

	public Button buttonCreate;

	public string Url = "http://195.2.79.241:5000/api_app/transaction_add";

	[System.Serializable]
	public class Transactions
	{
        public string id_wallet;
        public int id_category;
		public string amount;
		public string type;
		public string comment;
		public string data_of_transaction;
	}

	public void CreateTransaction()
	{
        buttonCreate.interactable = false;
        if (Cattegory.value == -1 || Type.value == 0)
		{

		}

		Transactions Transaction;
		Transaction = new Transactions();
		Transaction.amount = AmountInPutField.text;

        DateTime dateTime = DateTime.ParseExact(Data.text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        long unixTimestamp = (long)(dateTime - new DateTime(1970, 1, 2)).TotalSeconds;
        Transaction.data_of_transaction = unixTimestamp.ToString();
		Debug.Log(unixTimestamp.ToString());

		Transaction.comment = Comment.text;
		

		string ConvertedType;
		switch (Type.value)
		{
			case 0:
				ConvertedType = "income";
				foreach(Category category in AddTransactionMenu.GetComponent<categories>().categoriesObject.income)
				{
					if(Cattegory.captionText.text == category.name)
					{
						Transaction.id_category = category.id_category;
						break;
                    }
				}
                break;

			case 1:
				ConvertedType = "expense";
                foreach (Category category in AddTransactionMenu.GetComponent<categories>().categoriesObject.expense)
                {
                    if (Cattegory.captionText.text == category.name)
                    {
                        Transaction.id_category = category.id_category;
						break;
                    }
                }
                break;

			default:
				ConvertedType = " ";
                break;
		}
        Transaction.type = ConvertedType;

		Transaction.id_wallet = WalletId;
		string TransactionDataString = JsonUtility.ToJson(Transaction);
		Debug.Log(TransactionDataString);
		StartCoroutine(CreateTransactionCor(TransactionDataString));

		IEnumerator CreateTransactionCor(string TransactionDataString)
		{
			UnityWebRequest request = new UnityWebRequest(Url, "POST");
			request.SetRequestHeader("Content-Type", "application/json");

            byte[] TransactionDataRaw = Encoding.UTF8.GetBytes(TransactionDataString);
            request.uploadHandler = new UploadHandlerRaw(TransactionDataRaw);
			request.downloadHandler = new DownloadHandlerBuffer();

			yield return request.SendWebRequest();
            Debug.Log("Answer: " + request.downloadHandler.text);

            if (request.result != UnityWebRequest.Result.Success)
			{
				Debug.LogError("Œ¯Ë·Í‡:" + request.error);
			}
			else
			{
				AmountInPutField.text = null;
				Data.text = null;
				Comment.text = null;
				Cattegory.value = -1;
				Type.value = -1;
				buttonCreate.interactable = true;
				AddTransactionMenu.SetActive(false);
            }
		}
	}
}
