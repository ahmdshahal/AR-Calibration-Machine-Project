using UnityEngine;

public class StepMarkerTapHandler : MonoBehaviour
{
    public StepMarker stepMarker;

    private Camera _arCamera;

    void Start()
    {
        _arCamera = Camera.main;
    }

    void Update()
    {
        if (_arCamera == null) return;

        // Handle touch
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
        {
            TryHit(Input.GetTouch(0).position);
        }

        // Handle mouse (Editor testing)
        if (Input.GetMouseButtonDown(0))
        {
            TryHit(Input.mousePosition);
        }
    }

    void TryHit(Vector2 screenPos)
    {
        Ray ray = _arCamera.ScreenPointToRay(screenPos);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == transform || hit.transform.IsChildOf(transform))
                stepMarker.OnTap();
        }
    }
}