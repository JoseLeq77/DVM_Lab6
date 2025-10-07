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
    
    private bool canInteract = true; 
    
    [SerializeField] private float raycastDistance = 2f; 
    [SerializeField] private Color gizmoColor = Color.green; 
    
    private Vector3 lastLookDirection;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        lastLookDirection = transform.forward;
    }

    private void FixedUpdate()
    {
        Vector3 inputDirection = new Vector3(directionX, 0f, directionZ);
        inputDirection = inputDirection.normalized;
        velocity = new Vector3(inputDirection.x * moveSpeed, rb.linearVelocity.y, inputDirection.z * moveSpeed);
        rb.linearVelocity = velocity;
        
        if (inputDirection.magnitude > 0.1f)
        {
            lastLookDirection = inputDirection;
        }

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
    
    private void OnDrawGizmos()
    {
        Vector3 rayDirection = lastLookDirection.magnitude > 0.1f ? lastLookDirection : transform.forward;
        
        Gizmos.color = gizmoColor;
        
        Gizmos.DrawRay(transform.position, rayDirection * raycastDistance);
        
        RaycastHit hit;
        if (Physics.Raycast(transform.position, rayDirection, out hit, raycastDistance))
        {
            if (hit.collider.GetComponent<NPCController>() != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawSphere(hit.point, 0.1f);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        RoomTrigger room = other.GetComponent<RoomTrigger>();
        if (room != null && room.MusicIndex != currentRoomIndex)
        {
            StartRoomTransition(room.MusicIndex);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        RoomTrigger room = other.GetComponent<RoomTrigger>();
        if (room != null && room.MusicIndex == currentRoomIndex)
        {
            audioManager.PlaySFXOneShot(1);
            
            SceneController.GetInstance().FadeIn(() => 
            {
                currentRoomIndex = -1;
                SceneController.GetInstance().FadeOut();
            });
        }
    }
    
    private void StartRoomTransition(int roomMusicIndex)
    {
        SceneController.GetInstance().FadeIn(() => 
        {
            audioManager.PlaySFXOneShot(1);
            audioManager.PlayMusic(roomMusicIndex);
            currentRoomIndex = roomMusicIndex;
            SceneController.GetInstance().FadeOut();
        });
    }
    
    public void OnXMovement(InputAction.CallbackContext context)
    {
        directionX = context.ReadValue<float>();
    }
    
    public void OnZMovement(InputAction.CallbackContext context)
    {
        directionZ = context.ReadValue<float>();
    }
    
    public void OnInteract(InputAction.CallbackContext context)
    {
        if (canInteract)
        {
            Vector3 rayDirection = lastLookDirection;
                
            RaycastHit hit;
            if (Physics.Raycast(transform.position, rayDirection, out hit, raycastDistance))
            {
                NPCController npc = hit.collider.GetComponent<NPCController>();
                if (npc != null)
                {
                    npc.Interact();
                    canInteract = false;
                    Invoke("ResetInteraction", 1f);
                }
            }
        }
    }
    
    private void ResetInteraction()
    {
        canInteract = true;
    }
}