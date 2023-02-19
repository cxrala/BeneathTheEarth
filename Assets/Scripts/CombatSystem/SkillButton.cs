using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class SkillButton : MonoBehaviour
{
    public Button mybutton;
    public CombatControl mycombat;
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
        foreach (Skill s in mycombat.available_skills){
            if (s.display_name == mybuttontext){
                if (mycombat.hero_ap_left >= s.ap_cost){
                    mytimeline.text += mybuttontext+"\n";
                    mycombat.hero_timeline.Add(s);
                    mycombat.hero_ap_left -= s.ap_cost;
                    myapleft.text = mycombat.hero_ap_left.ToString();
                }
            }
        }
    }


}
