using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DataNamespace;

public class WalletList : MonoBehaviour
{
	public GameObject content; // ������, ���������� �������� ������
	public GameObject itemPrefab; // Prefab �������� ������

    

    public void FillList(WalletsData WalletsDataObj)
	{
		// �������� ��������� ������
		for (int i = 0; i < WalletsDataObj.wallets.Count; i++)
		{
			GameObject item = Instantiate(itemPrefab, content.transform);
			item.GetComponent<WalletListItem>().Name.text = WalletsDataObj.wallets[i].name;
            item.GetComponent<WalletListItem>().balance.text = WalletsDataObj.wallets[i].balance.ToString();
            item.GetComponent<WalletListItem>().currency.text = WalletsDataObj.wallets[i].currency;
        }
	}
}
