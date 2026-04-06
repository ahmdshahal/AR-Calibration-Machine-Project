using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System.Collections;

public class ARSessionHandler : MonoBehaviour
{
    public ARSession arSession;

    void OnEnable()
    {
        StartCoroutine(RestartARSession());
    }

    IEnumerator RestartARSession()
    {
        // Reset session
        arSession.Reset();

        // Tunggu sebentar sebelum re-enable
        yield return new WaitForSeconds(0.5f);

        // Pastikan session enabled
        arSession.enabled = false;
        yield return null;
        arSession.enabled = true;
    }
}