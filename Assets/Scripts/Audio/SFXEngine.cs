using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SFXEngine : MonoBehaviour {

    public static SFXEngine instance;
    private Dictionary<string, AudioClip> clips = new Dictionary<string, AudioClip>();
    public string[] inputKeys;
    public AudioClip[] inputClips;
    public AudioSource[] channels;
    public float sfxVolume = 1;

    void Awake() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            for (int i = 0; i < inputKeys.Length; i++) {
                clips.Add(inputKeys[i], inputClips[i]);
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        foreach (AudioSource i in channels) {
            i.volume = sfxVolume;
        }
    }

    // Update is called once per frame
    void Update() {

    }

    public void PlayClip(string key) {
        foreach (AudioSource i in channels) {
            if (!i.isPlaying) {
                i.clip = clips[key];
                i.Play();
                break;
            }
        }
    }

    public void PlayClipOnChannel(string key, int channel) {
        channels[channel].Stop();
        channels[channel].clip = clips[key];
        channels[channel].Play();
    }

    public bool ContainsClip(string key) {
        return clips.ContainsKey(key);
    }

    public void AddClip(string key, AudioClip clip) {
        clips.Add(key, clip);
    }

    public void AddClips(List<string> keys, List<AudioClip> clips) {
        for (int i = 0; i < keys.Count; i++) {
            this.clips.Add(keys[i], clips[i]);
        }
    }

    public void AddClips(string[] keys, AudioClip[] clips) {
        for (int i = 0; i < keys.Length; i++) {
            this.clips.Add(keys[i], clips[i]);
        }
    }

    public void ReplaceClip(string key, AudioClip clip) {
        clips[key] = clip;
    }

    public void ReplaceClips(List<string> keys, List<AudioClip> clips) {
        for (int i = 0; i < keys.Count; i++) {
            this.clips[keys[i]] = clips[i];
        }
    }

    public void ReplaceClips(string[] keys, AudioClip[] clips) {
        for (int i = 0; i < keys.Length; i++) {
            this.clips[keys[i]] = clips[i];
        }
    }

    public void AddOrReplaceClip(string key, AudioClip clip) {
        if (clips.ContainsKey(key)) {
            ReplaceClip(key, clip);
        } else {
            AddClip(key, clip);
        }
    }

    public void AddOrReplaceClips(List<string> keys, List<AudioClip> clips) {
        for (int i = 0; i < keys.Count; i++) {
            if (this.clips.ContainsKey(keys[i])) {
                ReplaceClip(keys[i], clips[i]);
            } else {
                AddClip(keys[i], clips[i]);
            }
        }
    }

    public void AddOrReplaceClips(string[] keys, AudioClip[] clips) {
        for (int i = 0; i < keys.Length; i++) {
            if (this.clips.ContainsKey(keys[i])) {
                ReplaceClip(keys[i], clips[i]);
            } else {
                AddClip(keys[i], clips[i]);
            }
        }
    }

    public void RemoveClip(string key) {
        clips.Remove(key);
    }

    public void RemoveClips(List<string> key) {
        foreach (string s in key) {
            clips.Remove(s);
        }
    }

    public void ClearClips() {
        clips.Clear();
    }
}
