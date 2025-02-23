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
		if(parts.Length < 3)
		{
			if(parts[^1].Length == 2)
			{
				Date.text = Date.text + "-";
				Date.caretPosition = Date.caretPosition + 1;
            }
		}
    }
}
