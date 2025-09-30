using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    private float directionX;
    private float directionZ;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private Vector3 velocity;

    [SerializeField] private AudioManager audioManager;
    private int currentRoomIndex = -1;
    private bool isWalking = false;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Vector3 inputDirection = new Vector3(directionX, 0f, directionZ);
        inputDirection = inputDirection.normalized;
        velocity = new Vector3(inputDirection.x * moveSpeed, rb.linearVelocity.y, inputDirection.z * moveSpeed);
        rb.linearVelocity = velocity;

        bool walkingNow = inputDirection.magnitude > 0.1f;
        if (walkingNow && !isWalking)
        {
            audioManager.PlayWalkLoop();
            isWalking = true;
        }
        else if (!walkingNow && isWalking)
        {
            audioManager.StopWalkLoop();
            isWalking = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RoomTrigger room = other.GetComponent<RoomTrigger>();
        if (room != null && room.MusicIndex != currentRoomIndex)
        {
            audioManager.PlaySFXOneShot(1); 
            audioManager.PlayMusic(room.MusicIndex); 
            currentRoomIndex = room.MusicIndex;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RoomTrigger room = other.GetComponent<RoomTrigger>();
        if (room != null && room.MusicIndex == currentRoomIndex)
        {
            audioManager.PlaySFXOneShot(1); 
            currentRoomIndex = -1;
        }
    }
    
    public void OnXMovement(InputAction.CallbackContext context)
    {
        directionX = context.ReadValue<float>();
    }
    public void OnZMovement(InputAction.CallbackContext context)
    {
        directionZ = context.ReadValue<float>();
    }
}