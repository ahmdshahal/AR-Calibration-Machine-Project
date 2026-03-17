using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotspotButton : MonoBehaviour
{
    [Header("References")]
    public SectionView targetSection;
    public TextMeshProUGUI label;

    private Camera _arCamera;

    void Start()
    {
        _arCamera = Camera.main;

        if (label != null && targetSection != null)
            label.text = targetSection.data.sectionName;
    }

    void Update()
    {
        // Billboard — selalu hadap kamera
        if (_arCamera != null)
            transform.LookAt(transform.position + _arCamera.transform.rotation * Vector3.forward,
                             _arCamera.transform.rotation * Vector3.up);
    }

    public void OnTap()
    {
        SectionManager.Instance.SelectSection(targetSection);
    }
}