using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataNamespace;
using TMPro;

public class TransactionListItem : MonoBehaviour
{
    public transaction transaction;
    public TMP_Text Category;
    public TMP_Text Amount;
    public TMP_Text Date;
    public TMP_Text Comment;

    public void ChangeAmountColor(string type)
    {
        if(type == "expense")
        {
            Amount.color = Color.red;
        }
        else
        {
            Amount.color = Color.green;
        }
    }
}
