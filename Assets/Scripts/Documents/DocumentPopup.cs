using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DocumentPopup : MonoBehaviour
{
    public static DocumentPopup Instance { get; private set; }

    [Header("Panel")]
    public GameObject popupPanel;

    [Header("Header")]
    public TMP_Text headerTitleText;

    [Header("Navbar")]
    public RectTransform navbarContainer;
    public Button navbarButtonPrefab;
    public Sprite fileSprite;
    public Color activeColor;
    public Color inactiveColor;

    [Header("Image Viewer")]
    public GameObject imageViewerRoot;
    public RectTransform imageContainer;
    public ScrollRect scrollRect;
    public Image documentImagePrefab;

    [Header("Close")]
    public Button closeButton;

    private DocumentConfig _currentConfig;
    private List<Button> _navbarButtons = new();
    private List<Image> _spawnedImages = new();

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Hide();

        closeButton.onClick.AddListener(Hide);
    }

    public void Show(DocumentConfig config)
    {
        _currentConfig = config;
        popupPanel.SetActive(true);
        BuildNavbar();
        ShowDocument(0);
        Canvas.ForceUpdateCanvases();
    }

    public void Hide()
    {
        popupPanel.SetActive(false);
        ClearImages();
        ClearNavbar();
    }

    // Navigation Bar
    private void BuildNavbar()
    {
        ClearNavbar();

        // Button for each document
        for (int i = 0; i < _currentConfig.documents.Count; i++)
        {
            int index = i;
            var btnGO = Instantiate(navbarButtonPrefab, navbarContainer);
            //btnGO.GetComponentInChildren<Image>().sprite = fileSprite;
            btnGO.onClick.AddListener(() => ShowDocument(index));

            _navbarButtons.Add(btnGO);
        }

        Canvas.ForceUpdateCanvases();
    }

    private void ClearNavbar()
    {
        foreach (var btn in _navbarButtons)
            Destroy(btn.gameObject);

        _navbarButtons.Clear();
    }

    private void UpdateNavbarButtonVisual(int index)
    {
        for (int i = 0; i < _navbarButtons.Count; i++)
        {
            _navbarButtons[i].GetComponent<Image>().color = (i == index ? activeColor : inactiveColor);
        }
    }

    // Image Viewer
    private void ShowDocument(int index)
    {
        var doc = _currentConfig.documents[index];

        headerTitleText.text = doc.documentName;
        imageViewerRoot.SetActive(true);
        UpdateNavbarButtonVisual(index);

        ClearImages();

        for (int i = 0; i < _currentConfig.documents[index].documentFiles.Length; i++)
        {
            var img = Instantiate(documentImagePrefab, imageContainer);
            img.name = _currentConfig.documents[index].documentFiles[i].name;
            img.sprite = _currentConfig.documents[index].documentFiles[i];
            //img.SetNativeSize();
            _spawnedImages.Add(img);
        }

        scrollRect.verticalNormalizedPosition = 1f;

        Canvas.ForceUpdateCanvases();
    }

    private void ClearImages()
    {
        foreach (var img in _spawnedImages)
            Destroy(img.gameObject);

        _spawnedImages.Clear();
    }
}
