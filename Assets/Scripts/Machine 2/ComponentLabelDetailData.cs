using UnityEngine;

[CreateAssetMenu(fileName = "ComponentLabelDetailData", menuName = "GMI/Component Label Detail Data")]
public class ComponentLabelDetailData : ScriptableObject
{
    public string componentName;
    public string specifications;
    [TextArea(3, 15)] public string description;
}