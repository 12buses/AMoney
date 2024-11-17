using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CreateWalletCheckInPutField : MonoBehaviour
{

    public TMP_InputField _Name; //название кошелька
    public TMP_InputField _Balance; // поле для балика
    public Button Button; // кнопка создания кошелька
    public GameObject CreateWalletButton; //кнопка создания кошелька как gameobject
    public GameObject _NameGameObject; //поле name как gameobject
    public GameObject _BalanceGameObject; // поле Balance как gameobject
    private bool IfNameCheckPassed = false;
    private bool IfBalanceCheckPassed = false;
    public Sprite InActiveButton;
    public Sprite IsActiveButton;
    public Sprite InputField;
    public Sprite InputFieldWrong;

    public void NameCheck()
    {
        IfNameCheckPassed = true;
        if (_Name.text.Length < 3 || _Name.text.Length > 15)
        {
            IfNameCheckPassed = false;
        }
        if (IfNameCheckPassed == true)
        {
            _NameGameObject.GetComponent<Image>().sprite = InputField;
        }
        else
        {
            _NameGameObject.GetComponent<Image>().sprite = InputFieldWrong;
        }
        SetActiveButton();
    }

    public void BalanceCheck()
    {
        IfBalanceCheckPassed = true;
        if (_Balance.text.Length < 3 || _Balance.text.Length > 15)
        {
            IfBalanceCheckPassed = false;
        }

        if (IfBalanceCheckPassed == true)
        {
            _BalanceGameObject.GetComponent<Image>().sprite = InputField;
        }
        else
        {
            _BalanceGameObject.GetComponent<Image>().sprite = InputFieldWrong;
        }
        SetActiveButton();
    }

    public void SetActiveButton()
    {
        if (IfNameCheckPassed == true && IfBalanceCheckPassed == true )
        {
            Button.interactable = true;
            CreateWalletButton.GetComponent<Image>().sprite = IsActiveButton;
        }
        else
        {
            Button.interactable = false;
            CreateWalletButton.GetComponent<Image>().sprite = InActiveButton;
        }
    }
}
