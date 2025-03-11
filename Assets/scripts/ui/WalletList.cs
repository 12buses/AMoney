using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DataNamespace;
using JetBrains.Annotations;

public class WalletList : MonoBehaviour
{
	public GameObject content; // Объект, содержащий элементы списка
	public GameObject itemPrefab; // Prefab элемента списка
	public GameObject DeleteMenu;
    public TMP_Text DeletedMenuText;
    public TMP_Text DeleteMenuText;
    public TMP_InputField BalanceEdit;
    public TMP_InputField NameOfWalletEdit;
    public TMP_Dropdown CurrencyEdit;
    public GameObject MainMenu;
    public GameObject EditMenu;
    public GameObject TransactionMenu;
    public GameObject EditWalletSaveButton;
    public TMP_Text LoginTransactionsMenu;


    public void FillList(WalletsData WalletsDataObj)
	{
        string currency;
        // Создание элементов списка
        for (int i = 0; i < WalletsDataObj.wallets.Count; i++)
		{
            if (WalletsDataObj.wallets[i].currency == "EURO") 
            { 
                currency = "EUR";
            } 
            else 
            { 
                currency = WalletsDataObj.wallets[i].currency;
            }
            GameObject item = Instantiate(itemPrefab, content.transform);
            WalletListItem walletListItem = item.GetComponent<WalletListItem>();

            walletListItem.Name.text = WalletsDataObj.wallets[i].name;
            walletListItem.NameString = WalletsDataObj.wallets[i].name;
            walletListItem.balance.text = WalletsDataObj.wallets[i].balance.ToString("0.00");
            walletListItem.BalanceFloat = WalletsDataObj.wallets[i].balance;
            walletListItem.currency.text = currency;
            walletListItem.CurrencyString = currency;
            walletListItem.WalletId = WalletsDataObj.wallets[i].id_wallet;
            walletListItem.DeleteMenu = DeleteMenu;
            walletListItem.DeletedMenuText = DeletedMenuText;
            walletListItem.DeleteMenuText = DeleteMenuText;
            walletListItem.BalanceEdit = BalanceEdit;
            walletListItem.NameOfWalletEdit = NameOfWalletEdit;
            walletListItem.CurrencyEdit = CurrencyEdit;
            walletListItem.MainMenu = MainMenu;
            walletListItem.EditMenu = EditMenu;
            walletListItem.EditWalletSaveButton = EditWalletSaveButton;
            walletListItem.Login = LoginTransactionsMenu;
            walletListItem.TransactionMenu = TransactionMenu;
        }
    }

    public void DeleteList()
    {
        foreach (Transform child in content.GetComponent<Transform>()) Destroy(child.gameObject);
    }
}
