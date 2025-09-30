using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsMenu : MonoBehaviour
{
    [SerializeField] private AudioConfigSO settings;
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private Slider masterSlider, musicSlider, sfxSlider;

    private void OnEnable()
    {
        masterSlider.onValueChanged.AddListener(SetMaster);
        musicSlider.onValueChanged.AddListener(SetMusic);
        sfxSlider.onValueChanged.AddListener(SetSFX);
    }
    private void OnDestroy()
    {
        masterSlider.onValueChanged.RemoveListener(SetMaster);
        musicSlider.onValueChanged.RemoveListener(SetMusic);
        sfxSlider.onValueChanged.RemoveListener(SetSFX);
    }
    private void Start()
    {
        masterSlider.value = settings.masterVolume;
        musicSlider.value = settings.musicVolume;
        sfxSlider.value = settings.sfxVolume;
    }

    public void SetMaster(float value)
    {
        settings.masterVolume = value;
        audioManager.ApplySettings();
    }
    public void SetMusic(float value)
    {
        settings.musicVolume = value;
        audioManager.ApplySettings();
    }
    public void SetSFX(float value)
    {
        settings.sfxVolume = value;
        audioManager.ApplySettings();
    }
}