using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public event Action<Tile> Disabled = delegate{  }; 

    private readonly Vector3 SizeDecrease = new Vector3(0, 0.5f, 0);
    
    [SerializeField]
    private TilesType _tilesType = default;
    [SerializeField]
    private TileBoard _tileBoard = null;

    [SerializeField]
    private float _timeLanding = 0;
    [SerializeField]
    private float _timeDecrease = 0;
    [SerializeField]
    private float _timeAppearanceColor = 0;

    [SerializeField]
    private List<Tile> _listUpperTiles = null;
    
    public TilesType TilesType => _tilesType;

    private Renderer _renderer = null;
    
    private void Awake()
    {
        _renderer = GetComponentInChildren<Renderer>();
        
        if (_listUpperTiles.Count > 0)
        {
            _renderer.material.color = Color.gray;
        }
    }

    private void OnEnable()
    {
        _tileBoard.MovedTile += TileBoard_MovedTile;
    }
    private void OnDisable()
    {
        _tileBoard.MovedTile -= TileBoard_MovedTile;
    }
    
    public void Move()
    {
        if (_listUpperTiles.Count == 0)
        {
            transform.DOMove(_tileBoard.GetPosition(this), _timeLanding)
                .OnComplete(() => _tileBoard.SearchPairsTile(this));
        
            gameObject.layer = default;
        }
    }

    public void Enable()
    {
        gameObject.SetActive(true);
    }

    public void Disable()
    {
        transform.DOScale(SizeDecrease, _timeDecrease)
            .OnComplete(() => gameObject.SetActive(false));
        
        Disabled(this);
    }
    
    private void TileBoard_MovedTile(Tile tile)
    {
        _listUpperTiles.Remove(tile);

        if (_listUpperTiles.Count == 0)
        {
            _renderer.material.DOColor(Color.white, _timeAppearanceColor);
        }
    }
}
