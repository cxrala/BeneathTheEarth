using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class CombatControl : MonoBehaviour
{
    public List<Entity> heroes = new List<Entity>();
    public List<Entity> enemies = new List<Entity>();
    public Sprite[] normal = new Sprite[4];
    public Sprite[] hurt = new Sprite[4];
    public Sprite[] low = new Sprite[4];
    public List<Skill> hero_timeline = new List<Skill>();
    public List<Skill> enemy_timeline = new List<Skill>();

    public List<Skill> available_skills = new List<Skill>();

    public GameObject[] skill_buttons;
    public List<GameObject> other_buttons = new List<GameObject>();

    private Color[] skill_colors;
    private Color[] other_colors;

    public TMP_Text hero_timeline_text;
    public TMP_Text enemy_timeline_text;
    public TMP_Text ap_left;

    public SkillRepository myskills = new SkillRepository();

    public int hero_ap_left = 4;

    public GameObject enemyPrefab;
    public GameObject canvas;

    public Vector2 enemyCentre = new Vector2(450, 280);

    public int enemyLevel = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitialiseBattle();
    }

    private void SetButtons()
    {
        skill_buttons = GameObject.FindGameObjectsWithTag("SkillButton");
        skill_colors = new Color[skill_buttons.Length];
        for (int i = 0; i < skill_buttons.Length; i++)
        {
            skill_colors[i] = skill_buttons[i].GetComponent<Image>().color;
        }
        other_buttons.Add(GameObject.Find("Reset"));
        other_buttons.Add(GameObject.Find("Execute"));
        other_colors = new Color[other_buttons.Count];
        for (int i = 0; i < other_buttons.Count; i++)
        {
            other_colors[i] = other_buttons[i].GetComponent<Image>().color;
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
        Debug.Log("Attempting to initialise Battle");
        StartCoroutine(WaitingToReceiveBattleStart());
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
            tempRect.offsetMax = new Vector2(0.5F, 0.5F);
            tempRect.offsetMin = new Vector2(0.5F, 0.5F);
            tempRect.pivot = new Vector2(0.5F, 0.5F);
            tempRect.anchoredPosition = new Vector2(0, 0) + enemyCentre;
            temp.type = enemy.types[i];
            temp.displayName = enemy.names[i];
            temp.is_enemy = true;
            temp.level = enemyLevel;
            temp.SetEnemySkills(enemy.maxHitpoints[i], enemy.levels[i]);
            //temp.box.spriteImage.transform.localScale = new Vector3(2, 2, 1);
            temp.box.spriteImage.gameObject.GetComponent<Animator>().SetInteger("TypeID", temp.typeId);
            enemies.Add(temp);
        }
        SetButtons();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RoundSetup(){
        for (int i = 0; i < skill_buttons.Length; i++) {
            skill_buttons[i].SetActive(true);
            skill_buttons[i].GetComponent<Image>().color = skill_colors[i];
            skill_buttons[i].GetComponent<Button>().enabled = true;
        }

        for (int i = 0; i < other_buttons.Count; i++) {
            other_buttons[i].SetActive(true);
            other_buttons[i].GetComponent<Image>().color = other_colors[i];
            other_buttons[i].GetComponent<Button>().enabled = true;
        }

        hero_timeline = new List<Skill>();
        enemy_timeline = new List<Skill>();
        hero_timeline_text.text = "";
        enemy_timeline_text.text = "";
        hero_ap_left = 4;
        ap_left.text = hero_ap_left.ToString();

        // Line up enemy actions for this round.
        enemy_timeline = myskills.StringsToSkills(enemies[0].skills);
        if (enemies[0].statuses.ContainsKey("stunned")){
            enemy_timeline[0] = myskills.nullSkill;
        }
        string new_enemy_timeline = "";
        foreach (Skill s in enemy_timeline){
            new_enemy_timeline += s.display_name+'\n';
        }
        enemy_timeline_text.text = new_enemy_timeline;
        // Show all hero skill buttons

        GameObject.Find("Reset").SetActive(true);
        GameObject.Find("Execute").SetActive(true);

        available_skills = new List<Skill>();
        foreach (Entity hero in heroes){
            foreach (Skill s in myskills.StringsToSkills(hero.skills)){
                available_skills.Add(s);
                Debug.Log(s.display_name);
            }
        }

        List<GameObject> buttons_to_reactivate = new List<GameObject>();
        foreach (Skill s in available_skills){
            GameObject target = GameObject.Find(s.display_name);
            Debug.Log(s.display_name + "|" + target.name);
            buttons_to_reactivate.Add(target);
        };

        foreach (GameObject g in skill_buttons){
            g.SetActive(false);
        };

        foreach (GameObject g in buttons_to_reactivate){
            Debug.Log(g.name);
            g.SetActive(true);
        }
    }

    public async void LaunchCombatSequence(){

        for (int i = 0; i < skill_buttons.Length; i++){
            skill_buttons[i].GetComponent<Image>().color = new Color(skill_colors[i].r/2, skill_colors[i].g/2, skill_colors[i].b/2, skill_colors[i].a);
            skill_buttons[i].GetComponent<Button>().enabled = false;
        }

        for (int i = 0; i < other_buttons.Count; i++)
        {
            other_buttons[i].GetComponent<Image>().color = other_colors[i] / 2;
            other_buttons[i].GetComponent<Image>().color = new Color(other_colors[i].r / 2, other_colors[i].g / 2, other_colors[i].b / 2, other_colors[i].a);
            other_buttons[i].GetComponent<Button>().enabled = false;
        }

        Debug.Log("done hiding buttons");

        // List<Skill> hero_skills = new List<Skill>(hero_timeline);
        for (int i=0; i<hero_timeline.Count; i++){
            Skill s = hero_timeline[i];
            if (s.target_mode == 1){
                s.ApplySkill(enemies[0]);
            }else if (s.target_mode == -1){
                s.ApplySkill(GetWeakestEntity(heroes)); // just buff hero with lowest hp
            }else if (s.target_mode == -2){
                s.Use(heroes);
            }else if (s.target_mode == -3){
                s.ApplySkill(GetRandomEntity(heroes));
            }
            List<string> hero_timeline_list = new List<string>(hero_timeline_text.text.Split('\n'));
            hero_timeline_list.RemoveAt(0);
            hero_timeline_text.text = string.Join('\n', hero_timeline_list);
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
                PlayerData.instance.SetHitpoint(j, heroes[j].health);
                if (heroes[j].IsDead()) {
                    BattleLoader.LoadDungeonFromLose();
                    return;
                }
            }
        }
        Debug.Log("hero moves done.");
        // List<Skill> enemy_skills = new List<Skill>(enemy_timeline);
        for (int i=0; i<enemy_timeline.Count; i++){
            Skill s = enemy_timeline[i];
            if (!enemies[0].IsDead())
            {
                if (s.target_mode == 1)
                {
                    s.ApplySkill(enemies[0]);
                }
                else if (s.target_mode == -1)
                {
                    s.ApplySkill(GetWeakestEntity(heroes)); // just buff hero with lowest hp
                }
                else if (s.target_mode == -2)
                {
                    s.Use(heroes);
                }
                else if (s.target_mode == -3)
                {
                    s.ApplySkill(GetRandomEntity(heroes));
                }
            }

            List<string> enemy_timeline_list = new List<string>(enemy_timeline_text.text.Split('\n'));
            enemy_timeline_list.RemoveAt(0);
            enemy_timeline_text.text = string.Join('\n', enemy_timeline_list);

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
                PlayerData.instance.SetHitpoint(j, heroes[j].health);
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
            if (people[i].health < lowest){
                weakest = people[i];
                lowest = people[i].health;
            }
        }
        return weakest;
    }

    public int GetTimelineLength(List<Skill> timeline){
        int t = 0;
        foreach(Skill s in timeline){
            t += s.ap_cost;
        }
        return t;
    }

}
