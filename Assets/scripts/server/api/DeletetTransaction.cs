using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DataNamespace;
using static EditTransaction;

public class DeletetTransaction : MonoBehaviour
{


    [System.Serializable]
    public class Root
    {
        public int id_wallet;
        public int id_transaction;
    }

    public void OnButtonClicked()
    {
        Root Root;
        Root = new Root();
        Root.id_wallet = 1;
    }
}
