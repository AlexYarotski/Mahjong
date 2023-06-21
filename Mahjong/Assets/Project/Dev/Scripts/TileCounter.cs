using System;
using UnityEngine;

public class TileCounter : MonoBehaviour
{
    public static event Action RemovedAllTiles = delegate { };

    [SerializeField]
    private Level _level = null;
    
    private int _numberTiles = 0;

    private void OnEnable()
    {
        TileBoard.ChoseTail += TileBoard_ChoseTail;
    }

    private void OnDisable()
    {
        TileBoard.ChoseTail -= TileBoard_ChoseTail;
    }

    private void Start()
    {
        _numberTiles = _level.GetComponentsInChildren<Tile>().Length;
    }
    
    private void TileBoard_ChoseTail()
    {
        if (--_numberTiles <= 0)
        {
            RemovedAllTiles();
        }
    }
}