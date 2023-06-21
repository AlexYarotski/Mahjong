using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public static async void Load(int sceneNumber)
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(sceneNumber);

        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }

    public static async void LoadNextScene()
    {
        var loadSceneAsync = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1) ?? SceneManager.LoadSceneAsync(0);
        
        while (!loadSceneAsync.isDone)
        {
            await Task.Yield();
        }
    }
}
