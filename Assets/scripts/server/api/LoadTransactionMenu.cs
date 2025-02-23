using DataNamespace;
using System.Collections;
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

	public GameObject CreateWalletObjScript;

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
		CreateWalletObjScript.GetComponent<AddTransactions>().WalletId = WalletIdd.ToString();
        WalletId x = new WalletId();
		x.id_wallet = WalletIdd;
		string userDataString = JsonUtility.ToJson(x);
		Debug.Log(userDataString);
        StartCoroutine(CategoriesForWallet(userDataString));
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
			Debug.Log("Answer: " + webRequest.downloadHandler.text);
			transactions transactionsDataOBJ = JsonUtility.FromJson<transactions>(webRequest.downloadHandler.text);
			if (transactionsDataOBJ.page0.Count > 0)
			{
				FillTransactions(transactionsDataOBJ);
			}
		}
	}

    public void FillTransactions(transactions transactions)
    {
        WholeExpense.text = transactions.expense;
        WholeIncome.text = transactions.income;
        Debug.Log(transactions.income);

        transactions.ConvertSecondsToDate();

        foreach (var current_transaction in transactions.page0)
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


    IEnumerator CategoriesForWallet(string postJSON)
    {
        UnityWebRequest webRequest = UnityWebRequest.PostWwwForm(UrlForCategories, "POST");
        webRequest.SetRequestHeader("Content-Type", "application/json");
        byte[] userDataRaw = Encoding.UTF8.GetBytes(postJSON);
        webRequest.uploadHandler = new UploadHandlerRaw(userDataRaw);
        webRequest.downloadHandler = new DownloadHandlerBuffer();
        yield return webRequest.SendWebRequest();
        if (webRequest.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Ошибка: " + webRequest.error);
        }
        else
        {
            Debug.Log("Answer: " + webRequest.downloadHandler.text);
			Categories categories = JsonUtility.FromJson<Categories>(webRequest.downloadHandler.text);
            foreach (var category in categories.expense)
            {
                category.name = category.name.ToString();
            }

            foreach (var category in categories.income)
            {
                category.name = category.name.ToString();
            }
            FillCategoriesObjects(categories);
        }
    }

    public void FillCategoriesObjects(Categories categories)
    {
        CreateTransactionObject.GetComponent<categories>().categoriesObject = categories;
    }
}
