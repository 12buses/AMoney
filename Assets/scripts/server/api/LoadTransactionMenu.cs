using DataNamespace;
using System.Collections;
using System.Net;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class LoadTransactionMenu : MonoBehaviour
{
	public string Url = "http://195.2.79.241:5000/api_app/transactions";
	public string UrlForCategories = "http://195.2.79.241:5000/api_app/categories";

    public int WalletIdd;

    public GameObject CreateTransactionObject;
	public GameObject itemPrefab;// Prefab элемента списка
	public GameObject content;// Объект, содержащий элементы списка
    public GameObject TransactionEditMenu;

	public GameObject CreateTransactionButton;

	public TMP_Text WholeIncome;
	public TMP_Text WholeExpense;

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
        foreach (var category in categories.expense)
        {
            category.name = category.name.ToString();
        }

        foreach (var category in categories.income)
        {
            category.name = category.name.ToString();
        }
        CreateTransactionObject.GetComponent<categories>().categoriesObject = categories;
        TransactionEditMenu.GetComponent<categories>().categoriesObject = categories;
    }

    public void CategoryReqUnsuccess()
    { }

    public void TransactinDataReqOnSuccess(string resultText)
    {
        transactionsDataOBJ transactionsDataOBJ = JsonUtility.FromJson<transactionsDataOBJ>(resultText);
        if (transactionsDataOBJ.page0.Count > 0)
        {
            IfCountTransactionMoreThan0();
        }

        void IfCountTransactionMoreThan0()
        {
            WholeExpense.text = transactionsDataOBJ.expense;
            WholeIncome.text = transactionsDataOBJ.income;
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
                item.GetComponent<TransactionListItem>().Comment.text = current_transaction.comment;
                item.GetComponent<TransactionListItem>().Date.text = current_transaction.FormattedData_of_transaction;
            }
        }
        
    }

    public void TransactinDataReqOnUnuccess()
    {

    }

    
}
