using UnityEngine;
using UnityEngine.UI;

public class AudioSettingsPanel : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    [SerializeField] private AudioConfigSO audioConfig;
    
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider musicSlider; 
    [SerializeField] private Slider sfxSlider;
    
    private void OnEnable()
    {
        masterSlider.value = audioConfig.masterVolume;
        musicSlider.value = audioConfig.musicVolume;
        sfxSlider.value = audioConfig.sfxVolume;
        
        masterSlider.onValueChanged.AddListener(ChangeMasterVolume);
        musicSlider.onValueChanged.AddListener(ChangeMusicVolume);
        sfxSlider.onValueChanged.AddListener(ChangeSFXVolume);
    }
    
    private void OnDisable()
    {
        masterSlider.onValueChanged.RemoveListener(ChangeMasterVolume);
        musicSlider.onValueChanged.RemoveListener(ChangeMusicVolume);
        sfxSlider.onValueChanged.RemoveListener(ChangeSFXVolume);
    }
    
    public void ChangeMasterVolume(float value)
    {
        audioConfig.masterVolume = value;
        audioManager.ApplySettings();
    }
    
    public void ChangeMusicVolume(float value)
    {
        audioConfig.musicVolume = value;
        audioManager.ApplySettings();
    }
    
    public void ChangeSFXVolume(float value)
    {
        audioConfig.sfxVolume = value;
        audioManager.ApplySettings();
    }
}