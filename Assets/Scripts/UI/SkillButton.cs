using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillButton : Item
{
    public Skill skill;
    public Entity performer;
    [SerializeField]
    private Button button;
    [SerializeField]
    private CombatControlDisplay combatControlDisplay;
    [SerializeField]
    private TMP_Text text;
    public Image image;

    // Start is called before the first frame update
    void Start() {
    }

    private void OnDisable() {
        button.onClick.RemoveListener(AddAction);
    }

    private void OnEnable() {
        button.onClick.AddListener(AddAction);
    }

    public void AddAction() {
        combatControlDisplay.AddAction(skill, performer);
    }

    public void SetSkill(Skill skill, Entity performer) {
        this.skill = skill;
        text.text = skill.displayName;
        this.performer = performer;
    }

    public void Initialise(Skill skill, Entity performer, CombatControlDisplay combatControlDisplay) {
        this.skill = skill;
        text.text = skill.displayName;
        this.performer = performer;
        this.combatControlDisplay = combatControlDisplay;
    }
}
