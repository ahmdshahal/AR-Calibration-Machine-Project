using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MachineSwitchUI : MonoBehaviour
{
    [Header("Machine References")]
    public GameObject machine1Root;
    public GameObject machine2Root;

    [Header("Button References")]
    public Button btnMachine1;
    public Button btnMachine2;

    [Header("Button Colors")]
    public Color activeColor = new Color(0.2f, 0.6f, 1f);
    public Color inactiveColor = new Color(0.4f, 0.4f, 0.4f);

    private int _activeMachine = 1;

    void Start()
    {
        btnMachine1.onClick.AddListener(() => SwitchMachine(1));
        btnMachine2.onClick.AddListener(() => SwitchMachine(2));

        SwitchMachine(1);
    }

    public void SwitchMachine(int machineIndex)
    {
        _activeMachine = machineIndex;

        machine1Root.SetActive(machineIndex == 1);
        machine2Root.SetActive(machineIndex == 2);

        // Update button color
        btnMachine1.GetComponent<Image>().color = machineIndex == 1 ? activeColor : inactiveColor;
        btnMachine2.GetComponent<Image>().color = machineIndex == 2 ? activeColor : inactiveColor;

        // Reset Machine 2 state saat switch
        if (machineIndex == 2)
            StepMarkerGroup.Instance?.ShowAllMarkers();
        else
            StepMarkerGroup.Instance?.HideAllMarkers();
    }
}