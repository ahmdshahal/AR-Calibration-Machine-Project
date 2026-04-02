using UnityEngine;

[CreateAssetMenu(fileName = "StepData", menuName = "GMI/Step Data")]
public class StepData : ScriptableObject
{
    public int stepNumber;
    public string stepTitle;
    [TextArea(3, 6)] public string stepDescription;
}