using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRepository
{
    public Skill nullSkill = new Skill("nullSkill", "Pass", 0, 0, new int[3] {0,0,0}, new Dictionary<string, Skill.status_data>());
    // public Skill 
    public IDictionary<string, Skill> all_skills = new Dictionary<string, Skill>();


    // Start is called before the first frame update
    public SkillRepository()
    {
        all_skills.Add("nullSkill", nullSkill);

        Skill punch = new Skill("punch", "Punch", 1, 1, new int[3] {1, 6, 1}, new Dictionary<string, Skill.status_data>());
        Skill wrench = new Skill("wrench", "Wrench", 2, 1, new int[3] {1, 4, 0}, new Dictionary<string, Skill.status_data>() {{"stunned", new Skill.status_data(){chance=0.5f, duration=1}}});
        Skill dash = new Skill("dash", "Dash", 2, 1, new int[3] {0, 0, 0}, new Dictionary<string, Skill.status_data>() {{"evasive", new Skill.status_data(){chance=0.75f, duration=3}}});
        Skill flyingkick = new Skill("flyingkick", "Flying Kick", 3, 1, new int[3] {3, 6, 0}, new Dictionary<string, Skill.status_data>());

        all_skills.Add("punch", punch);
        all_skills.Add("wrench", wrench);
        all_skills.Add("dash", dash);
        all_skills.Add("flyingkick", flyingkick);

        Skill tap = new Skill("tap", "Tap", 1, 1, new int[3] {0, 0, 3}, new Dictionary<string, Skill.status_data>());
        Skill patchup = new Skill("patchup", "Patch-up", 1, -1, new int[3] {-1, 4, 0}, new Dictionary<string, Skill.status_data>());
        Skill surge = new Skill("surge", "Surge", 2, -1, new int[3] {-2, 4, 2}, new Dictionary<string, Skill.status_data>());
        Skill massheal = new Skill("massheal", "Mass-Heal", 3, -2, new int[3] {0, 0, -4}, new Dictionary<string, Skill.status_data>());

        all_skills.Add("tap", tap);
        all_skills.Add("patchup", patchup);
        all_skills.Add("surge", surge);
        all_skills.Add("massheal", massheal);

        Skill thwomp = new Skill("thwomp", "Thwomp", 1, 1, new int[3] {1, 4, 0}, new Dictionary<string, Skill.status_data>() {{"weak", new Skill.status_data(){chance=0.5f, duration=1}}});
        Skill block = new Skill("block", "Block", 2, 1, new int[3] {0, 0, 0}, new Dictionary<string, Skill.status_data>() {{"shielded", new Skill.status_data(){chance=0.9f, duration=2}}});
        Skill hold = new Skill("hold", "Hold", 2, 1, new int[3] {1, 6, 0}, new Dictionary<string, Skill.status_data>() {{"weak", new Skill.status_data(){chance=0.9f, duration=2}}});
        Skill smash = new Skill("smash", "Smash", 3, 1, new int[3] {1, 6, 3}, new Dictionary<string, Skill.status_data>() {{"weak", new Skill.status_data(){chance=1.0f, duration=1}}});

        all_skills.Add("thwomp", thwomp);
        all_skills.Add("block", block);
        all_skills.Add("hold", hold);
        all_skills.Add("smash", smash);

        Skill scratch = new Skill("scratch", "Scratch", 1, 1, new int[3] {0, 0, 1}, new Dictionary<string, Skill.status_data>());
        Skill meow = new Skill("meow", "Meow", 1, -2, new int[3] {0, 0, -1}, new Dictionary<string, Skill.status_data>());

        all_skills.Add("scratch", scratch);
        all_skills.Add("meow", meow);

        //enemy skills

        Skill batscreech = new Skill("batscreech", "Supersonic Screech", 1, -1, new int[3] { 1, 8, 0 }, new Dictionary<string, Skill.status_data>());
        Skill slughug = new Skill("slughug", "Sluggish Embrace", 1, -2, new int[3] { 0, 0, 1 }, new Dictionary<string, Skill.status_data>() { { "weak", new Skill.status_data() { chance = 0.75f, duration = 1 } } });
        Skill slugslime = new Skill("slugslime", "Corrosive Slime", 1, -3, new int[3] { 1, 4, 0 }, new Dictionary<string, Skill.status_data>());
        Skill tardishield = new Skill("tardishield", "Cryptobiosis", 1, 1, new int[3] { 0, 0, 0 }, new Dictionary<string, Skill.status_data>() { { "shielded", new Skill.status_data() { chance = 0.75f, duration = 2 } } });
        Skill tardicrush = new Skill("tardicrush", "Intruder Countermeasures", 1, -1, new int[3] { 1, 6, 0 }, new Dictionary<string, Skill.status_data>() { { "weak", new Skill.status_data() { chance = 0.75f, duration = 2 } } });
        Skill tardiregen = new Skill("tardiregen", "Autodiagnosis", 1, 1, new int[3] { -1, 6, 1 }, new Dictionary<string, Skill.status_data>());
        Skill eyebeam = new Skill("eyebeam", "Scattershot Lazer", 1, -3, new int[3] { 1, 4, 0 }, new Dictionary<string, Skill.status_data>());
        Skill blobmorph = new Skill("blobmorph", "Morphic Elimination", 1, -2, new int[3] { 1, 6, 3 }, new Dictionary<string, Skill.status_data>());

        all_skills.Add("batscreech", batscreech);
        all_skills.Add("slughug", slughug);
        all_skills.Add("slugslime", slugslime);
        all_skills.Add("tardishield", tardishield);
        all_skills.Add("tardicrush", tardicrush);
        all_skills.Add("tardiregen", tardicrush);
        all_skills.Add("eyebeam", eyebeam);
        all_skills.Add("blobmorph", blobmorph);


    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Skill StringToSkill(string target){
        return all_skills[target];
    }
    public List<Skill> StringsToSkills(List<string> targets){
        List<Skill> results = new List<Skill>();
        foreach (string target in targets){
            results.Add(all_skills[target]);
        }
        return results;
    }
}
