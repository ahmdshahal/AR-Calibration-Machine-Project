using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DocumentLabelPopup : MonoBehaviour
{
    [Header("Data")]
    public DocumentConfig data;

    [Header("Label References")]
    public TextMeshProUGUI nameText;
    private GameObject labelCard;

    [Header("Line Settings")]
    public LineRenderer[] lineRenderers;
    public GameObject arrowHeadPrefab;
    public Transform labelAnchor;
    private List<Transform> targetPoints = new();

    private GameObject[] _arrowInstances;
    private Camera _arCamera;

    private void Start()
    {
        _arCamera = Camera.main;
        labelCard = labelAnchor.gameObject;

        /*if (data != null)
            nameText.text = data.componentName;*/

        for (int i = 0; i < lineRenderers.Length; i++)
        {
            targetPoints.Add(lineRenderers[i].transform);
            SetupLineRenderer(lineRenderers[i]);
        }

        _arrowInstances = new GameObject[lineRenderers.Length];
        for (int i = 0; i < lineRenderers.Length; i++)
        {
            if (arrowHeadPrefab != null && i < targetPoints.Count && _arrowInstances[i] == null)
                _arrowInstances[i] = Instantiate(arrowHeadPrefab, targetPoints[i].position,
                                                    Quaternion.identity, transform);
        }

        var btn = labelCard.GetComponent<Button>();
        if (btn == null) btn = labelCard.AddComponent<Button>();
        btn.onClick.AddListener(OnTapLabel);
    }

    void Update()
    {
        if (_arCamera == null)
            _arCamera = Camera.main;

        // Billboard
        if (_arCamera != null)
        {
            labelCard.transform.LookAt(
                labelCard.transform.position + _arCamera.transform.rotation * Vector3.forward,
                _arCamera.transform.rotation * Vector3.up);
        }

        DrawStraightLine();
    }

    void SetupLineRenderer(LineRenderer lr)
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

    void DrawStraightLine()
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

    void OnTapLabel()
    {
        if (data != null)
            DocumentPopup.Instance?.Show(data);
    }
}
