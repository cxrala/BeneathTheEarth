using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : ReorderItem
{
    public CombatControl.Action action;
    public TMP_Text text;

    public void SetAction(CombatControl.Action action) {
        this.action = action;
        text.text = action.skill.displayName;
    }
}
