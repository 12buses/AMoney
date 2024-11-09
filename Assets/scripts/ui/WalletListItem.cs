using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WalletListItem : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text balance;
    public TMP_Text currency;
    public int WalletId;
    public string NameString;
    public string BalanceString;
    public string CurrencyString;
    public GameObject DeleteMenu;
    public TMP_Text DeletedMenuText;
    public TMP_Text DeleteMenuText;
}
