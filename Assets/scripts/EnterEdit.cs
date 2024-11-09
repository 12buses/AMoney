using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterEdit : MonoBehaviour
{
    public GameObject wallet;
    public void Edit ()
    {
        PlayerPrefs.SetString("Name", wallet.GetComponent<WalletListItem>().NameString);
        PlayerPrefs.SetString("Curency", wallet.GetComponent<WalletListItem>().CurrencyString);
        PlayerPrefs.SetString("Balance", wallet.GetComponent<WalletListItem>().BalanceString);
    }
}
