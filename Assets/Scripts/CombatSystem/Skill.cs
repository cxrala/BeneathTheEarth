using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Skill
{
    public string skillID = "";
    public string displayName = "";
    public int apCost = 0;
    public int[] healthChange = {1, 6, 0};

    public TargetMode targetMode = TargetMode.None; // 2 = All Enemy, 1 = Enemy, -1 = Weakest Party Member, -2 = All party members, -3 = Random party member

    public TargetSide targetSide = TargetSide.Self;

    [Serializable]
    public enum TargetMode {
        None = 0,
        Chosen = 1,
        All = 2,
        Random = 3
    }

    [Serializable]
    public enum TargetSide {
        Self = -1,
        Opponent = 1
    }

    [Serializable]
    public struct StatusData {
        public Status status;
        public float chance;
        public int duration;
        public float power;
    }

    [Serializable]
    public enum Status {
        Evasive = 0,
        Stunned = 1,
        Shielded = 2,
        Weak = 3
    }
    
    // public List<status_info> status_list;
    public List<StatusData> statuses;

    void Start(){
        // this.statuses = new Dictionary<string, status_data>();
        // foreach (status_info s in status_list){
        //     status_data sd = new status_data();
        //     sd.chance = s.chance;
        //     sd.duration = s.duration;
        //     this.statuses.Add(s.name, sd);
        // }
    }

    public void Use(List<Entity> targets, Entity performer){
        for (int i=0; i<targets.Count; i++){
            ApplySkill(targets[i], performer);
        }
    }

    public int Resolve(int number, int size, int mod)
    {
        int multiplier = 1;
        if (number < 0){
            multiplier = -1;
            number *= -1;
        }
        int total = mod;
        for (int i=0; i<number; i++){
            total += UnityEngine.Random.Range(1, size+1);
        }
        return total*multiplier;
    }

    public void ApplySkill(Entity target, Entity performer){
        int change = Resolve(healthChange[0], healthChange[1], healthChange[2]);
        if (change > 0 && performer.IsAffected(Skill.Status.Weak)) {
            change = Math.Max(1, change - (int) performer.EffectPower(Skill.Status.Weak));
        }
        target.ChangeHealth(change);
        foreach (StatusData data in statuses)
        {
            float chance = data.chance;
            float decision = UnityEngine.Random.Range(0.0f, 1.0f);
            if (decision < chance){
                target.ModStatus(data.status, data.duration, data.power);
            }
        }
    }

    public Skill(string skill_id, string displayName, int apCost, TargetMode targetMode, TargetSide targetSide, int[] health_change, List<StatusData> statuses){
        // Skill nullSkill = new Skill(){skill_id = "null", display_name = "Nothing", ap_cost = 0, health_change = {0, 0, 0}};
        // return nullSkill;
        this.skillID = skill_id;
        this.displayName = displayName;
        this.apCost = apCost;
        this.targetMode = targetMode;
        this.targetSide = targetSide;
        this.healthChange = health_change;
        this.statuses = statuses;
    }
}

