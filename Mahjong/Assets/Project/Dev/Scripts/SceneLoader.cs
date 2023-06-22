using System.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField]
    private LevelSettings _levelSettings = null;
    
    public async void Load(string sceneName)
    {
        DOTween.KillAll();
        
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }

    public async void LoadNextScene()
    {
        DOTween.KillAll();

        var loadSceneAsync = SceneManager.LoadSceneAsync(_levelSettings.GetSceneName());
            
        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }
}
