using UnityEngine;
using UnityEngine.SceneManagement;

public class EngagementScreen : MonoBehaviour
{
    [Header("Delay before loading new scene.")]
    [SerializeField]
    private float openDelay;

    private void Reset()
    {
        // Default values.
        openDelay = 5.00f;
    }

    public void OpenMainMenu()
    {
        // To simulate delay.
        StartCoroutine(Open("S_MainMenu_Test"));
    }

    private System.Collections.IEnumerator Open(string scene)
    {
        // Custom delay.
        yield return new WaitForSeconds(openDelay);

        // Then, load new scene.
        SceneManager.LoadScene(scene);
    }
}
