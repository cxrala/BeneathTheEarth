using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// This class provides all of the singleton behaviour. Data which persists between scenes are added to and from a data object. 
/// Permanent behaviours (such as audio) are 
/// </summary>
public class Data : MonoBehaviour
{

    public static Data instance;

    void Awake() {
        if (instance == null) {
            instance = this;
            DontDestroyOnLoad(instance);
        } else {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() {
        
    }

    public Data GetInstance() {
        return instance;
    }

    public static new T GetComponent <T> () where T : UnityEngine.Component {
        return instance.gameObject.GetComponent<T>();
    }

    public static void AddComponent <T> () where T : UnityEngine.Component {
        instance.gameObject.AddComponent<T>();
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="key"></param>
    /// <param name="gameObject"></param>
    /// <returns></returns>
    public static bool AttachObject(string key, GameObject gameObject) {
        foreach (Transform t in instance.transform) {
            if (t.gameObject.GetComponent<AttachmentKey>().GetKey() == key) {
                return false;
            }
        }
        gameObject.AddComponent<AttachmentKey>().SetKey(key);
        gameObject.transform.parent = instance.transform;
        return true;
    }

    public static void SetObject(string key, GameObject gameObject) {
        foreach (Transform t in instance.transform) {
            if (t.gameObject.GetComponent<AttachmentKey>().GetKey() == key) {
                Destroy(t.gameObject);
            }
        }
        gameObject.AddComponent<AttachmentKey>().SetKey(key);
        gameObject.transform.parent = instance.transform;
    }

    public static GameObject GetObject(string key) {
        foreach (Transform t in instance.transform) { 
            if (t.gameObject.GetComponent<AttachmentKey>().GetKey() == key) {
                return t.gameObject;
            }
        }
        return null;
    }

    public static void RemoveObject(string key) {
        foreach (Transform t in instance.transform) {
            if (t.gameObject.GetComponent<AttachmentKey>().GetKey() == key) {
                Destroy(t.gameObject);
            }
        }
    }

    public static GameObject PopObject(string key, Transform newParent) {
        foreach (Transform t in instance.transform) {
            if (t.gameObject.GetComponent<AttachmentKey>().GetKey() == key) {
                t.parent = newParent;
                Destroy(t.gameObject.GetComponent<AttachmentKey>());
                return t.gameObject;
            }
        }
        return null;
    }

    public static GameObject PopObject(string key, GameObject newParent) {
        foreach (Transform t in instance.transform) {
            if (t.gameObject.GetComponent<AttachmentKey>().GetKey() == key) {
                t.parent = newParent.transform;
                Destroy(t.gameObject.GetComponent<AttachmentKey>());
                return t.gameObject;
            }
        }
        return null;
    }
}
