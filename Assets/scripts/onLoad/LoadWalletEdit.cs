using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LoadWalletEdit : MonoBehaviour
{
    public TMP_Text HelloText;
    public string Name;
    public string Balance;
    public string Currency;
    public TMP_InputField NameText;
    public TMP_InputField BalanceText;
    public TMP_Dropdown CurrencyDropDown;

    void Start()
    {
        HelloText.text = "Привет,  " + PlayerPrefs.GetString("UserLogin", "defaultString") + "!";
        Name = PlayerPrefs.GetString("Name", "defaultString");
        Balance = PlayerPrefs.GetString("Curency", "defaultString");
        Currency = PlayerPrefs.GetString("Balance", "defaultString");
        NameText.text = Name;
        BalanceText.text = Balance;
    }

}
