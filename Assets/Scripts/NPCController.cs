using UnityEngine;
using System.Collections;

public class NPCController : MonoBehaviour
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private float waitTime = 2f;
    
    [SerializeField] private string npcName = "NPC";
    [SerializeField] private string interactionMessage = "Hola soy un NPC";
    
    private int currentWaypoint = 0;
    private bool isInteracting = false;
    
    private void Start()
    {
        if (waypoints != null && waypoints.Length > 0)
        {
            StartCoroutine(FollowWaypoints());
        }
    }
    
    private IEnumerator FollowWaypoints()
    {
        if (waypoints.Length == 0)
        {
            yield break;
        }
            
        while (true)
        {
            Transform targetWaypoint = waypoints[currentWaypoint];
            
            while (Vector3.Distance(transform.position, targetWaypoint.position) > 0.1f)
            {
                if (!isInteracting)
                {
                    transform.position = Vector3.MoveTowards(
                        transform.position, 
                        targetWaypoint.position, 
                        moveSpeed * Time.deltaTime
                    );
                }
                yield return null;
            }
            
            yield return new WaitForSeconds(waitTime);
            
            currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
            
            while (isInteracting)
            {
                yield return null;
            }
        }
    }
    
    public void Interact()
    {
        if (!isInteracting)
        {
            isInteracting = true;
            DialogueManager.GetInstance().ShowDialogue(npcName, interactionMessage);
            StartCoroutine(EndInteraction());
        }
    }
    
    private IEnumerator EndInteraction()
    {
        yield return new WaitForSeconds(3f);
        DialogueManager.GetInstance().HideDialogue();
        isInteracting = false;
    }
}