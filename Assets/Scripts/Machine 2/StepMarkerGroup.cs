using UnityEngine;

public class StepMarkerGroup : MonoBehaviour
{
    public StepMarker[] stepMarkers;

    public static StepMarkerGroup Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    public void ShowAllMarkers()
    {
        foreach (var marker in stepMarkers)
            marker.gameObject.SetActive(true);
    }

    public void HideAllMarkers()
    {
        // Reset active state dulu
        if (StepMarker.CurrentActive != null)
            StepMarker.CurrentActive.Deactivate();

        foreach (var marker in stepMarkers)
            marker.gameObject.SetActive(false);

        StepOverlayUI.Instance?.HideStepOverlay();
    }
}