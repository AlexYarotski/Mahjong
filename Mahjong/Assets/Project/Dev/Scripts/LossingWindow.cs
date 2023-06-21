using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LossingWindow : MonoBehaviour
{
    [SerializeField]
    private Image _background = null;
    [SerializeField]
    private TextMeshProUGUI _text = null;

    [SerializeField]
    private Button _restart = null; 

    private void Awake()
    {
        _restart.onClick.AddListener(Restart);
        
        gameObject.SetActive(false);
    }
    
    private void Restart()
    {
        DOTween.KillAll();
        SceneLoader.Load(SceneManager.GetActiveScene().buildIndex);
    }
}
