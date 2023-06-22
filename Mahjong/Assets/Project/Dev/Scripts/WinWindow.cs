using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinWindow : MonoBehaviour
{
    private const string LevelIndex = "LevelIndex";
    
    private readonly Vector3 StartSize = new Vector3(1, 1, 1);

    [SerializeField]
    private TextMeshProUGUI _winTMP = null;

    [SerializeField]
    private float _timeAppearance = 0;

    [SerializeField]
    private Button _restart = null;
    [SerializeField]
    private Button _nextLevel = null;

    [SerializeField]
    private SceneLoader _sceneLoader = null;

    private void Awake()
    {
        _restart.onClick.AddListener(Restart);
        _nextLevel.onClick.AddListener(LoadNextLevel);

        _restart.gameObject.SetActive(false);
        _nextLevel.gameObject.SetActive(false);
        
        _winTMP.rectTransform.localScale = Vector3.zero;
    }

    public void Open()
    {
        _restart.gameObject.SetActive(true);
        _nextLevel.gameObject.SetActive(true);
        
        _winTMP.rectTransform.DOScale(StartSize, _timeAppearance);
    }

    private void Restart()
    {
        _sceneLoader.Load(SceneManager.GetActiveScene().name);
    }

    private void LoadNextLevel()
    {
        PlayerPrefs.SetInt(LevelIndex, PlayerPrefs.GetInt(LevelIndex) + 1);
        
        _sceneLoader.LoadNextScene();
    }
}
