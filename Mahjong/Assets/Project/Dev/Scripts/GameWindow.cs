using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Project.Dev.Scripts
{
    public class GameWindow : MonoBehaviour
    {
        [Header("Window")]
        [SerializeField]
        private LossingWindow _lossingWindow = null;
        [SerializeField]
        private WinWindow _winWindow = null;

        [SerializeField]
        private Button _restart = null;

        [SerializeField]
        private SceneLoader _sceneLoader = null;
        
        private void Awake()
        {
            _restart.onClick.AddListener(Restart);
        }
        private void OnEnable()
        {
            Level.RemovedAllTiles += Level_RemovedAllTiles;
            TileBoard.Lose += BoardTiles_Lose;
        }
        
        private void OnDisable()
        {
            Level.RemovedAllTiles -= Level_RemovedAllTiles;
            TileBoard.Lose -= BoardTiles_Lose;
        }
        
        private void Level_RemovedAllTiles()
        {
            _restart.gameObject.SetActive(false);
            _winWindow.Open();
        }

        private void BoardTiles_Lose()
        {
            _restart.gameObject.SetActive(false);
            _lossingWindow.gameObject.SetActive(true);
        }

        private void Restart()
        {
            _sceneLoader.Load(SceneManager.GetActiveScene().name);
        }
    }
}