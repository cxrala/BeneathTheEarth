using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTracker : MonoBehaviour
{
    public List<bool> enemyStatuses;
    private List<Enemy> enemies;
    // Start is called before the first frame update
    void Start()
    {
        enemyStatuses = new List<bool>();
        enemies = new List<Enemy>();
        int i = 0;
        foreach (Transform t in transform)
        {
            Enemy enemy = t.gameObject.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemyStatuses.Add(true);
                enemies.Add(enemy);
                enemy.id = i++;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void KillDeadEnemies(List<bool> statuses)
    {
        IEnumerator InnerFunction()
        {
            while (enemies == null)
            {
                yield return null;
            }
            for (int i = 0; i < statuses.Count; i++)
            {
                if (!statuses[i])
                {
                    Destroy(enemies[i].gameObject);
                }
                enemyStatuses[i] = statuses[i];
            }
        }
        StartCoroutine(InnerFunction());
    }
}
