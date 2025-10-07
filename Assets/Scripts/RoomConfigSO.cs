using UnityEngine;

[CreateAssetMenu(fileName = "RoomConfig", menuName = "Scriptable Objects/Room/Config")]
public class RoomConfigSO : ScriptableObject
{
    [Tooltip("Nombre identificador de la sala")]
    public string roomName;
    
    [Tooltip("�ndice de la m�sica para esta sala")]
    public int musicIndex;
}