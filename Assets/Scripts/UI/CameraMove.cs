using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour {
    public Transform lookAt;

    [SerializeField]
    public float boundX = 0.15f;

    [SerializeField]
    public float boundY = 0.05f;

    private void Start() {
        if (lookAt == null)
        {
            lookAt = GameObject.FindWithTag("Player").transform;
        }
    }

    void LateUpdate() {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > boundX) {
            delta.x = deltaX - boundX;
        } else if (deltaX < -boundX) {
            delta.x = deltaX + boundX;
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > boundY) {
            delta.y = deltaY - boundY;
        } else if (deltaY < -boundY) {
            delta.y = deltaY + boundY;
        }

        transform.position += new Vector3(delta.x, delta.y, 0);
    }
}
