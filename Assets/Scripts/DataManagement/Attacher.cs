using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Attacher : MonoBehaviour {
    [SerializeField]
    private string key;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        while (Data.instance == null) {
            yield return null;
        }
        Data.AttachObject(key, gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
