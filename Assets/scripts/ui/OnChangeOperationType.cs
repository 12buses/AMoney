using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OnChangeOperationType : MonoBehaviour
{
    public List<TMP_Dropdown.OptionData> OperationCategoruOptionForIncome;
    public List<TMP_Dropdown.OptionData> OperationCategoruOptionForExpense;
    public TMP_Dropdown OperationType;
    public TMP_Dropdown OperationCategory;
    public List<Sprite> iconsForIncome;
    public List<Sprite> iconsForExpense;


    public void OnOperationTypeValueChanged()
    {
        OperationCategory.ClearOptions();
        if(OperationType.value == 0)
        {
            OperationCategory.value = -1;
            OperationCategory.AddOptions(OperationCategoruOptionForIncome);
        }
        else if(OperationType.value == 1) 
        {
            OperationCategory.value = -1;
            OperationCategory.AddOptions(OperationCategoruOptionForExpense);
        }
    }
}
