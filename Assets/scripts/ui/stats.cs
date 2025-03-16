using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataNamespace;

public class stats : MonoBehaviour
{
    public GameObject ObjectWithWaletId;

    public string URL = "http://195.2.79.241:5000/api_app/statistics";

    private int WalletId;

    [SerializeField]
    class MyClass
    {
        public int id_wallet;
    }

    [SerializeField]
    public class CattegoryStat
    {
        public string name;
        public string sum;
    }

    [SerializeField]
    public class Root
    {
        public List<CattegoryStat> ListIncome;
        public List<CattegoryStat> ListExpense;
    }

    private void Start()
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
        
    }

    void GetStatUnsucseed()
    {

    }
}
