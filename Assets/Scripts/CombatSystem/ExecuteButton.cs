using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ExecuteButton : MonoBehaviour
{
    public Button mybutton;
    public CombatControl mycombat;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = mybutton.GetComponent<Button>();
		btn.onClick.AddListener(LaunchCombat);

    }

    // Update is called once per frame
    void LaunchCombat()
    {
        mycombat.LaunchCombatSequence();
    }
}
