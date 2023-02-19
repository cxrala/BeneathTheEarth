using System.Collections;
using System.Collections.Generic;
//using UnityEditor.Build;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public struct EnemyData {
    public Enemy owner;
    public List<string> types;
    public List<string> names;
    public List<int> hitpoints;
    public List<int> maxHitpoints;
    public List<int> levels;
}

public class Enemy : Mover {
    // Logic
    public float triggerLength = 0.4f;
    public float chaseLength = 1.0f;
    private bool chasing;
    private bool collidingWithPlayer;
    public Transform playerTransform;
    private Vector3 startingPosition;
    private Animator anim;

    public EnemyData data;

    public int id;

    private bool move;
    private float moveTime = 0.8f;
    private float idleTime = 0.8f;
    private float moveStart;
    private float idleStart;
    private Vector3 randVec;

    private bool encountered = false;

    // Hitbox
    public ContactFilter2D filter;
    private Collider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start() {
        base.Start();
        data.owner = this;
        startingPosition = transform.position;
        hitbox = GetComponent<Collider2D>();
        randVec = Random.insideUnitCircle;
        idleStart = Time.time;
        anim = GetComponent<Animator>();
        move = false;
        playerTransform = GameObject.FindWithTag("Player").transform;
    }

    public void Move(Vector3 motion)
    {
        if ((motion).x < 0) {
            anim.SetBool("mirror", true);
        } else {
            anim.SetBool("mirror", false);
        }
        UpdateVelocity(motion);
    }

    private void FixedUpdate() {
        // Is player in range?
        if (Vector3.Distance(playerTransform.position, startingPosition) < chaseLength)
        {
            if (Vector3.Distance(playerTransform.position, transform.position) < triggerLength) {
                chasing = true;
            }
            if (chasing) {
                if (!collidingWithPlayer) {
                    Move((playerTransform.position - transform.position).normalized);
                } else {
                    Move(startingPosition - transform.position);
                }
            } else {
                Move(startingPosition - transform.position);
            }
        } else {
            Move(startingPosition - transform.position);
            chasing = false;
            /*
            if (move) {
                if (Time.time - moveStart < moveTime) {
                    if (Vector3.Distance(transform.position, startingPosition) < triggerLength) {
                        UpdateMotor(randVec);
                    }
                } else {
                    idleStart = Time.time;
                    move = false;
                }
            } else {
                if (Time.time - idleStart > idleTime) {
                    randVec = Random.insideUnitCircle;
                    moveStart = Time.time;
                    move = true;
                }
            }
            */
        }

        // Check collide with player
        collidingWithPlayer = false;
        GetComponent<Collider2D>().OverlapCollider(filter, hits);
        for (int i = 0; i < hits.Length; i++) {
            if (hits[i] == null) continue;

            if (hits[i].tag == "Player" && !encountered && GameState.Playing())
            {
                encountered = true;
                collidingWithPlayer = true;
                BattleLoader.LoadBattle(this.gameObject);
                break;
            }

            hits[i] = null;
        }
    }

    protected override void Death() {
        Destroy(gameObject);
    }
}
