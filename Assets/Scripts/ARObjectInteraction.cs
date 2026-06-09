using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;

using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public class ARObjectInteraction : MonoBehaviour
{
    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 0.2f;

    [Header("Scale")]
    [SerializeField] private float scaleSpeed = 0.005f;
    [SerializeField] private float minScale = 0.2f;
    [SerializeField] private float maxScale = 5f;

    [Header("Double Tap")]
    [SerializeField] private float doubleTapTime = 0.3f;

    private float previousPinchDistance;

    private float lastTapTime;

    private Vector3 initialScale;
    private Quaternion initialRotation;

    private void Awake()
    {
        initialScale = transform.localScale;
        initialRotation = transform.rotation;
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }

    private void Update()
    {
        //HandleRotation();
        HandlePinchZoom();
        HandleDoubleTapReset();
    }

    private void HandleRotation()
    {
        if (Touch.activeTouches.Count != 1)
            return;

        Touch touch = Touch.activeTouches[0];

        if (touch.phase != UnityEngine.InputSystem.TouchPhase.Moved)
            return;

        float deltaX = touch.delta.x;

        transform.Rotate(
            Vector3.up,
            -deltaX * rotationSpeed,
            Space.World);
    }

    private void HandlePinchZoom()
    {
        if (Touch.activeTouches.Count != 2)
        {
            previousPinchDistance = 0;
            return;
        }

        Touch touch0 = Touch.activeTouches[0];
        Touch touch1 = Touch.activeTouches[1];

        float currentDistance =
            Vector2.Distance(
                touch0.screenPosition,
                touch1.screenPosition);

        if (previousPinchDistance == 0)
        {
            previousPinchDistance = currentDistance;
            return;
        }

        float deltaDistance =
            currentDistance - previousPinchDistance;

        float scaleMultiplier =
            1f + deltaDistance * scaleSpeed;

        Vector3 newScale =
            transform.localScale * scaleMultiplier;

        float uniformScale =
            Mathf.Clamp(
                newScale.x,
                minScale,
                maxScale);

        transform.localScale =
            Vector3.one * uniformScale;

        previousPinchDistance = currentDistance;
    }

    private void HandleDoubleTapReset()
    {
        if (Touch.activeTouches.Count != 1)
            return;

        Touch touch = Touch.activeTouches[0];

        if (touch.phase != UnityEngine.InputSystem.TouchPhase.Began)
            return;

        if (Time.time - lastTapTime < doubleTapTime)
        {
            ResetTransform();
        }

        lastTapTime = Time.time;
    }

    private void ResetTransform()
    {
        transform.localScale = initialScale;
        transform.rotation = initialRotation;
    }
}