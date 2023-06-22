using UnityEngine;

[CreateAssetMenu(fileName = "LevelSettings", menuName = "Settings/LevelSettings", order = 0)]
public class LevelSettings : ScriptableObject
{
    private const string LevelIndex = "LevelIndex";
    
    [SerializeField]
    private string[] _levelNames = null;

    public string GetSceneName()
    {
        var currentLevelIndex = PlayerPrefs.GetInt(LevelIndex);

        return _levelNames[currentLevelIndex % _levelNames.Length];
    }
}
