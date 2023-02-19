using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable {
    public int damage = 1;
    public float pushForce = 3.0f;

    protected override void OnCollide(Collider2D coll) {
        if (coll.tag == "Player") {
            coll.SendMessage("ReceiveDamage");
        }
    }
}
