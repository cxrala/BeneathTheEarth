using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CharacterMenu : MonoBehaviour {
    //Text fields
    public TextMeshPro[] skillTexts;

    //Logic
    public SpriteBox[] spriteBoxes = new SpriteBox[4];
    public Sprite[] normalSprites = new Sprite[4];
    public Sprite[] hurtSprites = new Sprite[4];
    public HealthBar[] bars = new HealthBar[4];
    public Player player;
    public Color selectedColor;
    public Color deselectedColor;
    public int selected;

    //Char selection
    public void OnArrowClick(bool right) {
        
    }

    private void OnSelectionChanged() {
        
    }

    private void Start()
    { 

    }

    public void SelectCharacter(int characterID)
    {
        player.SwapCharacter(characterID);
        UpdateMenu();
    }

    //Update character info
    public void UpdateMenu() {

        int[] hitpoints = PlayerData.instance.GetHitpoint();
        int[] maxHitpoints = PlayerData.instance.maxHitpoint;

        for (int i = 0; i < hitpoints.Length; i++)
        {
            bars[i].SetValAndMaxVal((float)hitpoints[i], (float)maxHitpoints[i]);
            if (bars[i].GetRatio() < 0.2) {
                spriteBoxes[i].SetSprite(hurtSprites[i]);
            } else {
                spriteBoxes[i].SetSprite(normalSprites[i]);
            }
        }

        int selected = player.GetCharacterID();
        this.selected = selected;
        //int selected = this.selected;
        for (int i = 0; i < hitpoints.Length; i++) {
            if (i == selected) {
                spriteBoxes[i].SetBackgroundColor(selectedColor);
            } else {
                spriteBoxes[i].SetBackgroundColor(deselectedColor);
            }
        }

    }
}
