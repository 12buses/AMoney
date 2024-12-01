using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShowPassword : MonoBehaviour
{
    public TMP_InputField passwordInputField;

    private bool isShowed = false;

    private void Start()
    {
        passwordInputField.contentType = TMP_InputField.ContentType.Password;
    }

    public void OnButtonAction()
    {
        isShowed = !isShowed;

        if (!isShowed)
            passwordInputField.contentType = TMP_InputField.ContentType.Password;
        else
            passwordInputField.contentType = TMP_InputField.ContentType.Standard;

        passwordInputField.ForceLabelUpdate();
    }
}
