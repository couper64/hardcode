using UnityEngine;

public class HideTriggerComponent : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<HideComponent>(out HideComponent h))
        {
            h.UnHide();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<HideComponent>(out HideComponent h))
        {
            h.Hide();
        }
    }
}
