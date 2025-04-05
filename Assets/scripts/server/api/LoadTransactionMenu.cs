using DataNamespace;
using System.Collections;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadTransactionMenu : MonoBehaviour
{
	public string Url = "http://195.2.79.241:5000/api_app/transactions";
	public string UrlForCategories = "http://195.2.79.241:5000/api_app/categories";

    public int WalletIdd;

    public GameObject CreateTransactionObject;
	public GameObject itemPrefab;// Prefab элемента списка
	public GameObject content;// Объект, содержащий элементы списка
    public GameObject TransactionEditMenu;
    public GameObject DeletePopUp;
    public GameObject CreateTransactionButton;
    public GameObject LokingForTransactionIcon;
    public GameObject FullCommentPopUp;

    public TMP_Text WholeIncome;
	public TMP_Text WholeExpense;
    public TMP_Text Balance;
    public TMP_Text FullCommentText;

	public class WalletId
	{
		public int id_wallet;
	}

	
    public void Reload()
    {
		try { foreach (Transform child in content.GetComponent<Transform>()) Destroy(child.gameObject); } catch { }
		LoadTranasactionMenu();
    }

    public void LoadTranasactionMenu()
	{
		CreateTransactionButton.GetComponent<AddTransactions>().WalletId = WalletIdd.ToString();
        WalletId x = new WalletId();
		x.id_wallet = WalletIdd;
		string userDataString = JsonUtility.ToJson(x);
		Debug.Log(userDataString);
        Req req = gameObject.AddComponent<Req>();
        req.PostReq(userDataString, UrlForCategories, result => CategoryReqSuccess(result), error => CategoryReqUnsuccess());
        req.PostReq(userDataString, Url, result => TransactinDataReqOnSuccess(result), error => TransactinDataReqOnUnuccess());
    }
    public void CategoryReqSuccess(string resultText)
    {
        Categories categories = JsonUtility.FromJson<Categories>(resultText);
        CreateTransactionObject.GetComponent<categories>().categoriesObject = categories;
        TransactionEditMenu.GetComponent<categories>().categoriesObject = categories;
        Debug.Log("Categories!!!!!!" + resultText);
    }

    public void CategoryReqUnsuccess()
    { }

    public void TransactinDataReqOnSuccess(string resultText)
    {
        Debug.Log($"answer: {resultText}");
        transactionsDataOBJ transactionsDataOBJ = JsonUtility.FromJson<transactionsDataOBJ>(resultText);
        Balance.text = "Твой баланс: " + transactionsDataOBJ.balance.ToString("0.00");
        WholeExpense.text = transactionsDataOBJ.expense.ToString("0.00");
        WholeIncome.text = transactionsDataOBJ.income.ToString("0.00");
        if (transactionsDataOBJ.page0.Count > 0)
        {
            IfCountTransactionMoreThan0();
        }

        void IfCountTransactionMoreThan0()
        {
#if true

#endif
            LokingForTransactionIcon.SetActive(false);
            Debug.Log(transactionsDataOBJ.income);

            transactionsDataOBJ.ConvertSecondsToDate();

            foreach (var current_transaction in transactionsDataOBJ.page0)
            {
                GameObject item = Instantiate(itemPrefab, content.transform);
                item.GetComponent<TransactionListItem>().transaction = current_transaction;
                string AmountText = null;

                item.GetComponent<TransactionListItem>().EditTransactionScene = TransactionEditMenu;
                item.GetComponent<TransactionListItem>().MainTransactionMenu = this;

                switch (current_transaction.type)
                {
                    case "income":
                        AmountText = "+" + current_transaction.amount;
                        item.GetComponent<TransactionListItem>().ChangeAmountColor("income");
                        item.GetComponent<TransactionListItem>().Category.text = "Доход";
                        break;

                    case "expense":
                        AmountText = "-" + current_transaction.amount;
                        item.GetComponent<TransactionListItem>().ChangeAmountColor("expense");
                        item.GetComponent<TransactionListItem>().Category.text = "Трата";
                        break;

                    default:
                        break;
                }

                item.GetComponent<TransactionListItem>().Amount.text = AmountText;
                item.GetComponent<TransactionListItem>().DeletePopUp = DeletePopUp;
                string FormatedComment = current_transaction.comment;
                if (FormatedComment.Length > 12) { FormatedComment = FormatedComment.Remove(12); FormatedComment = FormatedComment + "..."; item.GetComponent<TransactionListItem>().FullCommentButton.SetActive(true); }
                else { item.GetComponent<TransactionListItem>().FullCommentButton.SetActive(false); }
                item.GetComponent<TransactionListItem>().Comment.text = FormatedComment;
                item.GetComponent<TransactionListItem>().FullComment = FullCommentText;
                item.GetComponent<TransactionListItem>().Date.text = current_transaction.FormattedData_of_transaction;
                item.GetComponent<TransactionListItem>().FullCommentPopUp = FullCommentPopUp;
            }
        }
    }

    public void TransactinDataReqOnUnuccess()
    {

    }

    
}
