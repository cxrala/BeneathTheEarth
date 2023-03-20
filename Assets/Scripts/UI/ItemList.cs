using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour {
    [SerializeField]
    private List<Item> baseList = new List<Item>();
    [SerializeField]
    private int m_itemHeight;
    public int itemHeight { get { return m_itemHeight; } private set { m_itemHeight = value; } }
    [SerializeField]
    private Vector2 m_listDirection;
    public Vector2 listDirection { get { return m_listDirection; } private set { m_listDirection = value; } }
    [SerializeField]
    private RectTransform m_rectTransform;
    [SerializeField]
    private Vector2 m_startPoint;
    public Vector2 startPoint {
        get { return m_startPoint; }
        private set { m_startPoint = value; }
    }
    private Vector2 m_worldStartPoint;
    public Vector2 worldStartPoint {
        get { return m_worldStartPoint; }
        private set { m_worldStartPoint = value; }
    }

    [SerializeField]
    private Vector2 m_itemPivot;
    public Vector2 itemPivot {
        get { return m_itemPivot; }
        private set { m_itemPivot = value; }
    }

    public RectTransform rectTransform {
        get { return m_rectTransform; }
        private set { m_rectTransform = value; }
    }

    // Start is called before the first frame update
    void Start() {
        m_listDirection.Normalize();
        SetParents();
        UpdatePositions();
        Vector3[] corners = new Vector3[4];
        m_rectTransform.GetWorldCorners(corners);
        worldStartPoint = new Vector2(corners[0].x * startPoint.x + corners[2].x * (1 - startPoint.x), corners[1].y * startPoint.y + corners[0].y * (1 - startPoint.y));
    }

    // Update is called once per frame
    void Update() {

    }

    private void SetParents() {
        foreach (Item i in baseList) {
            i.SetParent(this);
        }
    }

    public void UpdatePositions() {
        for (int i = 0; i < baseList.Count; i++) {
            baseList[i].rectTransform.anchoredPosition = m_listDirection * m_itemHeight * i;
        }
    }

    public void Add(Item item) {
        baseList.Add(item);
        item.SetParent(this);
        // Updates all item positions
        UpdatePositions();
    }

    public void Insert(int index, Item item) {
        baseList.Insert(index, item);
        item.SetParent(this);
        // Updates all item positions
        UpdatePositions();
    }

    public void RemoveAt() {
        GameObject temp = baseList[baseList.Count - 1].gameObject;
        baseList.RemoveAt(baseList.Count - 1);
        Destroy(temp);
    }

    public void RemoveAt(int index) {
        GameObject temp = baseList[index].gameObject;
        baseList.RemoveAt(index);
        Destroy(temp);
    }
}
