using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] Canvas dialogueCanvas = null;
    [SerializeField] Text dialogueText;
    [SerializeField] Text dialogueName;
    [SerializeField] RawImage dialogueTexture;
    [SerializeField] DialogueScriptable dialogueObject;
    public Button nextButton;
    private void Start()
    {
        dialogueCanvas.enabled = false;
        StartCoroutine(DisplayDialogue());
    }

    IEnumerator DisplayDialogue()
    {
        dialogueCanvas.enabled = true;

        foreach (string dialogue in dialogueObject.dialogueStrings)
        {
            dialogueObject.dialogueQueue.Enqueue(dialogue);
        }

        foreach (string dialogue in dialogueObject.dialogueNameStrings)
        {
            dialogueObject.dialogueNameQueue.Enqueue(dialogue);
        }

        foreach (Texture dialogue in dialogueObject.dialogueImages)
        {
            dialogueObject.dialogueQueueImage.Enqueue(dialogue);
        }

        while (dialogueObject.dialogueQueue.Count > 0)
        {
            var waitForButton = new WaitForUIButtons(nextButton);
            yield return waitForButton.Reset();

            if (waitForButton.buttonPressed == nextButton)
            {
                dialogueText.text = dialogueObject.dialogueQueue.Dequeue();
                dialogueName.text = dialogueObject.dialogueNameQueue.Dequeue();
                dialogueTexture.texture = dialogueObject.dialogueQueueImage.Dequeue();
            }
        }
        if(dialogueObject.dialogueQueue.Count == 0)
        {
            SceneManager.LoadScene(2);
        }

        dialogueCanvas.enabled = false;
    }
}
