using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Windows;
namespace DataNamespace
{
    [System.Serializable]
    public class User //класс пользователей
    {
        public string login;
        public string email;
        public User(string name, string mail)
        {
            login = name;
            email = mail;

        }
    }

    [System.Serializable]
    public class WalletsData
    {
        public int id_user;
        public List<Wallet> wallets;
    }

    [System.Serializable]
    public class Wallet
    {
        public float balance;
        public string name;
        public string currency;
        public int id_wallet;
    }

    [System.Serializable]
    public class OurUser : User
    {
        public string password;
        public OurUser(string name, string mail, string pass)
            : base(name, mail)
        {
            password = pass;
        }
    }

    [System.Serializable]
    public class Info
    {
        public string email;
        public int id;
        public string login;
    }

    [System.Serializable]
    public class UniqueCheck
    {
        public string email;  
        public string login;
    }

    [System.Serializable]
    public class transaction
    {
        public string amount;
        public float ReadyAmount;
        public string comment;
        public int data_of_creation;
        public int data_of_transaction;
        public int id_transaction;
        public string id_category;
        public int id_wallet;
        public string type;
        public string FormattedData_of_Creation;
        public string FormattedData_of_transaction;

        public void ConvertAmount()
        {
            try
            {
                ReadyAmount = float.Parse(amount.Replace(',', '.'), CultureInfo.InvariantCulture);
            }
            catch (FormatException)
            {
                Debug.Log("Ошибка: Некорректный формат строки.");
            }
        }
    }

    [System.Serializable]
    public class transactionsDataOBJ
    {
        public float balance;
        public string id;
        public float expense;
        public float income;
        public List<transaction> page0;

        public void ConvertSecondsToDate()
        {
            foreach(transaction transaction in page0)
            {
                transaction.ConvertAmount();
                DateTime dateTime = new DateTime(1970, 01, 02).AddSeconds(transaction.data_of_transaction);
                transaction.FormattedData_of_transaction = dateTime.ToString("dd-MM-yyyy");
                dateTime = new DateTime(1970, 01, 02).AddSeconds(transaction.data_of_creation);
                transaction.FormattedData_of_Creation = dateTime.ToString("dd-MM-yyyy");
            }
        }
    }

    [System.Serializable]
    public class Category
    {
        public int id_category;
        public string name;
        public string type;
        public string value;
    }

    [System.Serializable]
    public class Categories
    {
        public List<Category> expense;
        public List<Category> income;
    }

    [System.Serializable]
    public class StatsLists
    {
        public List<Stats> ListExpense;
        public List<Stats> ListIncome;
        public void FormateName()
        {
            foreach (Stats stats in ListExpense)
            {
                stats.FormatedName = Regex.Replace(stats.name, @"\\u([0-9A-Fa-f]{4})", match =>
                ((char)Int32.Parse(match.Groups[1].Value, NumberStyles.HexNumber)).ToString());
            }

            foreach (Stats stats in ListIncome)
            {
                stats.FormatedName = Regex.Replace(stats.name, @"\\u([0-9A-Fa-f]{4})", match =>
                ((char)Int32.Parse(match.Groups[1].Value, NumberStyles.HexNumber)).ToString());
            }
        }
    }

    [System.Serializable]
    public class Stats
    {
        public string name;
        public float sum;
        public string FormatedName;
    }

    public class Req : MonoBehaviour
    {
        //================= POST Request =================
        public void PostReq(string jsonData, string url, System.Action<string> onSuccess = null, System.Action<string> onError = null)
        {
            StartCoroutine(PostReqCoroutine(jsonData, url, onSuccess, onError));
        }

        private IEnumerator PostReqCoroutine(string jsonData, string url, System.Action<string> onSuccess, System.Action<string> onError)
        {
            using (UnityWebRequest webRequest = new UnityWebRequest(url, "POST"))
            {
                webRequest.SetRequestHeader("Content-Type", "application/json");
                byte[] rawData = Encoding.UTF8.GetBytes(jsonData);
                webRequest.uploadHandler = new UploadHandlerRaw(rawData);
                webRequest.downloadHandler = new DownloadHandlerBuffer();

                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    onError?.Invoke(webRequest.error);
                    Debug.LogError($"POST Error: {webRequest.error}");
                }
                else
                {
                    onSuccess?.Invoke(webRequest.downloadHandler.text);
                    Debug.Log("POST Success: " + webRequest.downloadHandler.text);
                }
            }
            Invoke("DestroyThis", 1f);
        }

        private void DestroyThis()
        {
            Destroy(this);
        }

        //================= GET Request =================
        public void GetReq(string url, System.Action<string> onSuccess = null, System.Action<string> onError = null)
        {
            StartCoroutine(GetReqCoroutine(url, onSuccess, onError));
        }

        private IEnumerator GetReqCoroutine(string url, System.Action<string> onSuccess, System.Action<string> onError)
        {
            using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
            {
                yield return webRequest.SendWebRequest();

                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    onError?.Invoke(webRequest.error);
                    Debug.LogError($"GET Error: {webRequest.error}");
                }
                else
                {
                    onSuccess?.Invoke(webRequest.downloadHandler.text);
                    Debug.Log("GET Success: " + webRequest.downloadHandler.text);
                }
            }
            Invoke("DestroyThis", 0.7f);
        }
    }
}
