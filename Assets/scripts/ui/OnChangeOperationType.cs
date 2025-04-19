using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DataNamespace;

public class OnChangeOperationType : MonoBehaviour
{
	public List<TMP_Dropdown.OptionData> OperationCategoruOptionForIncome;
	public List<TMP_Dropdown.OptionData> OperationCategoruOptionForExpense;
	public TMP_Dropdown OperationType;
	public TMP_Dropdown OperationCategory;
	public List<Sprite> iconsForIncome;
	public List<Sprite> iconsForExpense;
	public GameObject ObjectWithCategories;

	public void OnEnterAdd()
	{
		OperationCategoruOptionForExpense.Clear();
        OperationType.value = -1;
        OperationCategory.value = -1;
        foreach (Category category in ObjectWithCategories.GetComponent<categories>().categoriesObject.expense)
		{
            TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
			optionData.text = category.name;
            OperationCategoruOptionForExpense.Add(optionData);
        }

        OperationCategoruOptionForIncome.Clear();
		foreach (Category category in ObjectWithCategories.GetComponent<categories>().categoriesObject.income)
		{
			TMP_Dropdown.OptionData optionData = new TMP_Dropdown.OptionData();
			optionData.text = category.name;
			OperationCategoruOptionForIncome.Add(optionData);
		}
    }
	public void OnOperationTypeValueChanged()
	{
		OperationCategory.ClearOptions();
        OperationCategory.value = -1;
        if (OperationType.value == 0)
		{
			OperationCategory.AddOptions(OperationCategoruOptionForIncome);
		}
		else if(OperationType.value == 1) 
		{
			OperationCategory.AddOptions(OperationCategoruOptionForExpense);
		}
    }
}
