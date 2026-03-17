using UnityEngine;

public class SectionView : MonoBehaviour
{
    [Header("Data")]
    public SectionData data;

    [Header("References")]
    public GameObject meshRoot;        // parent semua mesh section ini
    public GameObject hotspotObject;   // floating button GO
    public GameObject animationRoot;
    public Animator[] animators;       // animator untuk flow arrow, dll
    public SectionLabelGroup labelGroup;

    public void SetVisible(bool visible)
    {
        if (meshRoot != null) meshRoot.SetActive(visible);
    }

    public void SetAnimation(bool visible)
    {
        if (animationRoot != null) animationRoot.SetActive(visible);
    }

    public void SetHotspot(bool visible)
    {
        if (hotspotObject != null) hotspotObject.SetActive(visible);
    }

    public void SetLabel(bool visible)
    {
        if (labelGroup != null)
        {
            if (visible) labelGroup.ShowAllLabels();
            else labelGroup.HideAllLabels();
        }
    }

    public void PlayAnimation()
    {
        foreach (var anim in animators)
            anim.Play("FlowAnimation");
    }

    public void StopAnimation()
    {
        foreach (var anim in animators)
            anim.enabled = false;
    }
}