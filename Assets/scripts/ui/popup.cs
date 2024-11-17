using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    public GameObject popupPanel; // —сылка на панель всплывающего окна

    public void ShowPopup()
    {
        popupPanel.SetActive(!popupPanel.activeInHierarchy);
    }
}
