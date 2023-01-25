using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    [Header("Button to save Player Data.")]
    [SerializeField]
    private LevelProgressor levelProgressor;

    [Header("Button to Play/Pause.")]
    [SerializeField]
    // To prevent from interacting with UI when 
    // paused.
    private GameObject pausePanel;

    [Header("Button to send to another scene.")]
    [SerializeField]
    private string sceneToSend;

    [Header("Time to wait before opening new scene.")]
    [SerializeField]
    private float sceneOpenDelay;

    private void Reset()
    {
        // Defaults.
        sceneToSend = "";
        sceneOpenDelay = 0.00f;
    }

    // This function is called externally from OnClick
    // event.
    public void StartOpenSceneCoroutine()
    {
        // To simulate delay.
        StartCoroutine(Open(sceneToSend));
    }

    // This function is called externally from OnClick
    // event.
    public void PlayPause()
    {
        if (Time.timeScale.Equals(0.00f))
        {
            pausePanel.SetActive(false);

            Time.timeScale = 1.00f;
        }
        else
        {
            pausePanel.SetActive(true);

            Time.timeScale = 0.00f;
        }
    }

    // This function is called externally from OnClick
    // event.
    public void SavePlayerData()
    {
        // Just save.
        PlayerSaver.Save(levelProgressor.playerData);
    }

    private System.Collections.IEnumerator Open(string scene)
    {
        // Custom delay.
        yield return new WaitForSeconds(sceneOpenDelay);

        // Then, load new scene.
        SceneManager.LoadScene(scene);
    }
}
