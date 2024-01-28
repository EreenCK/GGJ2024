using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject dialogueParent;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Button option1Button;
    [SerializeField] private Button option2Button;

    [SerializeField] private float typingSpeed = 0.4f;
    [SerializeField] private float turnSpeed = 2f;

    private List<dialogueString> dialogueList;

    [Header("Player")]
    [SerializeField] private Player player;
    private Transform PlayerCam;

    private int currentDialogueIndex = 0;

    private void Start()
    {
        dialogueParent.SetActive(false);
        PlayerCam = Camera.main.transform;

    }
    public void DialogueStart(List<dialogueString> textToPrint ,Transform NPC)
    {
        dialogueParent.SetActive(true);
        player.enabled = false;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        StartCoroutine(TurnCameraTowardsNPC(NPC));
        dialogueList = textToPrint;
        currentDialogueIndex = 0;

        DisableButtons();

        StartCoroutine(PrintDialogue());

    }
    private void DisableButtons()
    {

        option1Button.interactable = false;
        option2Button.interactable = false;

        option1Button.GetComponentInChildren<TMP_Text>().text = "No option";
        option2Button.GetComponentInChildren<TMP_Text>().text = "No option";
    }
    IEnumerator TurnCameraTowardsNPC(Transform NPC)
    {
        Quaternion startRotation = PlayerCam.rotation;
        Quaternion targetRotation = Quaternion.LookRotation(NPC.position - PlayerCam.position);

        float elapsedTime = 0f;
        while (elapsedTime < 1f)
        {
            PlayerCam.rotation = Quaternion.Slerp(startRotation, targetRotation, elapsedTime);
            elapsedTime += Time.deltaTime * turnSpeed;
            yield return null;
        }
        PlayerCam.rotation = targetRotation;
    }
    private bool optionSelected = false; 
    IEnumerator PrintDialogue()
    {
        while (currentDialogueIndex < dialogueList.Count)
        {
            dialogueString line = dialogueList[currentDialogueIndex];
            line.startDialogueEvent?.Invoke();

            if (line.isQuestion)
            {
                yield return StartCoroutine(TypeText(line.text));

                option1Button.interactable = true;
                option2Button.interactable = true;

                option1Button.GetComponentInChildren<TMP_Text>().text = line.AnswerOption1;
                option2Button.GetComponentInChildren<TMP_Text>().text = line.AnswerOption2;

                option1Button.onClick.AddListener(() => HandleOptionSelected(line.Option1indexJump));
                option2Button.onClick.AddListener(() => HandleOptionSelected(line.Option2indexJump));

                yield return new WaitUntil(() => optionSelected);
            }
            else
            {
                yield return StartCoroutine(TypeText(line.text));
            }
            line.EndDialogueEvent?.Invoke();
            optionSelected = false;
        }
        DialogueStop();
    }
    private void HandleOptionSelected(int indexJump)
    {
        optionSelected = true;
        DisableButtons();
        currentDialogueIndex = indexJump;
    }
    private IEnumerator TypeText(string text)
    {
        dialogueText.text = "";
        foreach(char letter in text.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        if (!dialogueList[currentDialogueIndex].isQuestion)
        {
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.E));
        }
        if (dialogueList[currentDialogueIndex].isEnd)
            DialogueStop();

            currentDialogueIndex++;
        
    }
    private void DialogueStop()
    {
        StopAllCoroutines();
        dialogueText.text = "";


        dialogueParent.SetActive(false);
        player.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
}
