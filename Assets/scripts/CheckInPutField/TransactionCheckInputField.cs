using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TransactionCheckInputField : MonoBehaviour
{
	public Sprite InPutFieldSprite;
	public Sprite IncorectInputFieldSprite;
	public Sprite ButtonSprite;
	public Sprite IncorectButtonSprite;

	public TMP_Text CharsInComment;
	public TMP_Text DateErrorMessageText;
	public TMP_Text AmountErrorMessageText;

	public TMP_InputField Amount;
	public TMP_InputField Date;
	public TMP_InputField Comment;

	public TMP_Dropdown Type;
	public TMP_Dropdown Category;

	public Button SaveButton;

	private bool amountValidated = false;
	private bool dateValidated = true;

	void Update()
	{
		CharsInComment.text = Comment.text.Length.ToString() + "/300";
	}

	public void ValidateAmount()
	{
		amountValidated = true;
		if (Amount.text.Length > 1000000)
		{
			amountValidated = false;
			Amount.GetComponent<Image>().sprite = IncorectInputFieldSprite;
		}
		else if (Amount.text.Length < 1)
		{
			Amount.GetComponent<Image>().sprite = IncorectInputFieldSprite;
			AmountErrorMessageText.text = "Введите сумму операции!!";
		}
		else
		{
			Amount.GetComponent<Image>().sprite = InPutFieldSprite;

		}
	}

	public void ValidateDate()
	{
		dateValidated = true;
		string inputDate = Date.text;

		if (IsValidDate(inputDate, out DateTime parsedDate))
		{
			if (parsedDate > DateTime.Now)
			{
				DateErrorMessageText.text = "Дата не должна быть из будущего.";
			}
			else
			{
				dateValidated = true;
			}
		}
		else
		{
			DateErrorMessageText.text = "Некорректный формат даты. Используйте формат: день.месяц.год";
		}
		SetButton();
	}

	private bool IsValidDate(string date, out DateTime parsedDate)
	{
		// Проверяем формат даты
		string[] parts = date.Split('-');
		if (parts.Length != 3)
		{
			parsedDate = default;
			return false;
		}

		// Пробуем разобрать дату
		return DateTime.TryParse($"{parts[2]}-{parts[1]}-{parts[0]}", out parsedDate);
	}

	private void SetButton()
	{
		if (dateValidated == true && amountValidated == true)
		{
			SaveButton.interactable = true;
			SaveButton.GetComponent<Image>().sprite = ButtonSprite;
		}
		else
		{
			SaveButton.interactable = false;
			SaveButton.GetComponent<Image>().sprite = IncorectButtonSprite;
		}
	}

	public void Clean()
	{
		DateErrorMessageText.text = "";
		AmountErrorMessageText.text = "";
		
		Amount.text = "";
		Amount.GetComponent<Image>().sprite = InPutFieldSprite;
        Date.text = "";

		Date.GetComponent<Image>().sprite = InPutFieldSprite;
        Comment.text = "";

		Type.value = -1;
		Category.value = -1;

		SaveButton.GetComponent<Image>().sprite = ButtonSprite;
	}

	public void DateAutoAd()
	{
        // Проверяем формат даты
        string[] parts = Date.text.Split('-');

        // Если у нас меньше 3 частей, добавляем дефис
        if (parts.Length < 3)
        {
            if (parts.Length > 0 && parts[^1].Length == 2)
            {
                Date.text += "-";
                Invoke("SetCaretPosition", 0.001f);
            }
        }

        // Если у нас 2 части и первая часть состоит из 1 символа, добавляем ноль
        if (parts.Length >= 2 && parts[0].Length <= 1)
        {
            parts[0] = parts[0].PadLeft(2, '0'); // Присваиваем результат обратно
            UpdateDateText(parts);
        }

        // Если у нас 3 части и вторая часть состоит из 1 символа, добавляем ноль
        if (parts.Length >= 3 && parts[1].Length <= 1)
        {
            parts[1] = parts[1].PadLeft(2, '0'); // Присваиваем результат обратно
            UpdateDateText(parts);
        }
    }

    private void UpdateDateText(string[] parts)
    {
        Date.text = string.Join("-", parts); // Обновляем текст
        Invoke("SetCaretPosition", 0.001f);
    }

    private void SetCaretPosition()
    {
        int newCaretPosition = Date.text.Length;
        Date.caretPosition = newCaretPosition;
        Date.selectionAnchorPosition = newCaretPosition;
        Date.selectionFocusPosition = newCaretPosition;
    }

	public void OnDateEndEdit()
	{
		// Удаляем все символы, кроме цифр и дефисов
		string sanitizedInput = System.Text.RegularExpressions.Regex.Replace(Date.text, @"[^0-9-]", "");

		// Разбиваем строку на части
		string[] parts = sanitizedInput.Split('-');

		// Проверяем, что у нас есть три части (день, месяц, год)
		if (parts.Length == 3)
		{
			// Форматируем день
			parts[0] = parts[0].PadLeft(2, '0'); // Добавляем ноль слева, если нужно
												 // Форматируем месяц
			parts[1] = parts[1].PadLeft(2, '0'); // Добавляем ноль слева, если нужно

			// Форматируем год
			if (parts[2].Length == 1) // Если год состоит из 1 цифры
			{
				parts[2] = "200" + parts[2]; // Превращаем в 200X
			}
			else if (parts[2].Length == 2) // Если год состоит из 2 цифр
			{
				int year = int.Parse(parts[2]);
				// Проверяем, является ли дата будущей
				int currentYear = System.DateTime.Now.Year % 100; // Получаем последние 2 цифры текущего года
				if (year <= currentYear) // Если год больше текущего года, добавляем 2000
				{
					parts[2] = "20" + parts[2];
				}
				else // Если год меньше или равен текущему, добавляем 1900
				{
					parts[2] = "19" + parts[2];
				}
			}
			else if (parts[2].Length == 3) // Если год состоит из 3 цифр
			{
                int year = int.Parse(parts[2]);
                // Проверяем, является ли дата будущей
                int currentYear = System.DateTime.Now.Year % 1000; // Получаем последние 3 цифры текущего года
                if (year <= currentYear) // Если год больше текущего года, добавляем 2000
                {
                    parts[2] = "20" + parts[2].Substring(1); // Превращаем в 20XXX
                }
                else // Если год меньше или равен текущему, добавляем 1900
                {
                    parts[2] = "19" + parts[2].Substring(1); // Превращаем в 20XXX
                }

			}

			// Собираем обратно в строку
			string formattedDate = $"{parts[0]}-{parts[1]}-{parts[2]}";

			// Проверяем, изменился ли текст, чтобы избежать бесконечного цикла
			if (Date.text != formattedDate)
			{
				Date.text = formattedDate;
			}
		}
	}
}
