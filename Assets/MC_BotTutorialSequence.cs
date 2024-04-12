using EzySlice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MC_BotTutorialSequence : MonoBehaviour
{
    public MC_TutorialBotMover botMover;
    public MC_BotTalk botTalk;
    public Collider playerAdvancementBlocker;
    public Collider playerDetectionCollider;
    public Transform targetPosition;
    public string[] dialogueLines;
    public AudioClip[] dialogueSounds;
    public GameObject nextButton;

    public int currentDialogueIndex = 0;
    private bool isDialogueActive = false;

    void Start()
    {
        botMover.agent.SetDestination(targetPosition.position);
    }

    void Update()
    {
        if (!botMover.agent.pathPending && botMover.agent.remainingDistance < 0.1f && !isDialogueActive)
        {
            StartDialogue();
        }
    }

    public void StartDialogue()
    {
        isDialogueActive = true;
        botTalk.TalkLine(dialogueSounds[0], dialogueLines[0]);
    }

    public void AdvanceDialogue()
    {
        if (currentDialogueIndex < dialogueSounds.Length)
        {
            botTalk.TalkLine(dialogueSounds[currentDialogueIndex], dialogueLines[currentDialogueIndex]);
        }
        else
        {
            EndDialogue();
        }
    }

    public void EndDialogue()
    {
        nextButton.SetActive(false);
        botMover.UnPause();
        playerAdvancementBlocker.enabled = false;
        playerDetectionCollider.enabled = true;
        botTalk.ClearText();
        botMover.MoveToRandomPosition();
    }
}
