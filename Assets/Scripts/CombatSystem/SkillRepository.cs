using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillRepository
{
    public Skill nullSkill = new Skill("nullSkill", "Pass", 0, 0, new int[3] {0,0,0}, new List<Skill.StatusData>());
    // public Skill 
    public IDictionary<string, Skill> all_skills = new Dictionary<string, Skill>();


    // Start is called before the first frame update
    public SkillRepository() {
        all_skills.Add("nullSkill", nullSkill);

        Skill punch = new Skill("punch", "Punch", 1, 1, new int[3] { 1, 6, 1 }, new List<Skill.StatusData>());
        Skill wrench = new Skill("wrench", "Wrench", 2, 1, new int[3] { 1, 4, 0 }, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.stunned, chance = 0.2f, duration = 1, power = 1 } });
        Skill dash = new Skill("dash", "Dash", 2, -1, new int[3] {0, 0, 0 }, new List<Skill.StatusData>() {new Skill.StatusData() { status = Skill.Status.evasive, chance = 0.75f, duration = 3, power = 0.5f }});
        Skill flyingkick = new Skill("flyingkick", "Flying Kick", 3, 1, new int[3] {3, 6, 0}, new List<Skill.StatusData>());

        all_skills.Add("punch", punch);
        all_skills.Add("wrench", wrench);
        all_skills.Add("dash", dash);
        all_skills.Add("flyingkick", flyingkick);

        Skill tap = new Skill("tap", "Tap", 1, 1, new int[3] {0, 0, 3}, new List<Skill.StatusData>());
        Skill patchup = new Skill("patchup", "Patch-up", 1, -1, new int[3] {-1, 4, 0}, new List<Skill.StatusData>());
        Skill surge = new Skill("surge", "Surge", 2, -1, new int[3] {-2, 4, 2}, new List<Skill.StatusData>());
        Skill massheal = new Skill("massheal", "Mass-Heal", 3, -2, new int[3] {-1, 2, 4}, new List<Skill.StatusData>());

        all_skills.Add("tap", tap);
        all_skills.Add("patchup", patchup);
        all_skills.Add("surge", surge);
        all_skills.Add("massheal", massheal);

        Skill thwomp = new Skill("thwomp", "Thwomp", 1, 1, new int[3] {1, 2, 2}, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.weak, chance = 0.5f, duration = 1, power = 3 } });
        Skill block = new Skill("block", "Block", 2, -2, new int[3] {0, 0, 0}, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.shielded, chance = 0.75f, duration = 2, power = 3 } });
        Skill hold = new Skill("hold", "Hold", 2, 1, new int[3] {1, 3, 3}, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.stunned, chance = 0.25f, duration = 1, power = 1 } });
        Skill smash = new Skill("smash", "Smash", 3, 1, new int[3] {1, 6, 3}, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.stunned, chance = 0.5f, duration = 2, power = 1 } });

        all_skills.Add("thwomp", thwomp);
        all_skills.Add("block", block);
        all_skills.Add("hold", hold);
        all_skills.Add("smash", smash);

        Skill scratch = new Skill("scratch", "Scratch", 1, 1, new int[3] {0, 0, 1}, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.weak, chance = 0.25f, duration = 2, power = 2 } });
        Skill meow = new Skill("meow", "Meow", 1, -2, new int[3] {-1, 1, 1}, new List<Skill.StatusData>());

        all_skills.Add("scratch", scratch);
        all_skills.Add("meow", meow);

        //enemy skills

        Skill batscreech = new Skill("batscreech", "Supersonic Screech", 1, -1, new int[3] { 1, 8, 0 }, new List<Skill.StatusData>());
        Skill slughug = new Skill("slughug", "Sluggish Embrace", 1, -2, new int[3] { 0, 0, 1 }, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.weak, chance = 0.75f, duration = 2, power = 3 } });
        Skill slugslime = new Skill("slugslime", "Corrosive Slime", 1, -3, new int[3] { 1, 4, 0 }, new List<Skill.StatusData>());
        Skill tardishield = new Skill("tardishield", "Cryptobiosis", 1, 1, new int[3] { 0, 0, 0 }, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.shielded, chance = 0.75f, duration = 3, power = 3 } });
        Skill tardicrush = new Skill("tardicrush", "Intruder Countermeasures", 1, -1, new int[3] { 1, 6, 0 }, new List<Skill.StatusData>() { new Skill.StatusData() { status = Skill.Status.weak, chance = 0.75f, duration = 3, power = 3 } });
        Skill tardiregen = new Skill("tardiregen", "Autodiagnosis", 1, 1, new int[3] { -1, 6, 1 }, new List<Skill.StatusData>());
        Skill eyebeam = new Skill("eyebeam", "Scattershot Lazer", 1, -3, new int[3] { 1, 4, 0 }, new List<Skill.StatusData>());
        Skill blobmorph = new Skill("blobmorph", "Morphic Elimination", 1, -2, new int[3] { 1, 6, 3 }, new List<Skill.StatusData>());

        all_skills.Add("batscreech", batscreech);
        all_skills.Add("slughug", slughug);
        all_skills.Add("slugslime", slugslime);
        all_skills.Add("tardishield", tardishield);
        all_skills.Add("tardicrush", tardicrush);
        all_skills.Add("tardiregen", tardiregen);
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
