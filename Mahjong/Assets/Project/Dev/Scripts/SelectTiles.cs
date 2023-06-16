using System;
using UnityEngine;

public class SelectTiles : MonoBehaviour
{
    [SerializeField]
    private LayerMask _layerMask = default;
    
    [SerializeField]
    private Camera _camera = null;

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out RaycastHit hitInfo, Int32.MaxValue, _layerMask))
            {
                hitInfo.collider.GetComponent<Tiles>().Move();
            }
        }
    }
}
