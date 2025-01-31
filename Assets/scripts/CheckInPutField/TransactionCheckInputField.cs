using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransactionCheckInputField : MonoBehaviour
{
    public TMP_Text CharsInComment;
    public TMP_Text DateErrorMessageText;

    public TMP_InputField Amount;
    public TMP_InputField Date;
    public TMP_InputField Comment;

    public TMP_Dropdown Type;
    public TMP_Dropdown Category;

    public Button SaveButton;

    bool DateValidated = false;

    void Update()
    {
        CharsInComment.text = Comment.text.Length.ToString() + "/300";
    }

    public void ValidateDate()
    {
        DateValidated = false;
        string inputDate = Date.text;

        if (IsValidDate(inputDate, out DateTime parsedDate))
        {
            if (parsedDate > DateTime.Now)
            {
                DateErrorMessageText.text = "Дата не должна быть из будущего.";
            }
            else
            {
                DateValidated = true;
            }
        }
        else
        {
            DateErrorMessageText.text = "Некорректный формат даты. Используйте формат: день.месяц.год";
        }
    }

    private bool IsValidDate(string date, out DateTime parsedDate)
    {
        // Проверяем формат даты
        string[] parts = date.Split('.');
        if (parts.Length != 3)
        {
            parsedDate = default;
            return false;
        }

        // Пробуем разобрать дату
        return DateTime.TryParse($"{parts[2]}-{parts[1]}-{parts[0]}", out parsedDate);
    }
}
