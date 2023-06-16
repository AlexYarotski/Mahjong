using System.Threading.Tasks;
using UnityEngine.SceneManagement;

namespace Project.Dev.Scripts
{
    public static class SceneLoader
    {
        public static async void Load(string sceneName)
        {
            var loadSceneAsync = SceneManager.LoadSceneAsync(sceneName);

            while (!loadSceneAsync.isDone)
            {
                await Task.Yield();
            }
        }
    }
}