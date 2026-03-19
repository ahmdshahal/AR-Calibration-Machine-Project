using UnityEngine;
using System.Collections;

public class SectionManager : MonoBehaviour
{
    [Header("References")]
    public GameObject fullMachineRoot;     // parent semua section mesh
    public SectionView[] sections;

    [Header("Settings")]
    public float fadeSpeed = 0.3f;

    [Header("Button")]
    public GameObject backSectionButton;
    public GameObject backMainMenuButton;

    private SectionView _activeSection;
    private bool _isOverview = true;

    public static SectionManager Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        ShowOverview();
    }

    // Called saat AR marker pertama terdeteksi
    public void ShowOverview()
    {
        _isOverview = true;
        fullMachineRoot.SetActive(true);

        foreach (var s in sections)
        {
            s.SetVisible(true);
            s.SetAnimation(true);
            s.SetHotspot(true);
            s.SetLabel(false);
        }

        // Button
        backSectionButton.SetActive(false);
        backMainMenuButton.SetActive(true);
    }

    public void SelectSection(SectionView section)
    {
        if (_activeSection == section) return;

        _isOverview = false;
        //fullMachineRoot.SetActive(false);

        // Hide section lain
        foreach (var s in sections)
        {
            s.SetVisible(false);
            s.SetHotspot(false);
            s.SetAnimation(false);
            s.SetLabel(false);
        }

        // Show section yang dipilih
        _activeSection = section;
        _activeSection.SetVisible(true);
        _activeSection.SetAnimation(true);
        _activeSection.SetLabel(true);
        //_activeSection.PlayAnimation();

        // Button
        backSectionButton.SetActive(true);
        backMainMenuButton.SetActive(false);

        //PopupUI.Instance.Show(_activeSection.data);
    }

    public void BackToOverview()
    {
        if (_activeSection != null)
        {
            //_activeSection.SetVisible(false);
            //_activeSection.StopAnimation();
            _activeSection = null;
        }

        ShowOverview();
        //PopupUI.Instance.Hide();
    }

    public void SelectNext()
    {
        if (_activeSection == null) return;

        int currentIndex = System.Array.IndexOf(sections, _activeSection);
        int nextIndex = (currentIndex + 1) % sections.Length;
        SelectSection(sections[nextIndex]);
    }
}