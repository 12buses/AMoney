using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class popup : MonoBehaviour
{
    public GameObject popupPanel; // ������ �� ������ ������������ ����

    public void ShowPopup()
    {
        popupPanel.SetActive(!popupPanel.activeInHierarchy);
    }
}
