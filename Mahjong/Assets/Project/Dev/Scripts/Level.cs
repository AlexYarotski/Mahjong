using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    private const string LevelIndex = "LevelIndex";
    
    public static event Action RemovedAllTiles = delegate {  };

    private List<Tile> _componentsInChildren;

    private void Awake()
    {
        PlayerPrefs.SetInt(LevelIndex, SceneManager.GetActiveScene().buildIndex);
        
        _componentsInChildren = GetComponentsInChildren<Tile>().ToList();
    }

    private void OnEnable()
    {
        foreach (var componentInChild in _componentsInChildren)
        {
            componentInChild.Disabled += ComponentInChild_Disabled;
        }
    }

    private void OnDisable()
    {
        foreach (var componentInChild in _componentsInChildren)
        {
            componentInChild.Disabled -= ComponentInChild_Disabled;
        }
    }

    private void ComponentInChild_Disabled(Tile tile)
    {
        _componentsInChildren.Remove(tile);

        if (_componentsInChildren.Count == 0)
        {
            RemovedAllTiles();
        }
    }
}
