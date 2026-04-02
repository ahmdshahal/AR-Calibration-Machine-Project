using UnityEngine;
using TMPro;

public class StepMarker : MonoBehaviour
{
    [Header("Data")]
    public StepData data;

    [Header("References")]
    public TMP_Text numberText;         // World Space TMP, tampilkan angka
    public GameObject labelArrowRoot;      // World Space label + arrow, default hidden
    public ComponentLabel componentLabel;  // reuse script label yang sudah ada

    private Camera _arCamera;
    private bool _isActive = false;

    public static StepMarker CurrentActive { get; private set; }

    void Start()
    {
        _arCamera = Camera.main;

        if (numberText != null && data != null)
            numberText.text = $"Step {data.stepNumber}";

        if (labelArrowRoot != null)
            labelArrowRoot.SetActive(false);
    }

    void Update()
    {
        if (_arCamera == null)
            _arCamera = Camera.main;

        // Billboard angka selalu hadap kamera
        if (_arCamera != null)
        {
            transform.LookAt(
                transform.position + _arCamera.transform.rotation * Vector3.forward,
                _arCamera.transform.rotation * Vector3.up);
        }
    }

    public void OnTap()
    {
        // Deactivate marker sebelumnya
        if (CurrentActive != null && CurrentActive != this)
            CurrentActive.Deactivate();

        _isActive = true;
        CurrentActive = this;

        // Show label arrow
        if (labelArrowRoot != null)
            labelArrowRoot.SetActive(true);

        // Show overlay buttons
        StepOverlayUI.Instance?.ShowStepOverlay(data);
    }

    public void Deactivate()
    {
        _isActive = false;

        if (labelArrowRoot != null)
            labelArrowRoot.SetActive(false);
    }

    public void OnBack()
    {
        Deactivate();
        CurrentActive = null;
        StepOverlayUI.Instance?.HideStepOverlay();
    }
}