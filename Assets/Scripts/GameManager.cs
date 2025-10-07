using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject audioSettingsPanel;
    [SerializeField] private int sceneIndexDestiny;

    public void ShowAudioSettingsPanel()
    {
        if (audioSettingsPanel != null)
        {
            audioSettingsPanel.SetActive(true);
        }
    }

    public void HideAudioSettingsPanel()
    {
        if (audioSettingsPanel != null)
        { 
            audioSettingsPanel.SetActive(false);
        }
    }

    public void ChangeScene()
    {
        SceneManager.LoadScene(sceneIndexDestiny);
    }
}
