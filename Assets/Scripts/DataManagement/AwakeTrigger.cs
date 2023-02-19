using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class AwakeTrigger : MonoBehaviour {

    public UnityEvent startEvent;

    void Awake() {
        if (startEvent != null) {
            startEvent.Invoke();
        }
    }
}
