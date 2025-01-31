using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TransactionFucns : MonoBehaviour
{
    public TMP_InputField DateInPutField;
    public void ChangeCurrentDate()
    {
        DateInPutField.text = System.DateTime.Now.ToString("dd-MM-yyyy");
    }
}
