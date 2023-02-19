using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[System.Serializable]
public struct DialogueData {
    public List<ResourceKey> sprites;
    public List<ResourceKey> audios;
    public List<Dialogue> dialogue;
}

[System.Serializable]
public struct ResourceKey {
    public string key;
    public string path;
}

[System.Serializable]
public struct Dialogue {
    public string name;
    public string sprite;
    public string audio;
    public List<string> sentences;
}