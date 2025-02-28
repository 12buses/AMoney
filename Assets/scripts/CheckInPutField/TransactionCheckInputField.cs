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
			AmountErrorMessageText.text = "������� ����� ��������!!";
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
				DateErrorMessageText.text = "���� �� ������ ���� �� ��������.";
			}
			else
			{
				dateValidated = true;
			}
		}
		else
		{
			DateErrorMessageText.text = "������������ ������ ����. ����������� ������: ����.�����.���";
		}
		SetButton();
	}

	private bool IsValidDate(string date, out DateTime parsedDate)
	{
		// ��������� ������ ����
		string[] parts = date.Split('-');
		if (parts.Length != 3)
		{
			parsedDate = default;
			return false;
		}

		// ������� ��������� ����
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
        // ��������� ������ ����
        string[] parts = Date.text.Split('-');

        // ���� � ��� ������ 3 ������, ��������� �����
        if (parts.Length < 3)
        {
            if (parts.Length > 0 && parts[^1].Length == 2)
            {
                Date.text += "-";
                Invoke("SetCaretPosition", 0.001f);
            }
        }

        // ���� � ��� 2 ����� � ������ ����� ������� �� 1 �������, ��������� ����
        if (parts.Length >= 2 && parts[0].Length <= 1)
        {
            parts[0] = parts[0].PadLeft(2, '0'); // ����������� ��������� �������
            UpdateDateText(parts);
        }

        // ���� � ��� 3 ����� � ������ ����� ������� �� 1 �������, ��������� ����
        if (parts.Length >= 3 && parts[1].Length <= 1)
        {
            parts[1] = parts[1].PadLeft(2, '0'); // ����������� ��������� �������
            UpdateDateText(parts);
        }
    }

    private void UpdateDateText(string[] parts)
    {
        Date.text = string.Join("-", parts); // ��������� �����
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
		// ������� ��� �������, ����� ���� � �������
		string sanitizedInput = System.Text.RegularExpressions.Regex.Replace(Date.text, @"[^0-9-]", "");

		// ��������� ������ �� �����
		string[] parts = sanitizedInput.Split('-');

		// ���������, ��� � ��� ���� ��� ����� (����, �����, ���)
		if (parts.Length == 3)
		{
			// ����������� ����
			parts[0] = parts[0].PadLeft(2, '0'); // ��������� ���� �����, ���� �����
												 // ����������� �����
			parts[1] = parts[1].PadLeft(2, '0'); // ��������� ���� �����, ���� �����

			// ����������� ���
			if (parts[2].Length == 1) // ���� ��� ������� �� 1 �����
			{
				parts[2] = "200" + parts[2]; // ���������� � 200X
			}
			else if (parts[2].Length == 2) // ���� ��� ������� �� 2 ����
			{
				int year = int.Parse(parts[2]);
				// ���������, �������� �� ���� �������
				int currentYear = System.DateTime.Now.Year % 100; // �������� ��������� 2 ����� �������� ����
				if (year <= currentYear) // ���� ��� ������ �������� ����, ��������� 2000
				{
					parts[2] = "20" + parts[2];
				}
				else // ���� ��� ������ ��� ����� ��������, ��������� 1900
				{
					parts[2] = "19" + parts[2];
				}
			}
			else if (parts[2].Length == 3) // ���� ��� ������� �� 3 ����
			{
                int year = int.Parse(parts[2]);
                // ���������, �������� �� ���� �������
                int currentYear = System.DateTime.Now.Year % 1000; // �������� ��������� 3 ����� �������� ����
                if (year <= currentYear) // ���� ��� ������ �������� ����, ��������� 2000
                {
                    parts[2] = "20" + parts[2].Substring(1); // ���������� � 20XXX
                }
                else // ���� ��� ������ ��� ����� ��������, ��������� 1900
                {
                    parts[2] = "19" + parts[2].Substring(1); // ���������� � 20XXX
                }

			}

			// �������� ������� � ������
			string formattedDate = $"{parts[0]}-{parts[1]}-{parts[2]}";

			// ���������, ��������� �� �����, ����� �������� ������������ �����
			if (Date.text != formattedDate)
			{
				Date.text = formattedDate;
			}
		}
	}
}
