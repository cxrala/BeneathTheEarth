using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string skill_id = "";
    public string display_name = "";
    public int ap_cost = 0;
    public int[] health_change = {1, 6, 0};
    public int target_mode = 0; // 2 = All Enemy, 1 = Enemy, -1 = Weakest Party Member, -2 = All party members, -3 = Random party member

    // public struct status_info {
    //     public string name;
    //     public float chance;
    //     public int duration;
    // }

    [System.Serializable]
    public struct StatusData {
        public Status status;
        public float chance;
        public int duration;
        public float power;
    }

    [System.Serializable]
    public enum Status {
        evasive = 0,
        stunned = 1,
        shielded = 2,
        weak = 3
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
        int change = Resolve(health_change[0], health_change[1], health_change[2]);
        if (change > 0 && performer.IsAffected(Skill.Status.weak)) {
            change = Math.Max(1, change - (int) performer.EffectPower(Skill.Status.weak));
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

    public Skill(string skill_id, string display_name, int ap_cost, int target_mode, int[] health_change, List<StatusData> statuses){
        // Skill nullSkill = new Skill(){skill_id = "null", display_name = "Nothing", ap_cost = 0, health_change = {0, 0, 0}};
        // return nullSkill;
        this.skill_id = skill_id;
        this.display_name = display_name;
        this.ap_cost = ap_cost;
        this.target_mode = target_mode;
        this.health_change = health_change;
        this.statuses = statuses;
    }
}

