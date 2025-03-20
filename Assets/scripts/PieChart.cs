using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;  // Для использования TextMeshPro

public class PieChart : MonoBehaviour
{
    [SerializeField] private GameObject segmentPrefab;
    [SerializeField] private Color[] colors;
    [SerializeField] private Transform legendContainer;
    [SerializeField] private GameObject legendItemPrefab;

    [Header("Настройки текста")]
    [SerializeField] private Color textColor = Color.black;
    [SerializeField] private float textFontSize = 24f;
    [SerializeField] private float textRadius = 120f;  // Радиус для смещения текста

    private List<Image> createdSegments = new List<Image>();
    private Dictionary<string, bool> categoryVisibility = new Dictionary<string, bool>();
    private List<LegendItem> legendItems = new List<LegendItem>();

    public string[] testCategories;
    public float[] testValues;
    private float[] percentage;

    private void Start()
    {
        InitializeCategoryVisibility();
        CreatePieChart();
        CreateLegend();
    }

    public void Restart()
    {
        InitializeCategoryVisibility();
        CreatePieChart();
        CreateLegend();
    }

    private void InitializeCategoryVisibility()
    {
        categoryVisibility.Clear();
        foreach (var category in testCategories)
        {
            categoryVisibility[category] = true;
        }
    }

    public void ToggleCategory(string category)
    {
        if (categoryVisibility.ContainsKey(category))
        {
            categoryVisibility[category] = !categoryVisibility[category];
            CreatePieChart();
            UpdateLegendColors();
        }
    }

    public void CreatePieChart()
    {
        Debug.Log("Создаётся новая круговая диаграмма");

        // Удаляем старые сегменты
        foreach (var segment in createdSegments)
        {
            Destroy(segment.gameObject);
        }
        createdSegments.Clear();

        percentage = new float[testValues.Length];

        // Вычисляем общий объем
        float totalValue = 0f;
        for (int i = 0; i < testValues.Length; i++)
        {
            if (categoryVisibility[testCategories[i]])
            {
                totalValue += testValues[i];
            }
        }
        if (totalValue == 0) return;

        float currentFill = 0f;
        for (int i = 0; i < testValues.Length; i++)
        {
            if (!categoryVisibility[testCategories[i]]) continue;
            // Создаем сегмент
            GameObject newSegment = Instantiate(segmentPrefab, transform);
            newSegment.name = testCategories[i];
            Image segmentImage = newSegment.GetComponent<Image>();
            createdSegments.Add(segmentImage);
            percentage[i] = testValues[i] / totalValue;
            segmentImage.type = Image.Type.Filled;
            segmentImage.fillMethod = Image.FillMethod.Radial360;
            segmentImage.fillAmount = percentage[i];
            segmentImage.color = colors[i % colors.Length];
            // Поворачиваем сегмент
            newSegment.transform.rotation = Quaternion.Euler(0, 0, -currentFill * 360f);
            currentFill += percentage[i];
        }
    }

    private void CreateLegend()
    {
        foreach (Transform child in legendContainer)
        {
            Destroy(child.gameObject);
        }
        legendItems.Clear();

        for (int i = 0; i < testCategories.Length; i++)
        {
            GameObject newLegendItem = Instantiate(legendItemPrefab, legendContainer);
            LegendItem legend = newLegendItem.GetComponent<LegendItem>();
            legend.Initialize(this, testCategories[i], colors[i % colors.Length], testValues[i], percentage[i]);
            legendItems.Add(legend);
        }
        UpdateLegendColors();
    }

    private void UpdateLegendColors()
    {
        foreach (var legend in legendItems)
        {
            bool isActive = categoryVisibility[legend.CategoryName];
            legend.SetActiveState(isActive);
        }
    }
}
