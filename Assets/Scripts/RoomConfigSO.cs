using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "Scriptable Objects/Room/Config")]
public class RoomConfigSO : ScriptableObject
{
    [Tooltip("Nombre identificador de la sala")]
    public string roomName;
    
    [Tooltip("Índice de la música para esta sala")]
    public int musicIndex;
}