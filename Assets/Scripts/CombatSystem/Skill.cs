using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public string skill_id = "";
    public string display_name = "";
    public int ap_cost = 0;
    public int[] health_change = {1, 6, 0};
    public int target_mode = 0; // 1 = Enemy, -1 = Weakest Party Member, -2 = All party members, -3 = Random party member
    
    // public struct status_info {
    //     public string name;
    //     public float chance;
    //     public int duration;
    // }

    public struct status_data {
        public float chance;
        public int duration;
    }
    
    // public List<status_info> status_list;
    public IDictionary<string, status_data> statuses;

    void Start(){
        // this.statuses = new Dictionary<string, status_data>();
        // foreach (status_info s in status_list){
        //     status_data sd = new status_data();
        //     sd.chance = s.chance;
        //     sd.duration = s.duration;
        //     this.statuses.Add(s.name, sd);
        // }
    }

    public void Use(List<Entity> targets){
        for (int i=0; i<targets.Count; i++){
            ApplySkill(targets[i]);
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
            total += Random.Range(1, size+1);
        }
        return total*multiplier;
    }

    public void ApplySkill(Entity target){
        target.ChangeHealth(Resolve(health_change[0], health_change[1], health_change[2]));
        foreach (var (key, value) in statuses)
        {
            string name = key;
            float chance = value.chance;
            int duration = value.duration;
            float decision = Random.Range(0.0f, 1.0f);
            if (decision < chance){
                target.ModStatus(name, duration);
            }
        }
    }

    public Skill(string skill_id, string display_name, int ap_cost, int target_mode, int[] health_change, IDictionary<string, status_data> statuses){
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

