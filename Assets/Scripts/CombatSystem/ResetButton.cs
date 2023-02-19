using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class ResetButton : MonoBehaviour
{
    public Button mybutton;
    public CombatControl mycombat;
    public TMP_Text mytimeline;
    public TMP_Text myapleft;
    // Start is called before the first frame update
    void Start()
    {
        Button btn = mybutton.GetComponent<Button>();
		btn.onClick.AddListener(ClearSchedule);

    }

    // Update is called once per frame
    void ClearSchedule()
    {
        mytimeline.text = "";
        mycombat.hero_timeline = new List<CombatControl.Action>();
        mycombat.hero_ap_left = 4;
        myapleft.text = 4.ToString();
    }
}
