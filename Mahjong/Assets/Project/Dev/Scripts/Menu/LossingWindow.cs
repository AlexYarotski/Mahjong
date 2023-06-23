using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LossingWindow : MonoBehaviour
{
    [SerializeField]
    private Button _restart = null; 
    
    [SerializeField]
    private SceneLoader _sceneLoader = null;

    private void Awake()
    {
        _restart.onClick.AddListener(Restart);
        
        gameObject.SetActive(false);
    }
    
    private void Restart()
    {
        _sceneLoader.Load(SceneManager.GetActiveScene().name);
    }
}
