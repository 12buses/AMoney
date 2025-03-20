using UnityEngine;
using TMPro;
using System;
using System.Globalization;

public class DateValidator : MonoBehaviour
{
    public TMP_InputField inputField;

    void Start()
    {
        inputField.onValidateInput += ValidateInput;
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        if (char.IsDigit(addedChar) || addedChar == '-' || addedChar == '\b')
        {
            string newText = text.Insert(charIndex, addedChar.ToString());
            if (addedChar == '\b' && charIndex > 0)
            {
                newText = text.Remove(charIndex - 1, 1);
            }

            string[] parts = newText.Split('-');

            // Запрет более 3 частей
            if (parts.Length > 3) return '\0';

            // Проверка дефисов
            if (addedChar == '-')
            {
                // Дефис не может быть первым или повторяться
                if (charIndex == 0 || text.EndsWith("-")) return '\0';

                // Проверка длины предыдущей части
                string currentPart = parts[parts.Length - 2];
                if (currentPart.Length != 2) return '\0';
            }

            // Проверка длины и значений частей
            for (int i = 0; i < parts.Length; i++)
            {
                if (i == 0 && parts[i].Length > 2) return '\0'; // День
                if (i == 1 && parts[i].Length > 2) return '\0'; // Месяц
                if (i == 2 && parts[i].Length > 4) return '\0'; // Год

                // Проверка числовых значений
                if (parts[i].Length > 0 && !int.TryParse(parts[i], out int num)) return '\0';

                // Проверка диапазонов
                if (i == 0 && parts[i].Length == 2) // День
                {
                    int day = int.Parse(parts[i]);
                    if (day < 1 || day > 31) return '\0';
                }
                if (i == 1 && parts[i].Length == 2) // Месяц
                {
                    int month = int.Parse(parts[i]);
                    if (month < 1 || month > 12) return '\0';
                }
            }

            // Проверка полной даты
            if (parts.Length == 3 && parts[2].Length == 4)
            {
                if (!DateTime.TryParseExact(
                    $"{parts[0]}-{parts[1]}-{parts[2]}",
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out DateTime date) || date > DateTime.Now)
                {
                    return '\0';
                }
            }

            return addedChar;
        }
        return '\0';
    }
}