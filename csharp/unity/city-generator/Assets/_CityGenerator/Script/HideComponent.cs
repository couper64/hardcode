using UnityEngine;

public class HideComponent : MonoBehaviour
{
    [SerializeField] private GameObject[] targets;

    public void Hide()
    {
        foreach (GameObject go in targets)
        {
            if (go.activeSelf)
            {
                go.SetActive(false);
            }
        }
    }

    public void UnHide()
    {
        foreach (GameObject go in targets)
        {
            if (!go.activeSelf)
            {
                go.SetActive(true);
            }
        }
    }
}
