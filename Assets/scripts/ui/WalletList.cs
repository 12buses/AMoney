using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DataNamespace;

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
    public GameObject EditWalletSaveButton;


    public void FillList(WalletsData WalletsDataObj)
	{
        
		// Создание элементов списка
		for (int i = 0; i < WalletsDataObj.wallets.Count; i++)
		{
			GameObject item = Instantiate(itemPrefab, content.transform);
			item.GetComponent<WalletListItem>().Name.text = WalletsDataObj.wallets[i].name;
            item.GetComponent<WalletListItem>().NameString = WalletsDataObj.wallets[i].name;
            item.GetComponent<WalletListItem>().balance.text = WalletsDataObj.wallets[i].balance.ToString();
            item.GetComponent<WalletListItem>().BalanceString = WalletsDataObj.wallets[i].balance.ToString();
            item.GetComponent<WalletListItem>().currency.text = WalletsDataObj.wallets[i].currency;
            item.GetComponent<WalletListItem>().CurrencyString = WalletsDataObj.wallets[i].currency;
            item.GetComponent<WalletListItem>().WalletId = WalletsDataObj.wallets[i].id_wallet;
			item.GetComponent<WalletListItem>().DeleteMenu = DeleteMenu;
            item.GetComponent<WalletListItem>().DeletedMenuText = DeletedMenuText;
            item.GetComponent<WalletListItem>().DeleteMenuText = DeleteMenuText;
            item.GetComponent<WalletListItem>().BalanceEdit = BalanceEdit;
            item.GetComponent<WalletListItem>().NameOfWalletEdit = NameOfWalletEdit;
            item.GetComponent<WalletListItem>().CurrencyEdit = CurrencyEdit;
            item.GetComponent<WalletListItem>().MainMenu = MainMenu;
            item.GetComponent<WalletListItem>().EditMenu = EditMenu;
            item.GetComponent<WalletListItem>().EditWalletSaveButton = EditWalletSaveButton;
        }
    }

    public void DeleteList()
    {
        foreach (Transform child in content.GetComponent<Transform>()) Destroy(child.gameObject);
    }
}
