using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataNamespace;
using TMPro;
using System;
using UnityEngine.UI;

public class TransactionListItem : MonoBehaviour
{
    public GameObject DeletePopUp;
    public GameObject EditTransactionScene;
    public LoadTransactionMenu MainTransactionMenu;

    public transaction transaction;

    public string DeleteTransactionURL = "http://195.2.79.241:5000/api_app/transaction_delete";

    public TMP_Text Category;
    public TMP_Text Amount;
    public TMP_Text Date;
    public TMP_Text Comment;
    public Button deleteButton;
    [System.Serializable]
    public class Root
    {
        public int id_wallet;
        public int id_transaction;
    }

    public void ShowDeletePopUp()
    {
        DeletePopUp.SetActive(true);
        DeletePopUp.GetComponent<deleteButton>().button.onClick.AddListener(OnDeleteButtonClicked);
    }

    public void OnDeleteButtonClicked()
    {
        Root Root = new Root();
        Root.id_wallet = transaction.id_wallet;
        Root.id_transaction = transaction.id_transaction;
        string RootDataString = JsonUtility.ToJson(Root);
        Req req = gameObject.AddComponent<Req>();
        req.PostReq(RootDataString, DeleteTransactionURL, result => reqSuccess(), error => reqUnsuccess());
    }

    public void reqSuccess()
    {
        MainTransactionMenu.Reload();
        DeletePopUp.SetActive(true);
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

    public void EnterEdit()
    {
        EditTransactionScene.SetActive(true);
        EditTransactionScene.GetComponent<OnChangeOperationType>().OnEnterAdd();
        EditTransaction EditTransaction = EditTransactionScene.GetComponent<EditTransaction>();
        EditTransaction.WalletId = transaction.id_wallet.ToString();
        EditTransaction.TransactionId = transaction.id_transaction.ToString();
        TransactionCheckInputField TransactionCheckInputField = EditTransactionScene.GetComponent<TransactionCheckInputField>();
        TransactionCheckInputField.Date.text = transaction.FormattedData_of_transaction;
        if (transaction.type == "expense")
        {
            TransactionCheckInputField.Type.value = 1;
            for (int i = 0; i < EditTransactionScene.GetComponent<categories>().categoriesObject.expense.Count; i++)
            {
                if (EditTransactionScene.GetComponent<categories>().categoriesObject.expense[i].id_category.ToString() == transaction.id_category)
                {
                    TransactionCheckInputField.Cattegory.value = i;
                }
            }
        }
        else
        {
            TransactionCheckInputField.Type.value = 0;
            for (int i = 0; i < EditTransactionScene.GetComponent<categories>().categoriesObject.income.Count; i++)
            {
                if (EditTransactionScene.GetComponent<categories>().categoriesObject.income[i].id_category.ToString() == transaction.id_category)
                {
                    TransactionCheckInputField.Cattegory.value = i;
                }
            }
        }
        TransactionCheckInputField.Amount.text = transaction.amount.ToString("0.00");
        TransactionCheckInputField.Comment.text = transaction.comment;
    }
}
