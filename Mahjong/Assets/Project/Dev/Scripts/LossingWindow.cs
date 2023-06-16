using DG.Tweening;
using Project.Dev.Scripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LossingWindow : MonoBehaviour
{
    private const string SampleScene = "SampleScene";
    
    [SerializeField]
    private Image _background = null;
    [SerializeField]
    private TextMeshProUGUI _text = null;

    [SerializeField]
    private Button _restart = null; 

    private void Awake()
    {
        _restart.onClick.AddListener(Restart);
    }
    
    private void Restart()
    {
        DOTween.KillAll();
        SceneLoader.Load(SampleScene);
    }
}
