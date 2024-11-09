using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openDeleteMenu : MonoBehaviour
{
    public GameObject wallet;

    public void Delete()
    {
        wallet.GetComponent<WalletListItem>().DeleteMenu.SetActive(true);
        wallet.GetComponent<WalletListItem>().DeleteMenu.GetComponent<deleteInfo>().id_wallet = wallet.GetComponent<WalletListItem>().WalletId;
        wallet.GetComponent<WalletListItem>().DeleteMenuText.text = "Вы уверены, что хотите удалить кошелек " + wallet.GetComponent<WalletListItem>().NameString + "?";
        wallet.GetComponent<WalletListItem>().DeletedMenuText.text = "Кошелек " + wallet.GetComponent<WalletListItem>().NameString + "?";
    }
}
