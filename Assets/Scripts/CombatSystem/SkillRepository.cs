using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Skill;

public class SkillRepository
{
    public Skill nullSkill = new Skill("nullSkill", "Pass", 0, TargetMode.None, TargetSide.Self, new int[3] {0,0,0}, new List<Skill.StatusData>());
    // public Skill 
    public IDictionary<string, Skill> allSkills = new Dictionary<string, Skill>();

    private struct SkillLoadList {
        public SkillLoadData[] skills;
    }

    [Serializable]
    private struct StatusLoadData {
        public string status;
        public float chance;
        public int duration;
        public float power;
    }

    [Serializable]
    private struct SkillLoadData {
        public string skillID;
        public string displayName;
        public int apCost;
        public int[] healthChange;
        public string targetMode;
        public string targetSide;
        public List<StatusLoadData> statuses;
    }

    private Skill ConvertLoadData(SkillLoadData data) {
        List<StatusData> statuses = new List<StatusData>(data.statuses.Count);
        foreach (StatusLoadData statusData in data.statuses) {
            Debug.Log(statusData.status);
            statuses.Add(new StatusData() { status = Enum.Parse<Status>(statusData.status), chance = statusData.chance, duration = statusData.duration, power = statusData.power});
        }
        return new Skill(data.skillID, data.displayName, data.apCost, Enum.Parse<TargetMode>(data.targetMode), Enum.Parse<TargetSide>(data.targetSide), data.healthChange, statuses);
    }

    public SkillRepository(string[] paths) {
        foreach (string path in paths) {
            TextAsset skillFile = Resources.Load<TextAsset>(path);
            if (skillFile != null) {
                SkillLoadList skills = JsonUtility.FromJson<SkillLoadList>(skillFile.text);
                foreach (SkillLoadData skill in skills.skills) {
                    if (!allSkills.ContainsKey(skill.skillID)) {
                        allSkills.Add(skill.skillID, ConvertLoadData(skill));
                    }
                }
            }
        }
    }

    public Skill StringToSkill(string target){
        return allSkills[target];
    }
    public List<Skill> StringsToSkills(List<string> targets){
        List<Skill> results = new List<Skill>();
        foreach (string target in targets){
            results.Add(allSkills[target]);
        }
        return results;
    }
}
