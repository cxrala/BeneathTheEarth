using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Entity : MonoBehaviour
{
    public int health = 12;
    public int max_health = 12;
    public string type = "";
    public string displayName = "";
    public List<string> skills = new List<string>();
    public IDictionary<Skill.Status, int> statuses = new Dictionary<Skill.Status, int>();
    public IDictionary<Skill.Status, float> statusPowers = new Dictionary<Skill.Status, float>();
    public bool is_enemy = false;
    public HealthBar bar;
    public SpriteBox box;
    public TMP_Text nameText;
    public Color baseBackground = new Color(0.25F, 0.25F, 0.25F, 1);
    public Color hurtBackground = new Color(1, 0.25F, 0.25F, 1);
    public Color healBackground = new Color(0, 0.8F, 0, 1);
    public int flashTime = 50;
    private SFXEngine sfx;
    public Sprite normal;
    public Sprite attacking;
    public Sprite hurt;
    public Sprite low;
    public StatusDisplay statusDisplay;
    public string hurtSound = "hurt";
    public string healSound = "heal";
    public int level = 0;
    public int typeId = -1;

    // Start is called before the first frame update
    void Start()
    {
        sfx = GameObject.Find("SFX Engine").GetComponent<SFXEngine>();
        box.SetBackgroundColor(baseBackground);
        bar.SetValAndMaxVal(this.health, this.max_health);
        nameText.text = displayName;
    }

    public void SetName(string name) {
        this.displayName = name;
        this.nameText.text = name;
    }

    public void SetEnemySkills(int health, int level)
    {
        this.skills = new List<string>();
        if (is_enemy == true)
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

    public void SetHealth(int value, int max_value)
    {
        this.health = value;
        this.max_health = max_value;
        bar.SetValAndMaxVal(this.health, this.max_health);
    }

    public void ChangeHealth(int value)
    {
        if (value > 0)
        {   
            if (statuses.ContainsKey(Skill.Status.shielded)) {
                value = Math.Max(1, (int)(value - statusPowers[Skill.Status.shielded]));
            }
            if (IsAffected(Skill.Status.evasive)) {
                float decision = UnityEngine.Random.Range(0.0f, 1.0f);
                Debug.Log(EffectPower(Skill.Status.evasive));
                if (decision < EffectPower(Skill.Status.evasive)) {
                    value = 0;
                }
            }
            if (value > 0) {
                box.FlashBackgroundColor(hurtBackground, flashTime);
                sfx.PlayClip(hurtSound);
            }
            this.health -= value;
        }
        if (value < 0)
        {
            box.FlashBackgroundColor(healBackground, flashTime*2);
            sfx.PlayClip(healSound);
            this.health -= value;
        }
        if (this.health < 0) {
            this.health = 0;
        }
        if (this.health > this.max_health) {
            this.health = this.max_health;
        }
        bar.SetValAndMaxVal(this.health, this.max_health);
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
        UpdateStatusDisplay();
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
        UpdateStatusDisplay();
    }

    public void UpdateStatusDisplay() {
        statusDisplay.UpdateStatuses(new List<Skill.Status>(this.statuses.Keys));
    }

    public bool IsDead(){
        if (this.health <= 0){
            return true;
        }
        return false;
    }

    public bool IsStunned() {
        return statuses.ContainsKey(Skill.Status.stunned);
    }

    public bool IsAffected(Skill.Status status) {
        return statuses.ContainsKey(status);
    }

    public float EffectPower(Skill.Status status) {
        return statusPowers[status];
    }
}
