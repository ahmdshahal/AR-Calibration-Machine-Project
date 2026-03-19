using UnityEngine;

public class UIAnchorFollower : MonoBehaviour
{
    [Header("Assign UI Anchor (RectTransform in Canvas)")]
    public RectTransform objectPivot; // GameObject kosong di canvas UI Screen Space - Camera

    [Header("Camera Settings")]
    public float distanceFromCamera = 100f; // jarak ke camera

    private Camera uiCamera;        // Camera yang dipakai canvas (Screen Space - Camera)

    private void Start()
    {
        uiCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (objectPivot == null || uiCamera == null)
            return;

        // Get screen point dari anchor UI
        Vector3 screenPos = RectTransformUtility.WorldToScreenPoint(uiCamera, objectPivot.position);

        // Konversi screen point ke world position
        Vector3 worldPos = uiCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, distanceFromCamera));

        // Update posisi object 3D ini
        transform.position = worldPos;
    }
}
