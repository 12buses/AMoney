using UnityEngine;
using TMPro;
using System;

public class DateValidator : MonoBehaviour
{
    public TMP_InputField inputField;
    void Start()
    {
        // Назначаем валидатор
        inputField.onValidateInput += ValidateInput;
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        // Разрешаем вводить цифры, точки и символы управления (например, Backspace)
        if (char.IsDigit(addedChar) || addedChar == '-' || addedChar == '\b')
        {
            // Проверяем, соответствует ли текущий текст формату "день.месяц.год"
            string[] parts = text.Split('-');
            if (parts.Length > 3)
            {
                return '\0'; // Запрещаем ввод, если уже 3 части
            }

            // Проверяем, что каждая часть не превышает допустимую длину
            if (parts.Length == 1 && parts[0].Length >= 2 && addedChar != '-') // День
            {
                return '\0'; // Запрещаем ввод, если длина дня превышает 2
            }
            else if (parts.Length == 2 && parts[1].Length >= 2 && addedChar != '-') // Месяц
            {
                return '\0'; // Запрещаем ввод, если длина месяца превышает 2
            }
            else if (parts.Length == 3 && parts[2].Length >= 4 && addedChar != '-') // Год
            {
                return '\0'; // Запрещаем ввод, если длина года превышает 4
            }

            // Если вводится точка, проверяем, что она не вводится подряд
            if (addedChar == '-' && (text.EndsWith(".") || text.Length == 0))
            {
                return '\0'; // Запрещаем ввод точки
            }

            // Проверка на корректность даты
            if (parts.Length == 3 && parts[0].Length == 2 && parts[1].Length == 2 && parts[2].Length == 4)
            {
                string dateString = $"{parts[0]}.{parts[1]}.{parts[2]}";
                if (DateTime.TryParse(dateString, out DateTime date))
                {
                    // Проверяем, что дата не из будущего
                    if (date > DateTime.Now)
                    {
                        return '\0'; // Запрещаем ввод, если дата из будущего
                    }
                }
            }
            return addedChar; // Разрешаем ввод
        }
        return '\0'; // Запрещаем ввод других символов
    }
}
