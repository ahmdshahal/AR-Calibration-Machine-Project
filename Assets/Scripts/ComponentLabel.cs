using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

//[ExecuteAlways]
public class ComponentLabel : MonoBehaviour
{
    [Header("Data")]
    public ComponentLabelData data;

    [Header("References")]
    public Transform labelAnchor;      // posisi label card di world space

    [Header("UI References")]
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI specText;
    private GameObject labelCard;

    [Header("Line Settings")]
    public LineRenderer[] lineRenderers;
    private List<Transform> targetPoints = new();

    [Header("Arrow Settings")]
    public GameObject arrowHeadPrefab;  // prefab cone/triangle kecil
    private GameObject[] _arrowInstances;

    private Camera _arCamera;
    private Canvas _canvas;

    void Start()
    {
        _arCamera = Camera.main;
        _canvas = GetComponentInParent<Canvas>();
        labelCard = labelAnchor.gameObject;

        if (data != null)
        {
            nameText.text = data.componentName;
            specText.text = data.specifications;
        }

        // Setup line renderer
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            targetPoints.Add(lineRenderers[i].transform);
            SetupLineRenderers(lineRenderers[i]);
        }

        // Spawn arrow hanya saat Play Mode
            _arrowInstances = new GameObject[lineRenderers.Length];
            for (int i = 0; i < lineRenderers.Length; i++)
            {
                if (arrowHeadPrefab != null && i < targetPoints.Count && _arrowInstances[i] == null)
                    _arrowInstances[i] = Instantiate(arrowHeadPrefab, targetPoints[i].position,
                                                     Quaternion.identity, transform);
            }
        
    }

    void Update()
    {
        if (_arCamera == null)
            _arCamera = Camera.main;

        // Billboard label card
        if (_arCamera != null)
        {
            labelCard.transform.LookAt(
                labelCard.transform.position + _arCamera.transform.rotation * Vector3.forward,
                _arCamera.transform.rotation * Vector3.up);
        }

        DrawStraightLines();
    }

    void SetupLineRenderers(LineRenderer lr)
    {
        if (lr == null) return;

        lr.positionCount = 2;
        lr.useWorldSpace = true;
        lr.startWidth = 0.003f;
        lr.endWidth = 0.003f;
        lr.material = new Material(Shader.Find("Sprites/Default"));
        lr.startColor = Color.white;
        lr.endColor = Color.white;
    }

    void DrawStraightLines()
    {
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            if (lineRenderers[i] == null || i >= targetPoints.Count || targetPoints[i] == null) continue;

            lineRenderers[i].SetPosition(0, labelAnchor.position);
            lineRenderers[i].SetPosition(1, targetPoints[i].position);

            if (_arrowInstances != null && i < _arrowInstances.Length && _arrowInstances[i] != null)
            {
                _arrowInstances[i].transform.position = targetPoints[i].position;

                Vector3 direction = targetPoints[i].position - labelAnchor.position;
                if (direction != Vector3.zero)
                    _arrowInstances[i].transform.rotation = Quaternion.LookRotation(direction);
            }
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