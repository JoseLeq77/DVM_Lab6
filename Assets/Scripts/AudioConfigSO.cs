using UnityEngine;

[CreateAssetMenu(fileName = "AudioConfig", menuName = "Scriptable Objects/Audio/Config")]
public class AudioConfigSO : ScriptableObject
{
    [SerializeField] [Range(-80, 0)] public float masterVolume = 0;
    [SerializeField] [Range(-80, 0)] public float musicVolume = 0;
    [SerializeField] [Range(-80, 0)] public float sfxVolume = 0;
}