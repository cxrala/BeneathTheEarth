using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public enum State {
    playing,
    paused,
    loading,
    freezePlayer
}

public class GameState : MonoBehaviour
{
    public static GameState instance;
    [SerializeField]
    private State state = State.playing;

    void Awake() {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void SetGameState(State state) {
        instance.state = state;
    }

    public static State GetGameState() {
        return instance.state;
    }

    public static void SetPlaying() {
        instance.state = State.playing;
    }

    public static void SetPaused() {
        instance.state = State.paused;
    }

    public static bool Playing() {
        return instance.state == State.playing;
    }
}
