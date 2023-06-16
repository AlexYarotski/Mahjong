using DG.Tweening;
using UnityEngine;

public class Tiles : MonoBehaviour
{
    private const float DistanceToFinal = 2.5f;
    
    [SerializeField]
    private TilesType _tilesType = default;
    
    [SerializeField]
    private BoardTiles _boardTiles = null;

    [SerializeField]
    private float _timeAppearance = 0;
    [SerializeField]
    private float _timeDisappearance = 0;
    
    public TilesType TilesType => _tilesType;

    private int _quantityCollision = 0;

    public void Move()
    {
        transform.DOMove(_boardTiles.GetPositionTiles(this), _timeAppearance)
            .OnComplete(() => _boardTiles.CheckIdenticalTiles(this));
        gameObject.layer = default;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOMoveZ(transform.position.z + DistanceToFinal, _timeDisappearance));
        sequence.Append(transform.DOScale(Vector3.zero, 3))
            .OnComplete(() => gameObject.SetActive(false));
    }

    private void OnCollisionEnter(Collision collision)
    {
        _quantityCollision++;
        OffActive();
    }

    private void OnCollisionExit(Collision other)
    {
        _quantityCollision--;
        OnActive();
    }

    private void OffActive()
    {
        if (_quantityCollision == 0) return;

        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        var startColor = new Color[renderers.Length];
        
        for (int i = 0; i < renderers.Length; i++)
        {
            startColor[i] = renderers[i].material.color;
            renderers[i].material.color = Color.gray;
        }
    }

    private void OnActive()
    {
        if (_quantityCollision != 0) return;
        
        var renderers = gameObject.GetComponentsInChildren<Renderer>();
        var startColor = new Color[renderers.Length];
        
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = startColor[i];
        }
    }
}
