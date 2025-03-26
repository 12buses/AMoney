using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataNamespace;
using UnityEditor.PackageManager.Requests;
using System.Linq;

public class stats : MonoBehaviour
{
    public GameObject ObjectWithWaletId;
    public PieChart PieChartExpense;
    public PieChart PieChartIncome;

    public string URL = "http://195.2.79.241:5000/api_app/statistics";

    private int WalletId;
    [SerializeField] private Color[] colors;
    private Dictionary<string, Color> TransactionsColors;

    [SerializeField]
    class MyClass
    {
        public int id_wallet;
    }

    private void Start()
    {
        TransactionsColors = new Dictionary<string, Color>() 
        {
            {"Развлечения", colors[0]},
            {"Магазин", colors[1]},
            {"Еда", colors[2]},
            {"Другое", colors[3]},
            {"Зарплата", colors[4]},
            {"Пополнение кошелька", colors[5]}
        };
    }

    private void OnEnable()
    {
        GetStat();
    }

    public void GetStat()
    {
        WalletId = ObjectWithWaletId.GetComponent<LoadTransactionMenu>().WalletIdd;
        MyClass myClass = new MyClass();
        myClass.id_wallet = WalletId;
        string j = JsonUtility.ToJson(myClass);
        Req req = gameObject.AddComponent<Req>();
        req.PostReq(j, URL, result => GetStatSucseed(result), error => GetStatUnsucseed());

    }

    void GetStatSucseed(string result)
    {
        StatsLists statsLists = JsonUtility.FromJson<StatsLists>(result);
        statsLists.FormateName();

        PieChartExpense.testCategories = statsLists.ListExpense.Select(stat => stat.FormatedName).ToArray();
        PieChartExpense.testValues = statsLists.ListExpense.Select(stat => stat.sum).ToArray();
        List<Color> expenseColors = new List<Color>();
        foreach (var cat in PieChartExpense.testCategories)
        {
            if (TransactionsColors.TryGetValue(cat, out Color color))
            {
                expenseColors.Add(color);
            }
            else
            {
                expenseColors.Add(Color.gray);
            }
        }
        PieChartExpense.colors = expenseColors.ToArray();
        PieChartExpense.Restart();

        // Handle Income Pie Chart
        PieChartIncome.testCategories = statsLists.ListIncome.Select(stat => stat.FormatedName).ToArray();
        PieChartIncome.testValues = statsLists.ListIncome.Select(stat => stat.sum).ToArray();
        List<Color> incomeColors = new List<Color>();
        foreach (var cat in PieChartIncome.testCategories)
        {
            if (TransactionsColors.TryGetValue(cat, out Color color))
            {
                incomeColors.Add(color);
            }
            else
            {
                incomeColors.Add(Color.gray);
            }
        }
        PieChartIncome.colors = incomeColors.ToArray();
        PieChartIncome.Restart();
        Destroy(GetComponent<Req>());
    }

    void GetStatUnsucseed()
    {
        Destroy(GetComponent<Req>());
    }
}
