using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepOverlayUI : MonoBehaviour
{
    [Header("Overlay Buttons")]
    public GameObject detailButton;
    public GameObject backButton;

    [Header("Popup References")]
    public GameObject popupPanel;
    public TextMeshProUGUI popupTitle;
    public TextMeshProUGUI popupDescription;
    public Button closeButton;
    public Button detailBtn;
    public Button backBtn;

    private StepData _currentStepData;

    public static StepOverlayUI Instance { get; private set; }

    void Awake()
    {
        Instance = this;

        detailBtn.onClick.AddListener(OpenPopup);
        backBtn.onClick.AddListener(OnClickBack);
        closeButton.onClick.AddListener(ClosePopup);

        popupPanel.SetActive(false);
        HideStepOverlay();
    }

    public void ShowStepOverlay(StepData data)
    {
        _currentStepData = data;
        detailButton.SetActive(true);
        backButton.SetActive(true);
    }

    public void HideStepOverlay()
    {
        detailButton.SetActive(false);
        backButton.SetActive(false);
        ClosePopup();
    }

    void OpenPopup()
    {
        if (_currentStepData == null) return;

        popupPanel.SetActive(true);
        popupTitle.text = $"Step {_currentStepData.stepNumber}: {_currentStepData.stepTitle}";
        popupDescription.text = _currentStepData.stepDescription;
    }

    void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    void OnClickBack()
    {
        StepMarker.CurrentActive?.OnBack();
    }
}