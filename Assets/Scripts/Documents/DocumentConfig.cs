using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Document Config", menuName = "GMI/Document Config")]
public class DocumentConfig : ScriptableObject
{
    public string componentName;
    public List<DocumentEntry> documents;
}

[Serializable]
public class DocumentEntry
{
    public string documentName;
    public Sprite[] documentFiles;
}
