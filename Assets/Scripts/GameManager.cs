using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GameObject audioSettingsPanel;

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
}
