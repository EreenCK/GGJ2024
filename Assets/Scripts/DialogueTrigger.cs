using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private List<dialogueString> dialogueStrings = new List<dialogueString>();
    [SerializeField] private Transform NpcTransform;

    public bool hasSpoken = false;
    public GameObject konusma;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player") && !hasSpoken)
        {
            konusma.SetActive(true);
            
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && !hasSpoken && Input.GetKeyDown(KeyCode.E))
        {
            other.gameObject.GetComponent<DialogueManager>().DialogueStart(dialogueStrings, NpcTransform);
            hasSpoken = true;
            konusma.SetActive(false);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            konusma.SetActive(false);
        }
    }

}

[System.Serializable]
public class dialogueString
{
    public string text;
    public bool isEnd;

    [Header("Branch")]
    public bool isQuestion;
    public string AnswerOption1;
    public string AnswerOption2;
    public int Option1indexJump;
    public int Option2indexJump;

    [Header("Events")]
    public UnityEvent startDialogueEvent;
    public UnityEvent EndDialogueEvent;














}
