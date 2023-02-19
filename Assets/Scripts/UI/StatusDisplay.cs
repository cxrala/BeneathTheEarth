using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatusDisplay : MonoBehaviour
{
    public GameObject statusIcon;
    public List<Skill.Status> statuses;
    public List<SpriteBox> statusIcons;
    public List<Sprite> sprites;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DestroyIcons() {
        while(statusIcons.Count > 0) {
            Destroy(statusIcons[0].gameObject);
            statusIcons.RemoveAt(0);
        }
    }

    public void UpdateStatuses(List<Skill.Status> statuses) {
        Debug.Log("Updating status display");
        this.statuses.Clear();
        statuses.Sort();
        DestroyIcons();
        int index = 1;
        foreach (Skill.Status status in statuses) {
            Debug.Log(status);
            this.statuses.Add(status);
            SpriteBox temp = Instantiate(statusIcon, new Vector3(0, 0, 0), Quaternion.identity).GetComponent<SpriteBox>();
            statusIcons.Add(temp);
            temp.transform.parent = transform;
            RectTransform tempRect = temp.GetComponent<RectTransform>();
            tempRect.anchorMax = new Vector2(0.5F, 0.0F);
            tempRect.anchorMin = new Vector2(0.5F, 0.0F);
            tempRect.pivot = new Vector2(0.5F, 0.5F);
            tempRect.localScale = new Vector3(1, 1, 1);
            tempRect.anchoredPosition = new Vector2(0, (float)(100.0F*index/(statuses.Count + 1)));
            Debug.Log(tempRect.offsetMax);
            temp.SetSprite(sprites[(int) status]);
            index++;
        }
    }
}
