using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoardTiles : MonoBehaviour
{
    public static event Action Lose = delegate {  }; 

    private const float StartPosAxisX = -8;

    private readonly List<Tiles> TilesList = new List<Tiles>();
    private readonly List<Vector3> PositionTilesList = new List<Vector3>();

    [SerializeField]
    private float _tiliesGap = 0;

    [SerializeField]
    private int _quantityIdentical = 0;
    [SerializeField]
    private int _quantityTilesOnBoard = 0;
    [SerializeField]
    private float _timeMoveTiles = 0;
    
    private float _newPosX = 0;

    private void Awake()
    {
        SetPositionTilesList();
    }

    public Vector3 GetPositionTiles(Tiles tiles)
    {
        var index = TilesList.FindLastIndex(tl => tl.TilesType == tiles.TilesType);
        
        TilesList.Add(tiles);

        var indexLastTiles = TilesList.Count -1;
        
        if (index == -1 && TilesList.Count > 0)     
        {
            return PositionTilesList[indexLastTiles];          
        }   

        if (index != indexLastTiles)
        { 
            for (int i = 0; i < TilesList.Count; i++)
            {
                if (index == indexLastTiles)
                {
                    return PositionTilesList[indexLastTiles];
                }         
                if (index < i && i != indexLastTiles)
                {
                    TilesList[i].transform.DOMove(PositionTilesList[i + 1], 1);
                }
            }    
        }
        
        return PositionTilesList[index + 1];
    }
    
    public void CheckIdenticalTiles(Tiles tiles)
    {
        TilesList.Sort((x, y) => x.transform.position.x.CompareTo(y.transform.position.x));
        
        //TilesList.Add(tiles);
        //Check(tiles);
        
        if (TilesList[^1].transform.position != PositionTilesList[TilesList.Count - 1])
        {
            return;
        }
        
        var identicalTilesList = TilesList.FindAll(tl => tl.TilesType == tiles.TilesType).ToArray();
        
        if (identicalTilesList.Length == _quantityIdentical)
        {
            for (int i = 0; i < identicalTilesList.Length; i++)
            {
                identicalTilesList[i].Disable();
            }

            MoveTiles(identicalTilesList);
        }

        CheckLose();
    }
    
    private void SetPositionTilesList()
    {
        _newPosX = StartPosAxisX;
        
        for (int i = 0; i < _quantityTilesOnBoard; i++)
        {
            _newPosX += _tiliesGap;
            
            PositionTilesList.Add(new Vector3(_newPosX, transform.position.y, transform.position.z));
        }
    }
    
    private void MoveTiles(Tiles[] identicalTiles)
    {
        for (int i = 0; i < identicalTiles.Length; i++)
        {
            TilesList.Remove(identicalTiles[i]);
        }
        
        for (int i = 0; i < TilesList.Count; i++)
        {
            if (PositionTilesList[i] != TilesList[i].transform.position)
            {
                TilesList[i].transform.DOMove(PositionTilesList[i], _timeMoveTiles);
            }
        }
    }

    private void CheckLose()
    {
        if (TilesList.Count >= _quantityTilesOnBoard)
        {
            Lose();
        }
    }
}