using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace Project.Dev.Scripts
{
    public class WinWindow : MonoBehaviour
    {
        private readonly Vector3 StartSize = new Vector3(1, 1, 1);
        private readonly List<Tiles> TilesList = new List<Tiles>();

        [SerializeField]
        private Level _level = null;

        [SerializeField]
        private TextMeshProUGUI _youWin = null;
        [SerializeField]
        private float _timeAppearance = 0;
        
        private void Awake()
        {
            _youWin.rectTransform.localScale = Vector3.zero;
            
            SetTilesList();
        }

        private void OnEnable()
        {
            BoardTiles.ChosePair += BoardTiles_ChosePair;
        }

        private void OnDisable()
        {
            BoardTiles.ChosePair -= BoardTiles_ChosePair;
        }

        private void BoardTiles_ChosePair(Tiles tiles)
        {
            TilesList.Remove(tiles);

            if (TilesList.Count == 0)
            {
                _youWin.rectTransform.DOScale(StartSize, _timeAppearance);
            }
        }

        private void SetTilesList()
        {
            for (int i = 0; i < _level.GetComponentsInChildren<Tiles>().Length; i++)
            {
                TilesList.Add(_level.GetComponentsInChildren<Tiles>()[i]);
            } 
        }
    }
}