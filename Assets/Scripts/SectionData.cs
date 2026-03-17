using UnityEngine;

[CreateAssetMenu(fileName = "SectionData", menuName = "GMI/Section Data")]
public class SectionData : ScriptableObject
{
    public string sectionName;
    [TextArea] public string description;
    public Sprite icon;
}