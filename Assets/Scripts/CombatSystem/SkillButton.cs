using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SkillButton : MonoBehaviour
{
    public Button mybutton;
    public CombatControl mycombat;
    public Entity performer;
    public TMP_Text mytimeline;
    public TMP_Text myapleft;

    // Start is called before the first frame update
    void Start()
    {
        Button btn = mybutton.GetComponent<Button>();
		btn.onClick.AddListener(ScheduleSkill);
    }

    // Update is called once per frame
    void ScheduleSkill()
    {
        string mybuttontext = mybutton.name;
        foreach (Skill s in mycombat.availableSkills){
            if (s.displayName == mybuttontext){
                if (mycombat.heroApLeft >= s.apCost){
                    mytimeline.text += mybuttontext+"\n";
                    mycombat.heroTimeline.Add(new CombatControl.Action() { skill = s, performer = performer, target = null });
                    mycombat.heroApLeft -= s.apCost;
                    myapleft.text = mycombat.heroApLeft.ToString();
                }
            }
        }
    }
}
