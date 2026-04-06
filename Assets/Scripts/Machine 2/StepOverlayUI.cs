using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StepOverlayUI : MonoBehaviour
{
    [Header("Overlay Buttons")]
    public GameObject detailButton;
    public GameObject backButton;
    public GameObject stepSwitchRoot;
    public GameObject stepRoot;
    public GameObject partRoot;
    public Button showStepBtn;
    public Button showPartBtn;

    [Header("Button Colors")]
    public Color activeColor = new Color(0.2f, 0.6f, 1f);
    public Color inactiveColor = new Color(0.4f, 0.4f, 0.4f);

    [Header("Popup References")]
    public RectTransform BG;
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
        showStepBtn.onClick.AddListener(() => ActivateStep(true));
        showPartBtn.onClick.AddListener(() => ActivateStep(false));

        ActivateStep(true);

        popupPanel.SetActive(false);
        HideStepOverlay();
    }

    public void ShowStepOverlay(StepData data)
    {
        _currentStepData = data;
        detailButton.SetActive(true);
        backButton.SetActive(true);
        stepSwitchRoot.SetActive(false);
    }

    public void HideStepOverlay()
    {
        detailButton.SetActive(false);
        backButton.SetActive(false);
        stepSwitchRoot.SetActive(true);
        ClosePopup();
    }

    void OpenPopup()
    {
        if (_currentStepData == null) return;

        popupPanel.SetActive(true);
        popupTitle.text = $"Step {_currentStepData.stepNumber}: {_currentStepData.stepTitle}";
        popupDescription.text = _currentStepData.stepDescription;

        LayoutRebuilder.ForceRebuildLayoutImmediate(BG);
    }

    void ClosePopup()
    {
        popupPanel.SetActive(false);
    }

    void OnClickBack()
    {
        StepMarker.CurrentActive?.OnBack();
    }

    void ActivateStep(bool active)
    {
        stepRoot.SetActive(active);
        //partRoot.SetActive(!active);

        showStepBtn.GetComponent<Image>().color = active ? activeColor : inactiveColor;
        showPartBtn.GetComponent<Image>().color = !active ? activeColor : inactiveColor;

        StepMarkerGroup.Instance?.ShowAllLabels(!active);
    }
}