using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ReorderItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler {
    private ReorderList parentList;
    [SerializeField]
    private RectTransform m_rectTransform;

    public float listPosition;

    public RectTransform rectTransform {
        get { return m_rectTransform; }
        private set { m_rectTransform = value; }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData) {

    }

    public void OnDrag(PointerEventData eventData) {
        float temp = Vector2.Dot(eventData.position - parentList.worldStartPoint, parentList.listDirection);
        parentList.MoveItem(this, (int) (temp / parentList.itemHeight + 0.5));
        listPosition = temp;
        m_rectTransform.position = listPosition * parentList.listDirection + parentList.worldStartPoint;
    }

    public void OnEndDrag(PointerEventData eventData) {
        parentList.UpdatePositions();
    }

    public void SetParent(ReorderList parentList) {
        this.parentList = parentList;
        m_rectTransform.anchorMax = parentList.startPoint;
        m_rectTransform.anchorMin = parentList.startPoint;
        m_rectTransform.pivot = parentList.itemPivot;
        m_rectTransform.SetParent(parentList.rectTransform);
    }
}
