using UnityEngine;

[CreateAssetMenu(fileName = "ComponentLabelData", menuName = "GMI/Component Label Data")]
public class ComponentLabelData : ScriptableObject
{
    public string componentName;
    [TextArea] public string specifications;
}