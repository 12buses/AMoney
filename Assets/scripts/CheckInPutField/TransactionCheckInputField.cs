using DataNamespace;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    public TMP_Dropdown Cattegory;

    public Button SaveButton;

    public CanvasGroup canvasGroup;

    private bool amountValidated = false;
    private bool dateValidated = true;

    private Coroutine fadeCoroutine;

    void Update()
    {
        CharsInComment.text = Comment.text.Length.ToString() + "/300";
    }

    private void OnEnable()
    {
        dateValidated = true;
        amountValidated = false;
        ChangeCurrentDate();
        Cattegory.value = -1;
        Type.value = -1;
        AmountErrorMessageText.text = "";
        Amount.GetComponent<Image>().sprite = InPutFieldSprite;
    }

    public void ValidateAmount()
    {

        amountValidated = false;
        if (Amount.text.Length > 1000000)
        {
            Amount.GetComponent<Image>().sprite = IncorectInputFieldSprite;
            AmountErrorMessageText.text = "����� �������� ������� �������!!";
        }
        else if (Amount.text.Length < 1)
        {
            Amount.GetComponent<Image>().sprite = IncorectInputFieldSprite;
            AmountErrorMessageText.text = "������� ����� ��������!!";
        }
        else
        {
            Amount.GetComponent<Image>().sprite = InPutFieldSprite;
            amountValidated = true;
            AmountErrorMessageText.text = "";
        }
        SetButton();
    }

    /*public void ValidateDate()
    {
        dateValidated = true;
        string inputDate = Date.text;

        if (IsValidDate(inputDate, out DateTime parsedDate))
        {
            if (parsedDate > DateTime.Now)
            {
                //DateErrorMessageText.text = "���� �� ������ ���� �� ��������.";
            }
            else
            {
                dateValidated = true;
            }
        }
        else
        {
            //DateErrorMessageText.text = "������������ ������ ����. ����������� ������: ����.�����.���";
        }
        SetButton();
    }*/

    public void ChangeCurrentDate()
    {
        Date.text = System.DateTime.Now.ToString("dd-MM-yyyy");
        //ValidateDate();
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
        Cattegory.value = -1;

        SaveButton.GetComponent<Image>().sprite = ButtonSprite;
    }

    public void DateAutoAd()
    {
        if (Date.text.Length == Date.selectionFocusPosition && Date.text.Length == Date.selectionAnchorPosition)
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
        // ������� �����
        string sanitizedInput = System.Text.RegularExpressions.Regex.Replace(Date.text, @"[^0-9-]", "");
        string[] parts = sanitizedInput.Split('-');

        bool needsCorrection = false;
        DateTime resultDate;

        // ������� �������������� ������
        if (parts.Length == 3)
        {
            try
            {
                // �������������� ��� � ������
                parts[0] = parts[0].PadLeft(2, '0');
                parts[1] = parts[1].PadLeft(2, '0');

                // ��������� ����
                parts[2] = ProcessYearPart(parts[2]);

                // ������� ��������
                bool isValid = DateTime.TryParseExact(
                    $"{parts[0]}-{parts[1]}-{parts[2]}",
                    "dd-MM-yyyy",
                    CultureInfo.InvariantCulture,
                    DateTimeStyles.None,
                    out resultDate);

                // �������� �� ������� ����
                if (isValid && resultDate > DateTime.Now)
                {
                    resultDate = DateTime.Now;
                    needsCorrection = true;
                }

                if (!isValid)
                {
                    // ������������� �����������
                    int year = int.Parse(parts[2]);
                    int month = Math.Clamp(int.Parse(parts[1]), 1, 12);
                    int day = Math.Clamp(int.Parse(parts[0]), 1, DateTime.DaysInMonth(year, month));

                    resultDate = new DateTime(year, month, day);
                    needsCorrection = true;

                    // �������������� �������� �� �������
                    if (resultDate > DateTime.Now)
                    {
                        resultDate = DateTime.Now;
                    }
                }

                string formattedDate = resultDate.ToString("dd-MM-yyyy");

                // ���������� ���� �����
                if (Date.text != formattedDate)
                {
                    Date.text = formattedDate;
                    // ��������� ��������
                    if (fadeCoroutine != null)
                    {
                        StopCoroutine(fadeCoroutine);
                    }
                    fadeCoroutine = StartCoroutine(FadeAlpha());
                }
            }
            catch
            {
                // � ������ ������ - ������������� ������� ����
                Date.text = DateTime.Now.ToString("dd-MM-yyyy");
            }
        }
    }

    private string ProcessYearPart(string yearPart)
    {
        if (yearPart.Length == 1) return $"200{yearPart}";
        if (yearPart.Length == 2)
        {
            int year = int.Parse(yearPart);
            return year <= DateTime.Now.Year % 100 ? $"20{yearPart}" : $"19{yearPart}";
        }
        if (yearPart.Length == 3)
        {
            int year = int.Parse(yearPart);
            int current = DateTime.Now.Year % 1000;
            return year <= current ? $"20{yearPart[1]}{yearPart[2]}" : $"19{yearPart[1]}{yearPart[2]}";
        }
        return yearPart.PadLeft(4, '0').Substring(0, 4);
    }

    private IEnumerator FadeAlpha()
    {
        if (canvasGroup == null) yield break;

        float duration = 0.3f;
        float targetAlpha = 0.5f;

        // ������� ���������� ������������
        float startAlpha = canvasGroup.alpha;
        float time = 0f;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        // ������� �������������� ������������
        time = 0f;
        startAlpha = targetAlpha;

        while (time < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(startAlpha, 1f, time / duration);
            time += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        fadeCoroutine = null;
    }
}
 