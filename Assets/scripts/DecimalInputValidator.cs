using TMPro;
using UnityEngine;

public class DecimalInputValidator : MonoBehaviour
{
    public TMP_InputField decimalInputField; // Поле для ввода десятичной дроби
    public TMP_Text errorMessageText; // Поле для вывода сообщений об ошибках

    public void ValidateDecimal()
    {
        string inputValue = decimalInputField.text;

        if (IsValidDecimal(inputValue))
        {
            errorMessageText.text = "Десятичная дробь введена корректно: " + inputValue;
        }
        else
        {
            errorMessageText.text = "Некорректный формат. Используйте максимум 5 цифр до запятой и 2 после, и значение не должно быть отрицательным.";
        }
    }

    private bool IsValidDecimal(string value)
    {
        // Проверяем, соответствует ли строка формату
        string[] parts = value.Split(',');

        // Проверяем количество частей
        if (parts.Length > 2)
        {
            return false; // Больше одной запятой
        }

        // Проверяем целую часть
        if (parts.Length == 2)
        {
            if (parts[0].Length > 5 || parts[1].Length > 2)
            {
                return false; // Превышено количество символов
            }
        }
        else if (parts.Length == 1)
        {
            if (parts[0].Length > 5)
            {
                return false; // Превышено количество символов до запятой
            }
        }

        // Проверяем, что строка является числом и не отрицательна
        if (decimal.TryParse(value, out decimal result))
        {
            return result >= 0; // Проверяем, что число не отрицательное
        }

        return false; // Если не удалось разобрать число
    }
}
