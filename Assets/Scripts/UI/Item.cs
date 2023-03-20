using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour {
    private ItemList parentList;
    [SerializeField]
    private RectTransform m_rectTransform;
    public RectTransform rectTransform { get { return m_rectTransform; } private set { m_rectTransform = value; } }

    public void SetParent(ItemList parentList) {
        this.parentList = parentList;
        m_rectTransform.anchorMax = parentList.startPoint;
        m_rectTransform.anchorMin = parentList.startPoint;
        m_rectTransform.pivot = parentList.itemPivot;
        m_rectTransform.SetParent(parentList.rectTransform);
    }
}
