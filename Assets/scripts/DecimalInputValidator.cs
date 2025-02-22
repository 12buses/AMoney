using TMPro;
using UnityEngine;

public class DecimalInputValidator : MonoBehaviour
{
    public TMP_InputField decimalInputField; // ���� ��� ����� ���������� �����
    public TMP_Text errorMessageText; // ���� ��� ������ ��������� �� �������

    public void ValidateDecimal()
    {
        string inputValue = decimalInputField.text;

        if (IsValidDecimal(inputValue))
        {
            errorMessageText.text = "���������� ����� ������� ���������: " + inputValue;
        }
        else
        {
            errorMessageText.text = "������������ ������. ����������� �������� 5 ���� �� ������� � 2 �����, � �������� �� ������ ���� �������������.";
        }
    }

    private bool IsValidDecimal(string value)
    {
        // ���������, ������������� �� ������ �������
        string[] parts = value.Split(',');

        // ��������� ���������� ������
        if (parts.Length > 2)
        {
            return false; // ������ ����� �������
        }

        // ��������� ����� �����
        if (parts.Length == 2)
        {
            if (parts[0].Length > 5 || parts[1].Length > 2)
            {
                return false; // ��������� ���������� ��������
            }
        }
        else if (parts.Length == 1)
        {
            if (parts[0].Length > 5)
            {
                return false; // ��������� ���������� �������� �� �������
            }
        }

        // ���������, ��� ������ �������� ������ � �� ������������
        if (decimal.TryParse(value, out decimal result))
        {
            return result >= 0; // ���������, ��� ����� �� �������������
        }

        return false; // ���� �� ������� ��������� �����
    }
}
