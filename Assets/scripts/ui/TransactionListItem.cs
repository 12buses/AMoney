using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataNamespace;
using TMPro;
using System;

public class TransactionListItem : MonoBehaviour
{
    public GameObject EditTransactionScene;
    public LoadTransactionMenu MainTransactionMenu;

    public transaction transaction;

    public string DeleteTransactionURL = "http://195.2.79.241:5000/api_app/transaction_delete";

    public TMP_Text Category;
    public TMP_Text Amount;
    public TMP_Text Date;
    public TMP_Text Comment;

    [System.Serializable]
    public class Root
    {
        public int id_wallet;
        public int id_transaction;
    }

    public void OnDeleteButtonClicked()
    {
        Root Root;
        Root = new Root();
        Root.id_wallet = transaction.id_wallet;
        Root.id_transaction = transaction.id_transaction;
        string RootDataString = JsonUtility.ToJson(Root);
        Req req = gameObject.AddComponent<Req>();
        req.PostReq(RootDataString, DeleteTransactionURL, result => reqSuccess(), error => reqUnsuccess());
    }

    public void reqSuccess()
    {
        MainTransactionMenu.Reload();
    }

    public void reqUnsuccess()
    {

    }

    public void ChangeAmountColor(string type)
    {
        if(type == "expense")
        {
            Amount.color = Color.red;
        }
        else
        {
            Amount.color = Color.green;
        }
    }

    public void LoadTransactionEditMenu()
    {
        EditTransactionScene.GetComponent<EditTransaction>().WalletId = transaction.id_wallet.ToString();
        EditTransactionScene.GetComponent<EditTransaction>().TransactionId = transaction.id_transaction.ToString();
    }
}
