using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private readonly Vector3 SizeDecrease = new Vector3(0, 0.5f, 0);
    
    [SerializeField]
    private TilesType _tilesType = default;
    [SerializeField]
    private TileBoard _tileBoard = null;

    [SerializeField]
    private float _timeAppearance = 0;
    [SerializeField]
    private float _timeDecrease = 0;
    
    public TilesType TilesType => _tilesType;

    public void Move()
    {
        transform.DOMove(_tileBoard.GetPosition(this), _timeAppearance)
            .OnComplete(() => _tileBoard.SearchPairsTile(this));
        
        gameObject.layer = default;
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        transform.DOScale(SizeDecrease, _timeDecrease)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
