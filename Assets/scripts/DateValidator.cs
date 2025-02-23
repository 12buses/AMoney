using UnityEngine;
using TMPro;
using System;

public class DateValidator : MonoBehaviour
{
    public TMP_InputField inputField;
    void Start()
    {
        // ��������� ���������
        inputField.onValidateInput += ValidateInput;
    }

    private char ValidateInput(string text, int charIndex, char addedChar)
    {
        // ��������� ������� �����, ����� � ������� ���������� (��������, Backspace)
        if (char.IsDigit(addedChar) || addedChar == '-' || addedChar == '\b')
        {
            // ���������, ������������� �� ������� ����� ������� "����.�����.���"
            string[] parts = text.Split('-');
            if (parts.Length > 3)
            {
                return '\0'; // ��������� ����, ���� ��� 3 �����
            }

            // ���������, ��� ������ ����� �� ��������� ���������� �����
            if (parts.Length == 1 && parts[0].Length >= 2 && addedChar != '-') // ����
            {
                return '\0'; // ��������� ����, ���� ����� ��� ��������� 2
            }
            else if (parts.Length == 2 && parts[1].Length >= 2 && addedChar != '-') // �����
            {
                return '\0'; // ��������� ����, ���� ����� ������ ��������� 2
            }
            else if (parts.Length == 3 && parts[2].Length >= 4 && addedChar != '-') // ���
            {
                return '\0'; // ��������� ����, ���� ����� ���� ��������� 4
            }

            // ���� �������� �����, ���������, ��� ��� �� �������� ������
            if (addedChar == '-' && (text.EndsWith(".") || text.Length == 0))
            {
                return '\0'; // ��������� ���� �����
            }

            // �������� �� ������������ ����
            if (parts.Length == 3 && parts[0].Length == 2 && parts[1].Length == 2 && parts[2].Length == 4)
            {
                string dateString = $"{parts[0]}.{parts[1]}.{parts[2]}";
                if (DateTime.TryParse(dateString, out DateTime date))
                {
                    // ���������, ��� ���� �� �� ��������
                    if (date > DateTime.Now)
                    {
                        return '\0'; // ��������� ����, ���� ���� �� ��������
                    }
                }
            }
            return addedChar; // ��������� ����
        }
        return '\0'; // ��������� ���� ������ ��������
    }
}
