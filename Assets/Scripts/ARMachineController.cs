using UnityEngine;
using UnityEngine.XR.ARFoundation;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ARMachineController : MonoBehaviour
{
    public GameObject machinePrefab;

    private ARTrackedImageManager _manager;
    private GameObject _spawnedMachine;

    void Awake()
    {
        _manager = GetComponent<ARTrackedImageManager>();
    }

    void OnEnable()
    {
        _manager.trackablesChanged.AddListener(OnTrackedImagesChanged);
    }

    void OnDisable()
    {
        _manager.trackablesChanged.RemoveListener(OnTrackedImagesChanged);
    }

    void OnTrackedImagesChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        Camera mainCam = Camera.main;

        foreach (var trackedImage in args.added)
        {
            if (_spawnedMachine == null)
            {
                _spawnedMachine = Instantiate(machinePrefab, trackedImage.transform.position, trackedImage.transform.rotation * Quaternion.Euler(0, 180, 0));

                /*if (mainCam != null)
                {
                    Vector3 lookPos = mainCam.transform.position - _spawnedMachine.transform.position;
                    lookPos.y = 0;
                    _spawnedMachine.transform.rotation = Quaternion.LookRotation(lookPos);
                }*/
                SectionManager.Instance.ShowOverview();

                Debug.Log("Object Spawned");
            }
        }

        foreach (var trackedImage in args.updated)
        {
            if (_spawnedMachine != null)
                _spawnedMachine.transform.SetPositionAndRotation(
                    trackedImage.transform.position,
                    trackedImage.transform.rotation * Quaternion.Euler(0, 180, 0));

            /*if (mainCam != null)
            {
                Vector3 lookPos = mainCam.transform.position - _spawnedMachine.transform.position;
                lookPos.y = 0;
                _spawnedMachine.transform.rotation = Quaternion.LookRotation(lookPos);
            }*/
        }
    }
}