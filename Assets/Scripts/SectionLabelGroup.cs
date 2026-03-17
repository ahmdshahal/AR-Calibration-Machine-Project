using UnityEngine;

public class SectionLabelGroup : MonoBehaviour
{
    public ComponentLabel[] labels;

    // Dipanggil oleh SectionManager saat section di-activate
    public void ShowAllLabels()
    {
        foreach (var label in labels)
            label.Show();
    }

    // Dipanggil saat section di-hide atau back to overview
    public void HideAllLabels()
    {
        foreach (var label in labels)
            label.Hide();
    }
}