using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfoManager : MonoBehaviour
{
    public GameObject[] infoTexts;

    void Action()
    {
        for (int i = 0; i <= infoTexts.Length; i++)
        {
            infoTexts[i].SetActive(false);
            this.gameObject.SetActive(true);
        }
    }
}
