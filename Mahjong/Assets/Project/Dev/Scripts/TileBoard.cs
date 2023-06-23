using System;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;

public class TileBoard : MonoBehaviour
{
    public event Action<Tile> MovedTile = delegate{  }; 
    public static event Action Lose = delegate { };

    private readonly List<Tile> TilesList = new List<Tile>();

    [SerializeField]
    private int _quantityTilesInPairs = 0;
    [SerializeField]
    private float _timeMovement = 0;

    [SerializeField]
    private List<Vector3> PositionList = new List<Vector3>();

    public Vector3 GetPosition(Tile tile)
    {
        TilesList.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x));

        MovedTile(tile);

        TilesList.Add(tile);

        if (CheckIdenticalTiles(tile))
        {
            MoveTilesToRight(tile);

            CheckLose(tile);

            return GetPositionTiles(tile);
        }

        CheckLose(tile);

        if (TilesList.Count > PositionList.Count)
        {
            return PositionList[TilesList.Count - 2];
        }
        
        return PositionList[TilesList.Count - 1];
    }

    public void SearchPairsTile(Tile tile)
    {
        var quantityIdenticalTiles = TilesList.FindAll(it => it.TilesType == tile.TilesType);

        if (CheckQuantityIdenticalTiles(quantityIdenticalTiles))
        {
            RemoveIdenticalTile(quantityIdenticalTiles);

            return;
        }

        CheckLose(tile);
    }

    private void RemoveIdenticalTile(List<Tile> quantityIdenticalTiles)
    {
        var tileIndex = TilesList.LastIndexOf(quantityIdenticalTiles[^1]);

        if (TilesList[tileIndex].transform.position.z - PositionList[tileIndex].z > Mathf.Epsilon)
        {
            return;
        }

        for (int i = 0; i < quantityIdenticalTiles.Count; i++)
        {
            for (int j = 0; j < TilesList.Count; j++)
            {
                if (quantityIdenticalTiles[i] == TilesList[j])
                {
                    TilesList[j].Disable();
                    TilesList.Remove(TilesList[j]);
                }
            }
        }

        MoveTilesToLeft();
    }

    private void MoveTilesToLeft()
    {
        for (int i = 0; i < TilesList.Count; i++)
        {
            if (TilesList[i].transform.position.x - PositionList[i].x > Mathf.Epsilon)
            {
                TilesList[i].transform.DOMove(PositionList[i], _timeMovement);
            }
        }
    }
    
    private void MoveTilesToRight(Tile tile)
    {
        var tilesOnBoard = GetTilesOnBoardList();
        var indexLastIdenticalTile = tilesOnBoard.FindLastIndex(tb => tb.TilesType == tile.TilesType);
        
        for (int i = tilesOnBoard.Count - 1; i > indexLastIdenticalTile; i--)
        {
            if (TilesList.Count > PositionList.Count)
            {
                TilesList[i].transform.DOMove(PositionList[i], _timeMovement);
            }
            else
            {
                TilesList[i].transform.DOMove(PositionList[i + 1], _timeMovement);
            }
        }
    }
    
    private bool CheckQuantityIdenticalTiles(List<Tile> quantityIdenticalTiles)
    {
        while (quantityIdenticalTiles.Count > _quantityTilesInPairs)
        {
            quantityIdenticalTiles.RemoveAt(quantityIdenticalTiles.Count - 1);
        }

        return quantityIdenticalTiles.Count == _quantityTilesInPairs;
    }

    private bool CheckIdenticalTiles(Tile tile)
    {
        var isIdenticalTiles = GetTilesOnBoardList().FirstOrDefault(tb => tb.TilesType == tile.TilesType);

        return isIdenticalTiles != null;
    }
    
    private void CheckLose(Tile tile)
    {
        if (TilesList.Count == PositionList.Count)
        {
            if (tile == TilesList[^1])
            {
                if (!CheckPair())
                {
                    Lose();
                }
            }
        }
    }

    private bool CheckPair()
    {
        for (var i = 0; i < TilesList.Count; i++)
        {
            var countIdentical = TilesList.FindAll(tl => tl.TilesType == TilesList[i].TilesType).Count;

            if (countIdentical >= _quantityTilesInPairs)
            {
                return true;
            }
        }

        return false;
    }

    private Vector3 GetPositionTiles(Tile tile)
    {
        var tilesOnBoard = GetTilesOnBoardList();
        var indexLastIdenticalTile = tilesOnBoard.FindLastIndex(tb => tb.TilesType == tile.TilesType);

        if (indexLastIdenticalTile == TilesList.Count - 1)
        {
            return PositionList[indexLastIdenticalTile];
        }
        if (TilesList.Count > PositionList.Count)
        {
            return PositionList[indexLastIdenticalTile];
        }

        return PositionList[indexLastIdenticalTile + 1];
    }

    private List<Tile> GetTilesOnBoardList()
    {
        var tilesOnBoard = new List<Tile>();

        for (var i = 0; i < TilesList.Count; i++)
        {
            if (i == TilesList.Count - 1)
            {
                break;
            }

            tilesOnBoard.Add(TilesList[i]);
        }

        return tilesOnBoard;
    }
}
