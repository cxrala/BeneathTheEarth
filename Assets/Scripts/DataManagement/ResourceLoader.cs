using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class ResourceLoader : MonoBehaviour {

    public LoadedAudioTrack[] tracks;
    public LoadedSFX[] sfxs;
    public LoadedSprite[] sprites;
    public Dictionary<string, Sprite> loadedSprites = new Dictionary<string, Sprite>();

    public void LoadMusic() {
        foreach (LoadedAudioTrack t in tracks) {
            MusicEngine.instance.AddOrReplaceTrack(t.key, t.clip, t.length, t.bpm);
        }
    }

    public void LoadSFX() {
        foreach (LoadedSFX s in sfxs) {
            SFXEngine.instance.AddOrReplaceClip(s.key, s.clip);
        }
    }

    public void LoadSprite() {
        foreach (LoadedSprite s in sprites) {
            if (loadedSprites.ContainsKey(s.key)) {
                loadedSprites[s.key] = s.sprite;
            } else {
                loadedSprites.Add(s.key, s.sprite);
            }
        }
    }
}

[Serializable]
public struct LoadedAudioTrack {
    public string key;
    public AudioClip clip;
    public float length;
    public float bpm;
}

[Serializable]
public struct LoadedSFX {
    public string key;
    public AudioClip clip;
}

[Serializable]
public struct LoadedSprite {
    public string key;
    public Sprite sprite;
}
