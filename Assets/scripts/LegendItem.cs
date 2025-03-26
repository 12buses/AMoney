using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LegendItem : MonoBehaviour
{
    [SerializeField] private TMP_Text categoryText;
    [SerializeField] private TMP_Text valueText; // Добавляем поле для отображения значения
    [SerializeField] private TMP_Text percentageText;
    [SerializeField] private Image colorBox;
    private PieChart pieChart;
    public string CategoryName { get; private set; }

    private Color originalColor;
    private Color disabledColor = Color.gray; // Цвет для скрытых категорий

    public void Initialize(PieChart chart, string category, Color color, float value, float percentage)
    {
        pieChart = chart;
        CategoryName = category;
        categoryText.text = category;
        valueText.text = value.ToString("0.00");  // Отображаем значение рядом с категорией
        percentageText.text = (percentage * 100).ToString("0.00") + "%";
        originalColor = color;
        colorBox.color = color;

        this.gameObject.AddComponent<Button>().GetComponent<Button>().onClick.AddListener(() => pieChart.ToggleCategory(CategoryName));
    }

    public void SetActiveState(bool isActive)
    {
        colorBox.color = isActive ? originalColor : disabledColor;
        categoryText.color = isActive ? originalColor : disabledColor;
        valueText.color = isActive ? Color.black : Color.gray; // Меняем цвет текста в зависимости от видимости
    }
}
