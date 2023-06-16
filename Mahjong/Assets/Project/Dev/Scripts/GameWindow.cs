using UnityEngine;
using UnityEngine.UI;

namespace Project.Dev.Scripts
{
    public class GameWindow : MonoBehaviour
    {
        private const string SampleScene = "SampleScene";
        
        [SerializeField]
        private LossingWindow _lossingWindow = null;

        [SerializeField]
        private Button _restart = null;

        private void Awake()
        {
            _restart.onClick.AddListener(Restart);
            _lossingWindow.gameObject.SetActive(false);
        }
        private void OnEnable()
        {
            BoardTiles.Lose += BoardTiles_Lose;
        }

        private void OnDisable()
        {
            BoardTiles.Lose -= BoardTiles_Lose;
        }

        private void BoardTiles_Lose()
        {
            _restart.gameObject.SetActive(false);
            _lossingWindow.gameObject.SetActive(true);
        }

        private void Restart()
        {
            SceneLoader.Load(SampleScene);
        }
    }
}