using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EntityDisplay : MonoBehaviour, IEntityDisplay {
    [SerializeField]
    private Entity baseEntity;
    [SerializeField]
    private HealthBar bar;
    [SerializeField]
    private SpriteBox box;
    [SerializeField]
    private StatusDisplay statusDisplay;
    [SerializeField]
    private TMP_Text nameText;
    [SerializeField]
    private Color baseBackground = new Color(0.25F, 0.25F, 0.25F, 1);
    [SerializeField]
    private Color hurtBackground = new Color(1, 0.25F, 0.25F, 1);
    [SerializeField]
    private Color healBackground = new Color(0, 0.8F, 0, 1);
    [SerializeField]
    private string hurtSound = "hurt";
    [SerializeField]
    private string healSound = "heal";
    [SerializeField]
    private int flashTime = 50;
    [SerializeField]
    private Sprite normal;
    [SerializeField]
    private Sprite attacking;
    [SerializeField]
    private Sprite hurt;
    [SerializeField]
    private Sprite low;
    public Animator spriteAnimator;

    // Start is called before the first frame update
    void Start() {
        box.SetBackgroundColor(baseBackground);
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void SetEntity(Entity entity) {
        baseEntity = entity;
    }
    public void InitialiseHealthDisplay() {
        bar.SetValAndMaxVal(baseEntity.GetHealth(), baseEntity.GetMaxHealth());
    }

    public void UpdateHealthDisplay() {
        bar.SetValAndMaxVal(baseEntity.GetHealth(), baseEntity.GetMaxHealth());
    }

    public void UpdateStatusDisplay() {
        statusDisplay.UpdateStatuses(new List<Skill.Status>(baseEntity.statuses.Keys));
    }

    public void PerformSpriteAnimation(string animation) {
        switch (animation) {
            case "heal":
                box.FlashBackgroundColor(healBackground, flashTime * 2);
                SFXEngine.instance.PlayClip(healSound);
                break;
            case "hurt":
                box.FlashBackgroundColor(hurtBackground, flashTime);
                SFXEngine.instance.PlayClip(hurtSound);
                break;
        }
    }

    public void SetName(string name) {
        nameText.text = name;
    }

    public void OtherAnimation() {

    }
}
