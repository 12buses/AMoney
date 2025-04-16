using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataNamespace;
using TMPro;
using UnityEngine.UI;
using System.Globalization;
using System;

public class EditTransaction : MonoBehaviour
{
    public GameObject EditTransactionMenu;
    public GameObject MainTransactionMenu;

    public string WalletId;
    public string TransactionId;

    public TMP_InputField AmountInPutField;
    public TMP_InputField Data;
    public TMP_InputField Comment;

    public TMP_Dropdown Cattegory;
    public TMP_Dropdown Type;

    public Button ButtonEdit;

    public TMP_Text ErrorText;

    public string Url = "http://195.2.79.241:5000/api_app/transaction_edit";

    [System.Serializable]
    public class TransactionEditClass
    {
        public string id_wallet;
        public int id_category;
        public string id_transaction;
        public string amount;
        public string comment;
        public string data_of_transaction;
    }
    public void OnButtonClicked()
    {
        ButtonEdit.interactable = false;

        TransactionEditClass Transaction;
        Transaction = new TransactionEditClass();
        Transaction.amount = AmountInPutField.text;
        Transaction.id_transaction = TransactionId;
        DateTime dateTime = DateTime.ParseExact(Data.text, "dd-MM-yyyy", CultureInfo.InvariantCulture);
        long unixTimestamp = (long)(dateTime - new DateTime(1970, 1, 2)).TotalSeconds;
        Transaction.data_of_transaction = unixTimestamp.ToString();
        Debug.Log(unixTimestamp.ToString());

        Transaction.comment = Comment.text;

        switch (Type.value)
        {
            case 0:
                foreach (Category category in EditTransactionMenu.GetComponent<categories>().categoriesObject.income)
                {
                    if (Cattegory.captionText.text == category.name)
                    {
                        Transaction.id_category = category.id_category;
                        break;
                    }
                }
                break;

            case 1:
                foreach (Category category in EditTransactionMenu.GetComponent<categories>().categoriesObject.expense)
                {
                    if (Cattegory.captionText.text == category.name)
                    {
                        Transaction.id_category = category.id_category;
                        break;
                    }
                }
                break;

            default:
                break;
        }

        Transaction.id_wallet = WalletId;
        string TransactionDataString = JsonUtility.ToJson(Transaction);
        Debug.Log("!!!!!!TransactionDataString!!!!!!!!!!!!!!!!!! " + TransactionDataString);
        Req req = gameObject.AddComponent<Req>();
        req.PostReq(TransactionDataString, Url, result => reqSuccess(), error => reqUnsuccess());
    }

    void reqSuccess()
    {
        ButtonEdit.interactable = true;
        ErrorText.text = "";
        EditTransactionMenu.GetComponent<TransactionCheckInputField>().Clean();
        MainTransactionMenu.SetActive(true);
        EditTransactionMenu.SetActive(false);

    }

    void reqUnsuccess()
    {
        ButtonEdit.interactable = true;
        ErrorText.text = "Во время запроса произошла ошибка, попробуйте ещё раз.";
    }
}
