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
        if (popupPanel.activeInHierarchy ) { try { GameObject.FindWithTag("OpenedPopUp").SetActive(false); } catch { }
            popupPanel.tag = "OpenedPopUp";
        }
        else
        {
            popupPanel.tag = "ClosedPopUp";
        }
    }

    public void ClosePopup()
    {
        popupPanel.SetActive(false);
        popupPanel.tag = "ClosedPopUp";
    }
}
