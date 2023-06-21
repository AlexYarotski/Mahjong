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

        private void Awake()
        {
            _restart.onClick.AddListener(Restart);
        }
        private void OnEnable()
        {
            TileCounter.RemovedAllTiles += ScoreTile_RemovedAllTiles;
            TileBoard.Lose += BoardTiles_Lose;
        }
        
        private void OnDisable()
        {
            TileBoard.Lose -= BoardTiles_Lose;
            TileCounter.RemovedAllTiles -= ScoreTile_RemovedAllTiles;
        }
        
        private void ScoreTile_RemovedAllTiles()
        {
            _restart.gameObject.SetActive(false);
            _winWindow.OpenWindow();
        }

        private void BoardTiles_Lose()
        {
            _restart.gameObject.SetActive(false);
            _lossingWindow.gameObject.SetActive(true);
        }

        private void Restart()
        {
            SceneLoader.Load(SceneManager.GetActiveScene().buildIndex);
        }
    }
}