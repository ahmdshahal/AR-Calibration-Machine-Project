using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentDetailPopup : MonoBehaviour
{
    [Header("Panel")]
    public GameObject popupPanel;
    public RectTransform BG;

    [Header("Content")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI specificationsText;
    public TextMeshProUGUI descriptionText;

    [Header("Button")]
    public Button closeButton;

    public static ComponentDetailPopup Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        closeButton.onClick.AddListener(Hide);
        popupPanel.SetActive(false);
    }

    public void Show(ComponentLabelDetailData data)
    {
        popupPanel.SetActive(true);
        nameText.text = data.componentName;
        specificationsText.text = data.specifications;
        descriptionText.text = data.description;

        LayoutRebuilder.ForceRebuildLayoutImmediate(BG);
    }

    public void Hide()
    {
        popupPanel.SetActive(false);
    }
}