using System.Collections;
using UnityEngine;
using AndroidNativeCore;
using System;
using TMPro;
using System.Globalization;

public class CalendarDatePicker : MonoBehaviour
{
    public TMP_InputField DateInputfield;
    public CanvasGroup canvasGroup; // Ссылка на CanvasGroup для анимации
    private Coroutine fadeCoroutine;

    public void OnButtonClicked()
    {
        DateTime today = DateTime.Today;
        Pickers datePicker = new Pickers();
        datePicker.pickDate(today.Year, today.Month, today.Day, OnDatePicked);
    }

    private void OnDatePicked(int year, int month, int day)
    {
        string formattedDate = FormatAndValidateDate(day, month, year);
        
        if (DateInputfield.text != formattedDate)
        {
            DateInputfield.text = formattedDate;
            StartFadeAnimation();
        }
    }

    private string FormatAndValidateDate(int day, int month, int year)
    {
        try
        {
            // Собираем дату с ведущими нулями
            string dateStr = $"{day:00}-{month:00}-{year}";
            
            // Проверка парсинга
            if (!DateTime.TryParseExact(dateStr, "dd-MM-yyyy", 
                CultureInfo.InvariantCulture, 
                DateTimeStyles.None, 
                out DateTime parsedDate))
            {
                return DateTime.Now.ToString("dd-MM-yyyy");
            }

            // Проверка на будущую дату
            if (parsedDate > DateTime.Now)
            {
                return DateTime.Now.ToString("dd-MM-yyyy");
            }

            // Проверка валидности дня для месяца
            int daysInMonth = DateTime.DaysInMonth(year, month);
            day = Math.Clamp(day, 1, daysInMonth);
            
            return $"{day:00}-{month:00}-{year}";
        }
        catch
        {
            return DateTime.Now.ToString("dd-MM-yyyy");
        }
    }

    private void StartFadeAnimation()
    {
        if (fadeCoroutine != null)
        {
            StopCoroutine(fadeCoroutine);
        }
        fadeCoroutine = StartCoroutine(FadeAlpha());
    }

    private IEnumerator FadeAlpha()
    {
        if (canvasGroup == null) yield break;

        float duration = 0.3f;
        float targetAlpha = 0.5f;

        // Затемнение
        float elapsed = 0f;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(1f, targetAlpha, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Восстановление
        elapsed = 0f;
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(targetAlpha, 1f, elapsed/duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = 1f;
        fadeCoroutine = null;
    }
}