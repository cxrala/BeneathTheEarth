using System.Collections;
using System.Collections.Generic;
using System.IO;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using TMPro;

public class DialogueManager : MonoBehaviour {
    public TextMeshProUGUI namebox;
    public TextMeshProUGUI textbox;
    public Image sprite;
    public Animator animator;
    
    private bool hasFinished = false;
    [SerializeField]
    private bool loadingNext = false;
    [SerializeField]
    private bool canLoadNext = false;

    private string toDisplay = "";
    private string clipKey;

    public Dictionary<string, Sprite> sprites = new Dictionary<string, Sprite>();
    public List<Dialogue> dialogue;
    private Queue<string> sentences = new Queue<string>();

    private PlayerInput playerInput;

    public UnityEvent endOfDialogueEvent;

    void Awake() {
        playerInput = GetComponent<PlayerInput>();
    }

    void Start() {

    }

    public void ClearSprites() {
        sprites.Clear();
    }

    public void StartDialogue() {
        IEnumerator WaitUntilReady()
        {
            while (dialogue == null)
            {
                yield return null;
            }
            animator.SetBool("isOpen", true);
            NextDialogue();
        }
        StartCoroutine(WaitUntilReady());
    }

    public void NextDialogue() {
        if (dialogue.Count == 0) {
            EndDialogue();
            return;
        }
        hasFinished = false;
        if (sentences.Count == 0) {
            foreach (string s in dialogue[0].sentences) {
                sentences.Enqueue(s);
            }
            namebox.text = dialogue[0].name;
            sprite.sprite = sprites[dialogue[0].sprite];
            clipKey = dialogue[0].audio;
            dialogue.RemoveAt(0);
        }
        toDisplay = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(toDisplay));
    }

    private void Update() {
        if (playerInput.actions["Next Dialogue"].triggered) {
            if (!canLoadNext) {
                if (!hasFinished) {
                    StopAllCoroutines();
                    textbox.text = toDisplay;
                    hasFinished = true;
                    return;
                }
            } else {
                endOfDialogueEvent.Invoke();
            }
            NextDialogue();
        }

        if (playerInput.actions["Skip"].triggered && !loadingNext) {
            endOfDialogueEvent.Invoke();
            loadingNext = true;
        }
        
        //foreach (TextBox txt in floatingTexts) txt.UpdateTextBox();
    }

    IEnumerator TypeSentence(string sentence) {
        textbox.text = "";
        foreach (char letter in sentence.ToCharArray()) {
            textbox.text += letter;
            SFXEngine.instance.PlayClipOnChannel(clipKey, 0);
            yield return new WaitForSeconds(0.03f);
        }
        hasFinished = true;
    }

    public void EndDialogue() {
        animator.SetBool("isOpen", false);
        canLoadNext = true;
    }
}
