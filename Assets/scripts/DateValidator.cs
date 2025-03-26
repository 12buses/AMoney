using System;
using System.Text.RegularExpressions;
using UnityEngine;
using TMPro;
using Unity.Mathematics;

public class DateValidator : MonoBehaviour
{
    public TMP_InputField dateInput;

    private void Start()
    {
        dateInput = GetComponent<TMP_InputField>();

        dateInput.onValueChanged.AddListener(FormatInput);
        dateInput.onEndEdit.AddListener(ValidateDateOnEndEdit);
    }

    private void FormatInput(string input)
    {
        string cleaned = Regex.Replace(input, "[^0-9]", ""); // Оставляем только цифры

        if (cleaned.Length > 2) cleaned = cleaned.Insert(2, "-");
        if (cleaned.Length > 5) cleaned = cleaned.Insert(5, "-");

        if (cleaned.Length > 10) cleaned = cleaned.Substring(0, 10); // Ограничение на 10 символов

        dateInput.text = cleaned;

        Invoke(nameof(UpdateCaretPosition), Time.deltaTime / 100);
    }

    private void UpdateCaretPosition()
    {
        dateInput.caretPosition = dateInput.text.Length;
    }

    private bool IsValidDate(string date)
    {
        string[] parts = date.Split('-');
        if (parts.Length != 3 || parts[2].Length < 4) return false; // Год должен быть 4-значным

        if (!int.TryParse(parts[0], out int day) ||
            !int.TryParse(parts[1], out int month) ||
            !int.TryParse(parts[2], out int year)) return false;

        if (year < 1900 || year > DateTime.Now.Year) return false; // Год в адекватном диапазоне
        if (month < 1 || month > 12) return false;
        if (day < 1 || day > DateTime.DaysInMonth(year, month)) return false;

        return true;
    }

    private void ValidateDateOnEndEdit(string input)
    {
        if (string.IsNullOrEmpty(input) || !IsValidDate(input))
        {
            ResetToToday();
        }
    }

    public DateTime GetValidatedDate()
    {
        if (string.IsNullOrEmpty(dateInput.text) || !IsValidDate(dateInput.text))
        {
            ResetToToday();
        }

        return DateTime.ParseExact(dateInput.text, "dd.MM.yyyy", null);
    }

    public string GetValidatedDateStr()
    {
        if (string.IsNullOrEmpty(dateInput.text) || !IsValidDate(dateInput.text))
        {
            ResetToToday();
        }

        return dateInput.text;
    }

    private void ResetToToday()
    {
        dateInput.text = DateTime.Now.ToString("dd.MM.yyyy");
    }
}