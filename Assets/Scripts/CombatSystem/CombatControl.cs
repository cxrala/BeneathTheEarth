using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CombatControl : MonoBehaviour
{
    public struct Action {
        public Skill skill;
        public Entity performer;
        public List<Entity> target;
    }

    [SerializeField]
    private List<Entity> heroes = new List<Entity>();
    [SerializeField]
    private List<Entity> enemies = new List<Entity>();
    public Sprite[] normal = new Sprite[4];
    public Sprite[] hurt = new Sprite[4];
    public Sprite[] low = new Sprite[4];

    public List<Action> heroTimeline = new List<Action>();
    public List<Action> enemyTimeline = new List<Action>();

    [SerializeField] 
    public List<Skill> availableSkills = new List<Skill>();

    public GameObject[] skillButtons;
    public List<GameObject> otherButtons = new List<GameObject>();

    private Color[] skillColors;
    private Color[] otherColors;

    public TMP_Text heroTimelineText;
    public TMP_Text enemyTimelineText;
    public TMP_Text apLeft;

    public SkillRepository myskills;

    public int heroApLeft = 4;

    public GameObject enemyPrefab;
    public GameObject canvas;
    public CombatControlDisplay combatDisplay;

    public Vector2 enemyCentre = new Vector2(450, 280);

    public int enemyLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseBattle();
    }

    private void SetButtons()
    {
        skillButtons = GameObject.FindGameObjectsWithTag("SkillButton");
        skillColors = new Color[skillButtons.Length];
        for (int i = 0; i < skillButtons.Length; i++)
        {
            skillColors[i] = skillButtons[i].GetComponent<Image>().color;
        }
        otherButtons.Add(GameObject.Find("Reset"));
        otherButtons.Add(GameObject.Find("Execute"));
        otherColors = new Color[otherButtons.Count];
        for (int i = 0; i < otherButtons.Count; i++)
        {
            otherColors[i] = otherButtons[i].GetComponent<Image>().color;
        }

        RoundSetup();
    }

    IEnumerator WaitingToReceiveBattleStart()
    {
        while (Data.GetObject("enemyData") == null) {
            yield return null;
        }
    }

    private void InitialiseBattle()
    {
        Debug.Log("Attempt to load skills");
        SkillRepository test = new SkillRepository(new string[]{ "TextData/Skills/SkillRepository" });
        Debug.Log(test.allSkills);
        Debug.Log(test.allSkills.Count);
        foreach (string s in test.allSkills.Keys) {
            Debug.Log(s);
            Debug.Log(test.allSkills[s].skillID);
        }
        Debug.Log("Attempting to initialise Battle");
        StartCoroutine(WaitingToReceiveBattleStart());
        myskills = new SkillRepository(new string[] { "TextData/Skills/SkillRepository" });
        int[] hitpoint = PlayerData.instance.hitpoint;
        int[] maxhitpoint = PlayerData.instance.maxHitpoint;

        for (int i = 0; i < hitpoint.Length; i++)
        {
            heroes[i].SetHealth(hitpoint[i], maxhitpoint[i]);
        }
        string[] upgrades = { "flyingkick", "massheal", "smash" };
        for (int i = 0; i < 3; i++) {
            if (PlayerData.instance.upgraded[i]) {
                heroes[i].skills.Add(upgrades[i]);
            }
        }


        Debug.Log("Initialising Enemies");
        EnemyData enemy = Data.GetObject("enemyData").GetComponent<Enemy>().data;
        Debug.Log(enemy.hitpoints.Count);
        for (int i = 0; i < enemy.hitpoints.Count; i++)
        {
            Entity temp = Instantiate(enemyPrefab, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<Entity>();
            temp.transform.parent = canvas.transform;
            RectTransform tempRect = temp.GetComponent<RectTransform>();
            tempRect.anchorMax = new Vector2(0.5F, 0.5F);
            tempRect.anchorMin = new Vector2(0.5F, 0.5F);
            tempRect.pivot = new Vector2(0.5F, 0.5F);
            tempRect.anchoredPosition = new Vector2(0, 0) + enemyCentre;
            temp.type = enemy.types[i];
            temp.displayName = enemy.names[i];
            temp.isEnemy = true;
            temp.level = enemyLevel;
            temp.SetEnemySkills(enemy.maxHitpoints[i], enemy.levels[i]);
            //temp.box.spriteImage.transform.localScale = new Vector3(2, 2, 1);
            temp.display.spriteAnimator.SetInteger("TypeID", temp.typeId);
            enemies.Add(temp);
        }
        SetButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RoundSetup(){
        for (int i = 0; i < skillButtons.Length; i++) {
            skillButtons[i].SetActive(true);
            skillButtons[i].GetComponent<Image>().color = skillColors[i];
            skillButtons[i].GetComponent<Button>().enabled = true;
        }

        for (int i = 0; i < otherButtons.Count; i++) {
            otherButtons[i].SetActive(true);
            otherButtons[i].GetComponent<Image>().color = otherColors[i];
            otherButtons[i].GetComponent<Button>().enabled = true;
        }

        heroTimeline = new List<Action>();
        enemyTimeline = new List<Action>();
        heroTimelineText.text = "";
        enemyTimelineText.text = "";
        heroApLeft = 4;
        apLeft.text = heroApLeft.ToString();

        // Line up enemy actions for this round.
        enemyTimeline.Clear();
        foreach (Entity e in enemies) {
            e.UpdateStatuses();
            if (!e.statuses.ContainsKey(Skill.Status.Stunned)) {
                foreach (string s in e.skills) {
                    if (s != "nullSkill") {
                        Debug.Log(s);
                        enemyTimeline.Add(new Action() { skill = myskills.allSkills[s], performer = e, target = null });
                    }
                }
            }
        }
        string newEnemyTimeline = "";
        foreach (Action a in enemyTimeline){
            newEnemyTimeline += a.skill.displayName+'\n';
        }
        enemyTimelineText.text = newEnemyTimeline;
        // Show all hero skill buttons

        GameObject.Find("Reset").SetActive(true);
        GameObject.Find("Execute").SetActive(true);

        availableSkills = new List<Skill>();
        foreach (Entity hero in heroes) {
            hero.UpdateStatuses();
            foreach (Skill s in myskills.StringsToSkills(hero.skills)){
                availableSkills.Add(s);
            }
        }

        List<GameObject> buttonsToReactivate = new List<GameObject>();
        foreach (Skill s in availableSkills){
            GameObject target = GameObject.Find(s.displayName);
            buttonsToReactivate.Add(target);
        };

        foreach (GameObject g in skillButtons) {
            g.SetActive(false);
        };

        foreach (GameObject g in buttonsToReactivate) {
            g.SetActive(true);
        }
    }
    // Executes the next skill in the action list
    public void ExecuteSkill() {
        if (heroTimeline.Count != 0) {
            Action a = heroTimeline[0]; 
            heroTimeline.RemoveAt(0);
            Debug.Log(a.skill.skillID);
            Debug.Log((int)a.skill.targetMode * (int)a.skill.targetSide);
            if ((int)a.skill.targetMode * (int)a.skill.targetSide == 1) {
                a.skill.ApplySkill(enemies[0], a.performer);
            } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == -1) {
                a.skill.ApplySkill(GetWeakestEntity(heroes), a.performer); // just buff hero with lowest hp
            } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == -2) {
                a.skill.Use(heroes, a.performer);
            } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == -3) {
                a.skill.ApplySkill(GetRandomEntity(heroes), a.performer);
            }
            List<string> hero_timeline_list = new List<string>(heroTimelineText.text.Split('\n'));
            hero_timeline_list.RemoveAt(0);
            heroTimelineText.text = string.Join('\n', hero_timeline_list);
            Debug.Log("done waiting.");

            List<Entity> nextEnemies = new List<Entity>();
            foreach (Entity e in enemies) {
                if (e.IsDead()) {
                    Destroy(e.gameObject);
                } else {
                    nextEnemies.Add(e);
                }
            }
            enemies = nextEnemies;

            if (enemies.Count == 0) {
                BattleLoader.LoadDungeonFromWin();
                return;
            }

            for (int j = 0; j < heroes.Count; j++) {
                PlayerData.instance.SetHitpoint(j, heroes[j].GetHealth());
                if (heroes[j].IsDead()) {
                    BattleLoader.LoadDungeonFromLose();
                    return;
                }
            }
        } else if (enemyTimeline.Count != 0) {
            Action a = enemyTimeline[0];
            enemyTimeline.RemoveAt(0);
            if (!a.performer.IsDead() && !a.performer.IsStunned()) {
                if ((int)a.skill.targetMode * (int)a.skill.targetSide == -1) {
                    a.skill.ApplySkill(enemies[0], a.performer);
                } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == 1) {
                    a.skill.ApplySkill(GetWeakestEntity(heroes), a.performer); // just attack hero with lowest hp
                } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == 2) {
                    a.skill.Use(heroes, a.performer);
                } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == 3) {
                    a.skill.ApplySkill(GetRandomEntity(heroes), a.performer);
                }
            }

            List<string> enemy_timeline_list = new List<string>(enemyTimelineText.text.Split('\n'));
            enemy_timeline_list.RemoveAt(0);
            enemyTimelineText.text = string.Join('\n', enemy_timeline_list);

            List<Entity> nextEnemies = new List<Entity>();
            foreach (Entity e in enemies) {
                if (e.IsDead()) {
                    Destroy(e.gameObject);
                } else {
                    nextEnemies.Add(e);
                }
            }
            enemies = nextEnemies;

            if (enemies.Count == 0) {
                BattleLoader.LoadDungeonFromWin();
                return;
            }

            for (int j = 0; j < heroes.Count; j++) {
                PlayerData.instance.SetHitpoint(j, heroes[j].GetHealth());
                if (heroes[j].IsDead()) {
                    BattleLoader.LoadDungeonFromLose();
                    return;
                }
            }
        }
    }

    public async void LaunchCombatSequence(){

        for (int i = 0; i < skillButtons.Length; i++){
            skillButtons[i].GetComponent<Image>().color = new Color(skillColors[i].r/2, skillColors[i].g/2, skillColors[i].b/2, skillColors[i].a);
            skillButtons[i].GetComponent<Button>().enabled = false;
        }

        for (int i = 0; i < otherButtons.Count; i++)
        {
            otherButtons[i].GetComponent<Image>().color = otherColors[i] / 2;
            otherButtons[i].GetComponent<Image>().color = new Color(otherColors[i].r / 2, otherColors[i].g / 2, otherColors[i].b / 2, otherColors[i].a);
            otherButtons[i].GetComponent<Button>().enabled = false;
        }

        Debug.Log("done hiding buttons");

        // List<Skill> hero_skills = new List<Skill>(hero_timeline);
        for (int i=0; i<heroTimeline.Count; i++){
            Action a = heroTimeline[i];
            Debug.Log(a.skill.skillID);
            Debug.Log((int)a.skill.targetMode * (int)a.skill.targetSide);
            if ((int) a.skill.targetMode * (int)a.skill.targetSide == 1){
                a.skill.ApplySkill(enemies[0], a.performer);
            } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == -1){
                a.skill.ApplySkill(GetWeakestEntity(heroes), a.performer); // just buff hero with lowest hp
            } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == -2){
                a.skill.Use(heroes, a.performer);
            } else if ((int)a.skill.targetMode * (int)a.skill.targetSide == -3){
                a.skill.ApplySkill(GetRandomEntity(heroes), a.performer);
            }
            List<string> heroTimelineList = new List<string>(heroTimelineText.text.Split('\n'));
            heroTimelineList.RemoveAt(0);
            heroTimelineText.text = string.Join('\n', heroTimelineList);
            await Task.Delay(500);
            Debug.Log("done waiting.");

            List<Entity> nextEnemies = new List<Entity>();
            foreach (Entity e in enemies) {
                if (e.IsDead()) {
                    Destroy(e.gameObject);
                } else {
                    nextEnemies.Add(e);
                }
            }
            enemies = nextEnemies;

            if (enemies.Count == 0) {
                BattleLoader.LoadDungeonFromWin();
                return;
            }

            for (int j = 0; j < heroes.Count; j++) {
                PlayerData.instance.SetHitpoint(j, heroes[j].GetHealth());
                if (heroes[j].IsDead()) {
                    BattleLoader.LoadDungeonFromLose();
                    return;
                }
            }
        }
        Debug.Log("hero moves done.");
        // List<Skill> enemy_skills = new List<Skill>(enemy_timeline);
        for (int i=0; i<enemyTimeline.Count; i++){
            Action a = enemyTimeline[i];
            if (!a.performer.IsDead() && !a.performer.IsStunned())
            {
                if ((int) a.skill.targetMode * (int) a.skill.targetSide == -1)
                {
                    a.skill.ApplySkill(enemies[0], a.performer);
                }
                else if ((int)a.skill.targetMode * (int)a.skill.targetSide == 1)
                {
                    a.skill.ApplySkill(GetWeakestEntity(heroes), a.performer); // just attack hero with lowest hp
                }
                else if ((int)a.skill.targetMode * (int)a.skill.targetSide == 2)
                {
                    a.skill.Use(heroes, a.performer);
                }
                else if ((int)a.skill.targetMode * (int)a.skill.targetSide == 3)
                {
                    a.skill.ApplySkill(GetRandomEntity(heroes), a.performer);
                }
            }

            List<string> enemyTimelineList = new List<string>(enemyTimelineText.text.Split('\n'));
            enemyTimelineList.RemoveAt(0);
            enemyTimelineText.text = string.Join('\n', enemyTimelineList);

            await Task.Delay(500);

            List<Entity> nextEnemies = new List<Entity>();
            foreach (Entity e in enemies) {
                if (e.IsDead()) {
                    Destroy(e.gameObject);
                } else {
                    nextEnemies.Add(e);
                }
            }
            enemies = nextEnemies;

            if (enemies.Count == 0) {
                BattleLoader.LoadDungeonFromWin();
                return;
            }

            for (int j = 0; j < heroes.Count; j++) {
                PlayerData.instance.SetHitpoint(j, heroes[j].GetHealth());
                if (heroes[j].IsDead()) {
                    BattleLoader.LoadDungeonFromLose();
                    return;
                }
            }

        }
        RoundSetup();
    }

    public Entity GetRandomEntity(List<Entity> people){
        int i = Random.Range(0, people.Count);
        return people[i];
    }

    public Entity GetWeakestEntity(List<Entity> people){
        Entity weakest = people[0];
        int lowest = 1000;
        for (int i=0; i<people.Count; i++){
            if (people[i].GetHealth() < lowest){
                weakest = people[i];
                lowest = people[i].GetHealth();
            }
        }
        return weakest;
    }

    public int GetTimelineLength(List<Skill> timeline){
        int t = 0;
        foreach(Skill s in timeline){
            t += s.apCost;
        }
        return t;
    }

}
