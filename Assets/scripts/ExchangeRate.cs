using DataNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class ExchangeRate : MonoBehaviour
{
    public class ExchangeRateList
    {
        public List<ExchangeRateJson> ExchangeRates;
    }
    public class ExchangeRateJson
    {
        public int Cur_ID;
        public DateTime Date;
        public string Cur_Abbreviation;
        public int Cur_Scale;
        public string Cur_Name;
        public double Cur_OfficialRate;
    }

    public string Link = "https://api.nbrb.by/exrates/rates?periodicity=0";

    public TMP_Text EUR_buy;
    public TMP_Text USD_buy;
    public TMP_Text RUB_buy;
    public TMP_Text Date;

    private void OnEnable()
    {
        Req req = gameObject.AddComponent<Req>();
        req.GetReq(Link, result => ReqSuccess(result));
    }

    public void ReqSuccess(string result)
    {
        string NewString = "{\r\n    \"ExchangeRates\": " + result + "\r\n}";
        ExchangeRateList list = new ExchangeRateList();
        list = JsonUtility.FromJson<ExchangeRateList>(NewString);
        Debug.Log(list.ExchangeRates[0]);
        foreach (var exchangeRateJson in list.ExchangeRates)
        {
            Debug.Log(exchangeRateJson.Cur_Abbreviation);
            if (exchangeRateJson.Cur_Abbreviation == "EUR")
            {
                EUR_buy.text = exchangeRateJson.Cur_OfficialRate.ToString();
            }
            if (exchangeRateJson.Cur_Abbreviation == "USD")
            {
                USD_buy.text = exchangeRateJson.Cur_OfficialRate.ToString();
            }
            if (exchangeRateJson.Cur_Abbreviation == "RUB")
            {
                RUB_buy.text = exchangeRateJson.Cur_OfficialRate.ToString();
            }
        }
        Date.text = list.ExchangeRates[0].Date.ToString();
    }
}
