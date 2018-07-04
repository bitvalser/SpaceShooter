using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine;

public class TouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler {

    private Vector2 origin;
    private Vector2 direct;
    private Vector2 smoothDirect;
    public float smooth;
    private bool toched;
    private int PointId;

    void Awake()
    {
        toched = false;
    }

    public void OnPointerDown(PointerEventData data)
    {
        if (!toched)
        {
            toched = true;
            origin = data.position;
            PointId = data.pointerId;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == PointId)
        {
            toched = false;
            direct = Vector3.zero;
        }
    }

    public void OnDrag(PointerEventData data)
    {
        if (data.pointerId == PointId)
        {
            Vector2 curPos = data.position;
            Vector2 directRow = curPos - origin;
            direct = directRow.normalized;
        }
    }

    public Vector2 getDirect()
    {
        smoothDirect = Vector2.MoveTowards(smoothDirect, direct, smooth);
        return smoothDirect;
    }
}
