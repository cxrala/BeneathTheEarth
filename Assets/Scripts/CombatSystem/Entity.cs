using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entity : MonoBehaviour
{
    [SerializeField]
    private int health = 12;
    [SerializeField]
    private int maxHealth = 12;
    public string type = "";
    public string displayName = "";
    public List<string> skills = new List<string>();
    public IDictionary<Skill.Status, int> statuses = new Dictionary<Skill.Status, int>();
    public IDictionary<Skill.Status, float> statusPowers = new Dictionary<Skill.Status, float>();
    public bool isEnemy = false;
    public int level = 0;
    public int typeId = -1;
    [SerializeField]
    public EntityDisplay display;

    // Start is called before the first frame update
    void Start()
    {
        display.SetEntity(this);
        display.InitialiseHealthDisplay();
        display.SetName(displayName);
    }

    public void SetName(string name) {
        this.displayName = name;
        display.SetName(displayName);
    }

    public void SetEnemySkills(int health, int level)
    {
        this.skills = new List<string>();
        if (isEnemy == true)
        {
            int new_health = health;
            for (int i = 0; i < level; i++)
            {
                int droll = UnityEngine.Random.Range(1, 7);
                new_health += 3 + droll;
            }
            this.SetHealth(new_health, new_health);
            switch (this.type)
            {
                case "bat":
                    typeId = 0;
                    this.skills.Add("nullSkill");
                    this.skills.Add("batscreech");
                    this.skills.Add("nullSkill");
                    this.skills.Add("batscreech");
                    break;

                case "tardigrade":
                    typeId = 1;
                    this.skills.Add("tardishield");
                    this.skills.Add("tardicrush");
                    this.skills.Add("nullSkill");
                    this.skills.Add("tardiregen");
                    break;

                case "eye":
                    typeId = 2;
                    this.skills.Add("eyebeam");
                    this.skills.Add("eyebeam");
                    this.skills.Add("eyebeam");
                    this.skills.Add("eyebeam");
                    break;

                case "blob":
                    typeId = 3;
                    this.skills.Add("nullSkill");
                    this.skills.Add("nullSkill");
                    this.skills.Add("nullSkill");
                    this.skills.Add("blobmorph");
                    break;

                case "slug":
                    typeId = 4;
                    this.skills.Add("slughug");
                    this.skills.Add("nullSkill");
                    this.skills.Add("slugslime");
                    this.skills.Add("slugslime");
                    break;

                default:
                    typeId = 5;
                    this.skills.Add("nullSkill");
                    this.skills.Add("nullSkill");
                    this.skills.Add("nullSkill");
                    this.skills.Add("nullSkill");
                    break;

            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public int GetHealth() {
        return health;
    }

    public int GetMaxHealth() {
        return maxHealth;
    }

    public void SetHealth(int value, int max_value)
    {
        this.health = value;
        this.maxHealth = max_value;
        display.InitialiseHealthDisplay();
    }

    public void ChangeHealth(int value)
    {
        if (value > 0)
        {   
            if (statuses.ContainsKey(Skill.Status.Shielded)) {
                value = Math.Max(1, (int)(value - statusPowers[Skill.Status.Shielded]));
            }
            if (IsAffected(Skill.Status.Evasive)) {
                float decision = UnityEngine.Random.Range(0.0f, 1.0f);
                Debug.Log(EffectPower(Skill.Status.Evasive));
                if (decision < EffectPower(Skill.Status.Evasive)) {
                    value = 0;
                }
            }
            if (value > 0) {
                display.PerformSpriteAnimation("hurt");
            }
            this.health -= value;
        }
        if (value < 0) {
            display.PerformSpriteAnimation("heal");
            this.health -= value;
        }
        if (this.health < 0) {
            this.health = 0;
        }
        if (this.health > this.maxHealth) {
            this.health = this.maxHealth;
        }
        display.UpdateHealthDisplay();
    }

    public void ModStatus(Skill.Status name, int duration, float power){
        if (this.statuses.ContainsKey(name)){
            this.statuses[name] += duration+1;
            this.statusPowers[name] = Math.Max(power, statusPowers[name]);
        } else {
            this.statuses.Add(name, duration);
            this.statusPowers.Add(name, power);
        }
        Debug.Log("Adding status");
        display.UpdateStatusDisplay();
    }

    public void UpdateStatuses() {
        foreach (Skill.Status key in new List<Skill.Status>(this.statuses.Keys)){
            int d = this.statuses[key]-1;
            if (d <= 0){
                this.statuses.Remove(key);
                this.statusPowers.Remove(key);
            }else{
                this.statuses[key] = d;
            }
        }
        display.UpdateStatusDisplay();
    }

    public bool IsDead(){
        if (this.health <= 0){
            return true;
        }
        return false;
    }

    public bool IsStunned() {
        return statuses.ContainsKey(Skill.Status.Stunned);
    }

    public bool IsAffected(Skill.Status status) {
        return statuses.ContainsKey(status);
    }

    public float EffectPower(Skill.Status status) {
        return statusPowers[status];
    }
}
