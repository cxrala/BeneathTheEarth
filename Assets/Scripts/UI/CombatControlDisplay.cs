using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

public class CombatControlDisplay : MonoBehaviour
{
    [SerializeField]
    private List<Color> buttonColors;

    [SerializeField]
    private List<ItemList> skillLists;

    [SerializeField]
    private ReorderList heroTimeline;

    [SerializeField]
    private CombatControl combatController;

    [SerializeField]
    private GameObject actionButtonPrefab;
    [SerializeField]
    private GameObject skillButtonPrefab;

    public TMP_Text apLeft;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    // Executes the turn
    public async void Execute() {

        foreach (ActionButton a in heroTimeline.baseList) {
            combatController.heroTimeline.Add(a.action);
        }

        while (combatController.heroTimeline.Count> 0) {
            combatController.ExecuteSkill();
            heroTimeline.RemoveAt(0);
            await Task.Delay(500);
        }

        while (combatController.enemyTimeline.Count > 0) {
            combatController.ExecuteSkill();
            await Task.Delay(500);
        }
        combatController.RoundSetup();
        RoundSetup();
    }

    public void RoundSetup() {
        apLeft.text = combatController.heroApLeft.ToString();
        heroTimeline.Clear();
    }

    public void AddAction(Skill skill, Entity performer) {
        if (skill.apCost <= combatController.heroApLeft) {
            GameObject temp = Instantiate(actionButtonPrefab, new Vector3(0, 0, 0), Quaternion.identity);
            ActionButton tempAction = temp.GetComponent<ActionButton>();
            tempAction.SetAction(new CombatControl.Action() { skill = skill, performer = performer, target = new List<Entity>() });
            heroTimeline.Add(tempAction);
            combatController.heroApLeft -= skill.apCost;
            apLeft.text = combatController.heroApLeft.ToString();
        }
    }

    public void InitialiseBattle() {
        GameObject temp;
        SkillButton tempButton;
        // Create Skill Buttons
        for (int i = 0; i < skillLists.Count; i++) {
            foreach (Skill s in combatController.heroSkills[i]) {
                temp = Instantiate(skillButtonPrefab);
                tempButton = temp.GetComponent<SkillButton>();
                tempButton.Initialise(s, combatController.heroes[i], this);
                tempButton.image.color = buttonColors[i];
                skillLists[i].Add(tempButton);
            }
        }
        apLeft.text = combatController.heroApLeft.ToString();
    }
}
