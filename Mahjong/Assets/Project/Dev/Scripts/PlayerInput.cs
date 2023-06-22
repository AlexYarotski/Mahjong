using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerInput : MonoBehaviour,  IPointerDownHandler
{
    [SerializeField]
    private LayerMask _layer = default;
    
    public void OnPointerDown(PointerEventData eventData)
    {
        var screenPointToRay = Camera.main.ScreenPointToRay(eventData.position);

        if (Physics.Raycast(screenPointToRay, out RaycastHit hitInfo, Int32.MaxValue, _layer))
        {
            hitInfo.collider.GetComponent<Tile>().Move();
        }
    }
}
