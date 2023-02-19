using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DialogueLoader : MonoBehaviour
{
    public DialogueManager dialogueManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoadDialogue(string path) {
        Debug.Log("Loading dialogue " + path);
        TextAsset dialogueFile = Resources.Load<TextAsset>(path);
        if (dialogueFile != null) {
            dialogueManager.dialogue = null;
            DialogueData data = JsonUtility.FromJson<DialogueData>(dialogueFile.text);
            foreach (ResourceKey r in data.sprites) {
                if (!dialogueManager.sprites.ContainsKey(r.key)) {
                    Object[] sprites = Resources.LoadAll(r.path, typeof(Sprite));
                    for (int i = 0; i < sprites.Length; i++) {
                        dialogueManager.sprites.Add(r.key + "_" + i.ToString(), (Sprite)sprites[i]);
                    }
                }
            }
            foreach (ResourceKey r in data.audios) {
                if (!SFXEngine.instance.ContainsClip(r.key)) {
                    SFXEngine.instance.AddClip(r.key, Resources.Load<AudioClip>(r.path));
                }
            }
            dialogueManager.dialogue = data.dialogue;
        }
    }
}
