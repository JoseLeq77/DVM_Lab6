using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI dialogueText;
    [SerializeField] private float autoHideTime = 3f;
    
    private static DialogueManager instance;
    
    public static DialogueManager GetInstance()
    {
        return instance;
    }
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            
            if (dialoguePanel != null)
            {
                dialoguePanel.SetActive(false);
            }
        }
    }
    
    public void ShowDialogue(string name, string message)
    {
        StopAllCoroutines();
        
        nameText.text = name;
        dialogueText.text = message;
        dialoguePanel.SetActive(true);
        
        StartCoroutine(AutoHideDialogue());
    }
    
    public void HideDialogue()
    {
        dialoguePanel.SetActive(false);
    }
    
    private IEnumerator AutoHideDialogue()
    {
        yield return new WaitForSeconds(autoHideTime);
        HideDialogue();
    }
}