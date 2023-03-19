using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReorderList : MonoBehaviour
{
    [SerializeField]
    private List<ReorderItem> baseList = new List<ReorderItem>();
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
        get { return m_startPoint; } private set { m_startPoint = value; }
    }
    private Vector2 m_worldStartPoint;
    public Vector2 worldStartPoint {
        get { return m_worldStartPoint; } private set { m_worldStartPoint = value; }
    }

    [SerializeField]
    private Vector2 m_itemPivot;
    public Vector2 itemPivot {
        get { return m_itemPivot; } private set { m_itemPivot = value;}
    }
    
    public RectTransform rectTransform{
        get { return m_rectTransform; } private set { m_rectTransform = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        m_listDirection.Normalize();
        SetParents();
        UpdatePositions();
        Vector3[] corners = new Vector3[4];
        m_rectTransform.GetWorldCorners(corners);
        worldStartPoint = new Vector2(corners[0].x*startPoint.x + corners[2].x*(1 - startPoint.x), corners[1].y*startPoint.y + corners[0].y*(1 - startPoint.y));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SetParents() {
        foreach (ReorderItem i in baseList) {
            i.SetParent(this);
        }
    }

    public void UpdatePositions() {
        for (int i = 0; i < baseList.Count; i++) {
            baseList[i].rectTransform.anchoredPosition = m_listDirection * m_itemHeight * i;
        }
    }

    // Allows for items to be moved from one index to another. 
    public void MoveItem(ReorderItem item, int endIndex) {
        int startIndex = baseList.IndexOf(item);
        endIndex = Math.Min(Math.Max(0, endIndex), baseList.Count - 1);
        Debug.Log(startIndex+ " " + endIndex);
        if (startIndex < endIndex) {
            for (int i = startIndex; i < endIndex; i++) {
                baseList[i + 1].rectTransform.anchoredPosition = m_listDirection * m_itemHeight * i;
                baseList[i] = baseList[i + 1];
                Debug.Log("i++: " + i);
            }
        } else {
            for (int i = startIndex; i > endIndex; i--) {
                baseList[i - 1].rectTransform.anchoredPosition = m_listDirection * m_itemHeight * i;
                baseList[i] = baseList[i - 1];
                Debug.Log("i--: " + i);
            }
        }
        baseList[endIndex] = item;
    }

    public void Add(ReorderItem item) {
        baseList.Add(item);
        item.SetParent(this);
        // Updates all item positions
        UpdatePositions();
    }

    public void Insert(int index, ReorderItem item) {
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
