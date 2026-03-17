using UnityEngine;
using TMPro;

public class PopupUI : MonoBehaviour
{
    [Header("References")]
    public GameObject popupPanel;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public GameObject nextButton;
    public GameObject backButton;

    public static PopupUI Instance { get; private set; }

    void Awake()
    {
        Instance = this;
        popupPanel.SetActive(false);
    }

    public void Show(SectionData data)
    {
        popupPanel.SetActive(true);
        titleText.text = data.sectionName;
        descriptionText.text = data.description;
    }

    public void Hide()
    {
        popupPanel.SetActive(false);
    }

    // Assign ke Next button OnClick
    public void OnClickNext()
    {
        SectionManager.Instance.SelectNext();
    }

    // Assign ke Back button OnClick
    public void OnClickBack()
    {
        SectionManager.Instance.BackToOverview();
    }
}