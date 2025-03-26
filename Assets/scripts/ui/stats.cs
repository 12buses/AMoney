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
    public Color[] colors;
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
        PieChartExpense.colors = new Color[6];
        StatsLists statsLists = JsonUtility.FromJson<StatsLists>(result);
        statsLists.FormateName();
        PieChartExpense.testCategories = null;
        PieChartExpense.testCategories = statsLists.ListExpense.Select(stat => stat.FormatedName).ToArray();
        PieChartExpense.testValues = statsLists.ListExpense.Select(stat => stat.sum).ToArray();
        foreach(var cat in PieChartExpense.testCategories)
        {
            Color value;
            TransactionsColors.TryGetValue(cat, out value);
            //PieChartExpense.color;
        }
        PieChartExpense.Restart();
        

        PieChartIncome.testCategories = null;
        PieChartIncome.testCategories = statsLists.ListIncome.Select(stat => stat.FormatedName).ToArray();
        PieChartIncome.testValues = statsLists.ListIncome.Select(stat => stat.sum).ToArray();
        PieChartIncome.Restart();
    }

    void GetStatUnsucseed()
    {

    }
}
