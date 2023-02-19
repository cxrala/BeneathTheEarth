using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class StartTrigger : MonoBehaviour {

    public UnityEvent startEvent;

    // Start is called before the first frame update
    void Start() {
        if (startEvent != null) {
            startEvent.Invoke();
        }
    }
}
