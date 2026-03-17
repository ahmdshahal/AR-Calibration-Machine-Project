using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ComponentLabel : MonoBehaviour
{
    [Header("Data")]
    public ComponentLabelData data;

    [Header("References")]
    public Transform targetPoint;      // titik di komponen yang dituju arrow
    public Transform labelAnchor;      // posisi label card di world space

    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI specText;
    public GameObject labelCard;

    [Header("Line Settings")]
    public LineRenderer lineRenderer;
    public LineRenderer[] lineRenderers;
    public Transform[] targetPoints;
    public int curveResolution = 20;
    public float curveHeight = 0.05f;  // tinggi kurva bezier

    [Header("Arrow Settings")]
    public GameObject arrowHeadPrefab;  // prefab cone/triangle kecil
    private GameObject _arrowInstance;

    private Camera _arCamera;
    private Canvas _canvas;

    void Start()
    {
        _arCamera = Camera.main;
        _canvas = GetComponentInParent<Canvas>();

        if (data != null)
        {
            nameText.text = data.componentName;
            specText.text = data.specifications;
        }

        SetupLineRenderer();

        if (arrowHeadPrefab != null)
            _arrowInstance = Instantiate(arrowHeadPrefab, targetPoint.position, Quaternion.identity, transform);
    }

    void Update()
    {
        // Billboard label card
        if (_arCamera != null)
        {
            labelCard.transform.LookAt(
                labelCard.transform.position + _arCamera.transform.rotation * Vector3.forward,
                _arCamera.transform.rotation * Vector3.up);
        }

        // Update bezier line setiap frame
        // karena posisi bisa berubah saat AR tracking
        //DrawBezierLine();
        DrawStraightLine();
    }

    void SetupLineRenderer()
    {
        if (lineRenderer == null) return;

        lineRenderer.positionCount = 2;
        //lineRenderer.positionCount = curveResolution; // Curve
        lineRenderer.useWorldSpace = true;
        lineRenderer.startWidth = 0.002f;
        lineRenderer.endWidth = 0.002f;

        // Material line putih
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    void DrawStraightLine()
    {
        if (lineRenderer == null || targetPoint == null || labelAnchor == null) return;

        lineRenderer.SetPosition(0, labelAnchor.position);
        lineRenderer.SetPosition(1, targetPoint.position);

        if (_arrowInstance != null)
        {
            _arrowInstance.transform.position = targetPoint.position;

            Vector3 direction = targetPoint.position - labelAnchor.position;
            if (direction != Vector3.zero)
                _arrowInstance.transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void DrawBezierLine()
    {
        if (lineRenderer == null || targetPoint == null || labelAnchor == null) return;

        Vector3 start = labelAnchor.position;
        Vector3 end = targetPoint.position;

        // Control point untuk kurva — di tengah, diangkat ke atas
        Vector3 mid = (start + end) / 2f;
        mid += Vector3.up * curveHeight;

        for (int i = 0; i < curveResolution; i++)
        {
            float t = i / (float)(curveResolution - 1);
            lineRenderer.SetPosition(i, CalculateQuadraticBezier(start, mid, end, t));
        }
    }

    Vector3 CalculateQuadraticBezier(Vector3 p0, Vector3 p1, Vector3 p2, float t)
    {
        float u = 1 - t;
        return (u * u * p0) + (2 * u * t * p1) + (t * t * p2);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}