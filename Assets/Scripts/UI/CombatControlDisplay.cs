using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class CombatControlDisplay : MonoBehaviour
{
    [SerializeField]
    private List<Vector2> buttonLocations;
    [SerializeField]
    private List<Color> buttonColors;
    [SerializeField]
    private List<GameObject> skillButtons;
    [SerializeField]
    private CombatControl combatController;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Executes the turn
    public async void Execute() {
        while (combatController.heroTimeline.Count + combatController.enemyTimeline.Count > 0) {
            combatController.ExecuteSkill();
            await Task.Delay(500);
        }
        combatController.RoundSetup();
    }

    public void InitialiseBattle() {

    }
}
