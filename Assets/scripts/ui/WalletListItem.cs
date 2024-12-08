using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class WalletListItem : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text balance;
    public TMP_Text currency;
    public int WalletId;
    public string NameString;
    public float BalanceFloat;
    public string CurrencyString;
    public GameObject DeleteMenu;
    public TMP_Text DeletedMenuText;
    public TMP_Text DeleteMenuText;

    public TMP_InputField BalanceEdit;
    public TMP_InputField NameOfWalletEdit;
    public TMP_Dropdown CurrencyEdit;

    public GameObject EditMenu;
    public GameObject MainMenu;
    public GameObject EditWalletSaveButton;

    public void EntereEdit()
    {
        EditWalletSaveButton.GetComponent<EditWallet>().wallet_id = WalletId;
        BalanceEdit.text = BalanceFloat.ToString("0.00");
        NameOfWalletEdit.text = NameString;
        switch (CurrencyString)
        {
            case "EUR" :
                CurrencyEdit.value = 3;
                break;
            case "BYN":
                CurrencyEdit.value = 0;
                break;
            case "USD" :
                CurrencyEdit.value = 2;
                break;
            case "RUB":
                CurrencyEdit.value = 1; 
                break;
            default:
                CurrencyEdit.value = 0;
                break;
        }
        EditMenu.SetActive(true);
        MainMenu.SetActive(false);
    }
}
