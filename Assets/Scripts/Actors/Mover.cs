using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.RuleTile.TilingRuleOutput;

public abstract class Mover : MonoBehaviour {
    protected Collider2D m_collider;
    protected Rigidbody2D m_rigidbody;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    // Start is called before the first frame update
    protected virtual void Start() {
        m_collider = GetComponent<Collider2D>();
        m_rigidbody = GetComponent<Rigidbody2D>();
    }

    protected virtual void UpdateVelocity(Vector3 input) {
        if (GameState.Playing()) {
            m_rigidbody.velocity = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        } else {
            m_rigidbody.velocity = new Vector3(0, 0, 0);
        }
    }

    protected virtual void Death() {

    }
}
